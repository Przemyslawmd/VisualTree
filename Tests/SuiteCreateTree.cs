
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SuiteCreateTree
    {
        [TestMethod]
        public void TestCreateTreeBST_1()
        {
            List< int > keysToBuild = new List< int > { 3, 2, 1, 6 };
            List< int > keysToCheck = new List< int > { 1, 2, 6, 3 };
            new Test().CreateTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateTreeBST_2()
        {
            List< int > keysToBuild = new List< int > { 13, 9, 2, 21, 32, 7, 3, 12, 8, 16 };
            List< int > keysToCheck = new List< int > { 3, 8, 7, 2, 12, 9, 16, 32, 21, 13 };
            new Test().CreateTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeBST_3()
        {
            List< int > keysToBuild = new List< int > 
            { 
                21, 12, 9, 32, 8, 36, 33, 17, 23, 34, 4, 19, 25, 6, 37, 3, 38, 27, 29, 24 
            };
            List< int > keysToCheck = new List< int > 
            {  
                3, 6, 4, 8, 9, 19, 17, 12, 24, 29, 27, 25, 23, 34, 33, 38, 37, 36, 32, 21
            };
            new Test().CreateTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeAVL_1()
        {
            List< int > keysToBuild = new List< int > {  1, 6, 3, 2, 10 };
            List< int > keysToCheck = new List< int > {  2, 1, 10, 6, 3 };
            new Test().CreateTree( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeAVL_2()
        {
            var keysToBuild = new List< int > {  13, 9, 2, 21, 32, 7,  3,  12, 8, 16 };
            var keysToCheck = new List< int > {  2, 8, 7, 3, 12, 16, 13, 32, 21, 9 };
            new Test().CreateTree( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeAVL_3()
        {
            var keysToBuild = new List< int > 
            { 
                21, 12, 9, 32, 8, 36, 33, 17, 23, 34, 4, 19, 25, 6, 37, 3, 38, 27, 29, 24
            };
            var keysToCheck = new List< int > 
            {  
                3, 6, 4, 9, 8, 19, 17, 12, 24, 23, 29, 27, 25, 33, 36, 38, 37, 34, 32, 21
            };
            new Test().CreateTree( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeRB_1()
        {
            var keysToBuild = new List< int > {  1, 2, 3, 4, 5 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                [1] = NodeColor.BLACK,
                [3] = NodeColor.RED,
                [5] = NodeColor.RED,
                [4] = NodeColor.BLACK,
                [2] = NodeColor.BLACK
            };
            new Test().CreateTreeRB( keysToBuild, nodesToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateTreeRB_2()
        {
            var keysToBuild = new List< int > {  6, 5, 4, 3, 2, 1 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                [1] = NodeColor.RED,
                [2] = NodeColor.BLACK,
                [4] = NodeColor.BLACK,
                [3] = NodeColor.RED,
                [6] = NodeColor.BLACK,
                [5] = NodeColor.BLACK
            };
            new Test().CreateTreeRB( keysToBuild, nodesToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeRB_3()
        {
            var keysToBuild = new List< int > {  1, 6, 7, 10, 12, 3, 5 };
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1] = NodeColor.RED,
                 [5] = NodeColor.RED,
                 [3] = NodeColor.BLACK,
                 [7] = NodeColor.RED,
                [12] = NodeColor.RED,
                [10] = NodeColor.BLACK,
                 [6] = NodeColor.BLACK
            };
            new Test().CreateTreeRB( keysToBuild, nodesToCheck );
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateAVLTreeInStepMode_1()
        {
            List< int > keysToBuild = new List< int > {  1, 6, 3, 2, 10 };
            List< int > keysToCheck = new List< int > {  2, 1, 10, 6, 3 };
            new Test().CreateTreeInStepMode( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateAVLTreeInStepMode_2()
        {
            List< int > keysToBuild = new List< int > { 10, 12, 16, 10, 8, 3, 1, 2 };
            List< int > keysToCheck = new List< int > { 10, 16, 12 };
            new Test().CreateTreeInStepModeBackAndPartial( keysToBuild, keysToCheck, 4, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        //  Create AVL in step mode, next undo all actions till all nodes are deleted, 
        //  next make restore few actions.

        [TestMethod]
        public void TestCreateAVLTreeInStepMode_3()
        {
            List< int > keysToBuild = new List< int > { 1, 2, 3, 4, 5, 6, 7 };
            List< int > keysToCheck = new List< int > { 1, 4, 3, 2 };
            new Test().CreateTreeInStepModeBackAndPartial( keysToBuild, keysToCheck, 5, TreeType.AVL, 0, 20 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestCleanup]
        public void CleanUp()
        {
            StepMode.Destroy();
        }
    }
}

