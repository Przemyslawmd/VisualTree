
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SuiteBalanceTree
    {
        [TestMethod]
        public void TestBalanceTree_1()
        {
            List< int > keysToBuild = new List< int > { 20, 15, 30, 25, 40, 23, 28 };
            List< int > keysToCheck = new List< int > { 15, 23, 20, 28, 40, 30, 25 };
            new Test().BalanceTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestBalanceTree_2()
        {
            List< int > keysToBuild = new List< int > 
            { 
                28, 20, 45, 12, 23, 17, 34, 65, 56, 2, 3, 15, 22, 30, 25, 40, 31, 43, 24, 76, 29, 1, 
                100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110
            };
            List< int > keysToCheck = new List< int > 
            { 
                1, 3, 2, 15, 12, 20, 23, 22, 17, 25, 29, 28, 31, 40, 34, 30, 24, 45, 65, 56, 100, 102,
                101, 76, 104, 106, 105, 108, 110, 109, 107, 103, 43
            };
            new Test().BalanceTree( keysToBuild, keysToCheck, TreeType.CommonBST );
        }
    }
}

