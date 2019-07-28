using Prism.Navigation;

namespace XF.Service.FloatingView.ViewModels
{
    public class ThirdPageViewModel : ViewModelBase
    {
        public ThirdPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Page 3";
        }
    }
}
