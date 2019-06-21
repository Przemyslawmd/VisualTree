
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SuiteDeleteNodes
    {
        [TestMethod]
        public void TestDeleteNodesWithNoChildren()
        {
            List< int > keysToBuild = new List< int > { 6, 2, 5, 10, 1, 13, 4, 9 };
            List< int > keysToCheck = new List< int > { 1, 5, 2, 13, 10, 6 };
            List< int > keysToDelete = new List< int > { 4, 9 };
            new Test().DeleteNodes( keysToBuild, keysToCheck, keysToDelete, TreeType.CommonBST );
        }
    }
}

