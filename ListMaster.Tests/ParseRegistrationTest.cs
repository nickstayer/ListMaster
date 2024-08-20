using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListMaster;
using System.Linq;
using System;

namespace Tests
{
    [TestClass]
    public class ParseRegistrationTest
    {
        [TestMethod]
        public void ParseRegistrationSimpleTest()
        {
            var registration = "Адрес регистрации по месту жительства; "
                + "05.04.2022 - ; "
                + "обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15";
            var fileParser = new FileParser();
            var personReg = fileParser.ParseRegistration(registration);
            Assert.AreEqual("Адрес регистрации по месту жительства", personReg.Registrations.FirstOrDefault().Type);
            Assert.AreEqual("05.04.2022 -", personReg.Registrations.FirstOrDefault().Period);
            Assert.AreEqual("обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15", personReg.Registrations.FirstOrDefault().Address);
        }

        [TestMethod]
        public void ParseRegistrationWithoutPeriodTest()
        {
            var registration = new Registration
            {
                Address = "обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15",
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 4, 5),
                DateOut = new DateTime(2022, 4, 6),
            };

            FileParser.CompleteRegistrationFields(registration);
            Assert.AreEqual(Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF, registration.Type);
            Assert.AreEqual("05.04.2022 - 06.04.2022", registration.Period);
            Assert.AreEqual(new DateTime(2022, 4, 5), registration.DateIn);
            Assert.AreEqual(new DateTime(2022, 4, 6), registration.DateOut);
            Assert.AreEqual("обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15", registration.Address);
        }

        [TestMethod]
        public void ParseRegistrationWrongFormatTest()
        {
            var registration = "Адрес регистрации по месту жительства; "
                + "05.04.2022 - ; ";
            var fileParser = new FileParser();
            var personReg = fileParser.ParseRegistration(registration);
            Assert.IsNull(personReg.Registrations.FirstOrDefault());
        }

        [TestMethod]
        public void FillEmptyRegistrationFieldsTest()
        {
            var registration = "Адрес регистрации по месту жительства; "
                + "05.04.2022 - 31.12.2022; "
                + "обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15";
            var fileParser = new FileParser();
            var person = fileParser.ParseRegistration(registration);
            fileParser.CompleteRegistrationFields(person);
            Assert.AreEqual("Адрес регистрации по месту жительства", person.Registrations.FirstOrDefault().Type);
            Assert.AreEqual("05.04.2022 - 31.12.2022", person.Registrations.FirstOrDefault().Period);
            Assert.AreEqual("обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15", person.Registrations.FirstOrDefault().Address);
            Assert.AreEqual(new DateTime(2022, 4, 5), person.Registrations.FirstOrDefault().DateIn);
            Assert.AreEqual(new DateTime(2022, 12, 31), person.Registrations.FirstOrDefault().DateOut);
        }

        [TestMethod]
        public void FillEmptyRegistrationFieldsWithDeafaultValueTest()
        {
            var registration = "Адрес регистрации по месту жительства; "
                + "05.04.2022 - ; "
                + "обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15";
            var fileParser = new FileParser();
            var person = fileParser.ParseRegistration( registration);
            fileParser.CompleteRegistrationFields(person);
            Assert.AreEqual("Адрес регистрации по месту жительства", person.Registrations.FirstOrDefault().Type);
            Assert.AreEqual("05.04.2022 -", person.Registrations.FirstOrDefault().Period);
            Assert.AreEqual("обл. Челябинская, г. Златоуст, п. ЗЭС Челябэнерго, д. 6, кв. 15", person.Registrations.FirstOrDefault().Address);
            Assert.AreEqual(new DateTime(2022, 4, 5), person.Registrations.FirstOrDefault().DateIn);
            Assert.AreEqual(default, person.Registrations.FirstOrDefault().DateOut);
        }

        [TestMethod]
        public void IsNewRegistrationTrueTest()
        {
            var registration = new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                Period = "05.11.2022 - 31.12.2022",
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Дарвина, Советский р-н, д. 65, кв. 4"
            };
            var person = new Dossier();
            person.Registrations.Add(new Registration
            {
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 11, 5),
                DateOut = new DateTime(2022, 12, 31),
                Period = "05.11.2022 - 31.12.2022",
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Дарвина, Советский р-н, д. 65, кв. 4"
            });
            var actual = Utils.IsNewRegistration(registration, person);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsNewRegistrationFalseTest()
        {
            var registration = new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                Period = "05.11.2022 - 31.12.2022",
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Дарвина, Советский р-н, д. 65, кв. 4"
            };
            var person = new Dossier();
            person.Registrations.Add(new Registration
            {
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 11, 5),
                DateOut = new DateTime(2022, 12, 31),
                Period = "05.11.2022 - 31.12.2022",
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Дарвина, Советский р-н, д. 65, кв. 4"
            });
            var actual = Utils.IsNewRegistration(registration, person);
            Assert.IsTrue(actual);
        }
    }
}
