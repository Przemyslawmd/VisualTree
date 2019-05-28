
using System.Windows.Shapes;
using System.Windows;

namespace VisualTreeNET
{
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionTreeBST( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionTreeAVL( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionTreeRB( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionSettings( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionAbout( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void DrawTree( object sender, RoutedEventArgs e )
        {
            Line line = new Line();

            line.Stroke = SystemColors.WindowFrameBrush;
            line.X1 = 10.0;
            line.Y1 = 10.0;
            line.X2 = 20.0;
            line.Y2 = 20.0;
            CanvasTree.Children.Add( line );
        }
    }
}

