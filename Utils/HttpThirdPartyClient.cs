using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnsekAPITests.Utils
{
    public static class HttpThirdPartyClient
    {
        private static HttpClient _httpClient = new HttpClient();

        public static async Task<string> GetRequest(string token, string endpoint, string urlSegment)
        {
            HttpClient _httpClient = BuildHttpRequest(token, endpoint);
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(urlSegment);
            var statusCodeMessage = httpResponse.EnsureSuccessStatusCode();
            string res = await httpResponse.Content.ReadAsStringAsync();
            return res;
        }

        public static async Task<string> PostRequest(string endpoint,string urlSegment,string fileName)
        {
            HttpClient _httpClient = BuildHttpRequest("", endpoint);
            string res = string.Empty;

            JObject payload = new JObject();

            using (StreamReader file = File.OpenText(fileName))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                payload = (JObject)JToken.ReadFrom(reader);
            }

            HttpContent httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            _httpClient.Timeout = TimeSpan.FromMinutes(5);

            var httpResponse = _httpClient.PostAsync(urlSegment, httpContent).Result;
            var statusCodeMessage = httpResponse.EnsureSuccessStatusCode();
            res = await httpResponse.Content.ReadAsStringAsync();
            
            return res;
        }
        public static async Task<string> PostRequest(string token, string endpoint, string urlSegment, string fileName)
        {
            HttpClient _httpClient = BuildHttpRequest(token, endpoint);
            string res = string.Empty;

            JObject payload = new JObject();

            using (StreamReader file = File.OpenText(fileName))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                payload = (JObject)JToken.ReadFrom(reader);
            }

            Thread.Sleep(3000);

            HttpContent httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            _httpClient.Timeout = TimeSpan.FromMinutes(5);


            var httpResponse = _httpClient.PostAsync(urlSegment, httpContent).Result;
            Thread.Sleep(2000);
            var statusCodeMessage = httpResponse.EnsureSuccessStatusCode();
            res = await httpResponse.Content.ReadAsStringAsync();
            Thread.Sleep(2000);

            return res;
        }

        public static async Task<string> PutRequest(string token, string endpoint, string urlSegment)
        {
            var client = new RestClient(endpoint);
            HttpClient _httpClient = BuildHttpRequest(token, endpoint);
            string res = string.Empty;

            //HttpContent httpContent = new StringContent(urlSegment.ToString(), Encoding.UTF8);
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            
            //var req = new RestRequest(urlSegment, Method.Post);
            //req.RequestFormat = DataFormat.Json;
            //req.AddUrlSegment("id", 1);
            //req.AddUrlSegment("quantity", 1);

            //var response = client.Execute(req);

            var httpResponse = await _httpClient.PutAsync(urlSegment, null);
            Thread.Sleep(2000);
            var statusCodeMessage = httpResponse.EnsureSuccessStatusCode();
            res = await httpResponse.Content.ReadAsStringAsync();
            Thread.Sleep(2000);

            return res;
        }

        public static async Task<string> DeleteRequest(string token, string endpoint, string deleteUrlSegment)
        {
            HttpClient _httpClient = BuildHttpRequest(token, endpoint);
            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(deleteUrlSegment);
            HttpStatusCode code = httpResponse.StatusCode;
            string res = await httpResponse.Content.ReadAsStringAsync();
            return res;
        }

        public static HttpClient BuildHttpRequest(string token, string endpoint)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();

            _httpClient = new HttpClient(httpClientHandler, false);
            _httpClient.BaseAddress = new Uri(endpoint);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return _httpClient;
        }
    }
}
