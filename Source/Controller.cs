
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VisualTree
{
    class Controller
    {
        public void DrawTree( String text, Canvas canvas, ref Message.Code code )
        {
            Parser parser = new Parser();
            List< int > nodes = parser.GetNodesValues( text, ref code );

            tree = GetTree();
            tree.CreateNodes( nodes );
            
            Model model = Model.GetInstance();
            model.ModelTree( tree.Root );

            PrepareCanvasSize( canvas, model );
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
        
        private void PrepareCanvasSize( Canvas canvas, Model model )
        {
            model.GetTreeCanvasSize( out int canvasTreeWidth, out int canvasTreeHeight );
            canvas.Height = canvasTreeHeight;
            canvas.Width = canvasTreeWidth;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Tree tree;
    }
}

