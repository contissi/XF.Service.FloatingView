using System;
using Prism.Commands;
using Prism.Navigation;
using XF.Service.FloatingView.Interfaces;

namespace XF.Service.FloatingView.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Private Members

        private readonly IIntentService _intentService;

        #endregion

        #region Properties



        #endregion

        #region Commands

        private DelegateCommand _startFloatingViewCommand;
        public DelegateCommand StartFloatingViewCommand => _startFloatingViewCommand ?? (_startFloatingViewCommand = new DelegateCommand(StartFloatingView));

        private DelegateCommand _stopFloatingViewCommand;
        public DelegateCommand StopFloatingViewCommand => _stopFloatingViewCommand ?? (_stopFloatingViewCommand = new DelegateCommand(StopFloatingView));

        #endregion

        #region Constructor

        public MainPageViewModel(INavigationService navigationService, IIntentService intentService)
            : base(navigationService)
        {
            _intentService = intentService;
            Title = "Main Page";
        }

        #endregion

        #region Private Methods

        private void StartFloatingView()
        {
            _intentService.StartFloatingView();
        }

        private void StopFloatingView()
        {
            _intentService.StopFloatingView();
        }

        #endregion

        #region Overridden Methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            _intentService.AskForDrawOverPermission();
        }

        #endregion
    }
}
