using System;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Helpers.Interface;
using PoC_Thomas.Models.Entities;
using Prism.Mvvm;
using Prism.Navigation;
using Sharpnado.Tasks;
using SQLite;

namespace PoC_Thomas.ViewModels
{
    public class BaseViewModel : BindableBase, INavigatedAware, IInitializeAsync
    { 
        // Yoou can add this kind of property to reuse it in all other ViewModels.
        protected INavigationService NavigationService;
        protected ISqliteNetHelper SqliteNetHelper;
        protected UserEntity User;


        public BaseViewModel(INavigationService navigationService, ISqliteNetHelper sqliteNetHelper)
        {
            NavigationService = navigationService;
            SqliteNetHelper = sqliteNetHelper;
        }

        // Yoou can add this kind of command to reuse it in all other ViewModels
        protected virtual async Task DoBackCommand()
        {
            await NavigationService.GoBackAsync();
        }



        #region LifeCycle

        #region Initialize

        public void Initialize(INavigationParameters parameters)
        {
            TaskMonitor.Create(InitializeAsync(parameters),
                whenFaulted: (t) =>
                {
                    t.Exception.Handle(ex =>
                    {
                        HandleException(ex);
                        return true;
                    });
                });
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
           => Task.CompletedTask;

        #endregion

        #region OnNavigatedFrom

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            TaskMonitor.Create(OnNavigatedFromAsync(parameters),
                whenFaulted: (t) =>
                {
                    t.Exception.Handle(ex =>
                    {
                        HandleException(ex);
                        return true;
                    });
                });
        }

        protected virtual Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            return Task.FromResult(0);
        }


        #endregion

        #region OnNavigatedTo

        protected virtual async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await Task.FromResult(0);
        }


        public void OnNavigatedTo(INavigationParameters parameters)
        {
            TaskMonitor.Create(OnNavigatedToAsync(parameters),
                whenFaulted: (t) =>
                {
                    t.Exception.Handle(ex =>
                    {
                        HandleException(ex);
                        return true;
                    });
                });
        }

        public void HandleException(Exception ex)
        {
            Console.WriteLine("============ERROR=============");
            Console.WriteLine(ex); 
            Console.WriteLine("==============================");
        }

        #endregion

        #endregion
    }
}
