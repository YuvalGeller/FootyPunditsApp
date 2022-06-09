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
    class LoginViewModel : BaseViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public event Action<Page> Push;


        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set => SetValue(ref showEmailError, value);
        }

        private string email;

        public string Email
        {
            get => email;
            set => SetValue(ref email, value);
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set => SetValue(ref emailError, value);
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = false;
            if (string.IsNullOrEmpty(Email))
            {
                this.EmailError = "Email cannot be blank";
                this.ShowEmailError = true;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                this.ShowEmailError = addr.Address != email;
            }
            catch
            {
                this.EmailError = "Not a valid email";
                this.ShowEmailError = true;
            }

        }
        #endregion


        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set => SetValue(ref showPasswordError, value);
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetValue(ref password, value);
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set => SetValue(ref passwordError, value);
        }

        private void ValidatePassword()
        {
            ShowPasswordError = true;
            if (string.IsNullOrEmpty(Password))
                PasswordError = "Password cannot be blank";
            else if (Password.Length < 8)
                PasswordError = "Password must be more than 8 characters";
            else
                ShowPasswordError = false;
        }
        #endregion

        #region General Error
        private bool showGeneralError;

        public bool ShowGeneralError
        {
            get => showGeneralError;
            set => SetValue(ref showGeneralError, value);
        }

        private string generalError;

        public string GeneralError
        {
            get => generalError;
            set => SetValue(ref generalError, value);
        }
        #endregion

        public ICommand SignUpCommand { get; set; }// login Command
        public ICommand LoginCommand { get; set; }// login Command
        public ICommand ForgotPassCommand { get; set; }// login Command

        public LoginViewModel()
        {
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

        private bool ValidateForm()
        {
            if (((App)App.Current).CurrentUser != null)
            {
                GeneralError = "You are already logged in";
                return false;
            }

            ValidateEmail();
            ValidatePassword();

            return !(ShowEmailError || ShowPasswordError);
        }


        private async void Login()
        {
            FootyPunditsAPIProxy proxy = FootyPunditsAPIProxy.CreateProxy();

            if (ValidateForm())
            try
            {
                UserAccount u = await proxy.LoginAsync(Email, Password);
                if (u != null)
                {
                    ((App)App.Current).CurrentUser = u;
                    ((App)App.Current).CurrentUser.VotesHistories = await proxy.GetUserVoteHistory(u.AccountId);
                        //Push?.Invoke(new FootyPunditsApp.Views.TabControlView());
                        App.Current.MainPage = new NavigationPage(new FootyPunditsApp.Views.TabControlView());
                }
                else
                {
                    GeneralError = "This Account Doesn't Exist";

                }
            }

            catch (Exception)
            {
                GeneralError = "Something went Wrong";
            }
        }


        public Command RegisterCommand => new Command(Register);
        private void Register()
        {
            Push.Invoke(new Views.SignUpView());
        }


    }
}


