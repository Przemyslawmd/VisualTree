
using System.Collections.Generic;
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SuiteAddNodes
    {
        [TestMethod]
        public void TestAddNodesBST_1()
        {
            List< int > keysToBuild = new List< int > {  1, 6, 3, 2, 10 };
            List< int > keysToAdd = new List< int > {  4, 11, 5 };
            List< int > keysToCheck = new List< int > {  2, 5, 4, 3, 11, 10, 6, 1 };
            new Test().AddNodes( keysToBuild, keysToAdd, keysToCheck, TreeType.CommonBST ); 
            
            
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void TestAddNodesBST_2()
        {
            List< int > keysToBuild = new List< int > {  14, 2, 30, 28, 4, 5, 12 };
            List< int > keysToAdd = new List< int > { 22, 29, 11 };
            List< int > keysToCheck = new List< int > { 11, 12, 5, 4, 2, 22, 29, 28, 30, 14 };
            new Test().AddNodes( keysToBuild, keysToAdd, keysToCheck, TreeType.CommonBST ); 
        }
    }
}

