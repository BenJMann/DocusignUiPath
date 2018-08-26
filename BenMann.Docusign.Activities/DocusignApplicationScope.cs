using BenMann.Docusign;
using Docusign.Authentication;
using System;
using System.Activities;
using System.Activities.Statements;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace Docusign { 
    [DisplayName("DocuSign Application Scope")]
    public class DocusignApplicationScope : AsyncNativeActivity
    {
        [Category("Authentication")]
        [RequiredArgument]
        [Description("API endpoint for DocuSign")]
        [DisplayName("DocuSign API Url")]
        public InArgument<string> RestApiUrl { get; set; }
        [RequiredArgument]
        [Category("Authentication")]
        [DisplayName("Client ID")]
        [Description("DocuSign Integrator Credentials")]
        public InArgument<string> ClientId { get; set; }

        [OverloadGroup("G1")]
        [Category("Authentication")]
        [DisplayName("Client Secret - Insecure")]
        [Description("DocuSign Integrator Credential")]
        public InArgument<string> ClientSecretInsecure { get; set; }

        [OverloadGroup("G1")]
        [Category("Authentication")]
        [DisplayName("Client Secret - Secure")]
        [Description("DocuSign Integrator Credential")]
        public InArgument<SecureString> ClientSecretSecure { get; set; }

        [RequiredArgument]
        [DisplayName("Redirect Url")]
        [Category("Authentication")]
        [Description("DocuSign Integrator Credential")]
        public InArgument<string> RedirectUrl { get; set; }

        [Browsable(false)]
        public InArgument<int> timeoutMSImplementation = 10000;

        [Category("Authentication")]
        [DisplayName("Timeout MS")]
        [Description("Timeout for authentication")]
        [RequiredArgument]
        public InArgument<int> TimeoutMS
        {
            get
            {
                return timeoutMSImplementation;
            }
            set
            {
                timeoutMSImplementation = value;
            }
        }

        [Browsable(false)]
        public ActivityAction<AuthenticationAgent> AuthBody { get; set; }
        [Browsable(false)]
        public ActivityAction<AuthenticationAgent> MainBody { get; set; }

        AuthenticationAgent authAgent;

        Action AuthenticateAsyncDelegate;

        public DocusignApplicationScope()
        {
            AuthBody = new ActivityAction<AuthenticationAgent>
            {
                Argument = new DelegateInArgument<AuthenticationAgent>("authAgent"),
                Handler = new Sequence
                {
                    DisplayName = "Authentication",
                    Activities = {
                        new GetAuthorizationUrl(),
                        new Authenticate()
                    }
                }
            };
            MainBody = new ActivityAction<AuthenticationAgent>
            {
                Argument = new DelegateInArgument<AuthenticationAgent>("authAgent"),
                Handler = new Sequence { DisplayName = "Docusign Activities" }
            };
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
        }

        protected override IAsyncResult BeginExecute(NativeActivityContext context, AsyncCallback callback, object state)
        {
            
            string restApiUrl = RestApiUrl.Get(context);
            if (!restApiUrl.EndsWith("/")) restApiUrl += "/";
            string client_id = ClientId.Get(context);
            string client_secret;
            if (ClientSecretSecure.Get(context) != null)
                client_secret = SecureStringToString(ClientSecretSecure.Get(context));
            else
                client_secret = ClientSecretInsecure.Get(context);

            string redirect_uri = RedirectUrl.Get(context);
            int serverTimeout = TimeoutMS.Get(context);
            

            authAgent = new AuthenticationAgent(restApiUrl, client_id, client_secret, redirect_uri, serverTimeout);

            authAgent.GetAuthUrl();

            ScheduleAuthActivities(context);

            AuthenticateAsyncDelegate = new Action(AuthenticateAsync);
            return AuthenticateAsyncDelegate.BeginInvoke(callback, state);
        }

        void AuthenticateAsync()
        {
            authAgent.GetAuthCode();

            if (!authAgent.getAuthCodeSuccess)
            {
                throw new TimeoutException("Timeout exceeded waiting for authorization code.");
            }

            authAgent.GetAuthToken().Wait();
            authAgent.restApiError.Test();

            authAgent.GetUserInfo().Wait();
            authAgent.restApiError.Test();
        }

        protected override void EndExecute(NativeActivityContext context, IAsyncResult result)
        {
            AuthenticateAsyncDelegate.EndInvoke(result);
            ScheduleMainActivities(context);
        }

        void ScheduleAuthActivities(NativeActivityContext context)
        {
            if (AuthBody != null)
            {
                context.ScheduleAction<AuthenticationAgent>(AuthBody, authAgent, OnAuthActivityComplete);
            }
        }
        void OnAuthActivityComplete(NativeActivityContext context,
                         ActivityInstance completedInstance)
        {
            //Pass
        }


        void ScheduleMainActivities(NativeActivityContext context)
        {
            if (MainBody != null)
            {
                context.ScheduleAction<AuthenticationAgent>(MainBody, authAgent, OnMainActivityComplete);
            }
        }

        void OnMainActivityComplete(NativeActivityContext context,
                                 ActivityInstance completedInstance)
        {
            //Pass
        }

        string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

    }
}