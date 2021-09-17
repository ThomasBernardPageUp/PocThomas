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
using System.Linq;
using System.Collections.ObjectModel;

namespace PoC_Thomas.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        
        private IDataTransferHelper _dataTransferHelper;

        public DelegateCommand<CharacterDownDTO> CharacterTappedCommand { get; set; }
        public Command ProfileCommand { get; set; }
        public Command SearchCommand { get; set; }

        public string SearchedCharacterName { get; set; }
        public string SelectedGender { get; set; }

        // Constructor 
        public MenuViewModel(INavigationService navigationService, IDataTransferHelper dataTransfer, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            _dataTransferHelper = dataTransfer;


            this.CharacterTappedCommand = new DelegateCommand<CharacterDownDTO>(ShowCharacter);
            this.SearchCommand = new Command(SearchCharacters);
            this.ProfileCommand = new Command(ProfilePage);


            AllCharacters = new List<CharacterDownDTO>();
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
        
        public void SearchCharacters()
        {
            // 1) We reset the list
            Characters = AllCharacters;


            // 2) We filter with the name
            if(!string.IsNullOrEmpty(SearchedCharacterName))
            {
                Characters = AllCharacters.Where(character => character.Name.Contains(SearchedCharacterName)).ToList();
            }


            // We filter with the gender
            if(!string.IsNullOrEmpty(SelectedGender) && SelectedGender != "All")
            {
                Characters = Characters.Where(character => character.Gender.Contains(SelectedGender)).ToList();
            }
        }


        // Load all characters from API
        public async void LoadCharacters()
        {
            
            string url = "https://rickandmortyapi.com/api/character?page=";

            // 1 to 34
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

            // We get all diffents genders
            List<string> res = AllCharacters.Select(c => c.Gender).Distinct().ToList();
            AllGenders = new ObservableCollection<string>(res);
            AllGenders.Insert(0, "All");

            Characters = AllCharacters;
        }


        // Show the ProfilePage
        public async void ProfilePage()
        {
            await NavigationService.NavigateAsync(Constants.ProfilePage);
        }


        #endregion

        private ObservableCollection<string> _allGenders;
        public ObservableCollection<string> AllGenders
        {
            get { return _allGenders; }
            set { SetProperty(ref _allGenders, value); }
        }

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
