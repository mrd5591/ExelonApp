using ExelonApp.Util;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExelonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Notification> NotificationHistory { get; set; }
        public HomePage()
        {
            InitializeComponent();

            NotificationHistory = new ObservableCollection<Notification>();

            GetHistory();

            BindingContext = this;
        }
        //Button
        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            if (App.account.Properties.ContainsKey("ExelonAppBearerToken"))
                App.account.Properties.Remove("ExelonAppBearerToken");

            if (App.account.Properties.ContainsKey("ExelonAppUserID"))
                App.account.Properties.Remove("ExelonAppUserID");

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
                NotificationHistory.Clear();
                foreach(Notification notification in rss["resultSet"].ToObject<ObservableCollection<Notification>>().Where<Notification>(x => x.confirm).OrderByDescending(X => X.timestamp))
                {
                    NotificationHistory.Add(notification);
                }
                foreach(Notification notification in rss["resultSet"].ToObject<ObservableCollection<Notification>>().Where<Notification>(x => !x.confirm).OrderByDescending(X => X.timestamp))
                {
                    NotificationHistory.Add(notification);
                }

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
            bool answer = await DisplayAlert("Confirmation", "Are you sure you would like to confirm?", "Confirm", "Cancel");
            if (answer.Equals(true))
            {
                string response = RESTClient.Post(new Uri(App.url.AppendPathSegments("confirm", ((Button)sender).CommandParameter)), null);

                try
                {
                    JObject res = JObject.Parse(response);

                    bool result = (bool)res["result"];

                    if (result)
                        GetHistory();
                } catch(JsonReaderException ex)
                {

                }
            }
        }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        protected void RaisePropertyChange([CallerMemberName] string memberName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(memberName));
            }
        }
    }
}