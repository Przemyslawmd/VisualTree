
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
            messageCode = Message.Code.OK;  
        }   
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserProperData()
        {
            string data = "2,4,56,33,12,3,44,39, 31, 556,424";
            
            List< int > nodes = new Parser().GetNodesValues( data, ref messageCode );
            List< int > expectedNodes = new List< int >() {  2, 4, 56, 33, 12, 3, 44, 39, 31, 556, 424 };
            
            CollectionAssert.AreEqual( nodes, expectedNodes );
            Assert.AreEqual( messageCode, Message.Code.OK );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserImproperData()
        {
            string data = "2,4,56,33,12,3,44,3,3.556,44";
            
            List< int > nodes = new Parser().GetNodesValues( data, ref messageCode );

            Assert.IsNull( nodes );
            Assert.AreEqual( messageCode, Message.Code.IMPROPER_DATA );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        [TestMethod]
        public void ParserDuplicatedData()
        {
            string data = "2,4,56,33,12,3,44,3,3,556,44";
            
            List< int > nodes = new Parser().GetNodesValues( data, ref messageCode );

            Assert.IsNull( nodes );
            Assert.AreEqual( messageCode, Message.Code.DUPLICATED_SYMBOL );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private Message.Code messageCode;
    }
}

