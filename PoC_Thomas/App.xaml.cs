using System;
using System.IO;
using PageUpX.Core.Log;
using PageUpX.DataAccess.DataAccessor;
using PoC_Thomas.Commons;
using PoC_Thomas.Helpers;
using PoC_Thomas.Helpers.Interface;
using PoC_Thomas.Models.Entities;
using PoC_Thomas.Repositories;
using PoC_Thomas.Repositories.Interface;
using PoC_Thomas.Services.Interface;
using PoC_Thomas.ViewModels;
using PoC_Thomas.Views;
using PageUpX.DataAccess.SQLite;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace PoC_Thomas
{
    public partial class App
    {
        public static long UserId {get; set;}

        public App(IPlatformInitializer initializer): base(initializer)
        {
        }



        protected override async void OnInitialized()
        {
            try
            {
                await NavigationService.NavigateAsync($"{Constants.NavigationPage}/{Constants.LoginPage}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            try
            {
                RegisterHelpers(containerRegistry);
                RegisterServices(containerRegistry);
                RegisterDataAscessor(containerRegistry);
                RegisterRepositories(containerRegistry);

                //Register for navigation is always the last registration method
                RegisterForNavigation(containerRegistry);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void RegisterHelpers(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IHttpClientHelper, HttpClientHelper>();
            containerRegistry.RegisterSingleton<IDataTransferHelper, DataTransferHelper>();
            containerRegistry.RegisterSingleton<ISqliteNetHelper, SqliteNetHelper>();

        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            //Example  
            //containerRegistry.RegisterSingleton<ILoginService, LoginService>();

            containerRegistry.RegisterSingleton<IPuxLogger, ConsoleLoggerService>();
        }


        private void RegisterDataAscessor(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPuxSimpleDataAccessor<UserEntity>, PuxSimpleDataAccessorSQLite<UserEntity>>();
        }

        private void RegisterRepositories(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
        }

        private void RegisterForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(Constants.NavigationPage);


            //Here we register the page and the viewmodel to link them.
            //More, we had a name at this couple to reuse them more efficently.


            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>(Constants.LoginPage);
            containerRegistry.RegisterForNavigation<MenuPage, MenuViewModel>(Constants.MenuPage);
            containerRegistry.RegisterForNavigation<CharacterPage, CharacterViewModel>(Constants.CharacterPage);
            containerRegistry.RegisterForNavigation<AccountPage, AccountViewModel>(Constants.AccountPage);
            containerRegistry.RegisterForNavigation<ProfilePage, ProfileViewModel>(Constants.ProfilePage);
            containerRegistry.RegisterForNavigation<ProfilePage, ProfileViewModel>(Constants.ProfilePage);
        }


        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
