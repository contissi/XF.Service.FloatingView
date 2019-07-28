using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Service.FloatingView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseGradientContentPage : ContentPage
    {
        public Color GradientStartColor { get; set; }        public Color GradientEndColor { get; set; }

        public BaseGradientContentPage()
        {
            InitializeComponent();
        }
    }
}
