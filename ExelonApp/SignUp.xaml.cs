using System;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Flurl;

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

                string jsonResult = Post(new Uri(App.url.AppendPathSegment("authenticate")), jsonString);

                JObject rss = JObject.Parse(jsonResult);

                bool result = (bool)rss["result"];

                if (!result)
                {
                    string errorMessage = (string)rss["errorMessage"];
                } else
                {
                    await Navigation.PushAsync(new LogInPage());
                    Navigation.RemovePage(Navigation.NavigationStack[0]);
                }
            }
        }

        public static string Post(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            string jsonResult = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        public static string Put(Uri url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            string jsonResult = client.PutAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        private async void SwitchToSignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
        }
    }
}