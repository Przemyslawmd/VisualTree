
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
                if ( node.IsLeaf() )
                {
                    DeleteNodeLeaf( node );
                }
                else if ( node.AreBothChildren() )
                {
                    DeleteNodeWithBothChildren( node );
                }
                else
                {
                    DeleteNodeWithOneChild( node );
                }
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
        
        private void DeleteNodeLeaf( Node node )
        {
            if ( node.Color == NodeColor.RED )
            { 
                DetachNode( node );
            }
            else
            {
                NodeDoubleBlack nodeDB = new NodeDoubleBlack( node.Parent, node, null ); 
                DetachNode( node );
                FixDoubleBlackNode( nodeDB );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void DeleteNodeWithOneChild( Node node )
        {
            Node child = node.Left is null ? node.Right : node.Left;
                
            if ( node.Color == NodeColor.RED || child.Color == NodeColor.RED )
            {
                child.Color = NodeColor.BLACK;
                DetachNode( node );
            }
            else
            {
                NodeDoubleBlack nodeDB = new NodeDoubleBlack( node.Parent, node, child );
                DetachNode( node );
                FixDoubleBlackNode( nodeDB );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void DeleteNodeWithBothChildren( Node node )
        {
            Node leastSuccessor = FindLowestNode( node.Right );
            Node leastSuccessorChild = leastSuccessor.Right is null ? null : leastSuccessor.Right;
            
            if ( leastSuccessor.Color == NodeColor.RED || 
               ( leastSuccessorChild != null && leastSuccessorChild.Color == NodeColor.RED ))
            {
                if ( leastSuccessorChild != null )
                { 
                    leastSuccessorChild.Color = NodeColor.BLACK;
                }
                SwapColors( node, leastSuccessor );
                DetachNode( node );
            }
            else 
            { 
                NodeDoubleBlack nodeDB = new NodeDoubleBlack( leastSuccessor.Parent, leastSuccessor, leastSuccessorChild );
                SwapColors( node, leastSuccessor );
                DetachNode( node );
                FixDoubleBlackNode( nodeDB );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void FixDoubleBlackNode( NodeDoubleBlack nodeDB )
        {
            if ( nodeDB.Parent is null )
            {
                return;
            }
            
            Node sibling = nodeDB.Sibling;

            if ( sibling != null && sibling.Color == NodeColor.BLACK && HasRedNode( sibling ))
            {
                Node siblingRedChild = HasLeftNodeRed( sibling ) ? sibling.Left : sibling.Right;
                
                if ( sibling < sibling.Parent && 
                   ( HasBothNodesRed( sibling ) || HasLeftNodeRed( sibling )))
                {
                    // Left Left case
                    sibling.Left.Color = NodeColor.BLACK;
                    SwapColors( sibling, sibling.Parent );
                    RotateNode( sibling );
                }

                else if ( sibling < sibling.Parent && siblingRedChild > sibling )
                {
                    // Left Right Case
                    SwapColors( siblingRedChild, sibling );
                    RotateNode( siblingRedChild );
                    RotateNode( siblingRedChild );
                }

                else if (( sibling > sibling.Parent && siblingRedChild > sibling ) || 
                           HasBothNodesRed( sibling ))
                {
                    // Right Right Case
                    RotateNode( sibling );
                    SwapColors( sibling, sibling.Parent );
                }

                else if ( sibling > sibling.Parent && HasLeftNodeRed( sibling ))
                {
                    // Left Right Case
                    RotateNode( sibling.Left );
                    RotateNode( sibling.Left );
                }
            }
            
            else if ( sibling.Color == NodeColor.BLACK && HasBothNodesBlack( sibling ))
            {
                // Recolour
                if ( sibling.Parent.Color == NodeColor.BLACK )
                {
                    sibling.Color = NodeColor.RED;
                }
            }

            else if ( sibling.Color == NodeColor.RED )
            {
                SwapColors( sibling, sibling.Parent );
                RotateNode( sibling );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasLeftNodeRed( Node node )
        {
            return node.IsLeft() && node.Left.Color == NodeColor.RED;
        }

        private bool HasRightNodeRed( Node node )
        {
            return node.IsRight() && node.Right.Color == NodeColor.RED;
        }

        private bool HasRedNode( Node node )
        {
            return HasLeftNodeRed( node ) || HasRightNodeRed( node );
        }
        
        private bool HasBothNodesRed( Node node )
        {
            return HasLeftNodeRed( node ) && HasRightNodeRed( node );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasLeftNodeBlack( Node node )
        {
            return node.Left is null || node.Left.Color == NodeColor.BLACK;
        }

        private bool HasRightNodeBlack( Node node )
        {
            return node.Right is null || node.Right.Color == NodeColor.BLACK;
        }
        
        private bool HasBothNodesBlack( Node node )
        {
            return HasLeftNodeBlack( node ) && HasRightNodeBlack( node );
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

        private class NodeDoubleBlack
        {
            public NodeDoubleBlack( Node parent, Node nodeToRemove, Node child )
            {
                NodeReplace = child;
                Parent = parent;
                Sibling = nodeToRemove > parent ? parent.Left : parent.Right;
            }

            public Node NodeReplace { get; }

            public Node Parent { get; }

            public Node Sibling { get; } 
        }
    }
}

