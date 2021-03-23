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
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            string email = EmailInput.Text.Trim();
            string password = PasswordInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;

            //validate whatever and send to server
            ConfirmLabel.IsVisible = true;
        }
    }
}