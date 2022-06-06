using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using FootyPunditsApp.Services;
using FootyPunditsApp.Models;
using System.Linq;
using Xamarin.Essentials;
using FootyPunditsApp.Views;

namespace FootyPunditsApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public event Action<Page> Push;

        private string email;
        private string password;
        private string error;

        public string Error
        {
            get => error;
            set
            {
                error = value;
                //if (this.ShowError)
                //    ValidateError();
                OnPropertyChanged("Error");
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                //if (this.ShowEmailError)
                //    ValidateEmail();
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                //if (this.ShowPasswordlError)
                //    ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        public ICommand SignUpCommand { get; set; }// login Command
        public ICommand LoginCommand { get; set; }// login Command
        public ICommand ForgotPassCommand { get; set; }// login Command

        public LoginViewModel()
        {
            Error = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            LoginCommand = new Command(Login);
            SignUpCommand = new Command(CreateAccount);
            //ForgotPassCommand = new Command(ForgotPass);
        }
        // פעולות
        private void CreateAccount()
        {
            Push?.Invoke(new FootyPunditsApp.Views.TabControlView());
        }

        //private void ForgotPass()
        //{
        // Push?.Invoke(new FootyPunditsApp.Views.ForgetPassword1());
        //}

        private async void Login()
        {
            FootyPunditsAPIProxy proxy = FootyPunditsAPIProxy.CreateProxy();

            try
            {
                UserAccount u = await proxy.LoginAsync(Email, Password);
                if (u != null)
                {
                    ((App)App.Current).CurrentUser = u;
                    ((App)App.Current).CurrentUser.VotesHistories = await proxy.GetUserVoteHistory(u.AccountId);
                    Push?.Invoke(new FootyPunditsApp.Views.TabControlView());
                }
                else
                {
                    Error = "This Account Doesn't Exist";

                }
            }

            catch (Exception)
            {
                Error = "Something went Wrong";
            }
        }


        public Command RegisterCommand => new Command(Register);
        private void Register()
        {
            Push.Invoke(new Views.SignUpView());
        }


    }
}


