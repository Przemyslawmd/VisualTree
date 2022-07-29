
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SuiteDeleteNodes
    {
        [TestInitialize]
        public void Setup()
        {
            TreeServices.Start();
        }

        [TestCleanup]
        public void CleanUp()
        {
            TreeServices.Stop();            
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

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

        // U is black with one red child
        [TestMethod]
        public void TestDeleteNodesRB_01()
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

        // U is red with both black children 
        [TestMethod]
        public void TestDeleteNodesRB_02()
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

        // U is black with both red children
        [TestMethod]
        public void TestDeleteNodesRB_03()
        {
            var keysToBuild = new List< int > { 6, 4, 2, 1, 10, 8 };
            var keysToDelete = new List< int > { 8 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.RED,
                 [2]  = NodeColor.BLACK,
                 [6]  = NodeColor.RED,
                 [10] = NodeColor.BLACK,
                 [4]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild,  nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod] 
        public void TestDeleteNodesRB_04()
        {
            var keysToBuild = new List< int > { 12, 2, 3, 11, 9, 21, 17, 23, 5 };
            var keysToDelete = new List< int > { 3 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [2]  = NodeColor.BLACK,
                 [9]  = NodeColor.BLACK,
                 [5]  = NodeColor.RED,
                 [12] = NodeColor.BLACK,
                 [23] = NodeColor.RED,
                 [21] = NodeColor.BLACK,
                 [17] = NodeColor.RED,
                 [11] = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_05()
        {
            var keysToBuild = new List< int > { 2, 8, 1, 3, 14, 6, 30, 18, 4, 9, 7, 11, 10 };
            var keysToDelete = new List< int > { 18 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.BLACK,
                 [3]  = NodeColor.BLACK,
                 [7]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [4]  = NodeColor.RED,
                 [2]  = NodeColor.BLACK,
                 [10] = NodeColor.RED,
                 [9]  = NodeColor.BLACK,
                 [14] = NodeColor.BLACK,
                 [30] = NodeColor.RED,
                 [11] = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        // U and V are black
        // Sibling is red and at least one of its child is red
        // Sibling is right child and its red child is right child
        [TestMethod]
        public void TestDeleteNodesRB_06()
        {
            var keysToBuild = new List< int > { 2, 8, 1, 3, 14, 6, 30, 18, 4, 9, 7, 11, 10 };
            var keysToDelete = new List< int > { 2 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.BLACK,
                 [4]  = NodeColor.RED,
                 [7]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [3]  = NodeColor.BLACK,
                 [10] = NodeColor.RED,
                 [9]  = NodeColor.BLACK,
                 [14] = NodeColor.BLACK,
                 [11] = NodeColor.RED,
                 [30] = NodeColor.BLACK,
                 [18] = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_07()
        {
            var keysToBuild = new List< int > { 2, 8, 1, 3, 14, 6, 30, 18, 4, 9, 7, 11, 10 };
            var keysToDelete = new List< int > { 1 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [3]  = NodeColor.BLACK,
                 [2]  = NodeColor.RED,
                 [7]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [4]  = NodeColor.BLACK,
                 [10] = NodeColor.RED,
                 [9]  = NodeColor.BLACK,
                 [14] = NodeColor.BLACK,
                 [11] = NodeColor.RED,
                 [30] = NodeColor.BLACK,
                 [18] = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestDeleteNodesRB_08()
        {
            var keysToBuild = new List< int > { 2, 8, 1, 3, 14, 6, 30, 18, 4, 9, 7, 11, 10 };
            var keysToDelete = new List< int > { 3 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.BLACK,
                 [4]  = NodeColor.RED,
                 [7]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [2]  = NodeColor.BLACK,
                 [10] = NodeColor.RED,
                 [9]  = NodeColor.BLACK,
                 [14] = NodeColor.BLACK,
                 [11] = NodeColor.RED,
                 [30] = NodeColor.BLACK,
                 [18] = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        // U and V are black
        // Sibling is a left black child and has a right red child
        [TestMethod]
        public void TestDeleteNodesRB_09()
        {
            var keysToBuild = new List< int > { 2, 8, 1, 3, 14, 6, 30, 18, 4, 9, 7, 11, 10 };
            var keysToDelete = new List< int > { 11 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.BLACK,
                 [3]  = NodeColor.BLACK,
                 [7]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [4]  = NodeColor.RED,
                 [2]  = NodeColor.BLACK,
                 [9]  = NodeColor.RED,
                 [14] = NodeColor.RED,
                 [10] = NodeColor.BLACK,
                 [30] = NodeColor.BLACK,
                 [18] = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        // U and V are black
        // Sibling is a left black child and has a right red child 
        [TestMethod]
        public void TestDeleteNodesRB_10()
        {
            var keysToBuild = new List< int > { 2, 8, 1, 3, 14, 6, 30, 18, 4, 9, 7, 11, 10 };
            var keysToDelete = new List< int > { 14 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.BLACK,
                 [3]  = NodeColor.BLACK,
                 [7]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [4]  = NodeColor.RED,
                 [2]  = NodeColor.BLACK,
                 [9]  = NodeColor.RED,
                 [11] = NodeColor.RED,
                 [10] = NodeColor.BLACK,
                 [30] = NodeColor.BLACK,
                 [18] = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_11()
        {
            var keysToBuild = new List< int > { 4, 5, 12, 101, 1, 23, 13, 67, 89, 8, 32, 41, 98, 25, 3 };
            var keysToDelete = new List< int > { 67 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]   = NodeColor.RED,
                 [4]   = NodeColor.RED,
                 [3]   = NodeColor.BLACK,
                 [8]   = NodeColor.RED,
                 [13]  = NodeColor.RED,  
                 [12]  = NodeColor.BLACK,
                 [5]   = NodeColor.BLACK,
                 [25]  = NodeColor.BLACK,
                 [41]  = NodeColor.BLACK,
                 [32]  = NodeColor.RED,
                 [98]  = NodeColor.RED,
                 [101] = NodeColor.BLACK,
                 [89]  = NodeColor.BLACK,
                 [23]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_12()
        {
            var keysToBuild = new List< int > { 4, 5, 12, 101, 1, 23, 13, 67, 89, 8, 32, 41, 98, 25, 3 };
            var keysToDelete = new List< int > { 41, 67 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]   = NodeColor.RED,
                 [4]   = NodeColor.RED,
                 [3]   = NodeColor.BLACK,
                 [8]   = NodeColor.RED,
                 [13]  = NodeColor.RED,  
                 [12]  = NodeColor.BLACK,
                 [5]   = NodeColor.BLACK,
                 [25]  = NodeColor.BLACK,
                 [32]  = NodeColor.RED,
                 [98]  = NodeColor.RED,
                 [101] = NodeColor.BLACK,
                 [89]  = NodeColor.BLACK,
                 [23]  = NodeColor.BLACK,
            };
            
            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestDeleteNodesRB_13()
        {
            var keysToBuild = new List< int > { 4, 5, 12, 101, 1, 23, 13, 67, 89, 8, 32, 41, 98, 25, 3 };
            var keysToDelete = new List< int > { 41, 67, 23 };
            var nodesToCheck = new Dictionary< int, NodeColor >
            {
                 [1]   = NodeColor.RED,
                 [4]   = NodeColor.RED,
                 [3]   = NodeColor.BLACK,
                 [8]   = NodeColor.RED,
                 [13]  = NodeColor.RED,
                 [12]  = NodeColor.BLACK,
                 [5]   = NodeColor.BLACK,
                 [32]  = NodeColor.RED,
                 [98]  = NodeColor.RED,
                 [101] = NodeColor.BLACK,
                 [89]  = NodeColor.BLACK,
                 [25]  = NodeColor.BLACK,
            };

            new Test().DeleteNodesTreeRB( keysToBuild, nodesToCheck, keysToDelete );
        } 
    }
}

