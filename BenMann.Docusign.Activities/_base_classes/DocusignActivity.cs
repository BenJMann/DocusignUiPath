using BenMann.Docusign;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Docusign
{


    public abstract class DocusignActivity : AsyncCodeActivity
    {
        protected AuthenticationAgent authAgent;

        public DocusignActivity()
        {
            base.Constraints.Add(BenMann.Docusign.Activities.Constraints.CheckParent<DocusignApplicationScope>());
        }

        protected void LoadAuthentication(AsyncCodeActivityContext context)
        {
            var property = context.DataContext.GetProperties()["authAgent"];
            if (property.GetValue(context.DataContext) == null)
            {
                throw new Exception("DocuSign activities must be within DocuSign Context activity");
            }
            authAgent = (AuthenticationAgent)property.GetValue(context.DataContext);
        }

        protected async Task SendRestRequest(DocusignResponse restResponse, HttpMethod method, string path, object body, Dictionary<string, string> query = null)
        {
            HttpResponseMessage response = await HttpAgent.SendRestRequest(authAgent, method, path, body, query, true);
            string responseContent = await response.Content.ReadAsStringAsync();
            restResponse.Initialize(response, responseContent);
            if (restResponse.NeedsRefresh)
            {
                authAgent.RefreshAuthToken().Wait();
                response = await HttpAgent.SendRestRequest(authAgent, method, path, body);
                responseContent = await response.Content.ReadAsStringAsync();
                restResponse.Initialize(response, responseContent);
            }
        }
    }
}
