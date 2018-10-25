using Mvvm;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace XamlBrewer.Uwp.ArcPathSample.ViewModels
{
    public class EllipticalArcViewModel : BindableBase
    {
        private int _startPointX = 0;
        private int _startPointY = 0;
        private int _radiusX = 0;
        private int _radiusY = 0;
        private double _rotationAngle = 0;
        private int _endPointX = 0;
        private int _endPointY = 0;
        private bool _isClockwise = false;
        private bool _isLargeArc = false;
        private double _strokeThickness = 1;
        private Brush _stroke = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
        private bool _isStrokeRounded = false;

        public int RadiusX
        {
            get { return _radiusX; }
            set
            {
                SetProperty(ref _radiusX, value);
                OnPropertyChanged(nameof(Radius));
            }
        }

        public int RadiusY
        {
            get { return _radiusY; }
            set
            {
                SetProperty(ref _radiusY, value);
                OnPropertyChanged(nameof(Radius));
            }
        }

        public Size Radius => new Size(_radiusX, _radiusY);

        public int StartPointX
        {
            get { return _startPointX; }
            set
            {
                SetProperty(ref _startPointX, value);
                OnPropertyChanged(nameof(StartPoint));
            }
        }

        public int StartPointY
        {
            get { return _startPointY; }
            set
            {
                SetProperty(ref _startPointY, value);
                OnPropertyChanged(nameof(StartPoint));
            }
        }

        public Point StartPoint => new Point(_startPointX, _startPointY);

        public int EndPointX
        {
            get { return _endPointX; }
            set
            {
                SetProperty(ref _endPointX, value);
                OnPropertyChanged(nameof(EndPoint));
            }
        }

        public int EndPointY
        {
            get { return _endPointY; }
            set
            {
                SetProperty(ref _endPointY, value);
                OnPropertyChanged(nameof(EndPoint));
            }
        }

        public Point EndPoint => new Point(_endPointX, _endPointY);

        public double RotationAngle
        {
            get { return _rotationAngle; }
            set { SetProperty(ref _rotationAngle, value); }
        }

        public bool IsClockwise
        {
            get { return _isClockwise; }
            set
            {
                SetProperty(ref _isClockwise, value);
                OnPropertyChanged(nameof(SweepDirection));
            }
        }

        public SweepDirection SweepDirection => _isClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;

        public bool IsLargeArc
        {
            get { return _isLargeArc; }
            set { SetProperty(ref _isLargeArc, value); }
        }

        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set { SetProperty(ref _strokeThickness, value); }
        }

        public Brush Stroke
        {
            get { return _stroke; }
            set { SetProperty(ref _stroke, value); }
        }

        public bool IsStrokeRounded
        {
            get { return _isStrokeRounded; }
            set
            {
                SetProperty(ref _isStrokeRounded, value);
                OnPropertyChanged(nameof(StrokeLineCap));
            }
        }

        public PenLineCap StrokeLineCap => _isStrokeRounded ? PenLineCap.Round : PenLineCap.Flat;
    }
}
