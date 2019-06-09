
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VisualTree
{
    class Controller
    {
        public void DrawTree( String text, Canvas canvas, ref Message.Code code )
        {
            List< int > keys = new Parser().GetNodesValues( text, ref code );
            if ( keys is null )
            {
                return;
            }

            tree = GetTree();
            tree.CreateNodes( keys );
            
            Model model = Model.GetInstance();
            model.ModelTree( tree.Root );

            PrepareCanvas( canvas, model );
            new Painter().DrawTree( tree.Root, canvas );
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
        
        public void AddNodes( String text, Canvas canvas, ref Message.Code code )
        {
            if ( tree is null )
            {
                code = Message.Code.NO_TREE;
                return;
            }

            List< int > keys = new Parser().GetNodesValues( text, ref code );
            if ( keys is null )
            {
                return;
            }
            
            if ( tree.AreKeysAllowedToAdd( keys ) is false )
            {
                code = Message.Code.DUPLICATED_SYMBOL;
                return;
            }

            tree.CreateNodes( keys );
            
            Model model = Model.GetInstance();
            model.ModelTree( tree.Root );

            PrepareCanvas( canvas, model );
            new Painter().DrawTree( tree.Root, canvas );
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

