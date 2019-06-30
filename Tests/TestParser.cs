
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
            string data = "2,4,56,33,12,3,44,39, 31, 556,424";
            
            var nodes = new Parser().GetNodesValues( data, out Result result );
            var expectedNodes = new List< int >() {  2, 4, 56, 33, 12, 3, 44, 39, 31, 556, 424 };
            
            CollectionAssert.AreEqual( nodes, expectedNodes );
            Assert.AreEqual( result, Result.OK );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData()
        {
            string data = "2,4,56,33,12,3,44,3,3.556,44";
            
            var nodes = new Parser().GetNodesValues( data, out Result result );

            Assert.IsNull( nodes );
            Assert.AreEqual( result, Result.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedData()
        {
            Settings.RemoveDuplicatedNodes = false;
            string data = "2,4,56,33,12,3,44,3,3,556,44";

            var nodes = new Parser().GetNodesValues( data, out Result result );

            Assert.IsNull( nodes );
            Assert.AreEqual( result, Result.DUPLICATED_SYMBOL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedDataRemove()
        {
            Settings.RemoveDuplicatedNodes = true;
            string data = "2,4,56,33,12,3,44,3,3,556,44";
            var expectedNodes = new List< int >() {  2, 4, 56, 33, 12, 3, 44, 556 };
            
            var nodes = new Parser().GetNodesValues( data, out Result result );

            CollectionAssert.AreEqual( nodes, expectedNodes );
            Assert.AreEqual( result, Result.OK );
        }
    }
}

