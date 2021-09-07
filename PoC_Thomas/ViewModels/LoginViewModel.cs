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

namespace PoC_Thomas.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command CmdLogin { get; set; }
        public Command CmdAccount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        // Constructor
        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.CmdLogin = new Command(CommandLogin);
            this.CmdAccount = new Command(CommandAccount);
        }


        // On navigated to this page
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            var db = new SQLiteAsyncConnection(dbPath);

            await db.ExecuteAsync("CREATE TABLE IF NOT EXISTS 'UserEntity'('Id' INTEGER NOT NULL, 'Username' TEXT,'Password' TEXT, 'Picture' TEXT,PRIMARY KEY(\"Id\" AUTOINCREMENT));");
            await db.ExecuteAsync("CREATE TABLE IF NOT EXISTS 'CharacterEntity' ('Id' INTEGER NOT NULL, 'IdCreator' INTEGER NOT NULL, 'Name' TEXT, 'Image' TEXT, 'Species' TEXT, 'Origin' TEXT, PRIMARY KEY(\"Id\",\"IdCreator\") );");
        }



        // This function verify the userand the password and connect the user .
        public async void CommandLogin()
        {
            var db = new SQLiteAsyncConnection(App.DatabasePath);

            UserEntity user = await db.Table<UserEntity>().Where(u => u.Username == this.Username && u.Password == this.Password).FirstOrDefaultAsync();
            Console.WriteLine(db.Table<UserEntity>().ToString());

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
        public async void CommandAccount()
        {
            await NavigationService.NavigateAsync(Constants.AccountPage);
        }
        

    }
}
