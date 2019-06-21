
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
        public void TestDeleteNodes_1()
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
        public void TestDeleteNodes_2()
        {
            List< int > keysToBuild = new List< int > { 10, 5, 15, 3, 12, 1, 16, 2, 11 };
            List< int > keysToCheck = new List< int > { 2, 1, 3, 5, 11, 16, 15, 10 };
            List< int > keysToDelete = new List< int > { 12 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.CommonBST );
        }
    }
}

