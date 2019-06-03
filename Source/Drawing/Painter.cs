
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;

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
            ellipse.Width = Radius * 2;
            ellipse.Height = Radius * 2;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Black;

            Canvas.SetLeft( ellipse, node.PosHor - 15 );
            Canvas.SetTop( ellipse, node.PosVer - 15 );
            canvas.Children.Add( ellipse );

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
            Line line = new Line();
            line.Stroke = Brushes.Black;
            int shiftDirection = x1 < x2 ? 1 : -1;

            line.X1 = x1 + shiftDirection * ( Radius * Math.Sin( Math.PI * ParentAngle / 180 ));
            line.Y1 = y1 + ( Radius * Math.Cos( Math.PI * ParentAngle / 180 ));
            line.X2 = x2 - shiftDirection * ( Radius * Math.Sin( Math.PI * ChildAngle / 180 ));
            line.Y2 = y2 - ( Radius * Math.Cos( Math.PI * ChildAngle / 180 ));
            return line;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private readonly int Radius = 15;
        private readonly int ParentAngle = 40;
        private readonly int ChildAngle = 30;
    }
}

