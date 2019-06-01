
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TestTreeGeneral
    {
        [TestMethod]
        public void TestCreateCommonTree()
        {
            List< int > keys = new List< int > { 3, 2, 1, 6 };
            Tree tree = new TreeBST();
            tree.CreateNodes( keys );

            Stack< int > expectedNodes = new Stack< int >(); 
            expectedNodes.Push( 3 );
            expectedNodes.Push( 6 );
            expectedNodes.Push( 2 );
            expectedNodes.Push( 1 );

            CheckNode( tree.Root, expectedNodes );
            
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

