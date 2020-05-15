
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
                // to do check double black are removed
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
                NodeDoubleBlack nodeDB = new NodeDoubleBlack( node ); 
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
                NodeDoubleBlack nodeDB = new NodeDoubleBlack( child );
                DetachNode( node );
                FixDoubleBlackNode( nodeDB );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void DeleteNodeWithBothChildren( Node node )
        {
            Node leastSuccessor = FindLowestNode ( node.Right );
            Node leastSuccessorChild = leastSuccessor.Right is null ? null : leastSuccessor.Right;

            SwapColorsRecursively( node );
            
            if ( leastSuccessor.Color == NodeColor.RED || 
               ( leastSuccessorChild != null && leastSuccessorChild.Color == NodeColor.RED ))
            {
                leastSuccessorChild.Color = NodeColor.BLACK;
                DetachNode( node );
            }
            else 
            { 
                NodeDoubleBlack nodeDB = new NodeDoubleBlack( leastSuccessor );
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
            
            Node sibling = GetSibling( nodeDB.Node );

            if ( sibling.Color == NodeColor.BLACK && HasNodeRedChild( sibling ))
            {
                if ( sibling < sibling.Parent && 
                   ( HasNodeBothChildrenRed( sibling ) || ( sibling.IsLeft() && sibling.Left.Color == NodeColor.RED )))
                {
                    // Left Left case
                    SwapColors( sibling, sibling.Parent );
                    RotateNode( sibling );
                }

                if ( sibling < sibling.Parent && 
                     sibling.IsRight() && sibling.Right.Color == NodeColor.RED )
                {
                    // Left Right Case
                    RotateNode( sibling.Right );
                    RotateNode( sibling.Right );
                }

                if ( sibling > sibling.Parent && 
                   ( HasNodeBothChildrenRed( sibling ) || ( sibling.IsRight() && sibling.Right.Color == NodeColor.RED )))
                {
                    // Right Right Case
                    SwapColors( sibling, sibling.Parent );
                    RotateNode( sibling );
                }

                if ( sibling > sibling.Parent && 
                     sibling.IsLeft() && sibling.Left.Color == NodeColor.RED )
                {
                    // Left Right Case
                    RotateNode( sibling.Left );
                    RotateNode( sibling.Left );
                }
            }
            
            if ( sibling.Color == NodeColor.BLACK && HasNodeBothChildrenBlack( sibling ))
            {
                // Recolour
                if ( sibling.Parent.Color == NodeColor.BLACK )
                {
                    sibling.Color = NodeColor.RED;
                }
            }

            if ( sibling.Color == NodeColor.RED )
            {
                SwapColors( sibling, sibling.Parent );
                RotateNode( sibling );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeLeftRedChild( Node node )
        {
            return node.IsLeft() && node.Left.Color == NodeColor.RED;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeRightRedChild( Node node )
        {
            return node.IsRight() && node.Right.Color == NodeColor.RED;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeLeftBlackChild( Node node )
        {
            return node.Left is null || node.Left.Color == NodeColor.BLACK;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeRightBlackChild( Node node )
        {
            return node.Right is null || node.Right.Color == NodeColor.BLACK;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeRedChild( Node node )
        {
            return HasNodeLeftRedChild( node ) || HasNodeRightRedChild( node );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeBothChildrenRed( Node node )
        {
            return HasNodeLeftRedChild( node ) && HasNodeRightRedChild( node );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private bool HasNodeBothChildrenBlack( Node node )
        {
            return HasNodeLeftBlackChild( node ) && HasNodeRightBlackChild( node );
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

        private void SwapColorsRecursively( Node node )
        {
            while ( node.IsLeaf() == false )
            {
                Node leastSuccessor = FindLowestNode( node.Right );
                SwapColors( node, leastSuccessor );
                node = leastSuccessor;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private class NodeDoubleBlack
        {
            public NodeDoubleBlack( Node node )
            {
                Node = node;
                Parent = node.Parent;
            }

            public Node Node { get; }

            public Node Parent { get; }
        }
    }
}

