
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VisualTree
{
    class Controller
    {
        public Result DrawTree( String text )
        {
            var keys = new Parser().GetKeys( text, out Result result );
            if ( keys is null )
            {
                return result;
            }
                        
            DestroyTree();
            GetTree().CreateNodes( keys );
            ShowTree();
            return result;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result DrawTreePrepareSteps( String text )
        {
            var keys = new Parser().GetKeys( text, out Result result );
            if ( keys is null )
            {
                return result;
            }

            DestroyTree();
            tree = GetTree();
            TreeServices.StepMode.PrepareStepsForAddNodes( tree, keys );
            return Result.OK;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DestroyTree()
        {
            if ( tree != null )
            {
                Canvas canvas = ServiceControls.GetInstance().Canvas;
                canvas.Children.Clear();
                tree = null;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public Result AddNodes( String text )
        {
            var keys = CheckRequirementsToAddNodes( text, out Result result );
            if ( keys is null )
            {
                return result;
            }

            tree.CreateNodes( keys );
            ShowTree();
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result AddNodesPrepareSteps( String text )
        {
            var keys = CheckRequirementsToAddNodes( text, out Result result );
            if ( keys is null )
            {
                return result;
            }

            TreeServices.StepMode.PrepareStepsForAddNodes( tree, keys );
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result DeleteNodes()
        {
            var selectedNodes = CheckRequirementsToDeleteNodes( out Result result );

            if ( selectedNodes is null )
            {
                return result;
            }

            tree.DelSelectedNodes( selectedNodes );
            selectedNodes.Clear();            

            if ( tree.Root is null )
            {
                DestroyTree();
            }
            else
            { 
                tree.RestoreRoot();
                ShowTree();
            }
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result DeleteNodesPrepareSteps()
        {
            var selectedNodes = CheckRequirementsToDeleteNodes( out Result result );

            if ( selectedNodes is null )
            {
                return result;
            }

            TreeServices.StepMode.PrepareStepsForDeleteNodes( tree, selectedNodes );
            return Result.OK;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public Result BalanceTree()
        {
            if ( tree is null )
            {
                return Result.NO_TREE;
            }

            new DSW().BalanceTree( tree );
            ShowTree();
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result BalanceTreePrepareSteps()
        {
            if ( tree is null )
            {
                return Result.NO_TREE;
            }

            TreeServices.StepMode.PrepareStepsForTreeBalancing( tree );
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result RotateNode()
        {
            if ( tree is null )
            {
                return Result.NO_TREE;
            }

            var selectedNodes = TreeServices.Selection.Nodes;

            if ( selectedNodes.Count is 0 )
            {
                return Result.NO_NODE_SELECTED;
            }
            if ( selectedNodes.Count > 1 )
            {
                return Result.ROTATION_MULTIPLE;
            }
            if ( selectedNodes[0].Parent is null )
            {
                return Result.ROTATION_ROOT;
            }

            tree.RotateNode( selectedNodes[0] );
            tree.RestoreRoot();
            ShowTree();
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
            
        public void StepForward()
        {
            Step( TreeServices.StepMode.StepForward );
        }
        

        public void StepBackward()
        {
            Step( TreeServices.StepMode.StepBackward );
        }


        private void Step( Action< Tree > action )
        {
            action( tree );

            if ( tree.Root is null )
            { 
                Canvas canvas = ServiceControls.GetInstance().Canvas;
                canvas.Children.Clear();
                return;
            }
            ShowTree();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void SelectNode( Canvas canvas, int posX, int posY )
        {
            if ( TreeServices.Selection.CheckCoordinates( posX, posY ))
            {
                canvas.Children.Clear();
                new Painter().DrawTree( tree.Root, canvas );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private Tree GetTree()
        {
            if ( tree is null )
            {
                if ( Settings.TreeType == TreeType.CommonBST ) 
                { 
                    tree = new TreeBST();
                }
                else if ( Settings.TreeType == TreeType.AVL )
                {
                    tree = new TreeAVL();
                }
                else if ( Settings.TreeType == TreeType.RB )
                {
                    tree = new TreeRB();
                }
            }
            return tree;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ShowTree()
        {
            Model model = TreeServices.Model;
            model.ModelTree( tree );

            Canvas canvas = ServiceControls.GetInstance().Canvas;
            PrepareCanvas( canvas, model );
            new Painter().DrawTree( tree.Root, canvas );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void PrepareCanvas( Canvas canvas, Model model )
        {
            canvas.Children.Clear();
            model.GetTreeCanvasSize( out int canvasTreeWidth, out int canvasTreeHeight );
            canvas.Height = canvasTreeHeight;
            canvas.Width = canvasTreeWidth;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private List< int > CheckRequirementsToAddNodes( String text, out Result result )
        {
            if ( tree is null )
            {
                result = Result.NO_TREE;
                return null;
            }

            var keys = new Parser().GetKeys( text, out result );
            if ( keys is null )
            {
                return null;
            }
            
            if ( tree.AreKeysAllowedToAdd( keys ) is false )
            {
                result = Result.DUPLICATED_SYMBOL;
                return null;
            }
            return keys;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private List< Node > CheckRequirementsToDeleteNodes( out Result result )
        {
            if ( tree is null )
            {
                result = Result.NO_TREE;
                return null;
            }

            var selectedNodes = TreeServices.Selection.Nodes;

            if ( selectedNodes.Count is 0 )
            {
                result = Result.NO_NODE_SELECTED;
                return null;
            }

            result = Result.OK;
            return selectedNodes;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Tree tree;
    }
}

