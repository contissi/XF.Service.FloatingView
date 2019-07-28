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

        private bool _floatingViewIsActive;

        #endregion

        #region Properties

        public bool FloatingViewIsActive { get => _floatingViewIsActive; set => SetProperty(ref _floatingViewIsActive, value); }

        #endregion

        #region Commands

        private DelegateCommand _toggleFloatingViewCommand;
        public DelegateCommand ToggleFloatingViewCommand => _toggleFloatingViewCommand ??
                                                                (_toggleFloatingViewCommand =
                                                                    new DelegateCommand(ToggleFloatingView, CanExecute)
                                                                        .ObservesProperty(() => IsBusy));

        #endregion

        #region Constructor

        public MainPageViewModel(INavigationService navigationService, IIntentService intentService)
            : base(navigationService)
        {
            _intentService = intentService;
        }

        #endregion

        #region Private Methods

        private void ToggleFloatingView()
        {
            if (FloatingViewIsActive)
            {
                _intentService.StartFloatingView();
            }
            else
            {
                _intentService.StopFloatingView();
            }
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
