
using System.Collections.Generic;

namespace VisualTree
{
    class TreeBST : Tree
    {
        override public void AddNode( int key )
        {
            Node node = new Node( key );
            InsertNode( node );
        } 
        
        override public void DelSelectedNodes( List< Node > nodes )
        {
            foreach( Node node in nodes )
            {
                DetachNode( node );
            }
        }
    }
}

