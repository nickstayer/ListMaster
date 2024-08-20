using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListMaster;
using System;

namespace Tests
{
    [TestClass]
    public class ParseNameTest
    {
        [TestMethod]
        public void ParseNameSimpleTest()
        {
            var name = "Иванов";
            var actual = FileParser.ParseName(name);
            Assert.AreEqual("Иванов", actual);
        }

        [TestMethod]
        public void ParseFullNameSimpleTest()
        {
            var name = "Иванов Иван Иванович";
            var actual = FileParser.ParseName(name);
            Assert.AreEqual("Иванов Иван Иванович", actual);
        }

        [TestMethod]
        public void ParseFullNameWhiteSpaceTest()
        {
            var name = "Иванов Иван Иванович ";
            var actual = FileParser.ParseName(name);
            Assert.AreEqual("Иванов Иван Иванович", actual);
        }

        [TestMethod]
        [DataRow("Малык (Дзюбан) Анастасия Викторовна", "Малык Анастасия Викторовна")]
        [DataRow("Мусаева (мусаиева) Юлай (гулай) Танриверди Кзы", "Мусаева Юлай Танриверди Кзы")]
        public void ParseFullNameWithBracketsTest(string name, string expected)
        {
            var actual = FileParser.ParseName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseFullNameWithFourPartTest()
        {
            var name = "Мусаева (мусаиева) Юлай (гулай) Танриверди Кзы";
            var parser = new FileParser();
            var actual = parser.GetName(name);
            Assert.AreEqual("Мусаева", actual.Lastname);
            Assert.AreEqual("Юлай", actual.Firstname);
            Assert.AreEqual("Танриверди Кзы", actual.Othername);
        }

        [TestMethod]
        public void ParseFullNameWithDashTest()
        {
            var name = "Бусуйко-Бабак Наталья Александровна";
            var actual = FileParser.ParseName(name);
            Assert.AreEqual("Бусуйко-Бабак Наталья Александровна", actual);
        }

        [TestMethod]
        public void FillNameFieldsSimpleTest()
        {
            var person = new Dossier();
            person.Fullname = "Иванов Иван Иванович";

            var fileParser = new FileParser();
            fileParser.CompleteNameFields(person);

            Assert.AreEqual("Иванов", person.Lastname);
            Assert.AreEqual("Иван", person.Firstname);
            Assert.AreEqual("Иванович", person.Othername);
            Assert.AreEqual("Иванов Иван Иванович", person.Fullname);
        }

        [TestMethod]
        public void FillNameFieldsWithoutOthernameTest()
        {
            var person = new Dossier();
            person.Fullname = "Иванов Иван";

            var fileParser = new FileParser();
            fileParser.CompleteNameFields(person);

            Assert.AreEqual("Иванов", person.Lastname);
            Assert.AreEqual("Иван", person.Firstname);
            Assert.IsTrue(string.IsNullOrEmpty(person.Othername));
            Assert.AreEqual("Иванов Иван", person.Fullname);
        }

        [TestMethod]
        public void FillNameFieldsNoFullnameTest()
        {
            var person = new Dossier();
            person.Lastname = "Иванов";
            person.Firstname = "Иван";
            person.Othername = "Иванович";


            var fileParser = new FileParser();
            fileParser.CompleteNameFields(person);

            Assert.AreEqual("Иванов", person.Lastname);
            Assert.AreEqual("Иван", person.Firstname);
            Assert.AreEqual("Иванович", person.Othername);
            Assert.AreEqual("Иванов Иван Иванович", person.Fullname);
        }

        [TestMethod]
        public void FillNameFieldsHaveFullDataTest()
        {
            var person = new Dossier();
            person.Lastname = "Иванов";
            person.Firstname = "Иван";
            person.Othername = "Иванович";
            person.Fullname = "Иванов Иван Иванович";
            person.Bdate = new DateTime(1999, 12, 26);

            var fileParser = new FileParser();
            fileParser.CompleteNameFields(person);

            Assert.AreEqual("Иванов", person.Lastname);
            Assert.AreEqual("Иван", person.Firstname);
            Assert.AreEqual("Иванович", person.Othername);
            Assert.AreEqual("Иванов Иван Иванович", person.Fullname);
            Assert.AreEqual(new DateTime(1999, 12, 26), person.Bdate);
        }
    }
}
