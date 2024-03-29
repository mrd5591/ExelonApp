﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExelonApp.Util;
using Flurl;
using TinyAccountManager.Abstraction;

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

            try
            {
                string jsonResult = RESTClient.Put(new Uri(App.url.AppendPathSegment("authenticate")), jsonString);

                JObject rss = JObject.Parse(jsonResult);

                bool result = (bool)rss["result"];

                if (!result)
                {
                    ErrorMessage.Text = (string)rss["errorMessage"];
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    App.userID = exelonId;
                    App.bearerToken = (string)rss["token"];

                    if (!App.account.Properties.ContainsKey("ExelonAppBearerToken"))
                        App.account.Properties.Add("ExelonAppBearerToken", App.bearerToken);
                    else
                        App.account.Properties["ExelonAppBearerToken"] = App.bearerToken;

                    if (!App.account.Properties.ContainsKey("ExelonAppUserID"))
                        App.account.Properties.Add("ExelonAppUserID", App.userID);
                    else
                        App.account.Properties["ExelonAppUserID"] = App.userID;

                    await AccountManager.Current.Save(App.account);

                    RESTClient.SetBearerToken(App.bearerToken);

                    await Navigation.PushAsync(new HomePage());
                    Navigation.RemovePage(this);
                }
            } catch(JsonReaderException e)
            {
                ErrorMessage.Text = "An unexpected server error has occured. Please try again.";
            }
        }

        private async void SwitchToSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
            Navigation.RemovePage(this);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorMessage.Text = "";
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