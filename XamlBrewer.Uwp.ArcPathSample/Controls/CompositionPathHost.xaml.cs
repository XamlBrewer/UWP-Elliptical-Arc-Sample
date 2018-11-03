using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlBrewer.Uwp.Controls
{
    public abstract partial class CompositionPathHost : UserControl
    {
        public static readonly DependencyProperty StartPointXProperty =
            DependencyProperty.Register(nameof(StartPointX), typeof(double), typeof(RingSegment), new PropertyMetadata(0d, new PropertyChangedCallback(Render)));

        public static readonly DependencyProperty StartPointYProperty =
            DependencyProperty.Register(nameof(StartPointY), typeof(double), typeof(RingSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(RingSegment), new PropertyMetadata(1.0, Render));

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Color), typeof(RingSegment), new PropertyMetadata((Color)Application.Current.Resources["SystemAccentColor"], Render));

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Color), typeof(RingSegment), new PropertyMetadata(Colors.Transparent, Render));

        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register(nameof(IsClosed), typeof(bool), typeof(RingSegment), new PropertyMetadata(false, Render));

        public static readonly DependencyProperty IsStrokeRoundedProperty =
            DependencyProperty.Register(nameof(IsStrokeRounded), typeof(bool), typeof(RingSegment), new PropertyMetadata(false, Render));

        public double StartPointX
        {
            get { return (double)GetValue(StartPointXProperty); }
            set { SetValue(StartPointXProperty, value); }
        }

        public double StartPointY
        {
            get { return (double)GetValue(StartPointYProperty); }
            set { SetValue(StartPointYProperty, value); }
        }

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public Color Fill
        {
            get { return (Color)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            set { SetValue(IsClosedProperty, value); }
        }

        public bool IsStrokeRounded
        {
            get { return (bool)GetValue(IsStrokeRoundedProperty); }
            set { SetValue(IsStrokeRoundedProperty, value); }
        }

        protected Grid Container => Host;

        public CompositionPathHost()
        {
            this.InitializeComponent();
        }

        protected static void Render(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var path = (CompositionPathHost)d;
            if (path.Container.ActualWidth == 0)
            {
                return;
            }

            path.Render();
        }

        protected abstract void Render();
    }
}
