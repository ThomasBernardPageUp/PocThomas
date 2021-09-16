using System;
using System.Net.Http;
using System.Threading.Tasks;
using PoC_Thomas.Commons;
using PoC_Thomas.Helpers.Interface;
using Xamarin.Forms;
using Prism.Navigation;
using System.Collections.Generic;
using Test.Model.DTO.Down;
using PoC_Thomas.Helpers;
using PoC_Thomas.Models.Entities;
using Prism.Commands;

namespace PoC_Thomas.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        
        private IDataTransferHelper _dataTransferHelper;
        public DelegateCommand<CharacterDownDTO> CharacterTappedCommand { get; set; }
        public Command ProfileCommand { get; set; }



        // Constructor 
        public MenuViewModel(INavigationService navigationService, IDataTransferHelper dataTransfer, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            _dataTransferHelper = dataTransfer;
            CharacterTappedCommand = new DelegateCommand<CharacterDownDTO>(ShowCharacter);
            AllCharacters = new List<CharacterDownDTO>();
     

            ProfileCommand = new Command(ProfilePage);
        }


        #region OnNavigatedToAsync
        // Function call when the app show the menupage
        protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            // We load the first page of characters
            LoadCharacters();
        }



        // This function show the character's page 
        public async void ShowCharacter(CharacterDownDTO character)
        {
            CharacterEntity characterEntity = new CharacterEntity(character.Id, App.UserId, character.Name, character.Image, character.Species, character.Origin.Name);
            var parameter = new NavigationParameters { { "character", characterEntity } };

            await NavigationService.NavigateAsync(Constants.CharacterPage, parameter);
        }



        // Load all characters from API
        public async void LoadCharacters()
        {
            
            
            string url = "https://rickandmortyapi.com/api/character?page=";

            for (int i = 1; i < 35; i++)
            {
                var result = await _dataTransferHelper.SendClientAsync<CharactersDownDTO>(url + i, HttpMethod.Get);

                if (result.IsSuccess)
                {
                    AllCharacters.AddRange(result.Result.Results);
                }
                else
                {
                    Console.WriteLine("call error");
                }

            }

            Characters = AllCharacters;
            
        }


        // Show the ProfilePage
        public async void ProfilePage()
        {
            await NavigationService.NavigateAsync(Constants.ProfilePage);
        }


        #endregion


        private List<CharacterDownDTO> _charaters;
        public List<CharacterDownDTO> Characters
        {
            get { return _charaters; }
            set { SetProperty(ref _charaters, value); }
        }  

        private List<CharacterDownDTO> _allCharacters;
        public List<CharacterDownDTO> AllCharacters
        {
            get { return _allCharacters; }
            set { SetProperty(ref _allCharacters, value); }
        }
    }
}
