
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
            CreateAndCheckTree( keysToBuild, keysToCheck );
        }

        
        [TestMethod]
        public void TestCreateBSTTree_2()
        {
            List< int > keysToBuild = new List< int > { 13, 9, 2, 21, 32, 7, 3, 12, 8, 16 };
            List< int > keysToCheck = new List< int > { 13, 21, 32, 16, 9, 12, 2, 7, 8, 3 };
            CreateAndCheckTree( keysToBuild, keysToCheck );
        }


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
            CreateAndCheckTree( keysToBuild, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CreateAndCheckTree( List< int > keysToBuild, List< int > keysToCheck )
        {
            Tree tree = new TreeBST();
            tree.CreateNodes( keysToBuild );
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
    }
}

