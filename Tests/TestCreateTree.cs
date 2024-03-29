﻿
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TestCreateTree
    {
        [TestInitialize]
        public void Setup()
        {
            TreeServices.StartStepMode();
            utils = new Utils();
        }

        [TestCleanup]
        public void CleanUp()
        {
            TreeServices.StopStepMode();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeBST_1()
        {
            Tree tree = utils.CreateTree( new List< int >{ 3, 2, 1, 6 }, TreeType.CommonBST );
            utils.CheckNodes( tree.Root, new List< int >{ 1, 2, 6, 3 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateTreeBST_2()
        {
            Tree tree = utils.CreateTree( new List< int >{ 13, 9, 2, 21, 32, 7, 3, 12, 8, 16 }, TreeType.CommonBST );
            utils.CheckNodes( tree.Root, new List< int >{ 3, 8, 7, 2, 12, 9, 16, 32, 21, 13 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeBST_3()
        {
            Tree tree = utils.CreateTree( new List< int >{ 21, 12, 9, 32, 8, 36, 33, 17, 23, 34, 4, 19, 25, 6, 37, 3, 38, 27, 29, 24 }, TreeType.CommonBST );
            utils.CheckNodes( tree.Root, new List< int >{ 3, 6, 4, 8, 9, 19, 17, 12, 24, 29, 27, 25, 23, 34, 33, 38, 37, 36, 32, 21 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeAVL_1()
        {
            Tree tree = utils.CreateTree( new List< int >{  1, 6, 3, 2, 10 }, TreeType.AVL );
            utils.CheckNodes( tree.Root, new List< int >{  2, 1, 10, 6, 3 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeAVL_2()
        {
            Tree tree = utils.CreateTree( new List< int >{  13, 9, 2, 21, 32, 7,  3,  12, 8, 16 }, TreeType.AVL );
            utils.CheckNodes( tree.Root, new List< int >{ 2, 8, 7, 3, 12, 16, 13, 32, 21, 9 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeAVL_3()
        {
            Tree tree = utils.CreateTree( new List< int >{ 21, 12, 9, 32, 8, 36, 33, 17, 23, 34, 4, 19, 25, 6, 37, 3, 38, 27, 29, 24 }, TreeType.AVL );
            utils.CheckNodes( tree.Root, new List< int >{ 3, 6, 4, 9, 8, 19, 17, 12, 24, 23, 29, 27, 25, 33, 36, 38, 37, 34, 32, 21 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeRB_1()
        {
            Tree tree = utils.CreateTree( new List< int >{ 1, 2, 3, 4, 5 }, TreeType.RB );
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                [1] = NodeColor.BLACK,
                [3] = NodeColor.RED,
                [5] = NodeColor.RED,
                [4] = NodeColor.BLACK,
                [2] = NodeColor.BLACK
            };
            utils.CheckNodes( tree.Root, new List< int >( nodesToCheck.Keys ));
            utils.CheckNodes( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateTreeRB_2()
        {
            Tree tree = utils.CreateTree( new List< int >{  6, 5, 4, 3, 2, 1 }, TreeType.RB );
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                [1] = NodeColor.RED,
                [2] = NodeColor.BLACK,
                [4] = NodeColor.BLACK,
                [3] = NodeColor.RED,
                [6] = NodeColor.BLACK,
                [5] = NodeColor.BLACK
            };
            utils.CheckNodes( tree.Root, new List< int >( nodesToCheck.Keys ));
            utils.CheckNodes( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeRB_3()
        {
            Tree tree = utils.CreateTree( new List< int >{  1, 6, 7, 10, 12, 3, 5 }, TreeType.RB );
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.RED,
                 [5]  = NodeColor.RED,
                 [3]  = NodeColor.BLACK,
                 [7]  = NodeColor.RED,
                 [12] = NodeColor.RED,
                 [10] = NodeColor.BLACK,
                 [6]  = NodeColor.BLACK
            };
            utils.CheckNodes( tree.Root, new List< int >( nodesToCheck.Keys ));
            utils.CheckNodes( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateTreeRB_4()
        {
            Tree tree = utils.CreateTree( new List< int >{  12, 6, 9, 10, 1, 16, 15, 11, 2 }, TreeType.RB );
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.RED,
                 [6]  = NodeColor.RED,
                 [2]  = NodeColor.BLACK,
                 [11] = NodeColor.RED,
                 [10] = NodeColor.BLACK,
                 [15] = NodeColor.RED,
                 [16] = NodeColor.BLACK,
                 [12] = NodeColor.RED,
                 [9]  = NodeColor.BLACK
            };
            utils.CheckNodes( tree.Root, new List< int >( nodesToCheck.Keys ));
            utils.CheckNodes( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        [TestMethod]
        public void TestCreateTreeRB_5()
        {
            Tree tree = utils.CreateTree( new List< int >{  12, 6, 8, 9, 30, 10, 18, 2, 27, 1, 22, 23, 24, 4, 17 }, TreeType.RB );
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [1]  = NodeColor.BLACK,
                 [4]  = NodeColor.RED,
                 [6]  = NodeColor.BLACK,
                 [2]  = NodeColor.RED,
                 [10] = NodeColor.RED,
                 [9]  = NodeColor.BLACK,
                 [8]  = NodeColor.BLACK,
                 [17] = NodeColor.RED,
                 [18] = NodeColor.BLACK,
                 [24] = NodeColor.RED,
                 [23] = NodeColor.BLACK,
                 [22] = NodeColor.RED,
                 [30] = NodeColor.BLACK,
                 [27] = NodeColor.BLACK,
                 [12] = NodeColor.BLACK
            };
            utils.CheckNodes( tree.Root, new List< int >( nodesToCheck.Keys ));
            utils.CheckNodes( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateTreeRB_6()
        {
            Tree tree = utils.CreateTree( new List< int >{ 90, 80, 3, 7, 26, 57, 62, 16, 4, 10, 63, 41, 40, 8, 9, 32, 21, 5, 85, 87 }, TreeType.RB );
            var nodesToCheck = new Dictionary< int, NodeColor > 
            {  
                 [3]  = NodeColor.RED,
                 [5]  = NodeColor.RED,
                 [4]  = NodeColor.BLACK,
                 [8]  = NodeColor.RED,
                 [10] = NodeColor.RED,
                 [9]  = NodeColor.BLACK,
                 [7]  = NodeColor.RED,
                 [21] = NodeColor.RED,
                 [32] = NodeColor.RED,
                 [26] = NodeColor.BLACK,
                 [41] = NodeColor.BLACK,
                 [40] = NodeColor.RED,
                 [16] = NodeColor.BLACK,
                 [63] = NodeColor.RED,
                 [62] = NodeColor.BLACK, 
                 [85] = NodeColor.RED,
                 [90] = NodeColor.RED,
                 [87] = NodeColor.BLACK,
                 [80] = NodeColor.BLACK,
                 [57] = NodeColor.BLACK
            };
            utils.CheckNodes( tree.Root, new List< int >( nodesToCheck.Keys ));
            utils.CheckNodes( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
            
        [TestMethod]
        public void TestCreateAVLTreeInStepMode_1()
        {
            var keysToBuild = new List< int > {  1, 6, 3, 2, 10 };
            var keysToCheck = new List< int > {  2, 1, 10, 6, 3 };
            utils.CreateTreeInStepMode( keysToBuild, keysToCheck, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateAVLTreeInStepMode_2()
        {
            var keysToBuild = new List< int > { 10, 12, 16, 10, 8, 3, 1, 2 };
            var keysToCheck = new List< int > { 10, 16, 12 };
            utils.CreateTreeInStepModeBackAndPartial( keysToBuild, keysToCheck, 4, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestCreateAVLTreeInStepMode_3()
        {
            var keysToBuild = new List< int > { 1, 2, 3, 4, 5, 6, 7 };
            var keysToCheck = new List< int > { 1, 4, 3, 2 };
            utils.CreateTreeInStepModeBackAndPartial( keysToBuild, keysToCheck, 5, TreeType.AVL, 0, 20 );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Utils utils;
    }
}

