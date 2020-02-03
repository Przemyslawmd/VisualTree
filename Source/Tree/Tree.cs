
using System.Collections.Generic;
using System;

namespace VisualTree
{
    abstract class Tree
    {
        public void CreateNodes( List< int > keys )
        {
            foreach( int key in keys ) 
            { 
                AddNode( key );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public bool AreKeysAllowedToAdd( List< int > keys )
        {
            foreach( int key in keys )
            {
                if ( CheckKeyExists( key, Root ))
                {
                    return false;
                }
            }
            return true;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void Traverse( Node node, Action< Node > action )
        {
            if ( node.IsLeft() )
            {
                Traverse( node.Left, action );
            }

            action.Invoke( node );
    
            if ( node.IsRight() )
            {
                Traverse( node.Right, action );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public abstract void AddNode( int key );
        
        public abstract void DelSelectedNodes( List< Node > nodes );
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AttachNode( Node node )
        {
            if ( Root is null )
            {
                Root = node;
            }
            else if ( node.Left is null && node.Right is null )
            {
                AttachNodeNoChildren( node );
            }
            else if ( node.Left is null || node.Right is null )
            {
                AttachNodeOneChild( node );
            }
            else
            {
                AttachNodeTwoChildren( node );
            }

            RestoreRoot();
            ServiceListener.Notify( ActionTreeType.ADD, node );
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AttachNodeNoChildren( Node node )
        {
            SetChildOfParentNode( node, node );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AttachNodeOneChild( Node node )
        {
            if ( node.IsLeft() )
            {
                node.Left.Parent = node;

                if ( node.IsParent() )
                {
                    node.Parent.Left = node;
                }
            }
            else 
            {
                node.Right.Parent = node;

                if ( node.IsParent() )
                {
                    node.Parent.Right = node;
                }
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        void AttachNodeTwoChildren( Node node )
        {
            Node right = node.Right;

            if ( right.Left == node.Left )
            {
                right.Parent = node;
                SetChildOfParentNode( node, node );
                right.Left = null;
                node.Left.Parent = node;
                return;
            }

            Node inserted = node.Right.Parent;
            Node lastLeft = FindLowestNode( right );

            if ( inserted.Right != null && inserted.Right.Left is null )
            {
                lastLeft.Left = inserted;
                inserted.Parent = lastLeft;
                inserted.Left = null;
                inserted.Right = null;
            }
            else
            {
                lastLeft.Parent.Left = inserted;
                inserted.Parent = lastLeft.Parent;
                inserted.Right = lastLeft;
                inserted.Left = null;
                lastLeft.Parent = inserted;
            }

            SetChildOfParentNode( node, node );
            right.Parent = node;
            node.Left.Parent = node;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DetachNode( Node node )
        {
            if ( node.Right is null && node.Left is null )
            {
                DetachNodeNoChildren( node );
            }
            else if ( node.IsRight() && node.IsLeft() )
            {
                DetachNodeTwoChildren( ref node );
            }
            else
            {
                DetachNodeOneChild( node );
            }
            
            RestoreIfRootExists();
            ServiceListener.Notify( ActionTreeType.REMOVE, node );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DetachNodeNoChildren( Node node )
        {
            if ( node == Root )
            {
                Root = null;
                return;
            }

            SetChildOfParentNode( node, null );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DetachNodeOneChild( Node node )
        {
            Node child = node.IsLeft() ? node.Left : node.Right;
            SetChildOfParentNode( node, child );
            child.Parent = node.Parent;	

            if ( node == Root )
            {
                Root = child;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DetachNodeTwoChildren( ref Node node )
        {
            Node lowestSuccessor = FindLowestNode( node.Right );
                        
            if ( lowestSuccessor == node.Right )
            {
                lowestSuccessor.Parent = node.Parent;
                SetChildOfParentNode( node, lowestSuccessor );
                lowestSuccessor.Left = node.Left;
                node.Left.Parent = lowestSuccessor;
                return;
            }	
    
            if ( lowestSuccessor.Right != null )
            {
                lowestSuccessor.Right.Parent = lowestSuccessor.Parent;
            }
           
            lowestSuccessor.Parent.Left = lowestSuccessor.Right;
            lowestSuccessor.Parent = node.Parent;
            SetChildOfParentNode( node, lowestSuccessor );

            lowestSuccessor.Right = node.Right;
            lowestSuccessor.Left = node.Left;
            node.Right.Parent = lowestSuccessor;
            node.Left.Parent = lowestSuccessor;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void RestoreIfRootExists()
        {
            if ( Root != null )
            {
                RestoreRoot();
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public Node RestoreRoot()
        {
            while ( Root.IsParent() )
            {
                Root = Root.Parent;
            }
            return Root;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public Node RotateNode( Node node )
        {
            ServiceListener.Notify( ActionTreeType.ROTATION, node );
            Node parent = node.Parent;
            node.Parent = parent.Parent;

            if ( parent.IsParent() )
            {
                SetChildOfParentNode( parent, node );
            }

            return ( parent > node ) ? RotateRight( parent, node ) : RotateLeft( parent, node );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node RotateLeft( Node parent, Node child )
        {
            parent.Parent = child;
            parent.Right = child.Left;

            if ( child.IsLeft() )
            {
                child.Left.Parent = parent;
            }

            child.Left = parent;
            return child;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private Node RotateRight( Node parent, Node child )
        {
            parent.Parent = child;
            parent.Left = child.Right;

            if ( child.IsRight() )
            {
                child.Right.Parent = parent;
            }

            child.Right = parent;
            return child;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        protected void InsertNode( Node node )
        {
            ServiceListener.Notify( ActionTreeType.ADD, node );

            if ( Root == null )
            {
                Root = node;
                Root.Parent = null;
                return;
            }

            Node parent = FindParentForNewNode( node.Key );
            InsertNewNode( parent, node );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private Node FindParentForNewNode( int key )
        {
            Node node = Root;

            while ( true )
            {
                if ( node.Key > key && node.IsLeft() )
                {
                    node = node.Left;
                }
                else if ( node.Key < key && node.IsRight() )
                {
                    node = node.Right;
                }
                else
                { 
                    return node;
                }
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        protected Node FindLowestNode( Node node )
        {
            if ( node is null )
            {
                return null;
            }
            
            while ( node.IsLeft() )
            {
                node = node.Left;
            }
            return node;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void InsertNewNode( Node parent, Node newNode )
        {
            newNode.Parent = parent;
            SetChildOfParentNode( newNode, newNode );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void SetChildOfParentNode( Node node, Node child )
        {
            Node parent = node.Parent;

            if ( parent is null )
            {
                Root = child;
                return;
            }

            if ( parent > node )
            {
                parent.Left = child;
            }
            else
            {
                parent.Right = child;
            }
        }      
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private bool CheckKeyExists( int key, Node node )
        {
            if ( key < node.Key && node.IsLeft() )
            {
                return CheckKeyExists( key, node.Left );
            }
            if ( key > node.Key && node.IsRight() )
            {
                return CheckKeyExists( key, node.Right );
            }
            if ( key == node.Key )
            {
                return true;
            }
            return false;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public Node Root { get; private set; }
    }
}

