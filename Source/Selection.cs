
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
            bool isFoundInRows = false;
            var matrix = Model.Get().Matrix;
            var matrixRow = matrix[0];
            
            foreach ( var nodesRow in matrix )
            {
                int nodePos = nodesRow[0].PosVer;
                if (( nodePos + Radius ) >= posY && ( nodePos - Radius ) <= posY )
                {
                    isFoundInRows = true;
                    matrixRow = nodesRow;
                    break;
                }
            }

            if ( isFoundInRows is false ) 
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
                        Nodes.Remove( node );
                    }
                    else
                    {
                        Nodes.Add( node );
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
            Nodes = new List< Node >();
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private static Selection selection;
        
        public List< Node > Nodes { get; private set; }
        
        private readonly int Radius = Settings.Diameter / 2;
    }
}

