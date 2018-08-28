using BenMann.Docusign;
using System;
using System.Activities;
using System.ComponentModel;
using System.Net.Http;

namespace Docusign.Templates
{
    [DisplayName("Send Template")]
    public sealed class SendTemplate : DocusignActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<Template> Template { get; set; }

        public Template template;

        Action SendTemplateDelegate;


        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            LoadAuthentication(context);

            template = Template.Get(context);

            SendTemplateDelegate = new Action(_SendTemplate);
            return SendTemplateDelegate.BeginInvoke(callback, state);
        }

        void _SendTemplate() { 
            DocusignResponse response = new DocusignResponse();

            SendRestRequest(response, HttpMethod.Post, "envelopes", template).Wait();
            if (response.IsError)
            {
                response.Throw();
            }
        }
        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            SendTemplateDelegate.EndInvoke(result);
        }
    }
}


