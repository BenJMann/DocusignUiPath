using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace BenMann.Docusign
{
    public class HttpAgent
    {
        private const string api_endpoint = "/restapi/v2/accounts/{0}/";

        public static readonly HttpClient _Client = new HttpClient();
        public static readonly HttpListener _Listener = new HttpListener();
        public static async Task<HttpResponseMessage> SendRestRequest(AuthenticationAgent authAgent, HttpMethod method, string path, object body, Dictionary<string, string> query = null, bool logRequest = false) {

            //Load the headers
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Authorization", "Bearer " + authAgent.authToken.access_token }
            };

            //Convert the object to json
            var jsonSettings = new JsonSerializerSettings
            {
                //Converters = { new FormatNumbersAsTextConverter() },
                NullValueHandling = NullValueHandling.Ignore

            };
            string json = JsonConvert.SerializeObject(body, jsonSettings);

            if (logRequest)
            {
                string prettyJson = JsonConvert.SerializeObject(body, Formatting.Indented, jsonSettings);
                Console.Write(prettyJson);
            }

            //Create the endpoint
            string endpoint = GetEndpoint(authAgent, path);

            return await HttpAgent.Request(method, endpoint, query, json, headers);
        }
        private static string GetEndpoint(AuthenticationAgent authAgent, string path)
        {
            var base_uri = authAgent.userInfo.GetDefaultAccount().base_uri;
            var account_id = authAgent.userInfo.GetDefaultAccount().account_id;
            return base_uri + string.Format(api_endpoint, account_id) + path;
        }

        public static async Task<HttpResponseMessage> Request(HttpMethod pMethod, string pUrl, Dictionary<string, string> pQuery, string pJsonContent, Dictionary<string, string> pHeaders, string contentType = null)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = pMethod;

            string queryString = "";
            if (pQuery != null)
                queryString = BuildQuery(pQuery);

            httpRequestMessage.RequestUri = new Uri(pUrl + queryString);

            if (pHeaders != null)
            {
                foreach (var head in pHeaders)
                {
                    httpRequestMessage.Headers.Add(head.Key, head.Value);
                }
            }
            switch (pMethod.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    break;
            }

            if (contentType != null)
            {
                httpRequestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            }

            return await _Client.SendAsync(httpRequestMessage);
        }
        public static string BuildQuery(Dictionary<string, string> query)
        {
            string queryString = "";
            if (query.Count > 0)
            {
                queryString = "?";
                foreach (var item in query)
                {
                    if (item.Value != null)
                        queryString += item.Key + "=" + item.Value + "&";
                }
                queryString = queryString.Substring(0, queryString.Length - 1);
            }
            return queryString;
        }
    }
}
