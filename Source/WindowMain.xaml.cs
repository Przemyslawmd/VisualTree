
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
            
            if (canvas is null || canvas.Children.Count == 0)
            {
                return;
            }

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
                AddMenuIcon( IconType.CreateTree,          ActionDrawTree,           ref enumerator );
                AddMenuIcon( IconType.AddNodes,            ActionAddNodes,           ref enumerator );
                AddMenuIcon( IconType.DeleteNodes,         ActionDeleteNodes,        ref enumerator );
                AddMenuIcon( IconType.Rotate,              ActionRotationNode,       ref enumerator );
                AddMenuIcon( IconType.BalanceTree,         ActionBalanceTree,        ref enumerator );
                AddMenuIcon( IconType.BalanceTreeStepMode, ActionBalanceTreeInStep,  ref enumerator );
                AddMenuIcon( IconType.StepForward,         ActionStepForward,        ref enumerator );
                AddMenuIcon( IconType.StepBackward,        ActionStepBackward,       ref enumerator );
                AddMenuIcon( IconType.LeaveStepMode,       ActionStepModeLeave,      ref enumerator );
                AddMenuIcon( IconType.DestroyTree,         ActionDestroyTree,        ref enumerator );
            }
            else 
            { 
                AddMenuIcon( IconType.CreateTree,          ActionDrawTree,          ref enumerator );
                AddMenuIcon( IconType.CreateTreeStepMode,  ActionDrawTreeInStep,    ref enumerator );
                AddMenuIcon( IconType.AddNodes,            ActionAddNodes,          ref enumerator );
                AddMenuIcon( IconType.DeleteNodes,         ActionDeleteNodes,       ref enumerator );
                AddMenuIcon( IconType.AddNodesStepMode,    ActionAddNodesInStep,    ref enumerator );
                AddMenuIcon( IconType.DeleteNodesStepMode, ActionDeleteNodesInStep, ref enumerator );
                AddMenuIcon( IconType.StepForward,         ActionStepForward,       ref enumerator );
                AddMenuIcon( IconType.StepBackward,        ActionStepBackward,      ref enumerator );
                AddMenuIcon( IconType.LeaveStepMode,       ActionStepModeLeave,     ref enumerator );
                AddMenuIcon( IconType.DestroyTree,         ActionDestroyTree,       ref enumerator );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddMenuIcon( IconType icon, 
                                  Action< object, RoutedEventArgs> action, ref List< bool >.Enumerator enumerator )
        {
            enumerator.MoveNext();
            
            Rectangle rec = new Rectangle 
            {
                Height = 50,
                Width = 50,
                Fill = Application.Current.Resources[IconProperties[icon].Item1] as DrawingBrush
            };
            
            Button button = new Button
            {
                Content = rec,
                IsEnabled = enumerator.Current,
            };
            button.Click += new RoutedEventHandler( action );
            
            ToolTip toolTip = new ToolTip
            {
                Content = enumerator.Current ? IconProperties[icon].Item2 : IconProperties[icon].Item3,
            };
            ToolTipService.SetShowOnDisabled( button, toolTip.Content != null );
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

            foreach ( ActionTree action in Note.Get().Actions )
            {
                TextNotifications.Text += "Node : " + action.Node.Key + getActionString( action ) + Environment.NewLine;
            }
            Note.Get().ClearActions();


            string getActionString( ActionTree action )
            {
                if ( action.ActionTreeType == ActionTreeType.ADD_NODE )
                {
                    return "  added";
                }
                else if ( action.ActionTreeType == ActionTreeType.CHANGE_NODE_COLOR )
                {
                    return "  color changed";
                }
                else if ( action.ActionTreeType == ActionTreeType.REMOVE_NODE )
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

        private enum IconType
        {
            CreateTree,
            CreateTreeStepMode,
            AddNodes,
            AddNodesStepMode,
            DeleteNodes, 
            DeleteNodesStepMode,
            Rotate,
            BalanceTree,
            BalanceTreeStepMode,
            StepForward, 
            StepBackward, 
            LeaveStepMode,
            DestroyTree
        }
        
        private readonly Dictionary< TreeType, String > TreeTypeLabel = new Dictionary< TreeType, string >
        { 
            [TreeType.CommonBST] = "Tree type : Common BST",
            [TreeType.AVL]       = "Tree type : AVL",
            [TreeType.RB]        = "Tree type : Red Black"
        };

        private readonly Dictionary< IconType, Tuple< string, string, string >> IconProperties 
            = new Dictionary< IconType, Tuple< string, string, string >>
        { 
            [IconType.CreateTree]          = Tuple.Create( "PathTree", "Create Tree", (string) null ),
            [IconType.CreateTreeStepMode]  = Tuple.Create( "PathTreeStep", "Create Tree in Step Mode", (string) null ),
            [IconType.AddNodes]            = Tuple.Create( "PathPlus", "Add Nodes", (string) null ),
            [IconType.AddNodesStepMode]    = Tuple.Create( "PathPlusStep", "Add Nodes in Step Mode", (string) null ),
            [IconType.DeleteNodes]         = Tuple.Create( "PathMinus", "Delete Nodes", (string) null ),
            [IconType.DeleteNodesStepMode] = Tuple.Create( "PathMinusStep", "Delete Nodes in Step Mode", (string) null ),
            [IconType.Rotate]              = Tuple.Create( "PathRotation", "Rotate Node", (string) null ),
            [IconType.BalanceTree]         = Tuple.Create( "PathBalanceTree", "Balance Tree", (string) null ),
            [IconType.BalanceTreeStepMode] = Tuple.Create( "PathBalanceTreeStep", "Balance Tree in Step Mode", (string) null ),
            [IconType.StepForward]         = Tuple.Create( "PathStepForward", "Step Forward", "Active in Step Mode" ),
            [IconType.StepBackward]        = Tuple.Create( "PathStepBackward", "Step Backward", "Active in Step Mode" ),
            [IconType.LeaveStepMode]       = Tuple.Create( "PathStepModeLeave", "Leave Step Mode", "Active in Step Mode" ),
            [IconType.DestroyTree]         = Tuple.Create( "PathDestroyTree", "Destroy Tree", (string) null ),
        };
    }
}

