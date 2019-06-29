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
using System.Windows.Ink;

namespace ShapePlayJun19
{
    public class Worksheet
    {


        public void BuildPolylineHere ()
        {
            // Add the Polyline Element
            var myPolyline = new Polyline ()
            {
                Fill = new SolidColorBrush ( Color.FromArgb ( 255, 180, 0, 0 ) ),
                FillRule = FillRule.Nonzero,
                Stroke = Brushes.Green,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Bevel,
                /*Points = points,*/
            };




        }

















    }
}
