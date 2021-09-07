using System;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Helpers.Interface;
using PoC_Thomas.Models.Entities;
using Prism.Navigation;
using SQLite;
using SQLitePCL;
using Xamarin.Forms;

namespace PoC_Thomas.ViewModels
{
    public class AccountViewModel:BaseViewModel
    {
        public string Username { get; set;}
        public string Password { get; set; }
        public string Picture { get; set; }
        public Command CmdCreate { get; set; }
        private ISqliteNetHelper _sqliteNetHelper;

        // Constructor
        public AccountViewModel(INavigationService navigationService, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            this.CmdCreate = new Command(CreateAccount);
        }


        #region OnOnNavigatedToAsync
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);
        }
        #endregion


        // This function create a new UserEntity and save it in the Database
        public async void CreateAccount()
        {
            try
            {
                // If the username already exist in the database
                if (await SqliteNetHelper.UsernameExist(this.Username))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "This username is already used", "Ok");
                    Console.WriteLine("This username already exist in the database");
                }
                else
                {
                    await SqliteNetHelper.CreateUser(this.Username, this.Password, this.Picture);
                    await DoBackCommand();
                }
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
            
        }
    }
}
