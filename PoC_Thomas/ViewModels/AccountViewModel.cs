using System;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Helpers.Interface;
using PoC_Thomas.Models.Entities;
using PoC_Thomas.Repositories.Interface;
using PoC_Thomas.Services.Interface;
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
        public Command CreateCommand { get; set; }
        public Command PictureCommand { get; set; }

        private ICameraService _cameraService;
        private IUserRepository _userRepository;


        // Constructor
        public AccountViewModel(INavigationService navigationService, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            this.CreateCommand = new Command(CreateAccount);
            this.PictureCommand = new Command(TakePicture);
            this._cameraService = DependencyService.Get<ICameraService>();
            // this._userRepository = userRepository;
        }


        #region OnOnNavigatedToAsync
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);
        }
        #endregion

        public async void TakePicture()
        {
            var photo = await _cameraService.TakePictureAsync();

        }

        // This function create a new UserEntity and save it in the Database
        public async void CreateAccount()
        {
            if(Username == "" || Username == " " || Password == "" || Password == " ")
            {
                await App.Current.MainPage.DisplayAlert("Error", "please enter one username and one password", "Ok");
            }
            else
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
                        await SqliteNetHelper.CreateUser(this.Username, this.Password, this.PictureUrl);
                        await DoBackCommand();
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            } 
        }

        private string _pictureUrl;
        public string PictureUrl
        {
            get { return _pictureUrl; }
            set { SetProperty(ref _pictureUrl, value); }
        }
    }
}
