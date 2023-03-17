
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
            var nodes = new Parser().GetKeys( "2,4,56,33,12,3,44,39, 31, 556,424", out Result result );
            CollectionAssert.AreEqual( nodes, new List< int >(){ 2, 4, 56, 33, 12, 3, 44, 39, 31, 556, 424 } );
            Assert.AreEqual( result, Result.OK );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData()
        {
            var nodes = new Parser().GetKeys( "2,4,56,33,12,3,44,3,3.556,44", out Result result );
            Assert.IsNull( nodes );
            Assert.AreEqual( result, Result.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedData()
        {
            Settings.RemoveDuplicatedNodes = false;
            var nodes = new Parser().GetKeys( "2,4,56,33,12,3,44,3,3,556,44", out Result result );
            Assert.IsNull( nodes );
            Assert.AreEqual( result, Result.DUPLICATED_SYMBOL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedDataRemove()
        {
            Settings.RemoveDuplicatedNodes = true;
            var nodes = new Parser().GetKeys( "2,4,56,33,12,3,44,3,3,556,44", out Result result );
            CollectionAssert.AreEqual( nodes, new List< int >(){ 2, 4, 56, 33, 12, 3, 44, 556 } );
            Assert.AreEqual( result, Result.OK );
        }
    }
}

