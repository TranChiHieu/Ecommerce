using Bitis.Library.Shared.Ultity;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bitis.Ecommerce.Domain.Service
{
    public class WebProvider
    {
        protected NameValueCollection Header { get; }

        public WebProvider(string clientid, string clientsecret)
        {

            Header = new NameValueCollection();
            var authorizationValue = $"{clientid}:{clientsecret}";
            Header.Add("Authorization", $"Basic {authorizationValue.EncodeBase64()}");
        }
        public WebProvider()
        {
            Header = new WebHeaderCollection();
        }
        public TResult PostWebApi<TResult>(string requestUri, object dataJson)
        {
            string strResponse;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            if ((Header?.Count ?? 0) > 0)
                httpWebRequest.Headers.Add(Header);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(dataJson,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                strResponse = streamReader.ReadToEnd();
            }

            var result = JsonConvert.DeserializeObject<TResult>(strResponse);
            return result;
        }
        public TResult GetWebApi<TResult>(string requestUrl, NameValueCollection parameters)
        {
            Uri requestUri;
            if (parameters?.Count > 0)
            {
                var builder = new UriBuilder(requestUrl)
                {
                    Port = -1
                };
                var query = HttpUtility.ParseQueryString(builder.Query);
                query.Add(parameters);
                builder.Query = query.ToString();
                requestUri = builder.Uri;
            }
            else
            {
                requestUri = new Uri(requestUrl);
            }

            string strResponse;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            if ((Header?.Count ?? 0) > 0)
                httpWebRequest.Headers.Add(Header);


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                strResponse = streamReader.ReadToEnd();
            }

            var result = JsonConvert.DeserializeObject<TResult>(strResponse);
            return result;
        }
    }
}
