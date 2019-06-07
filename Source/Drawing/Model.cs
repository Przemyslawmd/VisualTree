
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
            
            if ( firstHorPosition < Padding )
            {
                shiftPos = Padding - firstHorPosition; 
                TraverseTree( node, new DelegateTraverseTree( ShiftNodeHorPosition ));
            }
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
            node.PosHor += shiftPos;
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
        
        private void FixNodesPositions( )
        {		
            int matrixRow = 0;
            int matrixCol = 0;

            while ( CheckNodesCollision( ref matrixRow, ref matrixCol ))
            {		
                Node currNode = matrix[matrixRow][matrixCol];
                Node nextNode = matrix[matrixRow][matrixCol + 1];
                
                int currPos = currNode.PosHor;
                int nextPos = nextNode.PosHor;

                if ( currPos >= nextPos )
                { 
                    shiftPos = ( currPos - nextPos + diameter + 5 ) / 2;
                }
                else
                {
                    shiftPos = ( nextPos - currPos ) + 5 / 2;
                }
                
                currNode.PosHor -= shiftPos; 
                nextNode.PosHor += shiftPos;
                
                if ( currNode.IsLeft() )
                {
                    ShiftChildSubTree( currNode.Left, - shiftPos );
                }
                if ( currNode.IsRight() )
                {
                    ShiftChildSubTree( currNode.Right, - shiftPos );
                }
                if ( nextNode.IsLeft() )
                {
                    ShiftChildSubTree( nextNode.Left, shiftPos );
                }
                if ( nextNode.IsRight() )
                {
                    ShiftChildSubTree( nextNode.Right, shiftPos );
                }
                if ( currNode.Parent == nextNode.Parent )
                {
                    continue;
                }

                Node sharedParent = FindSharedParent( currNode, nextNode );
                ShiftParentSubTree( sharedParent.Left, - shiftPos, currNode );
                ShiftParentSubTree( sharedParent.Right, shiftPos, nextNode ); 
            }
        }       
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        bool CheckNodesCollision( ref int matrixRow, ref int matrixCol )
        {
            List< Node > nodesRow;
            int currNodesCol;

            for ( int currMatrixRow = matrix.Count - 1; currMatrixRow >= 0; currMatrixRow-- )
            {
                nodesRow = matrix[ currMatrixRow ];
                currNodesCol = 0;

                for ( int i = 0; i < nodesRow.Count - 1; i++, currNodesCol++ )
                {						
                    if ( nodesRow[ i ].PosHor >= nodesRow[ i + 1 ].PosHor - Padding )
                    {
                        matrixRow = currMatrixRow;
                        matrixCol = currNodesCol;
                        return true;
                    }
                }
            }

            return false;
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ShiftChildSubTree( Node node, int shift )
        {
            if ( node.IsLeft() )
            {
                ShiftChildSubTree( node.Left, shift );
            }

            node.PosHor += shift;

            if ( node.IsRight() )
            {
                ShiftChildSubTree( node.Right, shift );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ShiftParentSubTree( Node node, int shift, Node excluded )
        {
            if ( node.IsLeft() && node.Left != excluded )
            {
                ShiftParentSubTree( node.Left, shift, excluded );
            }

            node.PosHor += shift;

            if ( node.IsRight() && node.Right != excluded )
            {
                ShiftParentSubTree( node.Right, shift, excluded );
            }
        }    
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node FindSharedParent( Node left, Node right )
        {	
            while( left.Parent != right.Parent )
            {
                left = left.Parent;
                right = right.Parent;
            }

            return left.Parent;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
            
        private delegate void DelegateTraverseTree( Node node );
            
        private int firstHorPosition;
        private int lastHorPosition;
        private int diameter;
        private int shiftPos;

        private int matrixHeight;
        private List< List< Node >> matrix;

        private readonly int Padding = 30;
    }
}

