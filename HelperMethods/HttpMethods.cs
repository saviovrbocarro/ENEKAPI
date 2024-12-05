using EnsekAPITests.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnsekAPITests.Steps
{
    public static class HttpMethods
    {
        public static async Task<string> GetMethod(string token, string testServerEndpoint, string endpoint)
        {

            var t = await HttpThirdPartyClient.GetRequest(token, testServerEndpoint, endpoint);
            return t;
        }

        public static async Task<string> PostMethod(string testServerEndpoint, string loginEndpoint)
        {

            var t = await HttpThirdPartyClient.PostRequest(testServerEndpoint, loginEndpoint, @"C:\Users\savio\source\repos\EnsekAPITests\bin\Debug\netcoreapp3.1\Json\login.json");
            return t;
        }
        public static async Task<string> PostMethod(string token, string testServerEndpoint, string loginEndpoint)
        {

            var t = await HttpThirdPartyClient.PostRequest(token, testServerEndpoint, loginEndpoint, @"C:\Users\savio\source\repos\EnsekAPITests\bin\Debug\netcoreapp3.1\Json\login.json");
            return t;
        }

        public static async Task<string> PutMethod(string token, string testServerEndpoint, string endpoint)
        {

            var t = await HttpThirdPartyClient.PutRequest(token, testServerEndpoint, endpoint);
            return t;
        }
        public static async Task<string> DeleteMethod(string token, string testServerEndpoint, string endpoint)
        {

            var t = await HttpThirdPartyClient.DeleteRequest(token, testServerEndpoint, endpoint);
            return t;
        }

    }
}
