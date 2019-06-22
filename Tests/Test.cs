
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VisualTree;

namespace Tests
{
    class Test
    {
        public void CreateTree( List< int > keysToBuild, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void AddNodes( List< int > keysToBuild, List< int > keysToAdd, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            tree.CreateNodes( keysToAdd );
            CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodes( List< int > keysToBuild, List< int > keysToCheck, List< int > nodesToDelete, 
                                 TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            
            Selection selection = Selection.GetInstance();
            AddNodesToSelection( tree.Root, selection.nodes, nodesToDelete );

            tree.DelSelectedNodes( selection.nodes );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void BalanceTree( List< int > keysToBuild, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            new DSW().BalanceTree( tree );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void CheckNode( Node node, List< int > expectedKeys )
        {
            if ( node.IsLeft() )
            {
                CheckNode( node.Left, expectedKeys );
            }
            if ( node.IsRight() )
            {
                CheckNode( node.Right, expectedKeys );
            }

            Assert.AreEqual( node.Key, expectedKeys[0] );
            expectedKeys.RemoveAt( 0 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddNodesToSelection( Node node, List< Node > selectedNodes, List< int > selectedKeys )
        {
            if ( node.IsLeft() )
            {
                AddNodesToSelection( node.Left, selectedNodes, selectedKeys );
            }

            if ( selectedKeys.Contains( node.Key ))
            {
                selectedNodes.Add( node );
            }

            if ( node.IsRight() )
            {
                AddNodesToSelection( node.Right, selectedNodes, selectedKeys );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private Tree GetTree( TreeType treeType )
        {
            if ( treeType == TreeType.CommonBST )
            {
                return new TreeBST();
            }
            return new TreeAVL();
        }
    }
}

