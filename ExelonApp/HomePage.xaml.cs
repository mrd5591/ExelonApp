﻿using ExelonApp.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Flurl;
using Newtonsoft.Json.Linq;
using TinyAccountManager.Abstraction;

namespace ExelonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public List<Notification> NotificationHistory { get; set; }
        public HomePage()
        {
            InitializeComponent();

            GetHistory();

            BindingContext = this;
        }
        //Button
        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
            Navigation.RemovePage(this);
        }
        private void RefreshButtonClicked(object sender, EventArgs args)
        {
            GetHistory();
        }

        private async void GetHistory()
        {
            string jsonResult = RESTClient.Get(new Uri(App.url.AppendPathSegments(new object[] { "history", App.userID })));

            JObject rss = JObject.Parse(jsonResult);

            bool result = (bool)rss["result"];

            if(result)
            {
                NotificationHistory = rss["resultSet"].ToObject<List<Notification>>();
            } else
            {
                App.account.Properties.Remove("ExelonAppBearerToken");
                await AccountManager.Current.Save(App.account);

                await Navigation.PushAsync(new LogInPage());
                Navigation.RemovePage(this);
            }
        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Please confirm again.", "Confirm", "Cancel");
            if (answer.Equals(true))
            {
                string response = RESTClient.Post(new Uri(App.url.AppendPathSegments("confirm", ((Notification)sender).notificationId)), null);

                try
                {
                    JObject res = JObject.Parse(response);

                    bool result = (bool)res["result"];

                    if (result)
                        GetHistory();
                } catch(JsonReaderException e)
                {

                }
            }
        }
    }
}