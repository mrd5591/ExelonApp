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
            //string jsonResult = Post(new Uri("http://10.0.2.2:8080/history/" + App.userID), "");
            //List<notificationHistory> histories = JsonConvert.DeserializeObject<List<notificationHistory>>(jsonResult);
            List<notificationHistory> histories = new List<notificationHistory>()
            {
                new notificationHistory()
                {
                    timestamp = "04/14/2021 00:00:01",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/13/2021 12:00:01",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/13/2021 11:57:43",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/12/2021 17:20:11",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/12/2021 09:00:01",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/11/2021 00:00:01",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/10/2021 13:12:11",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/10/2021 11:09:21",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/09/2021 00:00:01",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/09/2021 02:15:56",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/08/2021 09:16:12",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/08/2021 08:43:56",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/08/2021 06:33:26",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/08/2021 06:10:35",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/08/2021 05:43:53",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/08/2021 03:12:20",
                    message = "sameple message"
                },
                new notificationHistory()
                {
                    timestamp = "04/07/2021 19:47:03",
                    message = "sameple message"
                }
            };
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
