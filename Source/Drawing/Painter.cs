
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisualTree
{
    class Painter
    {
        public void DrawTree( Node node, Canvas canvas )
        {
            if ( node.IsLeft() )
            {
                DrawTree( node.Left, canvas );
            }

            DrawNode( node, canvas );

            if ( node.IsRight() )
            {
                DrawTree( node.Right, canvas );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void DrawNode( Node node, Canvas canvas )
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 30;
            ellipse.Height = 30;

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            ellipse.Fill = mySolidColorBrush;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Black;

            Canvas.SetLeft( ellipse, node.PosHor - 15 );
            Canvas.SetTop( ellipse, node.PosVer - 15 );

            canvas.Children.Add( ellipse );
        }
    }
}

