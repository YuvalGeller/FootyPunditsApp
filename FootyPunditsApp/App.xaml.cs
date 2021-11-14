using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootyPunditsApp.Views;


namespace FootyPunditsApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv = true;

        public App()
        {
            InitializeComponent();
            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            MainPage = new TabControlView();
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
