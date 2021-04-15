﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExelonApp
{
    public partial class App : Application
    {
        public static string userID { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
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
