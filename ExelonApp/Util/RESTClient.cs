using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace ExelonApp.Util
{
    class RESTClient
    {
        private static HttpClient client = new HttpClient{ BaseAddress = App.url };

        public static string Post(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string jsonResult = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        public static string Put(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string jsonResult = client.PutAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        public static string Get(Uri url)
        {
            string jsonResult = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        public static void SetBearerToken(string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
