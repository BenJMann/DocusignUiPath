using System;
using System.Collections.Generic;
using System.Activities;
using System.ComponentModel;
using System.Net.Http;

namespace BenMann.Docusign.Activities.Templates
{
    [DisplayName("Load Template")]
    public sealed class LoadTemplate : DocusignActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Template Name")]
        public InArgument<string> TemplateName { get; set; }

        [Category("Output")]
        public OutArgument<object> Template { get; set; }

        private string templateName;
        private string templateId;

        Action GetTemplateInfoDelegate;


        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            LoadAuthentication(context);

            templateName = TemplateName.Get(context);

            GetTemplateInfoDelegate = new Action(_GetTemplateInfo);
            return GetTemplateInfoDelegate.BeginInvoke(callback, state);
        }

        void _GetTemplateInfo()
        {
            //Get Template ID
            DocusignResponse response = new DocusignResponse();
            SendRestRequest(response, HttpMethod.Get, "templates", null).Wait();
            if (response.IsError)
            {
                response.Throw();
            }
            TemplateListResponse resObj = response.GetData<TemplateListResponse>();
            foreach (TemplateDetails td in resObj.envelopeTemplates)
            {
                if (td.name.Trim() == templateName.Trim())
                {
                    templateId = td.templateId;
                }
            }
            if (templateId == null)
            {
                throw new ArgumentException("Could not find template with name " + templateName);
            }

            /*Get Template Roles
            DocusignResponse response2 = new DocusignResponse();
            var path = string.Format("envelopes/{0}/recipients", templateId);
            var query = new Dictionary<string, string>()
            {
                { "include_tabs", "false" },
                { "include_extended", "true" }
            };
            SendRestRequest(response2, HttpMethod.Get, path, null, query).Wait();
            if (response2.IsError)
            {
                response2.Throw();
            }*/
            
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            GetTemplateInfoDelegate.EndInvoke(result);
            Template.Set(context, new Template(templateId));
        }
    }
    public class TemplateListResponse
    {
        public List<TemplateDetails> envelopeTemplates;
    }
    public class TemplateDetails
    {
        public string name;
        public string templateId;
    }
}
