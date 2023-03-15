
using static System.Math;

namespace VisualTree
{
    class DSW
    {
        public Node BalanceTree( Tree tree )
        {	
            int nodesCount = MakeSpin( tree );	
    
            double log = Log( nodesCount + 1 ) / Log( 2 );
            int number =  (int) Pow( 2, Floor( log )) - 1;		
            int rotationCount = nodesCount - number; 
    
            tree.RestoreRoot();
            Node node = tree.Root;
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
            tree.RestoreRoot();
            node = tree.Root;

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
        
                tree.RestoreRoot();
                node = tree.Root;
                rotationCount /= 2;		
            }
    
            return node;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private int MakeSpin( Tree tree )
        {	
            int nodesCount = 1;
            tree.RestoreRoot();
            Node node = tree.Root;

            while ( node.IsLeaf() is false )
            {
                if ( node.IsLeft() )
                {
                    node = tree.RotateNode( node.Left );
                }
                else if ( node.IsRight() )
                {
                    node = node.Right;
                    nodesCount++;
                }
            }
            return nodesCount;
        } 
    }
}

