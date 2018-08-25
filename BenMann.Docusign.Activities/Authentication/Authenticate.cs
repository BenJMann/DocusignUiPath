using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace BenMann.Docusign.Activities.Authentication
{
    public class Authenticate : AsyncCodeActivity
    {
        private readonly string AuthXamlUrlBase = "https://raw.githubusercontent.com/BenJMann/DocusignUiPath/master/AuthenticationWorkflows/Auth";
        private readonly string AuthXamlUrlExtension = ".xaml";
        private readonly string AuthDirectoryName = "DocusignAuthentication";
        private readonly string XamlFilenameBase = "Auth";
        private readonly string XamlFilenameExtension = ".xaml";

        private const string AuthMethodManual = "Manual";
        private const string AuthMethodSecure = "Automatic (Secure)";
        private const string AuthMethodInsecure = "Automatic (Insecure)";

        [Category("Input")]
        [DisplayName("Authentication Url")]
        public InArgument<string> AuthenticationUrl { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Email")]
        public InArgument<string> Email { get; set; }

        [Category("Input")]
        //[RequiredArgument]
        [DisplayName("Password (Insecure)")]
        public InArgument<string> PasswordInsecure { get; set; }

        [Category("Input")]
        //[RequiredArgument]
        [DisplayName("Password (Secure)")]
        public InArgument<SecureString> PasswordSecure { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Authentication Method")]
        public InArgument<string> AuthenticationMethod { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Authentication Browser")]
        public InArgument<string> AuthenticationBrowser { get; set; }

        public string authUrl;
        public string email;
        public SecureString passwordSecure;
        public string passwordInsecure;
        public string authMethod;
        public string authBrowser;

        string filename;
        string filepath;
        string resourceUrl;

        public Dictionary<string, object> authArguments;

        Action LoadFileDelegate;

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            authUrl = AuthenticationUrl.Get(context);
            email = Email.Get(context);
            passwordInsecure = PasswordInsecure.Get(context);
            passwordSecure = PasswordSecure.Get(context);
            authMethod = AuthenticationMethod.Get(context);
            authBrowser = AuthenticationBrowser.Get(context);

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
                };
            }
            else if (authMethod == AuthMethodInsecure)
            {
                authArguments = new Dictionary<string, object>
                {
                    { "authUrl", authUrl },
                    { "email", email },
                    { "password", passwordInsecure },
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

            if (!File.Exists(filename))
            {
                Console.Write(filename);
                HttpResponseMessage response = await HttpAgent.Request(HttpMethod.Get, resourceUrl, null, null, null);
                string responseContent = await response.Content.ReadAsStringAsync();
                File.WriteAllText(filepath, responseContent);
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
