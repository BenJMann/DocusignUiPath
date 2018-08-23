using System;
using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Authentication
{
    public class Authenticate : CodeActivity
    {
        [Category("Input")]
        [DisplayName("Authentication Url")]
        public InArgument<string> AuthenticationUrl { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string accountServerAuthUrl = AuthenticationUrl.Get(context);

            if (accountServerAuthUrl == null)
            {
                var property = context.DataContext.GetProperties()["authAgent"];
                if (property.GetValue(context.DataContext) == null)
                {
                    throw new Exception("DocuSign activities must be within DocuSign Context activity");
                }
                AuthenticationAgent authAgent = (AuthenticationAgent)property.GetValue(context.DataContext);
                accountServerAuthUrl = authAgent.authUrl;
            }
            System.Diagnostics.Process.Start(accountServerAuthUrl);
        }
    }
}
