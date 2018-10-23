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

            var root = arc.Container.GetVisual();
            var compositor = Window.Current.Compositor;
            var canvasPathBuilder = new CanvasPathBuilder(new CanvasDevice());

            canvasPathBuilder.BeginFigure(new Vector2(arc.StartPointX, arc.StartPointY));
            canvasPathBuilder.AddArc(
                new Vector2(arc.EndPointX, arc.EndPointY),
                arc.RadiusX,
                arc.RadiusY,
                (float)(arc.RotationAngle * Math.PI / 180),
                CanvasSweepDirection.CounterClockwise,
                CanvasArcSize.Small);
            canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);

            var canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
            var compositionPath = new CompositionPath(canvasGeometry);
            var pathGeometry = compositor.CreatePathGeometry();
            pathGeometry.Path = compositionPath;
            var spriteShape = compositor.CreateSpriteShape(pathGeometry);
            spriteShape.FillBrush = compositor.CreateColorBrush(arc.Fill);
            spriteShape.StrokeThickness = (float)arc.StrokeThickness;
            spriteShape.StrokeBrush = compositor.CreateColorBrush(arc.Stroke);
            //_secondhandSpriteShape.Offset = new Vector2(97.0f, 20.0f);
            //_secondhandSpriteShape.CenterPoint = new Vector2(3.0f, 80.0f);

            var shapeVisual = compositor.CreateShapeVisual();
            shapeVisual.Size = new Vector2(400.0f, 400.0f);
            shapeVisual.Shapes.Add(spriteShape);

            root.Children.RemoveAll();
            root.Children.InsertAtTop(shapeVisual);
        }
    }
}
