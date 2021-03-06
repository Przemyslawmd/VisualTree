﻿
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
    
            Node node = tree.RestoreRoot();
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
            node = tree.RestoreRoot();

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
        
                node = tree.RestoreRoot();
                rotationCount /= 2;		
            }
    
            return node;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private int MakeSpin( Tree tree )
        {	
            int nodesCount = 1;
            Node node = tree.RestoreRoot();

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

