
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
            InitializeStartingValues();

            Node root = tree.Root;
            SetNodesPosition( root, Padding, Padding, 0 );
            tree.Traverse( root, CalculateMatrixHeight );
            PrepareNodesMatrixRow();
            tree.Traverse( root, RegisterNode );
            FixNodesPositions();
            tree.Traverse( root, CalculateTreeWidth );
            
            if ( beginPosHor < Padding )
            {
                shiftPos = Padding - beginPosHor; 
                tree.Traverse( root, ShiftNodeHorPosition );
                beginPosHor += shiftPos;
                endPosHor += shiftPos;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void GetTreeCanvasSize( out int width, out int height )
        {
            height = ( matrixHeight + 1 ) * ( diameter + 10 ) + Padding;
            width = endPosHor + Padding;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private Model() { }
            
        private void InitializeStartingValues()
        {
            matrixHeight = 0;
            beginPosHor = Padding;
            endPosHor = 0;
            diameter = Settings.Diameter;
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
            node.PosHor += shiftPos;
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
        
        private void FixNodesPositions( )
        {		
            int row = 0;
            int col = 0;

            while ( CheckNodesCollision( ref row, ref col ))
            {		
                Node currNode = Matrix[row][col];
                Node nextNode = Matrix[row][col + 1];
                
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
        
        bool CheckNodesCollision( ref int rowWithCollision, ref int colWithCollision )
        {
            for ( int row = Matrix.Count - 1; row >= 0; row-- )
            {
                for ( int col = 0; col < Matrix[row].Count - 1; col++ )
                {						
                    if ( Matrix[row][col].PosHor >= Matrix[row][col + 1].PosHor - Padding )
                    {
                        rowWithCollision = row;
                        colWithCollision = col;
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
            
        private static Model model;
            
        private int beginPosHor;
        private int endPosHor;
        private int diameter;
        private int shiftPos;
        private readonly int Padding = Settings.Diameter;
        private int matrixHeight;
        
        public List< List< Node >> Matrix { get; private set; }
    }
}

