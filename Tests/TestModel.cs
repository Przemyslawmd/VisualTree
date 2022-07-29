
using VisualTree;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestModel
    {
        [TestInitialize]
        public void Setup()
        {
            TreeServices.Start();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TreeServices.Stop();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestModelBSTTree_1()
        {
            List< int > keys = new List< int > { 1, 2, 3 };
            Tree tree = new TreeBST();
            tree.CreateNodes( keys );   
            
            TreeServices.Model.ModelTree( tree );
            
            Stack< NodePosition > expectedPositions = new Stack< NodePosition >(); 
            expectedPositions.Push( new NodePosition( 1, 30, 30 ));
            expectedPositions.Push( new NodePosition( 2, 60, 70 ));
            expectedPositions.Push( new NodePosition( 3, 90, 110 ));
            
            CheckNode( tree.Root, expectedPositions );            
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestModelBSTTree_2()
        {
            List< int > keys = new List< int > { 4, 3, 10, 1 };
            Tree tree = new TreeBST();
            tree.CreateNodes( keys );   
            
            TreeServices.Model.ModelTree( tree );
            
            Stack< NodePosition > expectedPositions = new Stack< NodePosition >(); 
            expectedPositions.Push( new NodePosition( 4,   90, 30 ));
            expectedPositions.Push( new NodePosition( 10, 120, 70 ));
            expectedPositions.Push( new NodePosition( 3,   60, 70 ));
            expectedPositions.Push( new NodePosition( 1,   30, 110 ));
            
            CheckNode( tree.Root, expectedPositions );            
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void CheckNode( Node node, Stack< NodePosition > expectedPositions )
        {
            if ( node.IsLeft() )
            {
                CheckNode( node.Left, expectedPositions );
            }
            if ( node.IsRight() )
            {
                CheckNode( node.Right, expectedPositions );
            }

            NodePosition nodePosition = expectedPositions.Pop();
            Assert.AreEqual( node.Key, nodePosition.key );
            Assert.AreEqual( node.PosHor, nodePosition.posHor );
            Assert.AreEqual( node.PosVer, nodePosition.posVer );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private struct NodePosition
        { 
            public NodePosition( int key, int posHor, int posVer )
            {
                this.key = key;
                this.posHor = posHor;
                this.posVer = posVer;
            }
            
            public int key;
            public int posHor;
            public int posVer;
        }
    }
}

