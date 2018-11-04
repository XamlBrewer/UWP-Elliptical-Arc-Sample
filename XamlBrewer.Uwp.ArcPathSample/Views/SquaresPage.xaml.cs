using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using XamlBrewer.Uwp.Controls;

namespace XamlBrewer.Uwp.ArcPathSample
{
    public sealed partial class SquaresPage : Page
    {
        public SquaresPage()
        {
            this.InitializeComponent();
            Loaded += SquareOfSquaresPage_Loaded;
        }

        private void SquareOfSquaresPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            foreach (var square in Squares.Squares)
            {
                var segment = new RingSegment()
                {
                    Height = square.ActualHeight,
                    Width = square.ActualWidth,
                    CenterPointX = square.ActualHeight / 2,
                    CenterPointY = square.ActualWidth / 2,
                    StartAngle = random.Next(360),
                    SweepAngle = random.Next(360),
                    StrokeThickness = random.Next(25),
                    IsStrokeRounded = random.Next(100) > 50,
                    IsPie = random.Next(100) > 75,
                    IsClosed = random.Next(100) > 75,
                    Stroke = square.RandomColor()
                };

                segment.Radius = Math.Max((square.ActualHeight / 2) - segment.StrokeThickness, 0);

                square.Content = segment;
            }
        }
    }
}
