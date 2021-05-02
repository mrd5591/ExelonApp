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
        // TODO Set this to the Java server address
        public static Uri url = new Uri("http://71.175.40.192:2456");
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
                bool hasId = account.Properties.TryGetValue("ExelonAppUserID", out string id);
                if (hasToken && hasId && token != null && id != null)
                {
                    bearerToken = token;
                    userID = id;
                } else
                {
                    bearerToken = null;
                    userID = null;
                }
            } else
            {
                account = new Account()
                {
                    ServiceId = "ExelonAppAccountManager",
                    Username = "user1"
                    
                };

                bearerToken = null;
                userID = null;
            }
        }
        
        protected override void OnStart()
        {
            //AccountManager.Current.Remove("ExelonAppAccountManager");
            GetToken();

            if(bearerToken != null && userID != null)
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
