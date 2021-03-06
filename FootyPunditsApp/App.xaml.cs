using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootyPunditsApp.Views;
using FootyPunditsApp.Models;
using Syncfusion.Licensing;
using FootyPunditsApp.Services;

namespace FootyPunditsApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv = true;
        public UserAccount CurrentUser { get; set; }

        public static string APIKey = "0cccc1f57ccf41dfbeec13653210c1b0";
        public ChatService chatService = new ChatService();

        public App()
        {
            InitializeComponent();
            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            CurrentUser = null;

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg1MDgwQDMxMzkyZTM0MmUzMFZwbm11cFVzbFZ3Uk4xS2hFb2tQZnd5Z2tnV2NnNk90YUJpa0Z0dklYUzg9");

            string[] styles = new string[] { "MainBackgroundStyle", "InputFieldStyle", "TitleStyle", "TabStyle" };

            Resources.Add("secondaryText", Color.FromHex("626262"));

            MainPage = new NavigationPage(new LogInView());
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
