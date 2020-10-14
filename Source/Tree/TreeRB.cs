
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
                SetNodeColor( node, NodeColor.BLACK );
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
                SetNodeColor( child, NodeColor.BLACK );
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
                    SetNodeColor( leastSuccessorChild, NodeColor.BLACK );
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
                    SetNodeColor( sibling.Left, NodeColor.BLACK );
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
            else if ( sibling is null || 
                    ( sibling.Color == NodeColor.BLACK && HasBothNodesBlack( sibling )))
            {
                 // Recolour
                 if ( nodeDB.Parent.Color == NodeColor.BLACK && sibling != null )
                 {
                    SetNodeColor( sibling, NodeColor.RED );
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
            SetNodeColor( siblingA, NodeColor.BLACK );
            SetNodeColor( siblingB, NodeColor.BLACK );
            
            if ( siblingA.Parent.IsParent() )
            {
                SetNodeColor( siblingA.Parent, NodeColor.RED );
            }
            else
            {
                SetNodeColor( siblingA.Parent, NodeColor.BLACK );
            }
            
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
                SetNodeColor( parent, NodeColor.BLACK );
                SetNodeColor( grand, NodeColor.RED );
                return RotateNode( parent );
            }
            else 
            {
                node = RotateNode( node );
                SetNodeColor( node, NodeColor.BLACK );
                SetNodeColor( node.Parent, NodeColor.RED );
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
            SetNodeColor( nodeA, nodeB.Color );
            SetNodeColor( nodeB, temp );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void SetNodeColor( Node node, NodeColor color )
        {
            if ( node.Color == color )
            {
                return;
            }
            
            ServiceListener.Notify( ActionTreeType.CHANGE_NODE_COLOR, node );
            node.Color = color;
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

