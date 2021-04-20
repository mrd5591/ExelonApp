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
            string exelonId = ExelonID.Text.Trim();
            string password = Password.Text.Trim();

            LogInInfo myConnection = new LogInInfo();
            myConnection.exelonId = exelonId;
            myConnection.password = password;
            myConnection.os = Device.RuntimePlatform;
            myConnection.deviceId = DependencyService.Get<IDeviceUtils>().GetDeviceId();

            string jsonString = JsonConvert.SerializeObject(myConnection);

            //TODO Create util class
            //TODO Create error handler 
            string jsonResult = RESTClient.Put(new Uri("http://10.0.2.2:8080/authenticate"), jsonString);

            JObject rss = JObject.Parse(jsonResult);

            bool result = (bool)rss["result"];

            if (!result)
            {
                string errorMessage = (string)rss["errorMessage"];
            } else
            {
                App.userID = exelonId;
                await Navigation.PushAsync(new HomePage());
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
        }

        private async void SwitchToSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ExelonID.Text) || string.IsNullOrWhiteSpace(Password.Text))
            {
                SubmitButton.IsEnabled = false;
            } else
            {
                SubmitButton.IsEnabled = true;
            }
        }
    }
}