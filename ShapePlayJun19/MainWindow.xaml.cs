using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace ShapePlayJun19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Fields
        private enum SelectedShape
        { Unset, PolyLine1, Rectangle, Line, PolyLine, PolyLine2 }
        SelectedShape currentShape;

        DispatcherTimer dispatcherTimer;

        LinearGradientBrush linearGradientBrush;

        PointCollection points;
        Polyline polyline;
        static Random random = new Random ();
        
        Rectangle rectangle;
        RectangleSupport rectangleSupport;

        int tickCounter = 0;
        double degrees = 0;


        double add;

        // Constructor

        public MainWindow ()
        {
            InitializeComponent ();
            NameScope.SetNameScope ( this, new NameScope () );

            dispatcherTimer = new DispatcherTimer ()
            {
                /*Interval = new TimeSpan ( 0, 0, 0, 0, 500 ),*/
                //Interval = TimeSpan.FromMilliseconds ( 100 ),
                Interval = TimeSpan.FromMilliseconds ( 10 ),
            };
            dispatcherTimer.Tick += new EventHandler ( DispatcherTimer_Tick );

            BeginTimer ();
            points = new PointCollection ();
            //BuildPolyLine ();
        }


        // Delegates




        #region Events

        private void CanvasDrawingArea_MouseRightButtonDown ( object sender, MouseButtonEventArgs e )
        {
            polyline?.Points.Clear ();

        }


        private void CanvasDrawingArea_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            polyline?.Points.Clear ();

        }


        private void CircleOption_Click ( object sender, RoutedEventArgs e )
        {
            currentShape = SelectedShape.PolyLine1;
            BuildPolyLine1 ();

        }


        private void CircleOption1_Click ( object sender, RoutedEventArgs e )
        {
            currentShape = SelectedShape.PolyLine2;
            BuildPolyLine1 ();
        }


        void DispatcherTimer_Tick ( object sender, EventArgs e )
        {
            Timer_Ticked ();
        }


        private void LineOption_Click ( object sender, RoutedEventArgs e )
        {

        }


        private void PolyLineOption_Click ( object sender, RoutedEventArgs e )
        {
            currentShape = SelectedShape.PolyLine;
            BuildPolyLine ();
        }


        private void RectOption_Click ( object sender, RoutedEventArgs e )
        {
            currentShape = SelectedShape.Rectangle;
            BuildRectangle ();
        }
        #endregion Events



        // Methods


        void AddToPolyLine ()
        {
            double _x = random.Next ( 10, 1890 );
            double _y = random.Next ( 10, 930 );
            Point point = new Point ( _x, _y );
            points.Add ( point );

        }


        /// <summary>
        /// Radical difference changing from 185.0 by .1!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        void AddToPolyLine1 ()
        {
            degrees += add;
            //degrees += 175.1;
            //degrees += 180.1;
            //degrees += 180.1; 
            if ( degrees > 360.0 )
                degrees -= 360.0;
            double _x = ( Math.Sin ( degrees ) * 460.0 ) + 930;
            double _y = ( Math.Cos ( degrees ) * 460.0 ) + 470;
            Point point = new Point ( _x, _y );
            points.Add ( point );
        }


        void AddToPolyLine2 ()
        {
            //degrees += 188.0;
            //degrees += 175.5;
            degrees += 191.5;
            //degrees += 185.5;
            if ( degrees > 360.0 )
                degrees -= 360.0;
            double _angle = Math.PI * degrees / 180.0;
            double _x = ( Math.Sin ( _angle ) * 460.0 ) + 930;
            double _y = ( Math.Cos ( _angle ) * 460.0 ) + 470;
            Point point = new Point ( _x, _y );
            points.Add ( point );
        }


        struct RectangleSupport
        {

            public double X;
            public double Y;
            public double DeltaX ;
            public double DeltaY;
            public double DeltaRadX;
            public double DeltaRadY;

            public RectangleSupport ( int _)
            {
                X = 15;
                Y = 15;
                DeltaX = 0;
                DeltaY = 0;
                DeltaRadX = .1;
                DeltaRadY = .1;
            }

        }

        // Need to use CompositionTarget.Render!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        void AnimateRectangle ()
        {
            if ( ( rectangle.RadiusX >= 44.9 ) || ( rectangle.RadiusX <= -25.0 ) )
                rectangleSupport.DeltaRadX = -rectangleSupport.DeltaRadX;
            rectangle.RadiusX += rectangleSupport.DeltaRadX;
            rectangle.RadiusY += rectangleSupport.DeltaRadX;

            if ( ( rectangleSupport.X < 10.0 ) || ( rectangleSupport.X > 1000.0 ) )
                rectangleSupport.DeltaX = -rectangleSupport.DeltaX;
            rectangleSupport.X += rectangleSupport.DeltaX;
            if ( ( rectangleSupport.Y < 10.0 ) || ( rectangleSupport.Y > 600.0 ) )
                rectangleSupport.DeltaY = -rectangleSupport.DeltaY;
            rectangleSupport.Y += rectangleSupport.DeltaY;

            rectangle.Margin = new Thickness ( rectangleSupport.X, rectangleSupport.Y, 0.0, 0.0 );
        }


        void BeginTimer ()
        {
            dispatcherTimer.Start ();
        }


        void BuildLinearGradientBrush ()
        {
            linearGradientBrush = new LinearGradientBrush ()
            {
                StartPoint = new Point ( 0, 0 ),
                EndPoint = new Point ( 1, 1 )
            };
            linearGradientBrush.GradientStops.Add ( new GradientStop ( Colors.DarkBlue, 0 ) );
            linearGradientBrush.GradientStops.Add ( new GradientStop ( Colors.LightCoral, 0.499 ) );
            linearGradientBrush.GradientStops.Add ( new GradientStop ( Colors.DarkGreen, 0.5 ) );
            linearGradientBrush.GradientStops.Add ( new GradientStop ( Colors.LightGoldenrodYellow, 0.501 ) );
            linearGradientBrush.GradientStops.Add ( new GradientStop ( Colors.DarkMagenta, 0.9 ) );
            linearGradientBrush.GradientStops.Add ( new GradientStop ( Colors.White, 1 ) );
        }


        void BuildPolyLine ()
        {

            //dispatcherTimer.Start ();
            tickCounter = 0;
            polyline?.Points.Clear ();
            canvasDrawingArea.Children.Clear ();

            polyline = new Polyline ()
            {
                Fill = new SolidColorBrush ( Color.FromArgb ( 255, 180, 0, 0 ) ),
                FillRule = FillRule.EvenOdd,
                Stroke = Brushes.Green,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round,
                Points = points,
            };
            canvasDrawingArea.Children.Add ( polyline );
        }


        void BuildPolyLine1 ()
        {
            //dispatcherTimer.Start ();
            degrees = 0;
            tickCounter = 0;
            polyline?.Points.Clear ();
            canvasDrawingArea.Children.Clear ();

            add = 90.0 + ( random.Next ( 900 ) / 10 );

            polyline = new Polyline ()
            {
                Fill = new SolidColorBrush ( Color.FromArgb ( 255, 0, 0, 180 ) ),
                //FillRule = FillRule.Nonzero,
                FillRule = FillRule.EvenOdd,
                Stroke = Brushes.Green,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Bevel,
                Points = points,
            };
            canvasDrawingArea.Children.Add ( polyline );
        }


        void BuildRectangle ()
        {
            //tickCounter = 0;
            //polyline?.Points.Clear ();
            //dispatcherTimer.Stop ();
            canvasDrawingArea.Children.Clear ();
            BuildLinearGradientBrush ();
            rectangle = new Rectangle ()
            {
                Name = "rectangle",
                Height = 100.0,
                Width = 100.0,
                //Fill = new SolidColorBrush ( Color.FromArgb ( 255, 180, 0, 0 ) ),
                Fill = linearGradientBrush,
                Margin = new Thickness (15.0, 15.0, 0.0 ,0.0),
                RadiusX = 10,
                RadiusY = 10.0,
                Stroke = Brushes.BlueViolet,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round,
            };
            rectangleSupport = new RectangleSupport ()
            {
                X = 15.0,
                Y = 15.0,
                DeltaX = 11.1,
                DeltaY = 11.1,
                DeltaRadX =1.1,
                DeltaRadY = 1.1,
            };
            //rectangleSupport = new RectangleSupport ( 1 );

            canvasDrawingArea.Children.Add ( rectangle );
        }


        void Timer_Ticked ()
        {
            if ( currentShape == SelectedShape.Unset )
                return;
            //menuTextBlock.Text = $"Time is:  { DateTime.Now.ToLongTimeString ()}";
            tickCounter++;
            menuTextBlock.Text = $"Point count is:  {tickCounter.ToString ()}";

            switch ( currentShape )
            {
                case SelectedShape.PolyLine1:
                    AddToPolyLine1 ();
                    break;

                case SelectedShape.PolyLine2:
                    AddToPolyLine2 ();
                    break;

                case SelectedShape.Rectangle:
                    AnimateRectangle ();
                    //AddToPolyLine2 ();
                    break;

                default:
                    AddToPolyLine ();
                    break;
            }

        }



        //var doubleAnimationMarginX = new DoubleAnimation
        //{
        //    From = 10,
        //    To = 1100,
        //    AutoReverse = true,
        //    RepeatBehavior = RepeatBehavior.Forever,
        //};

        //var bounceEase = new BounceEase ()
        //{
        //    Bounciness = 0,
        //    EasingMode = EasingMode.EaseIn,
        //};
        ////Storyboard.SetTargetName ( dou)

        //var doubleAnimationMarginY = new DoubleAnimation
        //{
        //    From = 10,
        //    To = 700,
        //    AutoReverse = true,
        //    RepeatBehavior = RepeatBehavior.Forever,
        //    EasingFunction = bounceEase,
        //};






        //        // Add the Polyline Element
        //        myPolyline = new Polyline ();
        //        myPolyline.Stroke = System.Windows.Media.Brushes.SlateGray;
        //myPolyline.StrokeThickness = 2;
        //myPolyline.FillRule = FillRule.EvenOdd;
        //System.Windows.Point Point4 = new System.Windows.Point ( 1, 50 );
        //        System.Windows.Point Point5 = new System.Windows.Point ( 10, 80 );
        //        System.Windows.Point Point6 = new System.Windows.Point ( 20, 40 );
        //        PointCollection myPointCollection2 = new PointCollection ();
        //        myPointCollection2.Add(Point4);
        //myPointCollection2.Add(Point5);
        //myPointCollection2.Add(Point6);
        //myPolyline.Points = myPointCollection2;
        //myGrid.Children.Add(myPolyline);











        // Trying to make timer work.
        //TimerCallback timerCallback = new TimerCallback ( TimerCallbackHandler );


        /* If a System.Timers.Timer is used in a WPF application, it is worth noting that the System.Timers.Timer runs 
         * on a different thread than the user interface (UI) thread.In order to access objects on the user interface (UI) thread,
         * it is necessary to post the operation onto the Dispatcher of the user interface (UI) thread using Invoke or BeginInvoke.
         * Reasons for using a DispatcherTimer as opposed to a System.Timers.Timer are that the DispatcherTimer runs on the 
         * same thread as the Dispatcher and a DispatcherPriority can be set on the DispatcherTimer. */
        //TimerCallback timerCallback = new TimerCallback ( TimerCallbackHandler );
        // Had to place the timer constructor in the class (? app or main window ?) constructor.
        //Timer timer = new Timer (
        //    timerCallback,
        //    null,
        //    0,
        //    1000 );


        //// Trying to make timer work.
        //static void TimerCallbackHandler ( object state )
        //{

        //}




    }
}
