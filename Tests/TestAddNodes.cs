
using System.Collections.Generic;
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestAddNodes
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
        public void TestAddNodesBST_1()
        {
            var keysToBuild = new List< int > {  1, 6, 3, 2, 10 };
            var keysToAdd = new List< int > {  4, 11, 5 };
            Tree tree = utils.BuildTreeAndAddNodes( keysToBuild, keysToAdd, TreeType.CommonBST );
            utils.CheckNodes( tree.Root, new List< int > {  2, 5, 4, 3, 11, 10, 6, 1 } );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestAddNodesBST_2()
        {
            var keysToBuild = new List< int > {  14, 2, 30, 28, 4, 5, 12 };
            var keysToAdd = new List< int > { 22, 29, 11 };
            Tree tree = utils.BuildTreeAndAddNodes( keysToBuild, keysToAdd, TreeType.CommonBST );
            utils.CheckNodes( tree.Root, new List< int > { 11, 12, 5, 4, 2, 22, 29, 28, 30, 14 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestAddNodesAVL_1()
        {
            var keysToBuild = new List< int > { 1, 6, 3, 2, 10 };
            var keysToAdd = new List< int > { 4, 11, 5 };
            Tree tree = utils.BuildTreeAndAddNodes( keysToBuild, keysToAdd, TreeType.AVL );
            utils.CheckNodes( tree.Root, new List< int > { 2, 1, 5, 4, 11, 10, 6, 3 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestAddNodesAVL_2()
        {
            var keysToBuild = new List< int > { 14, 2, 30, 28, 4, 5, 12 };
            var keysToAdd = new List< int > { 22, 29, 11 };
            Tree tree = utils.BuildTreeAndAddNodes( keysToBuild, keysToAdd, TreeType.AVL );
            utils.CheckNodes( tree.Root, new List< int >{ 2, 5, 12, 11, 4, 22, 29, 30, 28, 14 } );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestAddNodesAVLInStepMode()
        {
            var keysToBuild = new List< int > { 7, 12, 8, 3, 1, 20, 30, 27 };
            var keysToAdd = new List< int > { 2, 11, 10 };
            var keysToCheck = new List< int > { 2, 1, 7, 3, 10, 12, 11, 27, 30, 20, 8 };
            utils.AddNodesInStepModeAndBack( keysToBuild, keysToAdd, keysToCheck, 4, TreeType.AVL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private Utils utils;
    }
}

