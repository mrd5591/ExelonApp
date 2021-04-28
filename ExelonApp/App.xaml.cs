using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExelonApp
{
    public partial class App : Application
    {
        public static string userID { get; set; }
        // TODO Set this to the MacMini IP address
        public static Uri url = new Uri("http://71.175.40.192:2456");
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
