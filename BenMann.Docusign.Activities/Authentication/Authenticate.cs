using BenMann.Docusign;
using System;
using System.Activities;
using System.Activities.Validation;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Docusign.Authentication
{
    public enum Browsers
    {
        IE, Chrome, Firefox
    }
    public enum AuthMethods
    {
        Secure, Insecure, Manual
    }
    public class Authenticate : AsyncCodeActivity
    {
        private readonly string AuthXamlUrlBase = "https://raw.githubusercontent.com/BenJMann/DocusignUiPath/master/AuthenticationWorkflows/Auth";
        private readonly string AuthXamlUrlExtension = ".xaml";
        private readonly string AuthDirectoryName = "DocusignAuthentication";
        private readonly string XamlFilenameBase = "Auth";
        private readonly string XamlFilenameExtension = ".xaml";

        private const string AuthMethodManual = "Manual";
        private const string AuthMethodSecure = "Secure";
        private const string AuthMethodInsecure = "Insecure";
        private const int BrowserLoadTimeoutDefault = 2000;

        private const string ReadMeFilename = "README_DocusignAuthentication.txt";
        private const string ReadMeUrl = "https://raw.githubusercontent.com/BenJMann/DocusignUiPath/master/AuthenticationWorkflows/"+ReadMeFilename;
        [Category("Input")]
        [DisplayName("Authentication Url")]
        public InArgument<string> AuthenticationUrl { get; set; }

        [Browsable(false)]
        public InArgument<string> AuthenticationMethodBase = "Manual";
        [Category("Input")]
        [DisplayName("Authentication Method")]
        public InArgument<string> AuthenticationMethod
        {
            get
            {
                return AuthenticationMethodBase ?? "Manual";
            }
            set
            {
                AuthenticationMethodBase = value;
            }
        }
        [Browsable(false)]
        public InArgument<string> AuthenticationBrowserBase = "IE";

        [Category("Input")]
        [DisplayName("Authentication Browser")]
        public InArgument<string> AuthenticationBrowser
        {
            get
            {
                return AuthenticationBrowserBase;
            }
            set
            {
                AuthenticationBrowserBase = value;
            }
        }

        [Category("Input")]
        [DisplayName("Browser Load Timeout")]
        public InArgument<int> BrowserLoadTimeout { get; set; }

        [Category("Input")]
        [DisplayName("Email")]
        public InArgument<string> Email { get; set; }

        [Category("Input")]
        [DisplayName("Password (Insecure)")]
        public InArgument<string> PasswordInsecure { get; set; }

        [Category("Input")]
        [DisplayName("Password (Secure)")]
        public InArgument<SecureString> PasswordSecure { get; set; }


        public string authUrl;
        public string email;
        public SecureString passwordSecure;
        public string passwordInsecure;
        public string authMethod;
        public string authBrowser;
        public int browserLoadTimeout;

        string filename;
        string filepath;
        string resourceUrl;

        public Dictionary<string, object> authArguments;
        public List<string> MahNames
        {
            get
            {
                return new List<string>
                {
                    "Relative Position", "Absolute Position"
                };
            }
            set { }
        }
        Action LoadFileDelegate;

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            authUrl = AuthenticationUrl.Get(context);
            authMethod = AuthenticationMethod.Get(context);
            authBrowser = AuthenticationBrowser.Get(context);
            email = Email.Get(context);
            if (authMethod != "Manual" && email == null)
            {
                throw new ArgumentException("Email must be supplied with Automatic Authorization");
            }
            passwordInsecure = PasswordInsecure.Get(context);
            if (authMethod == "Insecure" && passwordInsecure == null)
            {
                throw new ArgumentException("Insecure Password must be supplied with Insecure Authentication");
            }
            passwordSecure = PasswordSecure.Get(context);
            if (authMethod == "Secure" && passwordSecure == null)
            {
                throw new ArgumentException("Secure Password must be supplied with Secure Authentication");
            }

            browserLoadTimeout = BrowserLoadTimeout.Get(context);
            if (browserLoadTimeout == 0) browserLoadTimeout = BrowserLoadTimeoutDefault;

            BuildDefaultAuthUrl(context);
            BuildUrlAndFilename();
            BuildAuthArguments();

            LoadFileDelegate = new Action(LoadFile);
            return LoadFileDelegate.BeginInvoke(callback, state);

        }
        protected void BuildUrlAndFilename()
        {
            if (authMethod == AuthMethodSecure)
            {
                resourceUrl = AuthXamlUrlBase + "Secure" + authBrowser + AuthXamlUrlExtension;
                filename = XamlFilenameBase + "Secure" + authBrowser + XamlFilenameExtension;
            }
            else if (authMethod == AuthMethodInsecure)
            {
                resourceUrl = AuthXamlUrlBase + "Insecure" + authBrowser + AuthXamlUrlExtension;
                filename = XamlFilenameBase + "Insecure" + authBrowser + XamlFilenameExtension;
            }
            else if (authMethod == AuthMethodManual)
            {
                //Pass
            }
        }
        protected void BuildAuthArguments()
        {
            if (authMethod == AuthMethodSecure)
            {
                authArguments = new Dictionary<string, object>
                {
                    { "authUrl", authUrl },
                    { "email", email },
                    { "password", passwordSecure },
                    { "timeout", browserLoadTimeout },
                };
            }
            else if (authMethod == AuthMethodInsecure)
            {
                authArguments = new Dictionary<string, object>
                {
                    { "authUrl", authUrl },
                    { "email", email },
                    { "password", passwordInsecure },
                    { "timeout", browserLoadTimeout },
                };
            }
            else if (authMethod == AuthMethodManual)
            {
                authArguments = new Dictionary<string, object>
                {
                    { "authUrl", authUrl },
                };
            }
        }
        protected void BuildDefaultAuthUrl(AsyncCodeActivityContext context)
        {
            if (authUrl == null)
            {
                var property = context.DataContext.GetProperties()["authAgent"];
                if (property.GetValue(context.DataContext) == null)
                {
                    throw new Exception("DocuSign activities must be within DocuSign Context activity");
                }
                AuthenticationAgent authAgent = (AuthenticationAgent)property.GetValue(context.DataContext);
                authUrl = authAgent.authUrl;
            }
        }
        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            LoadFileDelegate.EndInvoke(result);
        }

        protected async void LoadReadmeFile()
        {
            string ReadMeFilepath = Path.Combine(AuthDirectoryName, ReadMeFilename);

            if (!File.Exists(ReadMeFilepath))
            {
                HttpResponseMessage response = await HttpAgent.Request(HttpMethod.Get, ReadMeUrl, null, null, null);
                string responseContent = await response.Content.ReadAsStringAsync();
                File.WriteAllText(ReadMeFilepath, responseContent);
            }
        }
        protected async void LoadFile()
        {
            if (authMethod == AuthMethodManual)
            {
                System.Diagnostics.Process.Start(authUrl);
                return;
            }


            //Try/catch
            Directory.CreateDirectory(AuthDirectoryName);
            filepath = Path.Combine(AuthDirectoryName, filename);
            AutoResetEvent syncEvent = new AutoResetEvent(false);

            if (!File.Exists(filepath))
            {
                HttpResponseMessage response = await HttpAgent.Request(HttpMethod.Get, resourceUrl, null, null, null);
                string responseContent = await response.Content.ReadAsStringAsync();
                File.WriteAllText(filepath, responseContent);

                LoadReadmeFile();
            }

            ActivityXamlServicesSettings settings = new ActivityXamlServicesSettings
            {
                CompileExpressions = true
            };

            Activity workflow = ActivityXamlServices.Load(filepath, settings);
            WorkflowInvoker invoker = new WorkflowInvoker(workflow);
            invoker.InvokeCompleted += delegate (object sender, InvokeCompletedEventArgs args)
            {
                syncEvent.Set();
            };
            invoker.InvokeAsync(authArguments);

            syncEvent.WaitOne();

        }
    }
}
