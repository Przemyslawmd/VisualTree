
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System;

namespace VisualTree
{
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
            ServiceControls.CreateServiceControls( CanvasTree );
            ServiceListener.AddListener( Note.Get() );
            PrepareMenuIcons( TreeType.CommonBST, false );
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

        private void ActionMenuTreeRB( object sender, RoutedEventArgs e )
        {
            ChangeTreeType( TreeType.RB );
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
            LabelTreeType.Content = TreeTypeLabel[newTreeType];
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
            ShowMinorWindow( new WindowAbout() );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionDrawTree( object sender, RoutedEventArgs e )
        {
            Result result = controller.DrawTree( TextNode.Text );
            CheckResult( result );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionDrawTreeInStep( object sender, RoutedEventArgs e )
        {
            Result result = controller.DrawTreePrepareSteps( TextNode.Text );
            TryPrepareMenuIconsForStepMode( result, TreeType.AVL );
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
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionAddNodesInStep( object sender, RoutedEventArgs e )
        {
            Result result = controller.AddNodesPrepareSteps( TextNode.Text );
            TryPrepareMenuIconsForStepMode( result, TreeType.AVL );
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
            TryPrepareMenuIconsForStepMode( result, TreeType.AVL );
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
            TryPrepareMenuIconsForStepMode( result, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionStepForward( object sender, RoutedEventArgs e )
        {
            controller.StepForward();
            CheckResult();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionStepBackward( object sender, RoutedEventArgs e )
        {
            controller.StepBackward();
            CheckResult();
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

        private void EnableDisableNotifications( object sender, RoutedEventArgs e )
        {
            Settings.Notifications = !Settings.Notifications;
            ButtonStateNotifications.Content = Settings.Notifications ? "Disable" : "Enable";

            if ( Settings.Notifications is false )
            {
                Note.Destroy();
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionClearNotificationsArea( object sender, RoutedEventArgs e )
        {
            TextNotifications.Text = null;
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
                AddMenuIcon( "PathTree", "Create Tree", ActionDrawTree, ref enumerator );
                AddMenuIcon( "PathPlus", "Add Nodes", ActionAddNodes, ref enumerator );
                AddMenuIcon( "PathMinus", "Delete Nodes", ActionDeleteNodes, ref enumerator );
                AddMenuIcon( "PathRotation", "Rotate Node", ActionRotationNode, ref enumerator );
                AddMenuIcon( "PathBalanceTree", "Balance Tree", ActionBalanceTree, ref enumerator );
                AddMenuIcon( "PathBalanceTreeInStep", "Balance Tree in Step Mode", ActionBalanceTreeInStep, ref enumerator );
                AddMenuIcon( "PathStepForward", "Step Forward", ActionStepForward, ref enumerator );
                AddMenuIcon( "PathStepBackward", "Step Backward", ActionStepBackward, ref enumerator );
                AddMenuIcon( "PathStepModeLeave", "Leave Step Mode", ActionStepModeLeave, ref enumerator );
                AddMenuIcon( "PathDestroyTree", "Destroy Tree", ActionDestroyTree, ref enumerator );
            }
            else if ( treeType is TreeType.AVL )
            { 
                AddMenuIcon( "PathTree", "Create Tree", ActionDrawTree, ref enumerator );
                AddMenuIcon( "PathTreeStep", "Create Tree in Step Mode", ActionDrawTreeInStep, ref enumerator );
                AddMenuIcon( "PathPlus", "Add Nodes", ActionAddNodes, ref enumerator );
                AddMenuIcon( "PathMinus", "Delete Nodes", ActionDeleteNodes, ref enumerator );
                AddMenuIcon( "PathPlusStep", "Add Nodes in Step Mode", ActionAddNodesInStep, ref enumerator );
                AddMenuIcon( "PathMinusStep", "Delete Nodes in Step Mode", ActionDeleteNodesInStep, ref enumerator );
                AddMenuIcon( "PathStepForward", "Step Forward", ActionStepForward, ref enumerator );
                AddMenuIcon( "PathStepBackward", "Step Backward", ActionStepBackward, ref enumerator );
                AddMenuIcon( "PathStepModeLeave", "Leave Step Mode", ActionStepModeLeave, ref enumerator );
                AddMenuIcon( "PathDestroyTree", "Destroy Tree", ActionDestroyTree, ref enumerator );
            }
            else if ( treeType is TreeType.RB )
            {
                AddMenuIcon( "PathTree", "Create Tree", ActionDrawTree, ref enumerator );
                AddMenuIcon( "PathTreeStep", "Create Tree in Step Mode", ActionDrawTreeInStep, ref enumerator );
                AddMenuIcon( "PathPlus", "Add Nodes", ActionAddNodes, ref enumerator );
                AddMenuIcon( "PathMinus", "Delete Nodes", ActionDeleteNodes, ref enumerator );
                AddMenuIcon( "PathPlusStep", "Add Nodes in Step Mode", ActionAddNodesInStep, ref enumerator );
                AddMenuIcon( "PathMinusStep", "Delete Nodes in Step Mode", ActionDeleteNodesInStep, ref enumerator );
                AddMenuIcon( "PathStepForward", "Step Forward", ActionStepForward, ref enumerator );
                AddMenuIcon( "PathStepBackward", "Step Backward", ActionStepBackward, ref enumerator );
                AddMenuIcon( "PathStepModeLeave", "Leave Step Mode", ActionStepModeLeave, ref enumerator );
                AddMenuIcon( "PathDestroyTree", "Destroy Tree", ActionDestroyTree, ref enumerator );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddMenuIcon( string resource, string toolTipText, Action< object, RoutedEventArgs> action, 
                                  ref List< bool >.Enumerator enumerator )
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
                IsEnabled = enumerator.Current,
            };
            button.Click += new RoutedEventHandler( action );
            
            ToolTip toolTip = new ToolTip
            {
                Content = toolTipText
            };
            ToolTipService.SetToolTip( button, toolTip );
            
            MenuPanel.Children.Add( button );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ShowMinorWindow( Window window )
        {
            Window settings = new WindowSettings();
            IsEnabled = false;
            window.Closed += MinorWindowClosed;
            window.Show();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void MinorWindowClosed( object sender, System.EventArgs e )
        {
            IsEnabled = true;
            SetMenuPanelToolTips( Settings.MenuPanelToolTips );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckResult( Result result = Result.OK )
        {
            if ( Settings.Notifications is false )
            {
                return;
            }
            
            if ( result != Result.OK )
            {
                TextNotifications.Text += messages.GetMessageText( result ) + Environment.NewLine;
                return;
            }

            foreach ( Action action in Note.Get().Actions )
            {
                TextNotifications.Text += "Node : " + action.Node.Key + getActionString( action ) + Environment.NewLine;
            }
            Note.Get().ClearActions();


            string getActionString( Action action )
            {
                if ( action.ActionType == ActionType.ADD )
                {
                    return "  added";
                }
                else if ( action.ActionType == ActionType.REMOVE )
                {
                    return "  deleted";
                }
                return "  rotated";
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void TryPrepareMenuIconsForStepMode( Result result, TreeType treeType )
        {
            if ( result == Result.OK )
            {
                PrepareMenuIcons( treeType, true );
            }
            else if ( Settings.Notifications )
            {
                TextNotifications.Text += messages.GetMessageText( result ) + Environment.NewLine;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void SetMenuPanelToolTips( bool state )
        {
            foreach ( var item in  MenuPanel.Children )
            {
                ToolTipService.SetIsEnabled(( DependencyObject ) item, state );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private readonly Message messages = new Message();
        private readonly Controller controller = new Controller();

        private readonly Dictionary< TreeType, String > TreeTypeLabel = new Dictionary< TreeType, string >
        { 
            [TreeType.CommonBST] = "Tree type : Common BST",
            [TreeType.AVL]       = "Tree type : AVL",
            [TreeType.RB]        = "Tree type : Red Black"
        };
    }
}

