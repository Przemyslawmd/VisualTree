
using static System.Math;
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
            Ellipse ellipse = new Ellipse
            {
                Width = Radius * 2,
                Height = Radius * 2,
                StrokeThickness = node.IsSelected ? 3 : 1,
                Stroke = Brushes.Black
            };

            Canvas.SetLeft( ellipse, node.PosHor - Radius );
            Canvas.SetTop( ellipse, node.PosVer - Radius );
            canvas.Children.Add( ellipse );

            TextBlock keyText = new TextBlock 
            {
                Text = node.Key.ToString(),
                Foreground = new SolidColorBrush( Colors.Black )
            };
            
            Canvas.SetLeft( keyText, node.PosHor - ( Radius / 2 ));
            Canvas.SetTop( keyText, node.PosVer - ( Radius / 2 ));
            canvas.Children.Add( keyText );

            if ( node.IsLeft() )
            {
                Line line = CreateLine( node.PosHor, node.PosVer, node.Left.PosHor, node.Left.PosVer );
                canvas.Children.Add( line );
            }

            if ( node.IsRight() )
            {
                Line line  = CreateLine( node.PosHor, node.PosVer, node.Right.PosHor, node.Right.PosVer );
                canvas.Children.Add( line );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Line CreateLine( int x1, int y1, int x2, int y2 )
        {
            Line line = new Line
            {
                Stroke = Brushes.Black
            };
            
            int shiftDirection = x1 < x2 ? 1 : -1;

            line.X1 = x1 + shiftDirection * ( Radius * Sin( PI * ParentAngle / 180 ));
            line.Y1 = y1 + ( Radius * Cos( PI * ParentAngle / 180 ));
            line.X2 = x2 - shiftDirection * ( Radius * Sin( PI * ChildAngle / 180 ));
            line.Y2 = y2 - ( Radius * Cos( PI * ChildAngle / 180 ));
            return line;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private readonly int Radius = Settings.Diameter / 2;
        private readonly int ParentAngle = 40;
        private readonly int ChildAngle = 30;
    }
}

