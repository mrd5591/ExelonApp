using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using

namespace ExelonApp
{
    public partial class App : Application
    {
        public static string userID { get; set; }
        // TODO Set this to the MacMini IP address
        public static Uri url = new Uri("http://10.0.2.2:8080");
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogInPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
