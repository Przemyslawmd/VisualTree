
using VisualTree;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestParser
    {
        [TestInitialize]  
        public void TestInit()  
        {  
            result = Result.OK;  
        }   
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserProperData()
        {
            string data = "2,4,56,33,12,3,44,39, 31, 556,424";
            
            List< int > nodes = new Parser().GetNodesValues( data, ref result );
            List< int > expectedNodes = new List< int >() {  2, 4, 56, 33, 12, 3, 44, 39, 31, 556, 424 };
            
            CollectionAssert.AreEqual( nodes, expectedNodes );
            Assert.AreEqual( result, Result.OK );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData()
        {
            string data = "2,4,56,33,12,3,44,3,3.556,44";
            
            List< int > nodes = new Parser().GetNodesValues( data, ref result );

            Assert.IsNull( nodes );
            Assert.AreEqual( result, Result.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedData()
        {
            string data = "2,4,56,33,12,3,44,3,3,556,44";
            
            List< int > nodes = new Parser().GetNodesValues( data, ref result );

            Assert.IsNull( nodes );
            Assert.AreEqual( result, Result.DUPLICATED_SYMBOL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private Result result;
    }
}

