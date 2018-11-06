using Mvvm.Services;
using XamlBrewer.Uwp.ArcPathSample;

namespace Mvvm
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("HomeIcon"), Text = "Home", NavigationDestination = typeof(HomePage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("TopLayerIcon"), Text = "XAML", NavigationDestination = typeof(ArcSegmentPage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("MiddleLayerIcon"), Text = "Composition", NavigationDestination = typeof(Win2DPage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("DonutIcon"), Text = "Ring Segment", NavigationDestination = typeof(CircleSegmentPage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("PacmanIcon"), Text = "Gallery", NavigationDestination = typeof(GalleryPage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("SquaresIcon"), Text = "Squares", NavigationDestination = typeof(SquaresPage) });
            SecondMenu.Add(new MenuItem() { Glyph = Icon.GetIcon("InfoIcon"), Text = "About", NavigationDestination = typeof(AboutPage) });
        }
    }
}
