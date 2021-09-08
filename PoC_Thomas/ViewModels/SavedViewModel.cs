using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Commons;
using PoC_Thomas.Helpers.Interface;
using PoC_Thomas.Models.Entities;
using Prism.Commands;
using Prism.Navigation;
using SQLite;
using Xamarin.Forms;

namespace PoC_Thomas.ViewModels
{
    public class SavedViewModel : BaseViewModel
    {
        public DelegateCommand<CharacterEntity> CmdDelete { get; set; }
        public DelegateCommand<CharacterEntity> CmdView { get; set; }

        public SavedViewModel(INavigationService navigationService, IDataTransferHelper dataTransfer, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            CmdDelete = new DelegateCommand<CharacterEntity>(DeleteChar);
            CmdView = new DelegateCommand<CharacterEntity>(ViewChar);

        }


        #region OnNavigatedToAsync
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            Characters = new ObservableCollection<CharacterEntity>(await SqliteNetHelper.GetCharacters(App.UserId));
        }
        #endregion


        // This function delete a character from the database
        public async void DeleteChar(CharacterEntity character)
        {
            try
            {
                await SqliteNetHelper.DeleteCharacter(character.Id, App.UserId);
                Characters.Remove(character);


                Console.WriteLine("Character deleted");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async void ViewChar(CharacterEntity character)
        {
            var parameter = new NavigationParameters { { "character", character } };
            await NavigationService.NavigateAsync(Constants.CharacterPage, parameter);
        }



        private ObservableCollection<CharacterEntity> _characters;
        public ObservableCollection<CharacterEntity> Characters
        {
            get { return _characters; }
            set { SetProperty(ref _characters, value); }
        }
    }
}
