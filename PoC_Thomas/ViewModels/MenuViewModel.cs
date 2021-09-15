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
        public DelegateCommand<CharacterDownDTO> CharacterTappedCommand { get; private set; }
        public Command ProfileCommand { get; set; }
        public Command PrevPage { get; set; }
        public Command NexPage { get; set; }


        // Constructor 
        public MenuViewModel(INavigationService navigationService, IDataTransferHelper dataTransfer, ISqliteNetHelper sqliteNetHelper) : base(navigationService, sqliteNetHelper)
        {
            _dataTransferHelper = dataTransfer;
            CharacterTappedCommand = new DelegateCommand<CharacterDownDTO>(ShowCharacter);
            PrevPage = new Command(() => { Page--; LoadCharacters(); });
            NexPage = new Command(() => { Page++; LoadCharacters(); });

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
            // var parameter = new NavigationParameters { { "character", character } };

            CharacterEntity characterEntity = new CharacterEntity(character.Id, App.UserId, character.Name, character.Image, character.Species, character.Origin.Name);
            var parameter = new NavigationParameters { { "character", characterEntity } };

            await NavigationService.NavigateAsync(Constants.CharacterPage, parameter);
        }



        // Load all characters from API
        public async void LoadCharacters()
        {
            if(this.Page > 34)
            {
                this.Page = 34;
            }
            else
            {
                if(this.Page < 1)
                {
                    this.Page = 1;
                }
            }
            
            string url = "https://rickandmortyapi.com/api/character?page=" + this.Page;
            var result = await _dataTransferHelper.SendClientAsync<CharactersDownDTO>(url, HttpMethod.Get);


            if (result.IsSuccess)
            {
                Characters = result.Result.Results;

                if (Characters != null)
                {
                    NumberOfResults = result.Result.Info.Count;
                }
                else
                {
                    NumberOfResults = 0;
                }
            }




            // We set the bacakground color :
            // Blue if male
            // Pink if female
            // White if other
            foreach (CharacterDownDTO character in Characters)
            {
                Color bg = Color.Default;
                switch (character.Gender)
                {
                    case "Male":
                        bg = Color.MediumAquamarine;
                        break;
                    case "Female":
                        bg = Color.MediumOrchid;
                        break;
                    default:
                        bg = Color.NavajoWhite;
                        break;
                }
                character.Background = bg.ToHex();
            }
        }


        // Show the ProfilePage
        public async void ProfilePage()
        {
            await NavigationService.NavigateAsync(Constants.ProfilePage);
        }


        #endregion

        private int _page;
        public int Page
        {
            get { return _page; }
            set { SetProperty(ref _page, value); }
        }

        private List<CharacterDownDTO> _charaters;
        public List<CharacterDownDTO> Characters
        {
            get { return _charaters; }
            set { SetProperty(ref _charaters, value); }
        }

        private int _numberOfResults;
        public int NumberOfResults
        {
            get { return _numberOfResults; }
            set { SetProperty(ref _numberOfResults, value); }
        }
    }
}
