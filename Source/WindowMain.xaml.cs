
using System.Collections.Generic;
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
            PrepareMenuIcons( TreeType.CommonBST, false );
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
            if ( Settings.TreeType == newTreeType )
            { 
                return;
            }

            MenuPanel.Children.Clear();
            PrepareMenuIcons( newTreeType, false );
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
            Result result = controller.DrawTree( TextNode.Text );
            CheckResult( result );
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
            Result result = controller.AddNodes( TextNode.Text );
            CheckResult( result );
        }
        
        private void ActionAddNodesInStep( object sender, RoutedEventArgs e )
        {
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ActionDeleteNodes( object sender, RoutedEventArgs e )
        {
            Result result = controller.DeleteNodes();
            CheckResult( result );
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
            Result result = controller.RotateNode();
            CheckResult( result );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionBalanceTree( object sender, RoutedEventArgs e )
        {
            Result result = controller.BalanceTree();
            CheckResult( result );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionBalanceTreeInStep( object sender, RoutedEventArgs e )
        {
            Result result = controller.BalanceTreePrepareSteps();
            
            if ( result != Result.OK )
            {
                MessageBox.Show( messages.GetMessageText( result ));
            }
            else
            {
                MenuPanel.Children.Clear();
                PrepareMenuIcons( TreeType.CommonBST, true );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionStepForward( object sender, RoutedEventArgs e )
        {
            controller.StepForward();
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

        private void PrepareMenuIcons( TreeType treeType, bool isStepMode )
        {
            List< bool > state = isStepMode ? iconStateStepMode : iconStateNormal;
            int index = 0;
            
            if ( treeType is TreeType.CommonBST )
            { 
                AddIconForMenu( "PathTree", new DelegateRoutedEvent( ActionDrawTree ), state[index++] );
                AddIconForMenu( "PathPlus", new DelegateRoutedEvent( ActionAddNodes ), state[index++] );
                AddIconForMenu( "PathMinus", new DelegateRoutedEvent( ActionDeleteNodes ), state[index++] );
                AddIconForMenu( "PathRotation", new DelegateRoutedEvent( ActionRotationNode ), state[index++] );
                AddIconForMenu( "PathBalanceTree", new DelegateRoutedEvent( ActionBalanceTree ), state[index++] );
                AddIconForMenu( "PathBalanceTreeInStep", new DelegateRoutedEvent( ActionBalanceTreeInStep ), state[index++] );
                AddIconForMenu( "PathStepForward", new DelegateRoutedEvent( ActionStepForward ), state[index++] );
                AddIconForMenu( "PathStepBackward", new DelegateRoutedEvent( ActionStepBackward ), state[index++] );
                AddIconForMenu( "PathStepModeLeave", new DelegateRoutedEvent( ActionStepModeLeave ), state[index++] );
                AddIconForMenu( "PathDestroyTree", new DelegateRoutedEvent( ActionDestroyTree ), state[index++] );
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

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void MinorWindowClosed( object sender, System.EventArgs e )
        {
            MenuMain.IsEnabled = true;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckResult( Result result )
        {
            if ( result != Result.OK )
            {
                MessageBox.Show( messages.GetMessageText( result ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private delegate void DelegateRoutedEvent( object sender , RoutedEventArgs e );
        private readonly Message messages;
        private readonly Controller controller;
        
        private readonly List< bool > iconStateNormal = new List< bool > 
        { 
            true, true, true, true, true, true, false, false, false, true 
        };

        private readonly List< bool > iconStateStepMode = new List< bool > 
        { 
            false, false, false, false, false, false, true, true, true, false 
        };
    }
}

