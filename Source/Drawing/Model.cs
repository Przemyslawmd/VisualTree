
using System.Collections.Generic;

namespace VisualTree
{
    class Model
    {
        public void ModelTree( Node node )
        {
            matrixHeight = 0;
            firstHorPosition = Padding;
            lastHorPosition = 0;
            diameter = 30;
            SetNodesPosition( node, Padding, Padding, 0 );

            TraverseTree( node, new DelegateTraverseTree( CalculateMatrixHeight ));
            PrepareNodesMatrixRow();
            TraverseTree( node, new DelegateTraverseTree( RegisterNode ));
            FixNodesPositions();
            TraverseTree( node, new DelegateTraverseTree( CalculateTreeWidth ));
            
            if ( firstHorPosition < 0 )
            {
                int shift = -1 * firstHorPosition + Padding; 
                firstHorPosition += shift;
                lastHorPosition += shift;
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
        
        private void CalculateMatrixHeight( Node node )
        {
            if ( node.MatrixRow > matrixHeight )
            {
                matrixHeight = node.MatrixRow;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void CalculateTreeWidth( Node node )
        {
            if ( node.PosHor < firstHorPosition )
            {
                firstHorPosition = node.PosHor;
            }
            else if ( node.PosHor > lastHorPosition )
            {
                lastHorPosition = node.PosHor;
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
            matrix = new List< List< Node >>();
            
            for ( int i = 0; i <= matrixHeight; i++ )
            {
                matrix.Add( new List< Node >() );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void RegisterNode( Node node )
        {	
            List< Node > nodesRow = matrix[ node.MatrixRow ];
            nodesRow.Add( node );
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

            for ( int currNodesRow = matrixHeight; currNodesRow >= 0; currNodesRow-- )
            {
                nodesRow = matrix[ currNodesRow ];
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
        
        public void GetTreeCanvasSize( out int width, out int height )
        {
            height = ( matrixHeight + 1 ) * ( diameter + 10 ) + Padding;
            width = lastHorPosition + Padding;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
            
        public delegate void DelegateTraverseTree( Node node );
            
        private int firstHorPosition;
        private int lastHorPosition;
        private int diameter;
        
        private int matrixHeight;
        private List< List< Node >> matrix;

        private readonly int Padding = 30;
    }
}

