using ExelonApp.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
       private void RefreshButtonClicked(object sender, EventArgs args)
       {
            GetHistory();
       }

        private void GetHistory()
        {
            NotificationHistory = JsonConvert.DeserializeObject<List<Notification>>(RESTClient.Get(new Uri(App.url.ToString() + "/history/" + App.userID)));
        }
    }
}