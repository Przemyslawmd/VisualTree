
using System.Collections.Generic;

namespace VisualTree
{
    class Selection
    {
        public static Selection Get()
        {
            if ( selection is null )
            {
                selection = new Selection();
            }
            return selection;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public static void Destroy()
        {
            selection = null;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public bool CheckCoordinates( int posX, int posY)
        {
            bool isFound = false;
            List< List< Node >> matrix = Model.Get().Matrix;

            var matrixRow = matrix[ 0 ];
            foreach ( List< Node > nodesRow in matrix )
            {
                int nodePos = nodesRow[ 0 ].PosVer;
                if (( nodePos + Radius ) >= posY && ( nodePos - Radius ) <= posY )
                {
                    isFound = true;
                    matrixRow = nodesRow;
                    break;
                }
            }

            if ( isFound == false ) 
            {
                return false;
            }
    
            foreach ( Node node in matrixRow )
            {
                if (( node.PosHor + Radius ) >= posX && ( node.PosHor - Radius ) <= posX )
                {
                    node.IsSelected = !node.IsSelected;
                    
                    if ( node.IsSelected is false )
                    {
                        nodes.Remove( node );
                    }
                    else
                    {
                        nodes.Add( node );
                    }
                    return true;
                }
            }

            return false;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private Selection()
        {
            nodes = new List< Node >();
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private static Selection selection;
        
        public List< Node > nodes { get; private set; }
        
        private readonly int Radius = 15;
    }
}

