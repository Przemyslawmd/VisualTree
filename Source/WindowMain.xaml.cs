
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace VisualTree
{
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
            PrepareMenuIcons();
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
            Message.Code code = controller.DrawTree( TextNode.Text, CanvasTree );

            if ( code != Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionDestroyTree( object sender, RoutedEventArgs e )
        {
            controller.DestroyTree( CanvasTree );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionAddNodes( object sender, RoutedEventArgs e )
        {
            Message.Code code = controller.AddNodes( TextNode.Text, CanvasTree );

            if ( code !=  Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionDeleteNodes( object sender, RoutedEventArgs e )
        {
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionRotationNode( object sender, RoutedEventArgs e )
        {
            Message.Code code = controller.RotateNode( CanvasTree );

            if ( code !=  Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionBalanceTree( object sender, RoutedEventArgs e )
        {
        }

        private void ActionBalanceTreeInStep( object sender, RoutedEventArgs e )
        {
        }

        private void ActionStepForward( object sender, RoutedEventArgs e )
        {
        }

        private void ActionStepBackward( object sender, RoutedEventArgs e )
        {
        }

        private void ActionStepModeLeave( object sender, RoutedEventArgs e )
        {
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionCheckNode( object sender, MouseButtonEventArgs e )
        {
            Canvas canvas = sender as Canvas;
            Point point = e.GetPosition( canvas );
            controller.SelectNode( canvas, (int) point.X, (int) point.Y );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void PrepareMenuIcons()
        {
            AddIconForMenu( new DelegateRoutedEvent( ActionDrawTree ), "PathTree" );
            AddIconForMenu( new DelegateRoutedEvent( ActionAddNodes ), "PathPlus" );
            AddIconForMenu( new DelegateRoutedEvent( ActionDeleteNodes ), "PathMinus" );
            AddIconForMenu( new DelegateRoutedEvent( ActionRotationNode ), "PathRotation" );
            AddIconForMenu( new DelegateRoutedEvent( ActionBalanceTree ), "PathBalanceTree" );
            AddIconForMenu( new DelegateRoutedEvent( ActionBalanceTreeInStep ), "PathBalanceTreeInStep" );
            AddIconForMenu( new DelegateRoutedEvent( ActionStepForward ), "PathStepForward" );
            AddIconForMenu( new DelegateRoutedEvent( ActionStepBackward ), "PathStepBackward" );
            AddIconForMenu( new DelegateRoutedEvent( ActionStepModeLeave ), "PathStepModeLeave" );
            AddIconForMenu( new DelegateRoutedEvent( ActionDestroyTree ), "PathDestroyTree" );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddIconForMenu( DelegateRoutedEvent delegateAction, string resource )
        {
            Rectangle rec = new Rectangle 
            {
                Height = 50,
                Width = 50,
                Fill = Application.Current.Resources[resource] as DrawingBrush
            };
            
            Button button = new Button
            {
                Content = rec,
            };
            button.Click += new RoutedEventHandler( delegateAction );
            
            MenuPanel.Children.Add( button );
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private delegate void DelegateRoutedEvent( object sender , RoutedEventArgs e );
        private Message messages;
        private Message.Code code;
        private Controller controller;
    }
}

