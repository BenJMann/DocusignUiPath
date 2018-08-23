using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace BenMann.Docusign
{
    public class DocusignResponse
    {
        public HttpResponseMessage response;
        public bool IsSuccessStatusCode;
        public HttpStatusCode statusCode;
        public string responseBody;
        private bool isInitialized = false;

        public bool IsError
        {
            get
            {
                if (!isInitialized) throw new InvalidOperationException("Object must be initialized before accessing IsError");
                return !this.IsSuccessStatusCode;
            }
        }
        public bool NeedsRefresh
        {
            get
            {
                if (!isInitialized) throw new InvalidOperationException("Object must be initialized before accessing NeedsRefresh");
                if (!IsError) return false;
                return GetData<DocusignError>().message == "The access token provided is expired, revoked or malformed.";
            }
        }

        public void Initialize(HttpResponseMessage response, string responseBody)
        {
            isInitialized = true;
            this.response = response;
            this.IsSuccessStatusCode = response.IsSuccessStatusCode;
            this.statusCode = response.StatusCode;
            this.responseBody = responseBody;
        }

        public void Throw()
        {
            if (!IsError) throw new InvalidOperationException("Can only throw when there is an error");
            DocusignError error = GetData<DocusignError>();
            error.Throw();
        }

        public T GetData<T>()
        {
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }

    public class DocusignError
    {
        public string message;

        public void Throw()
        {
            throw new Exception(message);
        }
    }
}
