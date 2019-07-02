
using System.Collections.Generic;

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

        override public void DelSelectedNodes( List< Node > nodes )
        {
            foreach( Node node in nodes )
            {
                DetachNode( node );

                if ( Root != null )
                {
                    Traverse( Root, ( Node n ) => ( n as NodeAVL ).UpdateLevel() );
                    Traverse( Root, RestoreAVLProperty );
                }
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckAVLPropertyAfterInsertNode( NodeAVL node )
        {
            if ( node.IsParent() is false )
            {
                return;
            }

            NodeAVL child = node;
            node = ( NodeAVL ) node.Parent;

            while ( node != null )
            {
                node.IncLevelAVL( child );
                RestoreAVLProperty( node );
                child = node;
                node = ( NodeAVL ) node.Parent; 
            }
        }            
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void FixAVLTree( Node node )
        {
            void action( Node a, Node b ) { RotateNode( a ); UpdateLevelToRoot( b ); }

            if ( node.Left is null )
            {
                Node child = node.Right;
        
                if ( child.IsLeft() )
                {
                    RotateTwice( node, child, TwiceRotation.RIGHT_LEFT );	
                }
                else
                {
                    action( child, node );
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
                    action( child, node );
                }
            }
            else if ( node.Left.Level > node.Right.Level )
            {
                Node child = node.Left;

                if ( child.Left is null || ( child.IsRight() && child.Left.Level < child.Right.Level ))
                {
                    RotateTwice( node, child, TwiceRotation.LEFT_RIGHT );
                }
                else
                {
                    action( child, node );
                }
            }
            else if ( node.Left.Level < node.Right.Level )
            {
                Node child = node.Right;

                if ( child.Right is null || ( child.IsLeft() && child.Left.Level > child.Right.Level ))
                {
                    RotateTwice( node, child, TwiceRotation.RIGHT_LEFT );
                }
                else
                {
                    action( child, node );
                }
            }

            RestoreRoot();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void RestoreAVLProperty( Node node )
        {
            if (( node as NodeAVL ).CheckAVLProperty() is false )
            {
                FixAVLTree( node );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void UpdateLevelToRoot( Node node )
        {
            ( node as NodeAVL ).UpdateLevel();

            while ( node.IsParent() )
            {
                (( NodeAVL ) node.Parent).IncLevelAVL(( NodeAVL ) node );
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

