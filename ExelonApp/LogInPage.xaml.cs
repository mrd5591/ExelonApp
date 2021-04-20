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
using ExelonApp.Util;

namespace ExelonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }
        private async void SubmitButton_Clicked(object sender, EventArgs args)
        {
            string exelonID = ExelonID.Text.Trim();
            string password = Password.Text.Trim();

            LogInInfo myConnection = new LogInInfo();
            myConnection.exelonID = exelonID;
            myConnection.password = password;
            myConnection.os = Device.RuntimePlatform;
            myConnection.deviceId = DependencyService.Get<IDeviceUtils>().GetDeviceId();

            string jsonString = JsonConvert.SerializeObject(myConnection);

            //TODO Create util class
            //TODO Create error handler 
            string jsonResult = RESTClient.Post(new Uri("http://10.0.2.2:8080/authenticate"), jsonString);

            JObject rss = JObject.Parse(jsonResult);

            string error = (string)rss["error"];

            if (error.Equals("true"))
            {
                string errorMessage = (string)rss["errorMessage"];
            }
            else
            {
                App.userID = exelonID;
                await Navigation.PushAsync(new HomePage());
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
        }

        private async void SwitchToSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
}