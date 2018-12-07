using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XF.Service.FloatingView.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Private Members

        //private readonly IPlatformService _platformService;
        private bool _serviceIsRunning;

        #endregion

        #region Properties

        public bool ServiceIsRunning { get => _serviceIsRunning; set => SetProperty(ref _serviceIsRunning, value); }

        #endregion

        #region Commands

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(NavigateTo));

        #endregion

        #region Constructor

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        #endregion

        #region Private Methods

        private async void NavigateTo(string navTarget)
        {
            await NavigationService.NavigateAsync(navTarget);
        }

        private void StartService()
        {
            throw new NotImplementedException();
        }

        private void StopService()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overridden Methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            //ServiceIsRunning = _platformService.IsFloatingServiceRunning();
        }

        #endregion
    }
}
