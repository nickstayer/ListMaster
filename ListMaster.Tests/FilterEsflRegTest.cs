using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ListMaster;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class FilterEsflRegTest
    {
        [TestMethod]
        public void WrongSourceTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_MIGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void ConstActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod]
        public void TempActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_TEMP,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 1);
        }


        [TestMethod]
        public void ConstActualConstlActualSameAddressRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });

            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(2022, 11, 26),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 1);//с более поздней датой въезда
            Assert.AreEqual(new DateTime(2022, 11, 26),
                actual.FirstOrDefault().DateIn);
        }

        [TestMethod]
        public void TempActualTempActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_TEMP,
                DateIn = new DateTime(2022, 8, 23),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_TEMP,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "Россия, рп. Роза, р-н Коркинский, обл. Челябинская, ул. 9 Мая, д. 51"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 2);//все действующие
        }

        [TestMethod]
        public void REL_ConstActualTempActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(1980, 7, 3),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "обл. донецкая, г. угледар, ул. шахтерская, д. 16, кв. 6"
            });
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_TEMP,
                DateIn = new DateTime(2023, 7, 17),
                DateOut = new DateTime(2024, 7, 5),
                Address = "Челябинская обл., г. Миасс, ул. Готвальда, д. 17, кв. 63"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 2);
        }

        [TestMethod]
        public void REL_ConstActual2TempActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(2002, 10, 22),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "обл. донецкая, г. угледар, ул. шахтерская, д. 16, кв. 6",
                Role = Consts.REG_ROLE_PERSON_ADDRESS
            });
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_CONST,
                DateIn = new DateTime(2002, 10, 22),
                DateOut = Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT,
                Address = "обл. донецкая, г. угледар, ул. шахтерская, д. 16, кв. 6",
                Role = "Дополнительный адрес лица"
            });
            registrations.Add(new Registration
            {
                Source = Consts.ESFL_REG_SOURCE_REGUCHET,
                Type = Consts.ESFL_REG_TYPE_TEMP,
                DateIn = new DateTime(2023, 7, 17),
                DateOut = new DateTime(2024, 7, 5),
                Address = "Челябинская обл., г. Миасс, ул. Готвальда, д. 17, кв. 63"
            });
            var actual = Utils.FilterEsflRegUchetRegistrations(registrations);
            Assert.IsTrue(actual.Count == 2);
        }
    }
}
