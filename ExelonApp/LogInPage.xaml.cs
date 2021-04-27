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
        public string errorMessage { get; set; }
        public LogInPage()
        {
            InitializeComponent();
        }
        private async void SubmitButton_Clicked(object sender, EventArgs args)
        {
            SubmitButton.IsEnabled = false;

            string exelonId = ExelonID.Text.Trim();
            string password = Password.Text.Trim();

            LogInInfo myConnection = new LogInInfo();
            myConnection.exelonId = exelonId;
            myConnection.password = password;
            myConnection.os = Device.RuntimePlatform;

            IDeviceUtils device = DependencyService.Get<IDeviceUtils>();
            myConnection.deviceId = device.GetDeviceId();
            myConnection.pnsToken = device.GetPNSToken();

            string jsonString = JsonConvert.SerializeObject(myConnection);

            //TODO Create util class
            //TODO Create error handler 
            string jsonResult = RESTClient.Put(new Uri("http://71.175.40.192:2456/authenticate"), jsonString);

            try
            {
                JObject rss = JObject.Parse(jsonResult);

                bool result = (bool)rss["result"];

                if (!result)
                {
                    errorMessage = (string)rss["errorMessage"];
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    App.userID = exelonId;
                    await Navigation.PushAsync(new HomePage());
                    Navigation.RemovePage(Navigation.NavigationStack[0]);
                }
            } catch(JsonReaderException e)
            {
                errorMessage = "An unexpected server error has occured. Please try again.";
            }
        }

        private async void SwitchToSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            errorMessage = "";
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