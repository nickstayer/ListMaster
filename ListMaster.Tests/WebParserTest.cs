using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListMaster;


namespace Tests
{
    [TestClass]
    public class WebParserTest
    {
        //[TestMethod]
        //[DataRow("18.04.2009", false)]
        //[DataRow("17.04.2009", false)]
        //[DataRow("19.04.2009", true)]
        //public void ChildAgeTest(string bDate, bool expected)
        //{
        //    DateTime.TryParse(bDate, out DateTime bDateDT);
        //    var actual = Utils.IsChild(bDateDT);
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        public void ParseDocTest()
        {
            var docTypeSeriesNumber = "Паспорт гражданина Российской Федерации 7507 №142291";
            var docTypeSeriesNumberTuple = WebParser.DocTypeSeriesNumberEsfl(docTypeSeriesNumber);
            var doc = new Document()
            {
                Type = docTypeSeriesNumberTuple.Item1,
                Series = docTypeSeriesNumberTuple.Item2,
                Number = docTypeSeriesNumberTuple.Item3
            };
            Assert.AreEqual("7507", doc.Series);
            Assert.AreEqual("142291", doc.Number);
            Assert.AreEqual("Паспорт гражданина Российской Федерации", doc.Type);
        }

        [TestMethod]
        public void ParseDocNoDataTest()
        {
            string docTypeSeriesNumber = null;
            var docTypeSeriesNumberTuple = WebParser.DocTypeSeriesNumberEsfl(docTypeSeriesNumber);
            var doc = new Document()
            {
                Type = docTypeSeriesNumberTuple.Item1,
                Series = docTypeSeriesNumberTuple.Item2,
                Number = docTypeSeriesNumberTuple.Item3
            };
            Assert.AreEqual(string.Empty, doc.Series);
            Assert.AreEqual(string.Empty, doc.Number);
            Assert.AreEqual(string.Empty, doc.Type);
        }

        [TestMethod]
        public void ParseDepTest()
        {
            var depCodeName = "740-039 ОТДЕЛЕНИЕМ УФМС РОССИИ ПО ЧЕЛЯБИНСКОЙ ОБЛАСТИ В ГОРОДЕ ТРОИЦКЕ И ТРОИЦКОМ РАЙОНЕ";
            var depCodeNameTuple = WebParser.DepCodeAndNameEsfl(depCodeName);
            var doc = new Document()
            {
                DepartmentCode = depCodeNameTuple.Item1,
                DepartmentName = depCodeNameTuple.Item2,
            };
            Assert.AreEqual("740-039", doc.DepartmentCode);
            Assert.AreEqual("ОТДЕЛЕНИЕМ УФМС РОССИИ ПО ЧЕЛЯБИНСКОЙ ОБЛАСТИ В ГОРОДЕ ТРОИЦКЕ И ТРОИЦКОМ РАЙОНЕ", doc.DepartmentName);
        }

        [TestMethod]
        public void ParseDepNoDataTest()
        {
            string depCodeName = null;
            var depCodeNameTuple = WebParser.DepCodeAndNameEsfl(depCodeName);
            var doc = new Document()
            {
                DepartmentCode = depCodeNameTuple.Item1,
                DepartmentName = depCodeNameTuple.Item2,
            };
            Assert.AreEqual(string.Empty, doc.DepartmentCode);
            Assert.AreEqual(string.Empty, doc.DepartmentName);
        }
    }
}
