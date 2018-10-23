using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Controls;
using XamlBrewer.Uwp.ArcPathSample.ViewModels;

namespace XamlBrewer.Uwp.ArcPathSample
{
    public sealed partial class Win2DPage : Page
    {
        private EllipticalArcViewModel _viewModel = new EllipticalArcViewModel();

        public Win2DPage()
        {
            this.InitializeComponent();
        }

        public EllipticalArcViewModel ViewModel => _viewModel;
    }
}
