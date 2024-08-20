using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ListMaster;

namespace Tests
{
    [TestClass]
    public class ParseDocumentTest
    {
        [TestMethod]
        public void CompleteDocumentFieldsSimpleTest()
        {
            var person = new Dossier();
            person.Documents.Add(new Document 
            { 
                Series = "1234",
                Number = "567891"
            });

            var fileParser = new FileParser();
            fileParser.CompleteDocumentFields(person);

            Assert.AreEqual("1234", person.Documents.FirstOrDefault().Series);
            Assert.AreEqual("567891", person.Documents.FirstOrDefault().Number);
            Assert.AreEqual("1234 567891", person.Documents.FirstOrDefault().SeriesNumber);
        }

        [TestMethod]
        public void CompleteDocumentFieldsNoSeriesAndNumberTest()
        {
            var person = new Dossier();
            person.Documents.Add(new Document
            {
                SeriesNumber = "1234567891",
            });

            var fileParser = new FileParser();
            fileParser.CompleteDocumentFields(person);

            Assert.AreEqual("1234", person.Documents.FirstOrDefault().Series);
            Assert.AreEqual("567891", person.Documents.FirstOrDefault().Number);
            Assert.AreEqual("1234567891", person.Documents.FirstOrDefault().SeriesNumber);
        }

        [TestMethod]
        public void CompleteDocumentFieldsNoSeriesAndNumberWithoutWhiteSpaceTest()
        {
            var person = new Dossier();
            person.Documents.Add(new Document
            {
                SeriesNumber = "1234567891",
            });

            var fileParser = new FileParser();
            fileParser.CompleteDocumentFields(person);

            Assert.AreEqual("1234", person.Documents.FirstOrDefault().Series);
            Assert.AreEqual("567891", person.Documents.FirstOrDefault().Number);
            Assert.AreEqual("1234567891", person.Documents.FirstOrDefault().SeriesNumber);
        }

        [TestMethod]
        public void CompleteDocumentFieldsNoDocumentTest()
        {
            var person = new Dossier();
            var fileParser = new FileParser();
            fileParser.CompleteDocumentFields(person);
            Assert.IsNull(person.Documents.FirstOrDefault());
        }

        [TestMethod]
        public void ParseSeriesSimpleTest()
        {
            var series = "1234";
            var expected = "1234";
            var actual = FileParser.ParseSeries(series);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseSeriesWrongFormatTest()
        {
            var series2 = "";
            var series3 = "abcd";
            var expected = string.Empty;
            var actual2 = FileParser.ParseSeries(series2);
            var actual3 = FileParser.ParseSeries(series3);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        public void ParseNumberSimpleTest()
        {
            var number = "123456";
            var expected = "123456";
            var actual = FileParser.ParseNumber(number);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseNumberNormalizeTest()
        {
            var number = "23456";
            var expected = "023456";
            var actual = FileParser.ParseNumber(number);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseSeriesNormalizeTest()
        {
            var series = "234";
            var expected = "0234";
            var actual = FileParser.ParseSeries(series);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseNumberWrongFormatTest()
        {
            var number1 = "";
            var number2 = "abcd";
            var expected = string.Empty;
            var actual1 = FileParser.ParseNumber(number1);
            var actual2 = FileParser.ParseNumber(number2);
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }

        [TestMethod]
        public void ParseDepartmentCodeDashTest()
        {
            var code = "123456";
            var expected = "123-456";
            var actual = FileParser.ParseDepartmentCode(code);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseDepartmentCodeSimpleTest()
        {
            var code = "123-456";
            var expected = "123-456";
            var actual = FileParser.ParseDepartmentCode(code);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseDepartmentCodeWrongFormatTest()
        {
            var code = "123";
            var expected = string.Empty;
            var actual = FileParser.ParseDepartmentCode(code);
            Assert.AreEqual(expected, actual);
        }
    }
}
