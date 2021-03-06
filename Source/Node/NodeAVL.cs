﻿
namespace VisualTree
{
    class NodeAVL : Node
    {
        public NodeAVL( int key ) : base( key ) { }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public bool CheckAVLProperty()
        {
            int leftChildLevel = Left ?. Level ?? 0;
            int rightChildLevel = Right ?. Level ?? 0;

            return System.Math.Abs( leftChildLevel - rightChildLevel ) <= 1;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void IncLevelAVL( NodeAVL child )
        {
            if ( child is null )
            {
                Level = 1;
            }
            else if (( child.Level + 1 ) > Level )
            {
                Level = child.Level + 1;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void UpdateLevel()
        {
            int leftChildLevel = Left ?. Level ?? 0;
            int rightChildLevel = Right ?. Level ?? 0;

            Level = ( leftChildLevel > rightChildLevel ) ? leftChildLevel + 1 : rightChildLevel + 1;
        }
    }
}

