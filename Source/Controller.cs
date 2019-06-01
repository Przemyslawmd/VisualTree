
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualTree
{
    class Controller
    {
        public void DrawTree( String text, ref Message.Code code )
        {
            Parser parser = new Parser();
            List< int > nodes = parser.GetNodesValues( text, ref code );

            Tree tree = new TreeBST();
            tree.CreateNodes( nodes );
            Model model = new Model();
            model.ModelTree( tree.Root );
        }
    }
}
