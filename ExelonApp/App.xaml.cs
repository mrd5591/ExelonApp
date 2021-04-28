using System;
using Xamarin.Forms;
using TinyAccountManager.Abstraction;
using ExelonApp.Util;

namespace ExelonApp
{
    public partial class App : Application
    {
        public static string userID { get; set; }
        public static string bearerToken { get; set; }
        // TODO Set this to the MacMini IP address
        public static Uri url = new Uri("http://10.0.2.2:8080");
        private static Util.IAccountManager accountManager;
        public static Account account;
        public App()
        {
            InitializeComponent();

            accountManager = DependencyService.Get<Util.IAccountManager>();
            accountManager.Initialize();
        }

        private async void GetToken()
        {
            bool exists = await AccountManager.Current.Exists("ExelonAppAccountManager");

            if(exists)
            {
                account = await AccountManager.Current.Get("ExelonAppAccountManager");
                bool hasToken = account.Properties.TryGetValue("ExelonAppBearerToken", out string token);
                if (hasToken && token != null)
                {
                    bearerToken = token;
                } else
                {
                    bearerToken = null;
                }
            } else
            {
                account = new Account()
                {
                    ServiceId = "ExelonAppAccountManager"
                };

                bearerToken = null;
            }
        }
        
        protected override void OnStart()
        {
            GetToken();

            if(bearerToken != null)
            {
                RESTClient.SetBearerToken(bearerToken);
                MainPage = new NavigationPage(new HomePage());
            } else
            {
                MainPage = new NavigationPage(new LogInPage());
            }
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
