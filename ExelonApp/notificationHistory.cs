using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace ExelonApp
{
    public class notificationHistory
    {
        public string timestamp { get; set; }
        public string message { get; set; }
        public string notificationId { get; set; }

        public List<notificationHistory> GetHistories()
        {
            string jsonResult = Post(new Uri("http://10.0.2.2:8080/history/" + App.userID), "");
            List<notificationHistory> histories = JsonConvert.DeserializeObject<List<notificationHistory>>(jsonResult);
            return histories;
        }
        public static string Post(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            string jsonResult = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

    }
}
