using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ListMaster;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class FilterDossRegTest
    {
        [TestMethod]
        public void WrongRoleTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = "Адрес места пребывания",
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void WrongTypeTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = "Регистрация ИГ по месту пребывания",
                DateIn = new DateTime(2022, 9, 30),
                DateOut = new DateTime(),
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void ConstActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod]
        public void ConstNotActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(2022, 11, 26),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void TempActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = DateTime.Today,
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod]
        public void TempNotActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 7, 18),
                DateOut = new DateTime(2022, 7, 28),
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void ConstAndHotelActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF,
                DateIn = new DateTime(2022, 12, 10),
                DateOut = DateTime.Today,
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Тухачевского, д. 6"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 2);
        }

        [TestMethod]
        public void ConstActualHotelNotActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF,
                DateIn = new DateTime(2022, 12, 10),
                DateOut = new DateTime(2022, 12, 15),
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Тухачевского, д. 6"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual(Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF, actual.FirstOrDefault().Type);
        }

        [TestMethod]
        public void ConstNotActualHotelActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(2022, 12, 15),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF,
                DateIn = new DateTime(2022, 12, 10),
                DateOut = DateTime.Today,
                Address = "Россия, г. Челябинск, обл. Челябинская, ул. Тухачевского, д. 6"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual(Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF, actual.FirstOrDefault().Type);
        }

        [TestMethod]
        public void ConstActualConstlActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 11, 25),
                DateOut = new DateTime(),
                Address = "Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4"
            });

            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                DateIn = new DateTime(2022, 09, 09),
                DateOut = new DateTime(),
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Пирогова, Ленинский р-н, д. 36"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual("Россия, п. Береговой, р-н Каслинский, обл. Челябинская, ул. Чапаева, д. 4",
                actual.FirstOrDefault().Address);
        }

        [TestMethod]
        public void TempActualTempActualRegTest()
        {
            var registrations = new List<Registration>();
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 8, 23),
                DateOut = DateTime.Today,
                Address = "Россия, г. Магнитогорск, обл. Челябинская, ул. Ленинградская, Ленинский р-н, д. 39, кв. 47"
            });
            registrations.Add(new Registration
            {
                Role = Consts.REG_ROLE_PERSON_ADDRESS,
                Type = Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                DateIn = new DateTime(2022, 9, 30),
                DateOut = DateTime.Today,
                Address = "Россия, рп. Роза, р-н Коркинский, обл. Челябинская, ул. 9 Мая, д. 51"
            });
            var actual = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
            Assert.IsTrue(actual.Count == 2);
        }

        [TestMethod]
        public void GroupRegistrationWithSameAddressTest()
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
            var regsLists = Utils.GroupRegistrationsByAddress(registrations);
            Assert.IsTrue(regsLists.Count == 2);
        }

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

            registrations = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );

            var targetEntry = registrations
                .Where(reg => reg.Address == "Россия, рп. Роза, р-н Коркинский, обл. Челябинская, ул. 9 Мая, д. 51")
                .ToList()
                .FirstOrDefault();

            Assert.IsTrue(registrations.Count == 2);
            Assert.IsTrue(targetEntry.DateIn == new DateTime(2022, 9, 30));
        }
    }
}
