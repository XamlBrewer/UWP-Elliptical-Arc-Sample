using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlBrewer.Uwp.Controls
{
    public sealed partial class EllipticalArc : UserControl
    {
        public static readonly DependencyProperty StartPointXProperty =
            DependencyProperty.Register(nameof(StartPointX), typeof(int), typeof(EllipticalArc), new PropertyMetadata(0, new PropertyChangedCallback(Render)));

        public static readonly DependencyProperty StartPointYProperty =
            DependencyProperty.Register(nameof(StartPointY), typeof(int), typeof(EllipticalArc), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty EndPointXProperty =
            DependencyProperty.Register(nameof(EndPointX), typeof(int), typeof(EllipticalArc), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty EndPointYProperty =
            DependencyProperty.Register(nameof(EndPointY), typeof(int), typeof(EllipticalArc), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.Register(nameof(RadiusX), typeof(int), typeof(EllipticalArc), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.Register(nameof(RadiusY), typeof(int), typeof(EllipticalArc), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty RotationAngleProperty =
            DependencyProperty.Register(nameof(RotationAngle), typeof(double), typeof(EllipticalArc), new PropertyMetadata(0.0, Render));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(EllipticalArc), new PropertyMetadata(1.0, Render));

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Color), typeof(EllipticalArc), new PropertyMetadata((Color)Application.Current.Resources["SystemAccentColor"], Render));

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Color), typeof(EllipticalArc), new PropertyMetadata(Colors.Transparent, Render));

        public static readonly DependencyProperty IsClockwiseProperty =
            DependencyProperty.Register(nameof(IsClockwise), typeof(bool), typeof(EllipticalArc), new PropertyMetadata(false, Render));

        public static readonly DependencyProperty IsLargeArcProperty =
            DependencyProperty.Register(nameof(IsLargeArc), typeof(bool), typeof(EllipticalArc), new PropertyMetadata(false, Render));

        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register(nameof(IsClosed), typeof(bool), typeof(EllipticalArc), new PropertyMetadata(false, Render));

        public static readonly DependencyProperty IsStrokeRoundedProperty =
            DependencyProperty.Register(nameof(IsStrokeRounded), typeof(bool), typeof(EllipticalArc), new PropertyMetadata(false, Render));

        public int StartPointX
        {
            get { return (int)GetValue(StartPointXProperty); }
            set { SetValue(StartPointXProperty, value); }
        }

        public int StartPointY
        {
            get { return (int)GetValue(StartPointYProperty); }
            set { SetValue(StartPointYProperty, value); }
        }

        public int EndPointX
        {
            get { return (int)GetValue(EndPointXProperty); }
            set { SetValue(EndPointXProperty, value); }
        }

        public int EndPointY
        {
            get { return (int)GetValue(EndPointYProperty); }
            set { SetValue(EndPointYProperty, value); }
        }

        public int RadiusX
        {
            get { return (int)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }

        public int RadiusY
        {
            get { return (int)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        public double RotationAngle
        {
            get { return (double)GetValue(RotationAngleProperty); }
            set { SetValue(RotationAngleProperty, value); }
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

        public bool IsClockwise
        {
            get { return (bool)GetValue(IsClockwiseProperty); }
            set { SetValue(IsClockwiseProperty, value); }
        }

        public bool IsLargeArc
        {
            get { return (bool)GetValue(IsLargeArcProperty); }
            set { SetValue(IsLargeArcProperty, value); }
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

        public EllipticalArc()
        {
            this.InitializeComponent();

            Loaded += EllipticalArc_Loaded;
            Unloaded += EllipticalArc_Unloaded;
        }

        private void EllipticalArc_Loaded(object sender, RoutedEventArgs e)
        {
            Render(this, null);
        }

        private void EllipticalArc_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= EllipticalArc_Loaded;
            Unloaded -= EllipticalArc_Unloaded;
        }

        private static void Render(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var arc = (EllipticalArc)d;
            if (arc.Container.ActualWidth == 0)
            {
                return;
            }

            var root = arc.Container.GetVisual();
            var compositor = Window.Current.Compositor;
            var canvasPathBuilder = new CanvasPathBuilder(new CanvasDevice());

            // Figure
            canvasPathBuilder.BeginFigure(new Vector2(arc.StartPointX, arc.StartPointY));
            canvasPathBuilder.AddArc(
                new Vector2(arc.EndPointX, arc.EndPointY),
                arc.RadiusX,
                arc.RadiusY,
                (float)(arc.RotationAngle * Math.PI / 180),
                arc.IsClockwise ? CanvasSweepDirection.Clockwise : CanvasSweepDirection.CounterClockwise,
                arc.IsLargeArc ? CanvasArcSize.Large : CanvasArcSize.Small);
            canvasPathBuilder.EndFigure(arc.IsClosed ? CanvasFigureLoop.Closed : CanvasFigureLoop.Open);

            // Path
            var canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
            var compositionPath = new CompositionPath(canvasGeometry);
            var pathGeometry = compositor.CreatePathGeometry();
            pathGeometry.Path = compositionPath;
            var spriteShape = compositor.CreateSpriteShape(pathGeometry);
            spriteShape.FillBrush = compositor.CreateColorBrush(arc.Fill);
            spriteShape.StrokeThickness = (float)arc.StrokeThickness;
            spriteShape.StrokeBrush = compositor.CreateColorBrush(arc.Stroke);
            spriteShape.StrokeStartCap = arc.IsStrokeRounded ? CompositionStrokeCap.Round : CompositionStrokeCap.Flat;
            spriteShape.StrokeEndCap = arc.IsStrokeRounded ? CompositionStrokeCap.Round : CompositionStrokeCap.Flat;

            // Visual
            var shapeVisual = compositor.CreateShapeVisual();
            shapeVisual.Size = new Vector2((float)arc.Container.ActualWidth, (float)arc.Container.ActualHeight);
            shapeVisual.Shapes.Add(spriteShape);
            root.Children.RemoveAll();
            root.Children.InsertAtTop(shapeVisual);
        }
    }
}
