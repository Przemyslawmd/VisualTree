
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
            foreach ( Node node in nodes )
            {
                Node nodeToReplace = FindLowestNode( node.Right );
                if ( nodeToReplace is null )
                {
                    nodeToReplace = node.Left;
                }
                
                DoubleBlackNode doubleBlackNode = CheckDoubleBlackBeforeDelete( nodeToReplace );
                DetachNode( node );
                SwapColors( node, nodeToReplace );
                CheckTreeAfterDelete( node, nodeToReplace, doubleBlackNode );
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

        private DoubleBlackNode CheckDoubleBlackBeforeDelete( Node nodeToReplace )
        {
            if ( nodeToReplace is null )
            {
                return null;
            }
            if ( nodeToReplace.Color == NodeColor.RED || 
               ( nodeToReplace.IsRight() && nodeToReplace.Right.Color == NodeColor.RED ))
            {
                if ( nodeToReplace.IsRight() )
                { 
                    nodeToReplace.Right.Color = NodeColor.BLACK;
                }
                return null;
            }
            if ( nodeToReplace.IsRight() && nodeToReplace.Right.Color == NodeColor.BLACK )
            {
                return new DoubleBlackNode( nodeToReplace.Right, nodeToReplace );
            }
            if ( nodeToReplace.IsRight() is false )
            {
                return new DoubleBlackNode( null, nodeToReplace );
            }
            return null;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckTreeAfterDelete( Node nodeDeleted, Node nodeToReplace, DoubleBlackNode doubleBlackNode )
        {
            if ( doubleBlackNode is null )
            {
                return;
            }

            Node sibling = GetSibling( doubleBlackNode.Node );

            if ( sibling.Color == NodeColor.BLACK )
            { 
                if ( sibling.IsRight() && sibling.Right.Color == NodeColor.RED )
                {
                    Node node = RotateNode( sibling.Right );
                    RotateNode( node );
                    return;
                }
                if ( sibling.IsLeft() && sibling.Left.Color == NodeColor.RED )
                {
                    Node node = RotateNode( sibling.Left );
                    RotateNode( node );
                    return;
                }
                if (( sibling.IsRight() is false || sibling.Right.Color == NodeColor.BLACK ) &&
                      sibling.IsLeft() is false || sibling.Left.Color == NodeColor.BLACK )
                {
                      sibling.Parent.Color = NodeColor.RED;  
                }
            }
            else
            {
                RotateNode( sibling );
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

        /*******************************************************************************************/
        /*******************************************************************************************/

        private class DoubleBlackNode
        {
            public DoubleBlackNode( Node node, Node parent )
            {
                Node = node;
                Parent = parent;
            }

            public Node Node { get; }

            public Node Parent {  get; }
        }
    }
}

