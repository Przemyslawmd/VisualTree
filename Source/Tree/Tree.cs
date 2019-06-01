
using System.Collections.Generic;

namespace VisualTree
{
    abstract class Tree
    {
        public Tree()
        {
            Root = null;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void CreateNodes( List< int > nodes )
        {
            foreach( int key in nodes ) 
            { 
                AddNode( key );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public abstract void AddNode( int key );
        
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
        
        Node FindParentForNewNode( int key )
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

        void InsertNewNode( Node parent, Node newNode )
        {
            newNode.Parent = parent;
            SetChildOfParentNode( newNode, newNode );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        void SetChildOfParentNode( Node node, Node child )
        {
            Node parent = node.Parent;

            if ( parent.Key > node.Key )
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
        
        public Node Root { get; private set; }
    }
}

