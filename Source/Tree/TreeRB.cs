﻿
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
                Node parent = node.Parent;
                Node lowestGreater = FindLowestNode( node.Right );
                DetachNode( node );
                CheckTreeAfterDelete( parent, node, lowestGreater );
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

        public void CheckTreeAfterDelete( Node parent, Node nodeDeleted, Node lowestGreater )
        {
            Node nodeReplace = parent > nodeDeleted ? parent.Left : parent.Right;
            
            if ( nodeDeleted.Color == NodeColor.RED )
            {
                if ( nodeReplace != null )
                { 
                    nodeReplace.Color = NodeColor.BLACK;
                }
                return;
            }
        }

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
    }
}

