using System;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Helpers.Interface;
using PoC_Thomas.Models.Entities;
using Prism.Navigation;
using SQLite;
using Test.Model.DTO.Down;
using Xamarin.Forms;

namespace PoC_Thomas.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        public Command CmdSave { get; set; }


        public CharacterViewModel(INavigationService navigationService, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            CmdSave = new Command(SaveCharacter);
        }


        #region OnNavigatedToAsync
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            if (parameters.ContainsKey("character"))
            {
                Character = parameters.GetValue<CharacterEntity>("character");
            }
        }
        #endregion


        // With this function we save the character in the Database
        public async void SaveCharacter()
        {
            try
            {
                var result = await SqliteNetHelper.CheckCharacter(Character.Id, App.UserId);

                // if the character is already saved for this user
                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "This character is already registered, if you register it again it will delete the old one", "Ok");
                    await SqliteNetHelper.DeleteCharacter(Character.Id, App.UserId);
                }

                await SqliteNetHelper.db.InsertAsync(Character); // Insert the character into the db
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
            await DoBackCommand();
        }

        private CharacterEntity _character;
        public CharacterEntity Character
        {
            get { return _character; }
            set { SetProperty(ref _character, value); }
        }
    }
}
