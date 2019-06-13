
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VisualTree
{
    class Controller
    {
        public Message.Code DrawTree( String text, Canvas canvas )
        {
            Message.Code code = Message.Code.OK;
            List< int > keys = new Parser().GetNodesValues( text, ref code );
            if ( keys is null )
            {
                return code;
            }

            tree = GetTree();
            tree.CreateNodes( keys );
            
            Model model = Model.GetInstance();
            model.ModelTree( tree.Root );

            PrepareCanvas( canvas, model );
            new Painter().DrawTree( tree.Root, canvas );
            return code;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DestroyTree( Canvas canvas )
        {
            if ( tree != null )
            {
                canvas.Children.Clear();
                tree = null;;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public Message.Code AddNodes( String text, Canvas canvas )
        {
            if ( tree is null )
            {
                return Message.Code.NO_TREE;
            }

            Message.Code code = Message.Code.OK;
            List< int > keys = new Parser().GetNodesValues( text, ref code );
            if ( keys is null )
            {
                return code;
            }
            
            if ( tree.AreKeysAllowedToAdd( keys ) is false )
            {
                return Message.Code.DUPLICATED_SYMBOL;
            }

            tree.CreateNodes( keys );
            
            Model model = Model.GetInstance();
            model.ModelTree( tree.Root );

            PrepareCanvas( canvas, model );
            new Painter().DrawTree( tree.Root, canvas );
            return Message.Code.OK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Message.Code RotateNode( Canvas canvas )
        {
            if ( tree is null )
            {
                return Message.Code.NO_TREE;
            }

            Selection selection = Selection.GetInstance();
            List< Node > selectedNodes = selection.nodes;

            if ( selectedNodes.Count is 0 )
            {
                return Message.Code.NONE_NODE_SELECTED;
            }

            if ( selectedNodes.Count > 1 )
            {
                return Message.Code.ROTATION_MULTIPLE;
            }

            Node node = selectedNodes[ 0 ];

            if ( node.Parent is null )
            {
                return Message.Code.ROTATION_ROOT;
            }

            tree.RotateNode( node );
            tree.FixRoot();
            Model model = Model.GetInstance();
            model.ModelTree( tree.Root );

            PrepareCanvas( canvas, model );
            new Painter().DrawTree( tree.Root, canvas );
            return Message.Code.OK;
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
                tree = new TreeBST();
            }
            return tree;
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

