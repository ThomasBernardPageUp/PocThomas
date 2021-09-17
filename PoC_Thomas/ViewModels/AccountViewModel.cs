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
        public Command FileCommand { get; set; }

        private IStorageService _storageService;
        private ICameraService _cameraService;
        // private IUserRepository _userRepository;


        // Constructor
        public AccountViewModel(INavigationService navigationService, ISqliteNetHelper sqliteNetHelper, IStorageService storageService) : base(navigationService, sqliteNetHelper)
        {
            this.CreateCommand = new Command(CreateAccount);
            this.PictureCommand = new Command(TakePicture);
            this.FileCommand = new Command(LoadFile);
            this._cameraService = DependencyService.Get<ICameraService>();
            this._storageService = storageService;
            //this._userRepository = userRepository;
        }


        #region OnOnNavigatedToAsync
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            //var res = await _userRepository.GetItemsAsync();
        }
        #endregion

        public async void LoadFile()
        {
            PictureUrl = await _storageService.PickImagesAsync();
        }

        public async void TakePicture()
        {
            PictureUrl = await _cameraService.TakePhotoAsync();

            if(PictureUrl == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Camera not available", "retry");
            }
        }

        // This function create a new UserEntity and save it in the Database
        public async void CreateAccount()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
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
                        // await SqliteNetHelper.CreateUser(this.Username, this.Password, this.PictureUrl);
                        await SqliteNetHelper.CreateUser(this.Username, this.Password, PictureUrl);

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
