using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Concurrent;


namespace BenMann.Docusign
{
    public class AuthenticationError
    {
        public readonly bool IsError;
        public readonly string ErrorMessage;

        private const string errorHeader = "Error communicating with DocuSign API: ";

        public AuthenticationError(bool IsError, string ErrorMessage = "")
        {
            this.IsError = IsError;
            this.ErrorMessage = ErrorMessage;
        }
        public bool NeedsRefresh()
        {
            if (IsError)
            {
                if (ErrorMessage == "The access token provided is expired, revoked or malformed.") return true;
            }
            return false;
        }
        public void Test()
        {
            if (!IsError) return;
            else throw new WebException(errorHeader + ErrorMessage);
        }
    }
    public class AuthenticationAgent
    {
        private  string RestApiUrl;
        private  string ClientId;
        private  string ClientSecret;
        private  string RedirectUrl;

        private readonly int serverTimeout;

        public AuthenticationError restApiError = new AuthenticationError(false);

        private const string OAuthEndpoint = "oauth/auth";
        private const string TokenEndpoint = "oauth/token";
        private const string UserInfoEndpoint = "oauth/userinfo";

        public string authUrl;
        public ConcurrentDictionary<string, string> authCode;
        public volatile AuthToken authToken;
        public volatile UserInfo userInfo;
        public volatile bool getAuthCodeSuccess;

        public AuthenticationAgent(string RestApiUrl, string ClientId, string ClientSecret, string RedirectUrl, int serverTimeout = 10000)
        {
            this.RestApiUrl = RestApiUrl;
            this.ClientId = ClientId;
            this.ClientSecret = ClientSecret;
            this.RedirectUrl = RedirectUrl;

            this.serverTimeout = serverTimeout;
        }

        /*
        public void MessWithRestApiUrl()
        {
            RestApiUrl = "www.google.com/";
        }
        public void MessWithClientId()
        {
            ClientId = "123";
        }
        public void MessWithClientSecret()
        {
            ClientSecret = "lol";
        }
        public void MessWithRedirectUrl()
        {
            RedirectUrl = "localhost:1000";
        }



        public void MessWithUserInfo_AccountID()
        {
            userInfo.GetDefaultAccount().account_id = "123";
        }

        public void MessWithUserInfo_BaseUri()
        {
            userInfo.GetDefaultAccount().base_uri = "www.dsoifosifd333dfdf.com";
        }
      */
        public void MessWithAuthToken_AccessToken()
        {
            authToken.access_token = "lol";
        }
          

        private void SetError(string json)
        {
            Dictionary<string, string> ErrorDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (!ErrorDict.ContainsKey("error"))
            {
                this.restApiError = new AuthenticationError(true, "Unexpected behaviour from Docusign API");
            }
            else
            {
                this.restApiError = new AuthenticationError(true, ErrorDict["error"]);
            }
        }



        public void GetAuthUrl()
        {
            Dictionary<string, string> query = new Dictionary<string, string>()
            {
                { "response_type", "code" },
                { "scope", "signature" },
                { "client_id", ClientId },
                { "state", "abc123" },
                { "redirect_uri", RedirectUrl }
            };

            authUrl = RestApiUrl + OAuthEndpoint + HttpAgent.BuildQuery(query);
        }
        public void GetAuthCode()
        {
            Action runServer = new Action(RunServer);
            TimeoutAction t = new TimeoutAction(serverTimeout, runServer);
            getAuthCodeSuccess = t.Start();
        }
        
        public void RunServer()
        {
            var uri = new Uri(RedirectUrl);
            string redirectUrlBase = uri.GetLeftPart(UriPartial.Authority);
            string redirectUrlPath = uri.AbsolutePath;



            HttpAgent._Listener.Prefixes.Add(redirectUrlBase + "/");
            HttpAgent._Listener.Start();

            string path = "";
            ConcurrentDictionary<string, string> query = new ConcurrentDictionary<string, string>();

            while (path != redirectUrlPath || !query.ContainsKey("code"))
            {
                HttpListenerContext context = HttpAgent._Listener.GetContext();
                path = context.Request.Url.AbsolutePath;

                if (path == redirectUrlPath)
                {
                    query = new ConcurrentDictionary<string, string>();

                    string queryString = context.Request.Url.Query;
                    if (queryString.Length > 0)
                        queryString = queryString.Substring(1, queryString.Length - 1);
                    string[] queryItems = queryString.Split('&');

                    foreach (var item in queryItems)
                    {
                        string[] pair = item.Split('=');
                        if (pair.Length == 2)
                            query[pair[0]] = pair[1];
                    }
                }
                byte[] _responseArray = Encoding.UTF8.GetBytes("<!DOCTYPE html> <html><head><style> body { text-align: center } #AuthCodeSuccess { font-family: Arial, Helvetica, sans-serif; font-size: 30pt; display: inline-block; margin: 100px auto; background-color: #eee; text-align: center} </style></head><body><div id='AuthCodeSuccess' background: red > Authentication Code Retrieved </div></body></html>"); //Write web  page response
                context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length);
                context.Response.KeepAlive = false;
                context.Response.Close();
            }
            this.authCode = query;
        }


        public async Task GetAuthToken()
        {
            byte[] tokenBytes = System.Text.Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret);
            string codeAuthBase64 = System.Convert.ToBase64String(tokenBytes);

            var body = "grant_type=authorization_code&code=" + authCode["code"];
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Authorization", "Basic "+codeAuthBase64 }
            };

            HttpResponseMessage response = await HttpAgent.Request(HttpMethod.Post, RestApiUrl + TokenEndpoint, null, body, headers, "application/x-www-form-urlencoded");
            string json = await response.Content.ReadAsStringAsync();

            authToken = JsonConvert.DeserializeObject<AuthToken>(json);

            if (!response.IsSuccessStatusCode||
                authToken.access_token == null ||
                authToken.expires_in == 0 ||
                authToken.refresh_token == null ||
                authToken.token_type == null)
            {
                SetError(json);
            }

        }

        public async Task RefreshAuthToken()
        {
            byte[] tokenBytes = System.Text.Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret);
            string codeAuthBase64 = System.Convert.ToBase64String(tokenBytes);

            var body = "grant_type=refresh_token&refresh_token=" + authToken.refresh_token;
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Authorization", "Basic "+codeAuthBase64 }
            };

            HttpResponseMessage response = await HttpAgent.Request(HttpMethod.Post, RestApiUrl + TokenEndpoint, null, body, headers, "application/x-www-form-urlencoded");
            string json = await response.Content.ReadAsStringAsync();

            authToken = JsonConvert.DeserializeObject<AuthToken>(json);

            if (!response.IsSuccessStatusCode ||
                authToken.access_token == null ||
                authToken.expires_in == 0 ||
                authToken.refresh_token == null ||
                authToken.token_type == null)
            {
                SetError(json);
            }
        }

        public async Task GetUserInfo()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Authorization", "Bearer " + authToken.access_token }
            };
            HttpResponseMessage response = await HttpAgent.Request(HttpMethod.Get, RestApiUrl + UserInfoEndpoint, null, "", headers);
            string json = await response.Content.ReadAsStringAsync();
            userInfo = JsonConvert.DeserializeObject<UserInfo>(json);

            if (!response.IsSuccessStatusCode||
                userInfo.accounts == null) {
                SetError(json);
            }
        }
    }
}
