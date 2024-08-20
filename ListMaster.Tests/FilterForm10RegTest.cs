using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ListMaster;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class FilterForm10RegTest
    {
        [TestMethod]
        public void DeleteAddressDuplicatesTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 8, 23),
                DateOut = DateTime.Today,
                Address = "Россия, рп. Роза, р-н Коркинский, обл. Челябинская, ул. 9 Мая, д. 51"
            });
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = DateTime.Today,
                Address = "Россия, рп. Роза, р-н Коркинский, обл. Челябинская, ул. 9 Мая, д. 51"
            });
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = DateTime.Today,
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });

            registrations = Utils.FilterForm10Registrations(registrations);

            var targetEntry = registrations
                .Where(reg => reg.Address == "Россия, рп. Роза, р-н Коркинский, обл. Челябинская, ул. 9 Мая, д. 51")
                .ToList()
                .FirstOrDefault();

            Assert.IsTrue(registrations.Count == 2);
            Assert.IsTrue(targetEntry.DateIn == new DateTime(2022, 9, 30));
        }
    }
}
