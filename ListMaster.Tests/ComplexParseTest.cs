using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ListMaster;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ComplexParseTest
    {
        [TestMethod]
        public void ComplexParseSimpleText()
        {
            var person = new Dossier();
            person.Fullname = "Иванов Иван Иванович";
            person.Documents.Add(new Document
            {
                Series = "1234",
                Number = "567891"
            });
            var registration = "Адрес регистрации по месту жительства; "
                + "05.04.2022 - ; "
                + "обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15";

            var fileParser = new FileParser();
            fileParser.CompleteNameFields(person);
            fileParser.CompleteDocumentFields(person);
            var personReg = fileParser.ParseRegistration(registration);
            fileParser.CompleteRegistrationFields(personReg);
            fileParser.AddRegistration(person, personReg);
  
            Assert.AreEqual("Иванов", person.Lastname);
            Assert.AreEqual("Иван", person.Firstname);
            Assert.AreEqual("Иванович", person.Othername);
            Assert.AreEqual("Иванов Иван Иванович", person.Fullname);

            Assert.AreEqual("1234", person.Documents.FirstOrDefault().Series);
            Assert.AreEqual("567891", person.Documents.FirstOrDefault().Number);
            Assert.AreEqual("1234 567891", person.Documents.FirstOrDefault().SeriesNumber);

            Assert.AreEqual("Адрес регистрации по месту жительства", person.Registrations.FirstOrDefault().Type);
            Assert.AreEqual("05.04.2022 -", person.Registrations.FirstOrDefault().Period);
            Assert.AreEqual("обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15", person.Registrations.FirstOrDefault().Address);
        }
    }
}
