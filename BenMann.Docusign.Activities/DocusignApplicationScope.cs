using System;
using System.Activities;
using System.Activities.Statements;
using System.ComponentModel;
using System.Drawing;
using BenMann.Docusign.Activities.Authentication;

namespace BenMann.Docusign.Activities
{
    [DisplayName("DocuSign Application Scope")]
    public class DocusignApplicationScope : AsyncNativeActivity
    {
        [Category("Input")]
        //[RequiredArgument]
        public InArgument<string> RestApiUrl { get; set; }
        //[RequiredArgument]
        [Category("Input")]
        public InArgument<string> ClientId { get; set; }
        //[RequiredArgument]
        [Category("Input")] 
        public InArgument<string> ClientSecret { get; set; }
        //[RequiredArgument]
        [Category("Input")]
        public InArgument<string> RedirectUrl { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<int> TimeoutMS { get; set; }

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
            string RestApiUrl = "https://account-d.docusign.com/";
            if (!RestApiUrl.EndsWith("/")) RestApiUrl += "/";
            string client_id = "33a8e07a-4345-43ab-8537-8abd586e3239";
            string client_secret = "241ac621-d800-42fa-9790-d7809bcfe37f";
            string redirect_uri = "http://localhost:5000/auth/callback";
            int serverTimeout = TimeoutMS.Get(context);

            authAgent = new AuthenticationAgent(RestApiUrl, client_id, client_secret, redirect_uri, serverTimeout);

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

    }
}