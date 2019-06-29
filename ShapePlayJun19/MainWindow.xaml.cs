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

namespace ShapePlayJun19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Fields
        private enum SelectedShape
        { Unset, Circle, Rectangle, Line, PolyLine }
        SelectedShape currentShape;

        DispatcherTimer dispatcherTimer;
        PointCollection points;
        Polyline polyline;
        static Random random = new Random ();

        int tickCounter = 0;
        double degrees = 0;

        // Constructor

        public MainWindow ()
        {
            InitializeComponent ();
            NameScope.SetNameScope ( this, new NameScope () );

            dispatcherTimer = new DispatcherTimer ()
            {
                /*Interval = new TimeSpan ( 0, 0, 0, 0, 500 ),*/
                Interval = TimeSpan.FromMilliseconds ( 100 ),
        };
            dispatcherTimer.Tick += new EventHandler ( DispatcherTimer_Tick );

            BeginTimer ();
            points = new PointCollection ();
            //BuildPolyLine ();
        }


        // Delegates




        // Events

        private void CanvasDrawingArea_MouseRightButtonDown ( object sender, MouseButtonEventArgs e )
        {
            polyline.Points.Clear ();

        }


        private void CanvasDrawingArea_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            polyline.Points.Clear ();

        }


        private void CircleOption_Click ( object sender, RoutedEventArgs e )
        {
            currentShape = SelectedShape.Circle;
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
            BuildPolyLine1 ();
        }



        // Methods

        void BeginTimer ()
        {
            dispatcherTimer.Start ();
        }


        void BuildPolyLine ()
        {
            tickCounter = 0;
            polyline?.Points.Clear ();
            canvasDrawingArea.Children.Clear ();

            polyline = new Polyline ()
            {
                Fill = new SolidColorBrush ( Color.FromArgb ( 255, 180, 0, 0 ) ),
                FillRule = FillRule.EvenOdd,
                //Fill = Brushes.HotPink,
                Stroke = Brushes.Green,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round,
                Points = points,
            };
            canvasDrawingArea.Children.Add ( polyline );
        }


        void BuildPolyLine1 ()
        {
            degrees = 0;
            tickCounter = 0;
            polyline?.Points.Clear ();
            canvasDrawingArea.Children.Clear ();
            
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


        public void Timer_Ticked ()
        {
            if ( currentShape == SelectedShape.Unset )
                return;
            //menuTextBlock.Text = $"Time is:  { DateTime.Now.ToLongTimeString ()}";
            tickCounter++;
            menuTextBlock.Text = $"Point count is:  {tickCounter.ToString ()}";

            switch ( currentShape )
            {
                case SelectedShape.Circle:
                    AddToPolyLine1 ();
                    break;

                case SelectedShape.Rectangle:
                    AddToPolyLine2 ();
                    break;

                default:
                    AddToPolyLine ();
                    break;
            }


        }


        void AddToPolyLine ()
        {
            double _x = random.Next ( 10, 1100 );
            double _y = random.Next ( 10, 650 );
            Point point = new Point ( _x, _y );
            points.Add ( point );

        }


        void AddToPolyLine1 ()
        {
            degrees += 185.0;
            if ( degrees > 360.0 )
                degrees -= 360.0;
            double _x = ( Math.Sin ( degrees ) * 300.0 ) + 500;
            double _y = ( Math.Cos ( degrees ) * 300.0 ) + 350;
            Point point = new Point ( _x, _y );
            points.Add ( point );
        }


        void AddToPolyLine2 ()
        {
            degrees += 185.5;
            if ( degrees > 360.0 )
                degrees -= 360.0;
            double _angle = Math.PI * degrees / 180.0;
            double _x = ( Math.Sin ( _angle ) * 300.0 ) + 500;
            double _y = ( Math.Cos ( _angle ) * 300.0 ) + 350;
            Point point = new Point ( _x, _y );
            points.Add ( point );
        }








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
