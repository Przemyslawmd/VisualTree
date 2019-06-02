
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

            Tree tree = new TreeBST();
            tree.CreateNodes( nodes );
            Model model = new Model();
            model.ModelTree( tree.Root );

            Painter painter = new Painter();
            painter.DrawTree( tree.Root, canvas );
        }
    }
}

