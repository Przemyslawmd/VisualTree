
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SuiteDeleteNodes
    {
        // Nodes to be deleted have no children
        
        [TestMethod]
        public void TestDeleteNodesBST_1()
        {
            List< int > keysToBuild = new List< int > { 6, 2, 5, 10, 1, 13, 4, 9 };
            List< int > keysToCheck = new List< int > { 1, 5, 2, 13, 10, 6 };
            List< int > keysToDelete = new List< int > { 4, 9 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
                
        // Node to be deleted has one child
        
        [TestMethod]
        public void TestDeleteNodesBST_2()
        {
            List< int > keysToBuild = new List< int > { 10, 5, 15, 3, 12, 1, 16, 2, 11 };
            List< int > keysToCheck = new List< int > { 2, 1, 3, 5, 11, 16, 15, 10 };
            List< int > keysToDelete = new List< int > { 12 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        // Nodes to be deleted have both children
        
        [TestMethod]
        public void TestDeleteNodesBST_3()
        {
            List< int > keysToBuild = new List< int > { 10, 5, 6, 12, 11, 8, 2, 13, 9, 7 };
            List< int > keysToCheck = new List< int > { 2, 7, 9, 6, 5, 11, 13, 10 };
            List< int > keysToDelete = new List< int > { 12, 8 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        // Nodes to be deleted have both children and one node is root
        
        [TestMethod]
        public void TestDeleteNodesBST_4()
        {
            List< int > keysToBuild = new List< int > { 16, 8, 2, 11, 20, 25, 18, 1, 13, 3, 22, 21, 24, 10, 9, 17, 19 };
            List< int > keysToCheck = new List< int > { 1, 3, 2, 9, 10, 13, 11, 8, 19, 18, 24, 22, 25, 21, 17 };
            List< int > keysToDelete = new List< int > { 20, 16 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.CommonBST );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        // Nodes to be deleted have no children
        
        [TestMethod]
        public void TestDeleteNodesAVL_1()
        {
            List< int > keysToBuild = new List< int > { 6, 2, 5, 10, 1, 13, 4, 9 };
            List< int > keysToCheck = new List< int > { 1, 2, 6, 13, 10, 5 };
            List< int > keysToDelete = new List< int > { 4, 9 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        // Node to be deleted has one child
        
        [TestMethod]
        public void TestDeleteNodesAVL_2()
        {
            List< int > keysToBuild = new List< int > { 10, 5, 15, 3, 12, 1, 16, 2, 11 };
            List< int > keysToCheck = new List< int > { 2, 1, 5, 3, 11, 16, 15, 10 };
            List< int > keysToDelete = new List< int > { 12 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        // Nodes to be deleted have two children
        
        [TestMethod]
        public void TestDeleteNodesAVL_3()
        {
            List< int > keysToBuild = new List< int > { 10, 5, 6, 12, 11, 8, 2, 13, 9, 7 };
            List< int > keysToCheck = new List< int > { 2, 5, 7, 9, 6, 11, 13, 10 };
            List< int > keysToDelete = new List< int > { 12, 8 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        // Nodes to be deleted have both children and one node is root
        
        [TestMethod]
        public void TestDeleteNodesAVL_4()
        {
            List< int > keysToBuild = new List< int > { 16, 8, 2, 11, 20, 25, 18, 1, 13, 3, 22, 21, 24, 10, 9, 17, 19 };
            List< int > keysToCheck = new List< int > { 1, 3, 2, 9, 10, 13, 11, 8, 18, 21, 19, 24, 25, 22, 17 };
            List< int > keysToDelete = new List< int > { 20, 16 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesAVLInStepMode_1()
        {
            List< int > keysToBuild = new List< int > { 1, 5, 10, 13, 17, 20, 34, 56, 78, 98, 100, 9, 79 };
            List< int > keysToCheck1 = new List< int > { 1, 9, 10, 5, 17, 34, 20, 79, 100, 98, 78, 13 };
            List< int > keysToCheck2 = new List< int > { 1, 9, 10, 5, 17, 34, 20, 79, 78, 100, 98, 56, 13 };
            List< int > keysToDelete = new List< int > { 56 };
            new Test().DeleteNodesInStepMode( keysToBuild, keysToDelete, keysToCheck1, keysToCheck2, TreeType.AVL );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        // Create AVL, next delete root, undone action and check tree
        
        [TestMethod]
        public void TestDeleteNodesAVLInStepMode_2()
        {
            List< int > keysToBuild = new List< int > { 1, 2, 3, 4 };
            List< int > keysToCheck1 = new List< int > { 1, 4, 3 };
            List< int > keysToCheck2 = new List< int > { 1, 4, 3, 2 };
            List< int > keysToDelete = new List< int > { 2 };
            new Test().DeleteNodesInStepMode( keysToBuild, keysToDelete, keysToCheck1, keysToCheck2, TreeType.AVL, 2, 2 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        // Create AVL, delete all nodes, undone few actions and check tree
        
        [TestMethod]
        public void TestDeleteNodesAVLInStepMode_3()
        {
            List< int > keysToBuild = new List< int > { 1, 2, 3, 4, 5 };
            List< int > keysToCheck1 = null;
            List< int > keysToCheck2 = new List< int > { 1, 3, 5, 4, 2 };
            List< int > keysToDelete = new List< int > { 3, 4, 5, 2, 1 };
            new Test().DeleteNodesInStepMode( keysToBuild, keysToDelete, keysToCheck1, keysToCheck2, TreeType.AVL, 10, 10 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_1()
        {
            var keysToBuild = new List< int > { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0 };
            var keysToDelete = new List< int > { 8 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [0]  = NodeColor.RED,
                 [1]  = NodeColor.BLACK,
                 [3]  = NodeColor.BLACK,
                 [2]  = NodeColor.BLACK,
                 [5]  = NodeColor.BLACK,
                 [7]  = NodeColor.BLACK,
                 [10] = NodeColor.BLACK,
                 [9]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [4]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild,  nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_2()
        {
            var keysToBuild = new List< int > { 3, 6, 19, 21, 36, 45, 2 };
            var keysToDelete = new List< int > { 3 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [2]  = NodeColor.BLACK,
                 [19] = NodeColor.BLACK,
                 [45] = NodeColor.RED,
                 [36] = NodeColor.BLACK,
                 [21] = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild,  nodesToCheck, keysToDelete );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestCleanup]
        public void CleanUp()
        {
            Selection.Destroy();
            StepMode.Destroy();
        }
    }
}

