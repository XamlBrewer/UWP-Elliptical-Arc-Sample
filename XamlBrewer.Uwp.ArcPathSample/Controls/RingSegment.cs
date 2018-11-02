using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace XamlBrewer.Uwp.Controls
{
    public class RingSegment : CompositionPathHost
    {
        public static readonly DependencyProperty CenterPointXProperty =
            DependencyProperty.Register(nameof(CenterPointX), typeof(int), typeof(RingSegment), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty CenterPointYProperty =
            DependencyProperty.Register(nameof(CenterPointY), typeof(int), typeof(RingSegment), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(int), typeof(RingSegment), new PropertyMetadata(0, Render));

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(RingSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty SweepAngleProperty =
            DependencyProperty.Register(nameof(SweepAngle), typeof(double), typeof(RingSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty IsPieProperty =
            DependencyProperty.Register(nameof(IsPie), typeof(bool), typeof(RingSegment), new PropertyMetadata(false, Render));

        private const double Degrees2Radians = Math.PI / 180;

        public int CenterPointX
        {
            get { return (int)GetValue(CenterPointXProperty); }
            set { SetValue(CenterPointXProperty, value); }
        }

        public int CenterPointY
        {
            get { return (int)GetValue(CenterPointYProperty); }
            set { SetValue(CenterPointYProperty, value); }
        }

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public double SweepAngle
        {
            get { return (double)GetValue(SweepAngleProperty); }
            set { SetValue(SweepAngleProperty, value); }
        }

        public bool IsPie
        {
            get { return (bool)GetValue(IsPieProperty); }
            set { SetValue(IsPieProperty, value); }
        }

        public RingSegment()
        {
            this.InitializeComponent();

            Loaded += RingSegment_Loaded;
            Unloaded += RingSegment_Unloaded;
        }

        private void RingSegment_Loaded(object sender, RoutedEventArgs e)
        {
            Render(this, null);
        }

        private void RingSegment_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= RingSegment_Loaded;
            Unloaded -= RingSegment_Unloaded;
        }

        protected override void Render()
        {
            var root = Container.GetVisual();
            var compositor = Window.Current.Compositor;
            var canvasPathBuilder = new CanvasPathBuilder(new CanvasDevice());

            // Figure
            if (IsPie)
            {
                StartPointX = CenterPointX;
                StartPointY = CenterPointY;
            }
            else
            {
                StartPointX = (int)(Radius * Math.Cos(StartAngle * Degrees2Radians) + CenterPointX);
                StartPointY = (int)(Radius * Math.Sin(StartAngle * Degrees2Radians) + CenterPointY);
            }

            canvasPathBuilder.BeginFigure(new Vector2(StartPointX, StartPointY));
            canvasPathBuilder.AddArc(
                new Vector2(CenterPointX, CenterPointY),
                Radius,
                Radius,
                (float)(StartAngle * Degrees2Radians),
                (float)(SweepAngle * Degrees2Radians));
            canvasPathBuilder.EndFigure(IsClosed || IsPie ? CanvasFigureLoop.Closed : CanvasFigureLoop.Open);

            // Path
            var canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
            var compositionPath = new CompositionPath(canvasGeometry);
            var pathGeometry = compositor.CreatePathGeometry();
            pathGeometry.Path = compositionPath;
            var spriteShape = compositor.CreateSpriteShape(pathGeometry);
            spriteShape.FillBrush = compositor.CreateColorBrush(Fill);
            spriteShape.StrokeThickness = (float)StrokeThickness;
            spriteShape.StrokeBrush = compositor.CreateColorBrush(Stroke);
            spriteShape.StrokeStartCap = IsStrokeRounded ? CompositionStrokeCap.Round : CompositionStrokeCap.Flat;
            spriteShape.StrokeEndCap = IsStrokeRounded ? CompositionStrokeCap.Round : CompositionStrokeCap.Flat;

            // Visual
            var shapeVisual = compositor.CreateShapeVisual();
            shapeVisual.Size = new Vector2((float)Container.ActualWidth, (float)Container.ActualHeight);
            shapeVisual.Shapes.Add(spriteShape);
            root.Children.RemoveAll();
            root.Children.InsertAtTop(shapeVisual);
        }
    }
}
