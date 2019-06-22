
using System.Collections.Generic;

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

        public void Traverse( Node node, DelegateTraverse callback )
        {
            if ( node.IsLeft() )
            {
                Traverse( node.Left, callback );
            }

            callback.Invoke( node );
    
            if ( node.IsRight() )
            {
                Traverse( node.Right, callback );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public abstract void AddNode( int key );
        
        public abstract void DelSelectedNodes( List< Node > nodes );
        
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
                DetachNodeTwoChildren( node );
            }
            else
            {
                DetachNodeOneChild( node );
            }
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
            Node parent = node.Parent;

            if ( parent is null )
            {
                Root = child;
            }
            else if ( parent > child )
            {
                parent.Left = child;
            }
            else
            {
                parent.Right = child;
            }
    
            child.Parent = parent;	
            
            if ( node == Root )
            {
                Root = child;
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DetachNodeTwoChildren( Node node )
        {
            Node minNode = FindMinInSubTree( node.Right );
            
            if ( minNode == node.Right )
            {
                minNode.Parent = node.Parent;
            
                if ( node.Parent is null )
                {
                    Root = minNode;
                }
                else
                {
                    SetChildOfParentNode( node, minNode );
                }
                
                minNode.Left = node.Left;
                node.Left.Parent = minNode;	
                return;
            }	
    
            if ( minNode.Right is null )
            {
                minNode.Parent.Left = null;
                minNode.Parent = node.Parent;
            }
            else
            {
                minNode.Parent.Left = minNode.Right;
                minNode.Right.Parent = minNode.Parent;
                minNode.Parent = node.Parent;
            }

            if ( node.Parent is null )
            {
                Root = minNode;
            }
            else
            {
                SetChildOfParentNode( node, minNode );
            }

            minNode.Right = node.Right;
            node.Right.Parent = minNode;
            minNode.Left = node.Left;
            node.Left.Parent = minNode;
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
        
        private Node FindMinInSubTree( Node node )
        {
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

        public delegate void DelegateTraverse( Node node );
    }
}

