
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VisualTree
{
    class Controller
    {
        public Result DrawTree( String text )
        {
            List< int > keys = new Parser().GetNodesValues( text, out Result result );
            if ( keys is null )
            {
                return result;
            }
                        
            DestroyTree();
            tree = GetTree();
            tree.CreateNodes( keys );
            ShowTree();
            return result;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DestroyTree()
        {
            if ( tree != null )
            {
                Canvas canvas = ServiceControls.GetInstance().Canvas;
                canvas.Children.Clear();
                Model.DestroyInstance();
                Selection.DestroyInstance();
                tree = null;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public Result AddNodes( String text )
        {
            if ( tree is null )
            {
                return Result.NO_TREE;
            }

            List< int > keys = new Parser().GetNodesValues( text, out Result result );
            if ( keys is null )
            {
                return result;
            }
            
            if ( tree.AreKeysAllowedToAdd( keys ) is false )
            {
                return Result.DUPLICATED_SYMBOL;
            }

            tree.CreateNodes( keys );
            ShowTree();
            return Result.OK;
        }
        
        public Result AddNodesPrepareSteps( String text )
        {
            if ( tree is null )
            {
                return Result.NO_TREE;
            }

            List< int > keys = new Parser().GetNodesValues( text, out Result result );
            if ( keys is null )
            {
                return result;
            }
            
            if ( tree.AreKeysAllowedToAdd( keys ) is false )
            {
                return Result.DUPLICATED_SYMBOL;
            }

            StepMode.GetInstance().PrepareStepsForAddNodes( tree, keys );
            return Result.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Result DeleteNodes()
        {
            if ( tree is null )
            {
                return Result.NO_TREE;
            }

            List< Node > selectedNodes = Selection.GetInstance().nodes;

            if ( selectedNodes.Count is 0 )
            {
                return Result.NO_NODE_SELECTED;
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

            StepMode.GetInstance().PrepareStepsForTreeBalancing( tree );
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

            List< Node > selectedNodes = Selection.GetInstance().nodes;

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
            StepMode.GetInstance().StepForward();
            ShowTree();
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void StepBackward()
        {
            StepMode.GetInstance().StepBackward();
            ShowTree();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void SelectNode( Canvas canvas, int posX, int posY )
        {
            Selection selection = Selection.GetInstance();
            
            if ( selection.CheckCoordinates( posX, posY ))
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
                if ( Settings.TreeType is TreeType.CommonBST )
                { 
                    tree = new TreeBST();
                }
                else 
                {
                    tree = new TreeAVL();
                }
            }
            return tree;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ShowTree()
        {
            Model model = Model.GetInstance();
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

        private Tree tree;
    }
}

