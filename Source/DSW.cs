
using System;

namespace VisualTree
{
    class DSW
    {
        public Node BalanceTree( Tree tree )
        {	
            Node node = tree.Root;	
            int nodesCount = MakeSpin( tree, node );	
    
            double log = Math.Log( nodesCount + 1 ) / Math.Log( 2 );
            int number =  (int) Math.Pow( 2, Math.Floor( log )) - 1;		
            int rotationCount = nodesCount - number; 
    
            node = tree.GetRoot();
            for ( int i = 0; i < rotationCount - 1; i++ )
            {			
                node = tree.RotateNode( node.Right );
                node = node.Right;		
            }	
    
            if ( rotationCount != 0 )
            {
                tree.RotateNode( node.Right );
            }
    
            rotationCount = number / 2;	
            node = tree.GetRoot();

            while ( rotationCount >= 1 )
            {		
                for ( int i = 0; i < rotationCount; i++ )
                {			
                    node = tree.RotateNode( node.Right );
                    if ( node.IsRight() )
                    {
                        node = node.Right;
                    }
                }
        
                node = tree.GetRoot();
                rotationCount /= 2;		
            }
    
            return node;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private int MakeSpin( Tree tree, Node node )
        {	
            int nodesCount = 1;

            while ( true )
            {
                if ( node.IsLeft() )
                {
                    node = tree.RotateNode( node.Left );
                    continue;
                }
                else if ( node.IsRight() )
                {
                    node = node.Right;
                    nodesCount++;
                    continue;
                }
                break;
            }

            return nodesCount;
        } 
    }
}

