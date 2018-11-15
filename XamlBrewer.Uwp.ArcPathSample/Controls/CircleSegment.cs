using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace XamlBrewer.Uwp.Controls
{
    public class CircleSegment : CompositionPathHost
    {
        public static readonly DependencyProperty CenterPointXProperty =
            DependencyProperty.Register(nameof(CenterPointX), typeof(double), typeof(CircleSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty CenterPointYProperty =
            DependencyProperty.Register(nameof(CenterPointY), typeof(double), typeof(CircleSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(CircleSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty SweepAngleProperty =
            DependencyProperty.Register(nameof(SweepAngle), typeof(double), typeof(CircleSegment), new PropertyMetadata(0d, Render));

        public static readonly DependencyProperty IsPieProperty =
            DependencyProperty.Register(nameof(IsPie), typeof(bool), typeof(CircleSegment), new PropertyMetadata(false, Render));

        private const double Degrees2Radians = Math.PI / 180;

        public double CenterPointX
        {
            get { return (double)GetValue(CenterPointXProperty); }
            set { SetValue(CenterPointXProperty, value); }
        }

        public double CenterPointY
        {
            get { return (double)GetValue(CenterPointYProperty); }
            set { SetValue(CenterPointYProperty, value); }
        }

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
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

        protected override void Render()
        {
            var root = Container.GetVisual();
            var compositor = Window.Current.Compositor;
            var canvasPathBuilder = new CanvasPathBuilder(new CanvasDevice());
            if (IsStrokeRounded)
            {
                canvasPathBuilder.SetSegmentOptions(CanvasFigureSegmentOptions.ForceRoundLineJoin);
            }
            else
            {
                canvasPathBuilder.SetSegmentOptions(CanvasFigureSegmentOptions.None);
            }

            // Figure
            if (IsPie)
            {
                StartPointX = CenterPointX;
                StartPointY = CenterPointY;
            }
            else
            {
                StartPointX = Radius * Math.Cos(StartAngle * Degrees2Radians) + CenterPointX;
                StartPointY = Radius * Math.Sin(StartAngle * Degrees2Radians) + CenterPointY;
            }

            canvasPathBuilder.BeginFigure(new Vector2((float)StartPointX, (float)StartPointY));

            canvasPathBuilder.AddArc(
                new Vector2((float)CenterPointX, (float)CenterPointY),
                (float)Radius,
                (float)Radius,
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
