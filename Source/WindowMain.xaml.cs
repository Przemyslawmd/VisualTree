
using System.Windows;

namespace VisualTree
{
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
            controller = new Controller();
            messages = new Message();
        }
               
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuTreeBST( object sender, RoutedEventArgs e )
        {
            
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuTreeAVL( object sender, RoutedEventArgs e )
        {
        
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuTreeRB( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuSettings( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuAbout( object sender, RoutedEventArgs e )
        {

        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionDrawTree( object sender, RoutedEventArgs e )
        {
            code = Message.Code.OK;
            controller.DrawTree( TextNode.Text, CanvasTree, ref code );

            if ( code != Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        private Message messages;
        private Message.Code code;
        private Controller controller;
    }
}

