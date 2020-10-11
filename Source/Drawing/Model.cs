
using System.Collections.Generic;

namespace VisualTree
{
    class Model
    {
        public static Model Get()
        {
            if ( model is null )
            {
                model = new Model();
            }
            return model;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public static void Destroy()
        {
            model = null;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
            
        public void ModelTree( Tree tree )
        {
            SetNodesPosition( tree.Root, Padding, Padding, 0 );
            SetMatrixHeight( tree );
            PrepareNodesMatrixRow();
            tree.Traverse( tree.Root, RegisterNode );
            FixNodesPositions( tree );
            tree.Traverse( tree.Root, CalculateTreeWidth );
            
            if ( beginPosHor < Padding )
            {
                int shift = Padding - beginPosHor; 
                tree.Traverse( tree.Root, ShiftNodeHorPosition );
                beginPosHor += shift;
                endPosHor += shift;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void GetTreeCanvasSize( out int width, out int height )
        {
            height = ( matrixHeight + 1 ) * ( Diameter + 10 ) + Padding;
            width = endPosHor + Padding;
        }

        /*******************************************************************************************/
        /* PRIVATE                                                                                 */
        /*******************************************************************************************/
        
        private Model() { }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void SetNodesPosition( Node node, int posHor, int posVer, int matrixRow )
        {
            if ( node.IsLeft() )
            {
                SetNodesPosition( node.Left, posHor - Diameter, posVer + Diameter + 10, matrixRow + 1 );
            }
            
            node.PosHor = posHor;
            node.PosVer = posVer;
            node.MatrixRow = matrixRow;

            if ( node.IsRight() )
            {
                SetNodesPosition( node.Right, posHor + Diameter, posVer + Diameter + 10, matrixRow + 1 );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void SetMatrixHeight( Tree tree )
        {
            matrixHeight = 0;
            tree.Traverse( tree.Root, CalculateMatrixHeight );
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
            if ( node.PosHor < beginPosHor )
            {
                beginPosHor = node.PosHor;
            }
            else if ( node.PosHor > endPosHor )
            {
                endPosHor = node.PosHor;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void ShiftNodeHorPosition( Node node )
        {	
            node.PosHor += ( Padding - beginPosHor );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void PrepareNodesMatrixRow()
        {
            Matrix = new List< List< Node >>();
            
            for ( int i = 0; i <= matrixHeight; i++ )
            {
                Matrix.Add( new List< Node >() );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void RegisterNode( Node node )
        {	
            Matrix[ node.MatrixRow ].Add( node );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void FixNodesPositions( Tree tree )
        {		
            while ( true )
            {		
                var ( IsCollision, Row, Column ) = CheckNodesCollision();

                if ( IsCollision is false )
                {
                    return;
                }
                
                Node currNode = Matrix[Row][Column];
                Node nextNode = Matrix[Row][Column + 1];
                
                int currPos = currNode.PosHor;
                int nextPos = nextNode.PosHor;

                int shift = currPos >= nextPos ? ( currPos - nextPos + Diameter + 5 ) / 2 : 
                                                 ( nextPos - currPos ) + 5 / 2;
                currNode.PosHor -= shift; 
                nextNode.PosHor += shift;
                
                if ( currNode.IsLeft() )
                {
                    ShiftChildSubTree( currNode.Left, -shift );
                }
                if ( currNode.IsRight() )
                {
                    ShiftChildSubTree( currNode.Right, -shift );
                }
                if ( nextNode.IsLeft() )
                {
                    ShiftChildSubTree( nextNode.Left, shift );
                }
                if ( nextNode.IsRight() )
                {
                    ShiftChildSubTree( nextNode.Right, shift );
                }
                if ( currNode.Parent == nextNode.Parent )
                {
                    continue;
                }

                Node sharedParent = tree.FindSharedParent( currNode, nextNode );
                ShiftParentSubTree( sharedParent.Left, -shift, currNode );
                ShiftParentSubTree( sharedParent.Right, shift, nextNode ); 
            }
        }       
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private ( bool IsCollision, int Row, int Column ) CheckNodesCollision()
        {
            for ( int row = Matrix.Count - 1; row >= 0; row-- )
            {
                for ( int col = 0; col < Matrix[row].Count - 1; col++ )
                {						
                    if ( Matrix[row][col].PosHor >= Matrix[row][col + 1].PosHor - Padding )
                    {
                        return ( true, row, col );
                    }
                }
            }
            return ( false, 0, 0 );
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
            
        private static Model model;
            
        private int beginPosHor = Settings.Diameter;
        private int endPosHor;
        private int matrixHeight;
        
        private readonly int Diameter = Settings.Diameter;
        private readonly int Padding = Settings.Diameter;

        public List< List< Node >> Matrix { get; private set; }
    }
}

