using System;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Flurl;
using TinyAccountManager.Abstraction;
using ExelonApp.Util;

namespace ExelonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private async void SubmitButton_Clicked(object sender, EventArgs args)
        {
            string firstName = FirstName.Text.Trim();
            string lastName = LastName.Text.Trim();
            string exelonID = ExelonID.Text.Trim();
            string backUpEmail = BackUpEmail.Text.Trim();
            string password = Password.Text.Trim();
            string confirmPassword = ConfirmPassword.Text.Trim();

            Regex emailFormat = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (emailFormat.IsMatch(backUpEmail) == false)
            {
                validEmail.IsVisible = true;
            }
            
            bool startWithLetter = Char.IsLetter(password[0]);
            var containNumber = new Regex(@"[0-9]+");
            var containUpperCase = new Regex(@"[A-Z]+");
            var containLowerCase = new Regex(@"[a-z]+");
            var minMaxChar = new Regex(@".{8,15}");
            var passwordFormat = startWithLetter.Equals(true) && containNumber.IsMatch(password) &&
                containUpperCase.IsMatch(password) && containLowerCase.IsMatch(password) && minMaxChar.IsMatch(password);
            
            if (passwordFormat.Equals(false))
            {
                validPassword.IsVisible = true;
            }
            
            if (String.Equals(password, confirmPassword) == false)
            {
                identicalPassword.IsVisible = true;
            }

            if ((emailFormat.IsMatch(backUpEmail) == true) && (passwordFormat.Equals(true)))
            {
                SignUpInfo myConnection = new SignUpInfo();
                myConnection.exelonId = exelonID;
                myConnection.email = backUpEmail;
                myConnection.firstName = firstName;
                myConnection.lastName = lastName;
                myConnection.password = password;

                string jsonString = JsonConvert.SerializeObject(myConnection);

                string jsonResult = RESTClient.Post(new Uri(App.url.AppendPathSegment("authenticate")), jsonString);

                JObject rss = JObject.Parse(jsonResult);

                bool result = (bool)rss["result"];

                if (!result)
                {
                    string errorMessage = (string)rss["errorMessage"];
                } else
                {
                    App.userID = exelonID;
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
            }
        }

        private async void SwitchToSignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
        }
    }
}