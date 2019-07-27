using Prism.Navigation;

namespace XF.Service.FloatingView.ViewModels
{
    public class SecondPageViewModel : ViewModelBase
    {
        public SecondPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Second Page";
        }
    }
}
