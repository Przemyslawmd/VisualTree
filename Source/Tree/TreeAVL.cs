
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

            NodeAVL child = (NodeAVL) node;
            node = (NodeAVL) node.Parent;

            while ( node != null )
            {
                node.IncLevelAVL( child );

                if ( node.VerifyAVL() is false )
                {
                    FixAVLTree( node );
                    return;
                }

                child = node;
                node = (NodeAVL) node.Parent; 
            }
        }            
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void FixAVLTree( Node node )
        {
            if ( node.Left is null )
            {
                Node child = node.Right;
        
                if ( child.IsLeft() )
                {
                    RotateTwice( node, child, TwiceRotation.RIGHT_LEFT );	
                }
                else
                {
                    RotateNodeAndUpdateLevel( node, child );
                }
            }
            else if ( node.Right is null )
            {
                Node child = node.Left;
                
                if ( child.IsRight() )
                {
                    RotateTwice( node, child, TwiceRotation.LEFT_RIGHT );	
                }
                else
                {
                    RotateNodeAndUpdateLevel( node, child );
                }
            }
            else if ( node.Left.Level > node.Right.Level )
            {
                Node child = node.Left;

                if ( child.Right is null || ( child.IsLeft() && child.Left.Level > child.Right.Level ))
                {
                    RotateNodeAndUpdateLevel( node, child );
                }
                else if ( child.Left is null || ( child.IsRight() && child.Left.Level < child.Right.Level ))
                {
                    RotateTwice( node, child, TwiceRotation.LEFT_RIGHT );
                }
                else
                {
                    RotateNodeAndUpdateLevel( node, child );
                }
            }
            else if ( node.Left.Level < node.Right.Level )
            {
                Node child = node.Right;

                if ( child.Left is null || ( child.IsRight() && child.Left.Level < child.Right.Level ))
                {
                    RotateNodeAndUpdateLevel( node, child );
                }
                else if ( child.Right is null || ( child.IsLeft() && child.Left.Level > child.Right.Level ))
                {
                    RotateTwice( node, child, TwiceRotation.RIGHT_LEFT );
                }
                else
                {
                    RotateNodeAndUpdateLevel( node, child );
                }
            }

            GetRoot();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void RotateNodeAndUpdateLevel( Node parent, Node child )
        {
            RotateNode( child );
            UpdateLevelToRoot( parent );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void UpdateLevelToRoot( Node node )
        {
            ((NodeAVL) node).UpdateLevel();

            while ( node.IsParent() )
            {
                ((NodeAVL) node.Parent).IncLevelAVL( (NodeAVL) node );
                node = node.Parent;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void RotateTwice( Node parent, Node child, TwiceRotation twiceRotation )
        {
            if ( twiceRotation is TwiceRotation.RIGHT_LEFT )
            {
                child = RotateNode( child.Left );	
                UpdateLevelToRoot( child.Right );		
            }
            else if ( twiceRotation is TwiceRotation.LEFT_RIGHT )
            {
                child = RotateNode( child.Right );
                UpdateLevelToRoot( child.Left );
            }
    
            RotateNode( child );
            UpdateLevelToRoot( parent );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private enum TwiceRotation { RIGHT_LEFT, LEFT_RIGHT };
    }
}

