
using System.Collections.Generic;

namespace VisualTree
{
    class TreeRB : Tree
    {
        override public void AddNode( int key )
        {
            NodeRB node = new NodeRB( key );
            InsertNode( node );

            if ( node.IsParent() is false )
            {
                node.Color = NodeColor.BLACK;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        override public void DelSelectedNodes( List< Node > nodes )
        {
            foreach ( Node node in nodes )
            {
                DetachNode( node );
            }
        }
    }
}

