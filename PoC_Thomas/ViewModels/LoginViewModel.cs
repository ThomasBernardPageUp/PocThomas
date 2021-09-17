using System;
using System.Net.Http;
using System.Threading.Tasks;
using PoC_Thomas.Commons;
using PoC_Thomas.Helpers.Interface;
using Xamarin.Forms;
using Prism.Navigation;
using System.IO;
using SQLite;
using PoC_Thomas.Models.Entities;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace PoC_Thomas.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public Command CreateAccountCommand { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        // Constructor
        public LoginViewModel(INavigationService navigationService, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            this.LoginCommand = new Command(VerifyLogin);
            this.CreateAccountCommand = new Command(AccountPage);
        }


        // On navigated to this page
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);


           
        }



        // This function verify the userand the password and connect the user .
        public async void VerifyLogin()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter values in entries", "Ok");
                Console.WriteLine("No value in entries");
                return;
            }

            UserEntity user = await SqliteNetHelper.UserConnection(this.Username, this.Password);

            if (user != null)
            {
                App.UserId = user.Id;
                Console.WriteLine("Connected with the user Id : " + App.UserId);

                await NavigationService.NavigateAsync(Constants.MenuPage);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Wrong login or password", "retry");
                Console.WriteLine("User not found in the database");
            }
        }


        // Go to the accountPage
        public async void AccountPage()
        {
            await NavigationService.NavigateAsync(Constants.AccountPage);
        }
        

    }
}
