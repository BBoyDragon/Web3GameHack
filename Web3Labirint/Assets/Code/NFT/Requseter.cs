using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography; 

namespace NFT
{
    public class Requseter
    {
        private static readonly HttpClient client = new HttpClient();
        private Header[] defaultHeadres;

        public void SetDefaultHeaders(Header[] headers)
        {
            defaultHeadres = headers;
        }

        public string Get(string url, Header[] headers = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                return SendRequest(request, headers);
            }
        }
        
        public string Post(string url, Header[] headers = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                return SendRequest(request, headers);
            }
        }
        
        public string Post(string url, string json, Header[] headers = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                return SendRequest(request, headers, json);
            }
        }
        
        public string Put(string url, string json, Header[] headers = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                return SendRequest(request, headers, json);
            }
        }

        private string SendRequest(HttpRequestMessage request, Header[] headers, string json = null)
        { 
            SetDeafaultHeadersToRequest(request);
            SetHeadersToRequest(request, headers);
            if (json != null)
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");;
            }
            var response = client.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result; 
        }

        private void SetDeafaultHeadersToRequest(HttpRequestMessage request)
        {
            SetHeadersToRequest(request, defaultHeadres);
        }

        private static void SetHeadersToRequest(HttpRequestMessage request, Header[] headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.name, header.value);
                }
            }
        }
    }

    public struct Header
    {
        public string name;
        public string value;
        
        public Header(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
