using System;
using System.IO;
using System.Threading.Tasks;
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
        public CharacterEntity CharacterEntity { get; set; }



        public CharacterViewModel(INavigationService navigationService) : base(navigationService)
        {
            CmdSave = new Command(SaveCharacter);
        }


        #region OnNavigatedToAsync
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            if (parameters.ContainsKey("character"))
            {
                Character = parameters.GetValue<CharacterDownDTO>("character");
            }
        }
        #endregion


        // With this function we save the character in the Database
        public async void SaveCharacter()
        {

            var db = new SQLiteAsyncConnection(App.DatabasePath);

            try
            {
                this.CharacterEntity = new CharacterEntity()
                {
                    Id = _character.Id,
                    IdCreator = App.UserId,
                    Name = _character.Name,
                    Image = _character.Image,
                    Species = _character.Species,
                    Origin = _character.Origin.Name
                };

                string query = "SELECT * FROM CharacterEntity WHERE CharacterEntity.Id =" + this.CharacterEntity.Id + " AND CharacterEntity.IdCreator = " + this.CharacterEntity.IdCreator;
                var result = await db.FindWithQueryAsync<CharacterEntity>(query);


                // if the character is already saved for this user
                if (result != null)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "This character is already registered, if you register it again it will delete the old one", "Ok");
                    await db.ExecuteAsync("DELETE FROM CharacterEntity WHERE Id =" + result.Id + " AND IdCreator =" + result.IdCreator);
                }

                await db.InsertAsync(CharacterEntity); // Insert the character into the db
                Console.WriteLine("Character saved");
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
            await DoBackCommand();
        }

        private CharacterDownDTO _character;
        public CharacterDownDTO Character
        {
            get { return _character; }
            set { SetProperty(ref _character, value); }
        }
    }
}
