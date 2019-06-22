
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
            ServiceControls.CreateServiceControls( CanvasTree );
            PrepareMenuIcons( TreeType.CommonBST );
            controller = new Controller();
            messages = new Message();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuTreeBST( object sender, RoutedEventArgs e )
        {
            ChangeTreeType( TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionMenuTreeAVL( object sender, RoutedEventArgs e )
        {
           ChangeTreeType( TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ChangeTreeType( TreeType newTreeType )
        {
            if ( Settings.treeType == newTreeType )
            { 
                return;
            }

            MenuPanel.Children.Clear();
            PrepareMenuIcons( newTreeType );
            controller.DestroyTree();
            Settings.SetTreeType( newTreeType );
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
            ShowMinorWindow( new WindowSettings() );
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
            Message.Code code = controller.DrawTree( TextNode.Text );

            if ( code != Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        private void ActionDrawTreeInStep( object sender, RoutedEventArgs e )
        {
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionDestroyTree( object sender, RoutedEventArgs e )
        {
            controller.DestroyTree();
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionAddNodes( object sender, RoutedEventArgs e )
        {
            Message.Code code = controller.AddNodes( TextNode.Text );

            if ( code !=  Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }
        
        private void ActionAddNodesInStep( object sender, RoutedEventArgs e )
        {
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionDeleteNodes( object sender, RoutedEventArgs e )
        {
            Message.Code code = controller.DeleteNodes();

            if ( code !=  Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionDeleteNodesInStep( object sender, RoutedEventArgs e )
        {
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionRotationNode( object sender, RoutedEventArgs e )
        {
            Message.Code code = controller.RotateNode();

            if ( code !=  Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionBalanceTree( object sender, RoutedEventArgs e )
        {
            Message.Code code = controller.BalanceTree();

            if ( code !=  Message.Code.OK )
            {
                MessageBox.Show( messages.GetMessageText( code ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

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

        private void PrepareMenuIcons( TreeType treeType )
        {
            if ( treeType is TreeType.CommonBST )
            { 
                AddIconForMenu( "PathTree", new DelegateRoutedEvent( ActionDrawTree ), true );
                AddIconForMenu( "PathPlus", new DelegateRoutedEvent( ActionAddNodes ), true );
                AddIconForMenu( "PathMinus", new DelegateRoutedEvent( ActionDeleteNodes ), true );
                AddIconForMenu( "PathRotation", new DelegateRoutedEvent( ActionRotationNode ), true );
                AddIconForMenu( "PathBalanceTree", new DelegateRoutedEvent( ActionBalanceTree ), true );
                AddIconForMenu( "PathBalanceTreeInStep", new DelegateRoutedEvent( ActionBalanceTreeInStep ), true );
                AddIconForMenu( "PathStepForward", new DelegateRoutedEvent( ActionStepForward ), false );
                AddIconForMenu( "PathStepBackward", new DelegateRoutedEvent( ActionStepBackward ), false );
                AddIconForMenu( "PathStepModeLeave", new DelegateRoutedEvent( ActionStepModeLeave ), false );
                AddIconForMenu( "PathDestroyTree", new DelegateRoutedEvent( ActionDestroyTree ), true );
            }
            else if ( treeType is TreeType.AVL )
            { 
                AddIconForMenu( "PathTree", new DelegateRoutedEvent( ActionDrawTree ), true );
                AddIconForMenu( "PathTreeStep", new DelegateRoutedEvent( ActionDrawTreeInStep ), true );
                AddIconForMenu( "PathPlus", new DelegateRoutedEvent( ActionAddNodes ), true );
                AddIconForMenu( "PathMinus", new DelegateRoutedEvent( ActionDeleteNodes), true );
                AddIconForMenu( "PathPlusStep", new DelegateRoutedEvent( ActionAddNodesInStep ), true );
                AddIconForMenu( "PathMinusStep", new DelegateRoutedEvent( ActionDeleteNodesInStep ), true );
                AddIconForMenu( "PathStepForward", new DelegateRoutedEvent( ActionStepForward ), false );
                AddIconForMenu( "PathStepBackward", new DelegateRoutedEvent( ActionStepBackward ), false );
                AddIconForMenu( "PathStepModeLeave", new DelegateRoutedEvent( ActionStepModeLeave ), false );
                AddIconForMenu( "PathDestroyTree", new DelegateRoutedEvent( ActionDestroyTree ), true );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddIconForMenu( string resource, DelegateRoutedEvent delegateAction, bool isEnabled )
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
                IsEnabled = isEnabled
            };
            button.Click += new RoutedEventHandler( delegateAction );
            
            MenuPanel.Children.Add( button );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ShowMinorWindow( Window window )
        {
            Window settings = new WindowSettings();
            MenuMain.IsEnabled = false;
            window.Closed += MinorWindowClosed;
            window.Show();
        }

        public void MinorWindowClosed( object sender, System.EventArgs e )
        {
            MenuMain.IsEnabled = true;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private delegate void DelegateRoutedEvent( object sender , RoutedEventArgs e );
        private Message messages;
        private Message.Code code;
        private Controller controller;
    }
}

