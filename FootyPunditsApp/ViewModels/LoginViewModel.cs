using FootyPunditsApp.Models;
using FootyPunditsApp.Services;
using FootyPunditsApp.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FootyPunditsApp.ViewModels
{
    class LoginViewModel : BaseViewModel    
    {
        private string email;
        private string password;
        private string error;

        public string Error// get eror
        {
            get
            {
                return error;
            }
            set
            {
                if (error != value)
                {
                    error = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email // get Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password // GetPassword
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SignUpCommand { get; set; }// login Command
        public ICommand LoginCommand { get; set; }// login Command
        public ICommand ForgotPassCommand { get; set; }// login Command


        public event Action<Page> Push;
        //public LoginPageViewModel()
        //{
        //    Error = string.Empty;
        //    Email = string.Empty;
        //    Password = string.Empty;
        //    LoginCommand = new Command(Login);
        //    SignUpCommand = new Command(CreateAccount);
        //    ForgotPassCommand = new Command(ForgotPass);


        //}
        // פעולות
        //private void CreateAccount()
        //{
        //    Push?.Invoke(new FootyPunditsApp.Views.SignUpPage());
        //}
        //private void ForgotPass()
        //{
        //    Push?.Invoke(new FootyPunditsApp.Views.ForgetPassword1());
        //}

    //    private async void Login()
    //    {
    //        FootyPunditsAPIProxy proxy = FootyPunditsAPIProxy.CreateProxy();

    //        try
    //        {
    //            UserAccount u = await proxy.LoginAsync(Email, Password);
    //            if (u != null)
    //            {
    //                ((App)App.Current).CurrentUser = u;
    //                Push?.Invoke(new FootyPunditsApp.Views.ProfilePage());
    //            }
    //            else
    //            {
    //                Error = "This Account Doesn't Exist";

    //            }
    //        }

    //        catch (Exception)
    //        {
    //            Error = "Something went Wrong";
    //        }
    //    }
    }
}
  
