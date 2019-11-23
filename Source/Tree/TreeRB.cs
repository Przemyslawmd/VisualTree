
using System.Collections.Generic;

namespace VisualTree
{
    class TreeRB : Tree
    {
        override public void AddNode( int key )
        {
            NodeRB node = new NodeRB( key );
            InsertNode( node );

            if ( node.IsParent() is false )
            {
                node.Color = NodeColor.BLACK;
                return;
            }

            CheckTreeAfterCreate( node );
            RestoreRoot();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        override public void DelSelectedNodes( List< Node > nodes )
        {
            Node doubleBlack;

            foreach ( Node node in nodes )
            {
                Node nodeToReplaceDeleted = null;
                if ( node.Right != null )
                {
                    nodeToReplaceDeleted = FindLowestNode( node.Right );
                }
 
                doubleBlack = CheckDoubleBlackBeforeDelete( node, nodeToReplaceDeleted );
                DetachNode( node );
                SwapColors( node, nodeToReplaceDeleted );
                CheckTreeAfterDelete( node, nodeToReplaceDeleted, doubleBlack );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CheckTreeAfterCreate( Node node )
        {
            while ( true )
            {
                if ( node.Color == NodeColor.BLACK || node.Parent is null || node.Parent.Color == NodeColor.BLACK )
                {
                    return;
                }
                
                Node parent = node.Parent;
                Node uncle = GetUncle( node );

                if ( uncle != null && uncle.Color == NodeColor.RED )
                {
                    node = FixTreeByChangingColor( parent, uncle );
                }
                else
                {
                    node = FixTreeByRotation( node );
                }
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public Node CheckDoubleBlackBeforeDelete( Node nodeDeleted, Node nodeToReplaceDeleted )
        {
            if ( nodeToReplaceDeleted.IsRight() && nodeToReplaceDeleted.Right.Color == NodeColor.RED )
            {
                nodeToReplaceDeleted.Right.Color = NodeColor.BLACK;
                return null;
            }
            else if ( nodeToReplaceDeleted.IsRight() && nodeToReplaceDeleted.Right.Color == NodeColor.BLACK )
            {
                return nodeToReplaceDeleted.Right;
            }
            return null;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CheckTreeAfterDelete( Node nodeDeleted, Node nodeToReplace, Node doubleBlack )
        {
            if ( doubleBlack is null )
            {
                return;
            }

            Node sibling = GetSibling( doubleBlack );

            if ( sibling.Color == NodeColor.BLACK && 
                 ( sibling.IsRight() && sibling.Right.Color == NodeColor.RED || 
                   sibling.IsLeft() && sibling.Left.Color == NodeColor.RED ))
            {

            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node GetUncle( Node node )
        {
            Node parent = node.Parent;
            Node grand = parent.Parent;

            if ( grand is null )
            {
                return null;
            }

            return grand > parent ? grand.Right : grand.Left;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node GetSibling( Node node )
        {
            Node parent = node.Parent;
            return node > parent ? parent.Left : parent.Right;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node FixTreeByChangingColor( Node siblingA, Node siblingB )
        {
            siblingA.Color = NodeColor.BLACK;
            siblingB.Color = NodeColor.BLACK;
            siblingA.Parent.Color = siblingA.Parent.IsParent() ? NodeColor.RED : NodeColor.BLACK;
            return siblingA.Parent;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node FixTreeByRotation( Node node )
        {
            Node parent = node.Parent;
            Node grand = parent.Parent;

            if (( grand > parent && parent > node ) || ( grand < parent && parent < node ))
            {
                parent.Color = NodeColor.BLACK;
                grand.Color = NodeColor.RED;
                return RotateNode( parent );
            }
            else 
            {
                node = RotateNode( node );
                node.Color = NodeColor.BLACK;
                node.Parent.Color = NodeColor.RED;
                return RotateNode( node );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void SwapColors( Node nodeA, Node nodeB )
        {
            if ( nodeA is null || nodeB is null )
            {
                return;
            }

            NodeColor temp = nodeA.Color;
            nodeA.Color = nodeB.Color;
            nodeB.Color = temp;
        }
    }
}

