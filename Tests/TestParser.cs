
using VisualTree;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestParser
    {
        [TestMethod]
        public void ParserProperData()
        {
            var keys = new Parser().GetKeys( "2,4,56,33,12,3,44,39, 31, 556,424", out Result result );
            CollectionAssert.AreEqual( keys, new List< int >(){ 2, 4, 56, 33, 12, 3, 44, 39, 31, 556, 424 } );
            Assert.AreEqual( result, Result.OK );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData_1()
        {
            var keys = new Parser().GetKeys( "2,4,56,33,12,3,44,3,3.556,44", out Result result );
            Assert.IsNull( keys );
            Assert.AreEqual( result, Result.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData_2()
        {
            var keys = new Parser().GetKeys( "2,4,56,33,12,3,44s,3,556", out Result result );
            Assert.IsNull( keys );
            Assert.AreEqual( result, Result.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData_3()
        {
            var keys = new Parser().GetKeys( "2,4,56,33,12,3,44,3, v, 44", out Result result );
            Assert.IsNull( keys );
            Assert.AreEqual( result, Result.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedSymbol()
        {
            var keys = new Parser().GetKeys( "2,4,56,33,12,3,44,3,3,556,44", out Result result );
            Assert.IsNull( keys );
            Assert.AreEqual( result, Result.DUPLICATED_SYMBOL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserSpaceAsSeparator()
        {
            var keys = new Parser().GetKeys( "2 4, 56 33  , 3 44, , 12    101,102", out Result result );
            CollectionAssert.AreEqual( keys, new List< int >(){ 2, 4, 56, 33, 3, 44, 12, 101, 102 } );
            Assert.AreEqual( result, Result.OK );
        }
    }
}

