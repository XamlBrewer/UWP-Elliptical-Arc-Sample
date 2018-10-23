using Windows.UI.Xaml.Controls;
using XamlBrewer.Uwp.ArcPathSample.ViewModels;

namespace XamlBrewer.Uwp.ArcPathSample
{
    public sealed partial class ArcSegmentPage : Page
    {
        private EllipticalArcViewModel _viewModel = new EllipticalArcViewModel
        {
            StartPointX = 10,
            StartPointY = 100,
            RadiusX = 100,
            RadiusY = 50,
            RotationAngle = 45,
            EndPointX = 200,
            EndPointY = 100
        };

        public ArcSegmentPage()
        {
            this.InitializeComponent();
        }

        public EllipticalArcViewModel ViewModel => _viewModel;
    }
}
