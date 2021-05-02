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
            SubmitButton.IsEnabled = false;

            string firstName = FirstName.Text.Trim();
            string lastName = LastName.Text.Trim();
            string exelonID = ExelonID.Text.Trim();
            string backUpEmail = BackUpEmail.Text.Trim();
            string password = Password.Text.Trim();
            string confirmPassword = ConfirmPassword.Text.Trim();

            Regex emailFormat = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            
            bool startWithLetter = Char.IsLetter(password[0]);
            var containNumber = new Regex(@"[0-9]+");
            var containUpperCase = new Regex(@"[A-Z]+");
            var containLowerCase = new Regex(@"[a-z]+");
            bool containsSpecial = Regex.IsMatch(password, @"(?i)^[a-z’'()/.!,\s-]+$");
            var minMaxChar = new Regex(@".{8,15}");
            var passwordFormat = startWithLetter && containsSpecial && containNumber.IsMatch(password) &&
                containUpperCase.IsMatch(password) && containLowerCase.IsMatch(password) && minMaxChar.IsMatch(password);
            
            if(firstName.Length < 2 || !Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
            {
                ErrorMessage.Text = "Please enter a valid first name.";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
                return;
            } else if(lastName.Length < 2 || !Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
            {
                ErrorMessage.Text = "Please enter a valid last name.";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
                return;
            } else if(exelonID.Length != 6 || !int.TryParse(exelonID, out _))
            {
                ErrorMessage.Text = "Please enter a valid exelon id. It must be 6 numbers.";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
                return;
            } else if(!emailFormat.IsMatch(backUpEmail))
            {
                ErrorMessage.Text = "Please enter a valid email address.";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
                return;
            } else if(!passwordFormat)
            {
                ErrorMessage.Text = "Please enter a valid password. It must contain uppercase, lowercase, numeric, and special characters.";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
                return;
            } else if(!password.Equals(confirmPassword))
            {
                ErrorMessage.Text = "The password fields must match!";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
                return;
            }

            SignUpInfo myConnection = new SignUpInfo();
            myConnection.exelonId = exelonID;
            myConnection.email = backUpEmail;
            myConnection.firstName = firstName;
            myConnection.lastName = lastName;
            myConnection.password = password;
            myConnection.os = Device.RuntimePlatform;

            IDeviceUtils device = DependencyService.Get<IDeviceUtils>();
            myConnection.deviceId = device.GetDeviceId();
            myConnection.pnsToken = device.GetPNSToken();

            string jsonString = JsonConvert.SerializeObject(myConnection);

            try
            {
                string jsonResult = RESTClient.Post(new Uri(App.url.AppendPathSegment("authenticate")), jsonString);

                JObject rss = JObject.Parse(jsonResult);

                bool result = (bool)rss["result"];

                if (!result)
                {
                    ErrorMessage.Text = (string)rss["errorMessage"];
                    ErrorMessage.IsVisible = true;
                    SubmitButton.IsEnabled = true;
                }
                else
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
            catch (JsonReaderException e)
            {
                ErrorMessage.Text = "An unexpected server error has occured. Please try again.";
                ErrorMessage.IsVisible = true;
                SubmitButton.IsEnabled = true;
            }
        }

        private async void SwitchToSignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
            Navigation.RemovePage(this);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorMessage.IsVisible = false;
            ErrorMessage.Text = "";
            if (string.IsNullOrWhiteSpace(BackUpEmail.Text) || string.IsNullOrWhiteSpace(LastName.Text) || string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(ExelonID.Text) || string.IsNullOrWhiteSpace(ConfirmPassword.Text) || string.IsNullOrWhiteSpace(Password.Text))
            {
                SubmitButton.IsEnabled = false;
            }
            else
            {
                SubmitButton.IsEnabled = true;
            }
        }
    }
}