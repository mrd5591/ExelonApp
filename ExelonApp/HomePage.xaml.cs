﻿using System;
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
        HomepageModel hm;
        public HomePage()
        {
            InitializeComponent();
            hm = new HomepageModel();
            homepage.ItemsSource = hm.Histories;
        }
        //Button
        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
       private async void RefreshButtonClicked(object sender, EventArgs args)
        {
            //TODO Call the server rather than creating a new home page
            await Navigation.PushAsync(new HomePage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);

        }
    }
}