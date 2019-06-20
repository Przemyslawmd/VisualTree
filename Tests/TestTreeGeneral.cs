
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TestTreeGeneral
    {
        [TestMethod]
        public void TestCreateBSTTree_1()
        {
            List< int > keysToBuild = new List< int > { 3, 2, 1, 6 };
            List< int > keysToCheck = new List< int > { 3, 6, 2, 1 };
            CreateAndCheckTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateBSTTree_2()
        {
            List< int > keysToBuild = new List< int > { 13, 9, 2, 21, 32, 7, 3, 12, 8, 16 };
            List< int > keysToCheck = new List< int > { 13, 21, 32, 16, 9, 12, 2, 7, 8, 3 };
            CreateAndCheckTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateBSTTree_3()
        {
            List< int > keysToBuild = new List< int > 
            { 
                21, 12, 9, 32, 8, 36, 33, 17, 23, 34, 4, 19, 25, 6, 37, 3, 38, 27, 29, 24 
            };
            List< int > keysToCheck = new List< int > 
            {  
                21, 32, 36, 37, 38, 33, 34, 23, 25, 27, 29, 24, 12, 17, 19, 9, 8, 4, 6, 3 
            };
            CreateAndCheckTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateAVLTree_1()
        {
            List< int > keysToBuild = new List< int > {  1, 6, 3, 2, 10 };
            List< int > keysToCheck = new List< int > {  3, 6, 10, 1, 2 };
            CreateAndCheckTree( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateAVLTree_2()
        {
            List< int > keysToBuild = new List< int > {  13, 9, 2, 21, 32, 7,  3,  12, 8, 16 };
            List< int > keysToCheck = new List< int > {  9, 21, 32, 13, 16, 12, 3, 7, 8, 2 };
            CreateAndCheckTree( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateAVLTree_3()
        {
            List< int > keysToBuild = new List< int > 
            { 
                21, 12, 9, 32, 8, 36, 33, 17, 23, 34, 4, 19, 25, 6, 37, 3, 38, 27, 29, 24
            };
            List< int > keysToCheck = new List< int > 
            {  
                21, 32, 34, 37, 38, 36, 33, 25, 27, 29, 23, 24, 12, 17, 19, 8, 9, 4, 6, 3
            };
            CreateAndCheckTree( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestBalanceTree_1()
        {
            List< int > keysToBuild = new List< int > { 20, 15, 30, 25, 40, 23, 28 };
            List< int > keysToCheck = new List< int > { 25, 30, 40, 28, 20, 23, 15 };
            CreateBalanceAndCheckTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestBalanceTree_2()
        {
            List< int > keysToBuild = new List< int > 
            { 
                28, 20, 45, 12, 23, 17, 34, 65, 56, 2, 3, 15, 22, 30, 25, 40, 31, 43, 24, 76, 29, 1, 
                100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110
            };
            List< int > keysToCheck = new List< int > 
            { 
                43, 103, 107, 109, 110, 108, 105, 106, 104, 76, 101, 102, 100, 56, 65, 45, 24, 30, 34, 
                40, 31, 28, 29, 25, 17, 22, 23, 20, 12, 15, 2, 3, 1
            };
            CreateBalanceAndCheckTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CreateAndCheckTree( List< int > keysToBuild, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            Stack< int > expected = new Stack< int >( keysToCheck ); 
            CheckNode( tree.Root, expected );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CreateBalanceAndCheckTree( List< int > keysToBuild, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            new DSW().BalanceTree( tree );
            Stack< int > expected = new Stack< int >( keysToCheck ); 
            CheckNode( tree.Root, expected );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckNode( Node node, Stack< int > expectedNodes )
        {
            if ( node.IsLeft() )
            {
                CheckNode( node.Left, expectedNodes );
            }
            if ( node.IsRight() )
            {
                CheckNode( node.Right, expectedNodes );
            }

            Assert.AreEqual( node.Key, expectedNodes.Pop() );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private Tree GetTree( TreeType treeType )
        {
            if ( treeType is TreeType.CommonBST )
            {
                return new TreeBST();
            }
            return new TreeAVL();
        }
    }
}

