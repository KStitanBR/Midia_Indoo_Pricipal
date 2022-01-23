using Midia_Indoo.Helps;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Midia_Indoo.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        private bool _Busy;
        public bool Busy
        {
            get { return _Busy; }
            set { SetProperty(ref _Busy, value); }
        }  
        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { SetProperty(ref _IsRefreshing, value); }
        }
        public DelegateCommand VoltarCommand { get; set; }
        public INavigationService NavigationService { get; }
        public IPageDialogService PageDialogService { get; }

        private bool isNavigating;

        protected Task SafeNavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            if (isNavigating)
                return Task.CompletedTask;
            isNavigating = true;
            try { return NavigationService.NavigateAsync(name, parameters, useModalNavigation, animated); }
            catch { return Task.CompletedTask; }
            finally
            {
                Device.StartTimer(
         TimeSpan.FromMilliseconds(500),
         () => isNavigating = false);
            }
        }


        public async Task<bool> MsgErrInterntOK(string title, string msg)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await PageDialogService.DisplayAlertAsync(title, msg, "OK");
                return true;
            }

            return false;

        }
        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
            VoltarCommand = new DelegateCommand(() => navigationService.GoBackAsync());

        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
