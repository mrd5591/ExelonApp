using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExelonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private void SubmitButton_Clicked(object sender, EventArgs args)
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

            if (String.Equals(password, confirmPassword))
            {
                identicalPassword.IsVisible = true;
            }


            string constr = "server=virmire.database.windows.net; uid=VirmireAdmin; password=GrandHat132; initial catalog=TestingDB";

            using (SqlConnection myConnection = new SqlConnection(constr))
            {
                string oString = "Select * from User where exelonID=@exelonID";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@exelonID", exelonID);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    if (oReader.Read())
                    {
                        checkID.IsVisible = true;
                    }
                    else
                    {
                        string newString = "Insert into User (firstName, lastName, exelonID, backUpEmail, password) " +
                            "Values (@firstName, @lastName, @exelonID, @backUpEmail, @password)";
                        SqlCommand newCmd = new SqlCommand(newString, myConnection);
                        newCmd.Parameters.AddWithValue("@firstName", firstName);
                        newCmd.Parameters.AddWithValue("@lastName", lastName);
                        newCmd.Parameters.AddWithValue("@exelonID", exelonID);
                        newCmd.Parameters.AddWithValue("@backUpEmail", backUpEmail);
                        newCmd.Parameters.AddWithValue("@password", password);
                        newCmd.ExecuteNonQuery();
                    }

                    myConnection.Close();
                }
            }
        }
    }
}