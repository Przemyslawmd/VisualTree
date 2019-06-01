
using System.Collections.Generic;

namespace VisualTree
{
    class Model
    {
        public void ModelTree( Node node )
        {
            nodesCountCol = 0;
            nodesCountRow = 0;
            firstHorPosition = 0;
            diameter = 30;
            SetNodesPosition( node, 20, 20, 0 );

            TraverseTree( node, new DelegateTraverseTree( CalculateNodesRows ));
            PrepareNodesMatrixRow();
            TraverseTree( node, new DelegateTraverseTree( RegisterNode ));
            FixNodesPositions();
            TraverseTree( node, new DelegateTraverseTree( CalculateFirstHorPosition ));
            
            if ( firstHorPosition < 0 )
            {
                firstHorPosition *= -1;
                firstHorPosition += 20;
                TraverseTree( node, new DelegateTraverseTree( ShiftNodeHorPosition ));
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void SetNodesPosition( Node node, int posHor, int posVer, int matrixRow )
        {
            if ( node.IsLeft() )
            {
                SetNodesPosition( node.Left, posHor - diameter, posVer + diameter + 10, matrixRow + 1 );
            }
            
            node.PosHor = posHor;
            node.PosVer = posVer;
            node.MatrixRow = matrixRow;

            if ( node.IsRight() )
            {
                SetNodesPosition( node.Right, posHor + diameter, posVer + diameter + 10, matrixRow + 1 );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void TraverseTree( Node node, DelegateTraverseTree callback )
        {
            if ( node.IsLeft() )
            {
                TraverseTree( node.Left, callback );
            }

            callback.Invoke( node );
    
            if ( node.IsRight() )
            {
                TraverseTree( node.Right, callback );
            }
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void CalculateNodesRows( Node node )
        {
            if ( node.MatrixRow > nodesCountRow )
            {
                nodesCountRow = node.MatrixRow;
            }
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CalculateNodesCountInCol( Node node )
        {
            if ( node.MatrixCol > nodesCountCol )
            {
                nodesCountCol = node.MatrixCol;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void CalculateFirstHorPosition( Node node )
        {
            if ( node.PosHor < firstHorPosition )
            {
                firstHorPosition = node.PosHor;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ShiftNodeHorPosition( Node node )
        {	
            node.PosHor += firstHorPosition;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void PrepareNodesMatrixRow()
        {
            nodesMatrix = new List< List< Node >>();
            
            for ( int i = 0; i <= nodesCountRow; i++ )
            {
                nodesMatrix.Add( new List< Node >() );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void RegisterNode( Node node )
        {	
            List< Node > nodesRow = nodesMatrix[ node.MatrixRow ];
            nodesRow.Add( node );
            node.MatrixCol = nodesRow.Count - 1;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        void FixNodesPositions( )
        {		
            int nodesRowNum = 0;
            int nodesColNum = 0;

            while ( CheckNodesCollision( ref nodesRowNum, ref nodesColNum ))
            {		
                // TODO
            }
        }       
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        bool CheckNodesCollision( ref int nodesRowNumber, ref int nodesColNumber )
        {
            List< Node > nodesRow;
            int currNodesCol;

            for ( int currNodesRow = nodesCountRow; currNodesRow >= 0; currNodesRow-- )
            {
                nodesRow = nodesMatrix[ currNodesRow ];
                currNodesCol = 0;

                for ( int i = 0; i < nodesRow.Count - 1; i++ )
                {						
                    if ( nodesRow[ i ].PosHor >= nodesRow[ i + 1 ].PosHor - 30 )
                    {
                        nodesRowNumber = currNodesRow;
                        nodesColNumber = currNodesCol;
                        return true;
                    }
                }
            }

            return false;
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public delegate void DelegateTraverseTree( Node node );
            
        private int firstHorPosition;
        private int diameter;
        
        private int nodesCountCol;
        private int nodesCountRow;
        private List< List< Node >> nodesMatrix;
    }
}

