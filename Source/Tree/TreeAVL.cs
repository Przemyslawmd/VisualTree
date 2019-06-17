
namespace VisualTree
{
    class TreeAVL : Tree
    {
        override public void AddNode( int key )
        {
            NodeAVL node = new NodeAVL( key );
            InsertNode( node );
            CheckAVLPropertyAfterInsertNode( node );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckAVLPropertyAfterInsertNode( NodeAVL node )
        {
            if ( node.IsParent() is false )
            {
                return;
            }

            NodeAVL child = node;
            node = node.Parent;
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void FixAVLTree( NodeAVL node )
        {
            return;
        }

    }
}

