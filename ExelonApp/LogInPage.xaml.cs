using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExelonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }
        private void SubmitButton_Clicked(object sender, EventArgs args)
        {
            string exelonID = ExelonID.Text.Trim();
            string password = Password.Text.Trim();

            LogInInfo myConnection = new LogInInfo();
            myConnection.exelonID = exelonID;
            myConnection.password = password;

            string jsonString = JsonConvert.SerializeObject(myConnection);

            string jsonResult = Post(new Uri("http://10.0.2.2:8080/authenticate"), jsonString);

            JObject rss = JObject.Parse(jsonResult);

            string error = (string)rss["error"];

            if (error.Equals("true"))
            {
                string errorMessage = (string)rss["errorMessage"];
            }
            else
            {
                //Move over to Homepage
                //Then
                App.userID = exelonID;
            }
        }

        public static string Post(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            string jsonResult = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        public static string Put(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            string jsonResult = client.PutAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }
    }
}