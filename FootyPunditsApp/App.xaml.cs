using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootyPunditsApp.Views;
using FootyPunditsApp.Models;



namespace FootyPunditsApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv = true;
        public UserAccount CurrentUser { get; set; }

        public App()
        {
            InitializeComponent();
            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            CurrentUser = null;

            MainPage = new LogInView();
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
