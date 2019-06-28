
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
            Result result = controller.DrawTreePrepareSteps( TextNode.Text );
            
            if ( result == Result.OK )
            {
                PrepareMenuIcons( TreeType.AVL, true );
            }
            else
            {
                MessageBox.Show( messages.GetMessageText( result ));
            }
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
            Result result = controller.AddNodesPrepareSteps( TextNode.Text );
            
            if ( result == Result.OK )
            {
                PrepareMenuIcons( TreeType.AVL, true );
            }
            else
            {
                MessageBox.Show( messages.GetMessageText( result ));
            }
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
            Result result = controller.DeleteNodesPrepareSteps();
            
            if ( result == Result.OK )
            {
                PrepareMenuIcons( TreeType.AVL, true );
            }
            else
            {
                MessageBox.Show( messages.GetMessageText( result ));
            }
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
            
            if ( result == Result.OK )
            {
                PrepareMenuIcons( TreeType.CommonBST, true );
            }
            else
            {
                MessageBox.Show( messages.GetMessageText( result ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionStepForward( object sender, RoutedEventArgs e )
        {
            controller.StepForward();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionStepBackward( object sender, RoutedEventArgs e )
        {
            controller.StepBackward();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionStepModeLeave( object sender, RoutedEventArgs e )
        {
            StepMode.Destroy();
            PrepareMenuIcons( Settings.TreeType, false );
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
            MenuPanel.Children.Clear();
            
            var state = isStepMode ? 
                new List< bool >() { false, false, false, false, false, false, true,  true,  true,  false } :
                new List< bool >() { true,  true,  true,  true,  true,  true,  false, false, false, true  } ; 
               
            var enumerator = state.GetEnumerator();

            if ( treeType is TreeType.CommonBST )
            { 
                AddIconForMenu( "PathTree", new DelegateRoutedEvent( ActionDrawTree ), ref enumerator );
                AddIconForMenu( "PathPlus", new DelegateRoutedEvent( ActionAddNodes ), ref enumerator );
                AddIconForMenu( "PathMinus", new DelegateRoutedEvent( ActionDeleteNodes ), ref enumerator );
                AddIconForMenu( "PathRotation", new DelegateRoutedEvent( ActionRotationNode ), ref enumerator );
                AddIconForMenu( "PathBalanceTree", new DelegateRoutedEvent( ActionBalanceTree ), ref enumerator );
                AddIconForMenu( "PathBalanceTreeInStep", new DelegateRoutedEvent( ActionBalanceTreeInStep ), ref enumerator );
                AddIconForMenu( "PathStepForward", new DelegateRoutedEvent( ActionStepForward ), ref enumerator );
                AddIconForMenu( "PathStepBackward", new DelegateRoutedEvent( ActionStepBackward ), ref enumerator );
                AddIconForMenu( "PathStepModeLeave", new DelegateRoutedEvent( ActionStepModeLeave ), ref enumerator );
                AddIconForMenu( "PathDestroyTree", new DelegateRoutedEvent( ActionDestroyTree ), ref enumerator );
            }
            else if ( treeType is TreeType.AVL )
            { 
                AddIconForMenu( "PathTree", new DelegateRoutedEvent( ActionDrawTree ), ref enumerator );
                AddIconForMenu( "PathTreeStep", new DelegateRoutedEvent( ActionDrawTreeInStep ), ref enumerator );
                AddIconForMenu( "PathPlus", new DelegateRoutedEvent( ActionAddNodes ), ref enumerator );
                AddIconForMenu( "PathMinus", new DelegateRoutedEvent( ActionDeleteNodes), ref enumerator );
                AddIconForMenu( "PathPlusStep", new DelegateRoutedEvent( ActionAddNodesInStep ), ref enumerator );
                AddIconForMenu( "PathMinusStep", new DelegateRoutedEvent( ActionDeleteNodesInStep ), ref enumerator );
                AddIconForMenu( "PathStepForward", new DelegateRoutedEvent( ActionStepForward ), ref enumerator );
                AddIconForMenu( "PathStepBackward", new DelegateRoutedEvent( ActionStepBackward ), ref enumerator );
                AddIconForMenu( "PathStepModeLeave", new DelegateRoutedEvent( ActionStepModeLeave ), ref enumerator );
                AddIconForMenu( "PathDestroyTree", new DelegateRoutedEvent( ActionDestroyTree ), ref enumerator );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddIconForMenu( string resource, DelegateRoutedEvent action, ref List< bool >.Enumerator enumerator )
        {
            enumerator.MoveNext();
            
            Rectangle rec = new Rectangle 
            {
                Height = 50,
                Width = 50,
                Fill = Application.Current.Resources[resource] as DrawingBrush
            };
            
            Button button = new Button
            {
                Content = rec,
                IsEnabled = enumerator.Current
            };
            button.Click += new RoutedEventHandler( action );
            
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
    }
}

