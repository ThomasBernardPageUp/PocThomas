using System;
using System.IO;
using System.Threading.Tasks;
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

        // Constructor
        public AccountViewModel(INavigationService navigationService) : base(navigationService)
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
                var db = new SQLiteAsyncConnection(App.DatabasePath);

                var result = await db.QueryAsync<UserEntity>("SELECT Id FROM UserEntity WHERE Username = '" + this.Username + "';");

                // If the username not already exist in the database
                if (result == null || result.Count == 0)
                {
                    var res = await db.ExecuteAsync("INSERT INTO UserEntity (Username, Password, Picture) VALUES ('" + this.Username + "', '" + this.Password + "', '" + this.Picture + "')");
                    Console.WriteLine("user " + this.Username + " created ! ");

                    await DoBackCommand();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "This username is already used", "Ok");
                    Console.WriteLine("This username already exist in the database");
                }

            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
            
        }
    }
}
