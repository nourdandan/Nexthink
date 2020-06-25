using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexthink.BusinessLayer;
using Nexthink.Parsers;

namespace UnitTestNextThink
{
    [TestClass]
    public class BusinessLayerTest
    {
        CsvParser parser = new CsvParser(@"Data\data.csv",true);
        XmlParser xmlparser = new XmlParser(@"Data\data.xml",true);
        DataFilter dataFilter1;
        DataFilter dataFilter2;

        public BusinessLayerTest()
        {
        }


        [TestMethod]
        public void TestDataFilteredCsv()
        {
            //Arrange
            dataFilter1 = new DataFilter(parser);
            //Act
            var test1 = dataFilter2.GetWithNumberOfMonitoLarger(1);
            var test2 = dataFilter2.GetWithNumberOfMonitoLarger(3);
            var test3 = dataFilter2.GetAll();

            //Asssert
            Assert.AreEqual(test1.Count(), 6);
            Assert.AreEqual(test2.Count(), 14);
            Assert.AreEqual(test3.Count(), 14);

            Assert.IsTrue(test1.Distinct().Count() == test1.Count);
            Assert.IsFalse(test2.Distinct().Count() == test2.Count);
        }


        [TestMethod]
        public void TestDataFilteredXml()
        {
            //Arrange
            dataFilter2 = new DataFilter(xmlparser);
            //Act
            var test1 = dataFilter2.GetWithNumberOfMonitoLarger(1);
            var test2 = dataFilter2.GetWithNumberOfMonitoLarger(3);
            var test3 = dataFilter2.GetAll();

            //Asssert
            Assert.AreEqual(test1.Count(), 6);
            Assert.AreEqual(test2.Count(), 14);
            Assert.AreEqual(test3.Count(), 14);

            Assert.IsTrue(test1.Distinct().Count() == test1.Count);
            Assert.IsFalse(test2.Distinct().Count() == test2.Count);

        }
    }
}
