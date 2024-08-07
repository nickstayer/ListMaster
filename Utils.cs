using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Diagnostics;

namespace ListMaster
{
    public class Utils
    {
        public static DateTime StringToDate(string date)
        {
            if (date == Consts.ESFL_REG_UNTIL_NOW)
                return Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT;
            DateTime.TryParse(date.Trim(), out DateTime result);
            return result;
        }

        public static bool IsFreshPeriod(Registration registration)
        {
            var today = DateTime.Today;
            var outDateIsFresh = (registration.DateOut >= today) || registration.DateOut == default;
            return outDateIsFresh;
        }

        public static bool IsNewRegistration(Registration newReg, Dossier oldPerson)
        {
            if (oldPerson.Registrations.Count == 0)
            {
                return true;
            }
            Registration regFromFile = oldPerson.Registrations.FirstOrDefault();
            var result = regFromFile.Equals(newReg);
            return !result;
        }

        public static List<Registration> FilterForm10Registrations(List<Registration> registrations)
        {
            registrations = registrations
                .Distinct(new RegistrationComparer())
                .Where(reg => !string.IsNullOrEmpty(reg.DateIn.ToShortDateString()))
                .Where(reg => IsFreshPeriod(reg))
                .ToList();
            registrations = DeleteAddressDuplicates(registrations);
            return registrations;
        }

        public static List<Registration> FilterDossierRegistrations(List<Registration> registrations,
            string targetRole, string targetTypeConst, string targetTypeTemp, string targetTypeHotel)
        {
            registrations = GetAllTargetTypeRegistrations(registrations,
                targetRole, targetTypeConst, targetTypeTemp, targetTypeHotel
                );
            if (registrations.Count == 0)
                return new List<Registration>();

            var constantRegs = registrations
                .Where(reg => reg.Type == targetTypeConst)
                .ToList();
            var constantReg = GetLatestDateInRegistration(constantRegs);

            var tempRegs = registrations
                .Where(reg => reg.Type == targetTypeTemp
                    || reg.Type == targetTypeHotel)
                .ToList();

            tempRegs = DeleteAddressDuplicates(tempRegs);

            var result = new List<Registration>();
            if (tempRegs.Count > 0)
            {
                result.AddRange(tempRegs);
            }
            if (constantReg != null)
            {
                result.Add(constantReg);
            }

            return result;
        }

        public static List<Registration> FilterEsflRegUchetRegistrations(List<Registration> registrations)
        {
            registrations = registrations
                .Where(reg => reg.Source != Consts.ESFL_REG_SOURCE_MIGUCHET)
                .ToList();

            if (registrations.Count == 0)
                return new List<Registration>();

            var constantRegs = registrations
                .Where(reg => reg.Type == Consts.ESFL_REG_TYPE_CONST)
                .ToList();

            var constantReg = GetLatestDateInRegistration(constantRegs);

            var tempRegs = registrations
                .Where(reg => reg.Type == Consts.ESFL_REG_TYPE_TEMP)
                .ToList();

            tempRegs = DeleteAddressDuplicates(tempRegs);

            var result = new List<Registration>();
            if (tempRegs.Count > 0)
            {
                result.AddRange(tempRegs);
            }
            if (constantReg != null)
            {
                result.Add(constantReg);
            }

            return result;
        }

        public static List<Registration> DeleteAddressDuplicates(List<Registration> registrations)
        {
            var groupedTempRegs = GroupRegistrationsByAddress(registrations);
            return groupedTempRegs
                .Select(regList => GetLatestDateInRegistration(regList))
                .ToList();
        }

        public static Registration GetLatestDateInRegistration(List<Registration> registrations)
        {
            var result = registrations
                .Select(reg =>
                {
                    return Tuple.Create(reg, reg.DateIn);
                })
                .OrderByDescending(t => t.Item2)
                .Select(t => t.Item1)
                .FirstOrDefault();

            return result;
        }

        public static List<List<Registration>> GroupRegistrationsByAddress(List<Registration> registrations)
        {
            var regListsGroups = registrations
                .GroupBy(reg => reg.Address)
                .Select(g => new 
                { 
                    g.Key, 
                    Count = g.Count(),
                    Regs = g.Select(reg => reg).ToList()
                });
            var regLists = new List<List<Registration>>();
            foreach(var regListgroup in regListsGroups)
            {
                regLists.Add(regListgroup.Regs);
            }
            return regLists;
        }

        public static List<Registration> GetAllTargetTypeRegistrations(List<Registration> registrations,
            string targetRole, string targetTypeConst, string targetTypeTemp, string targetTypeHotel)
        {
            registrations = registrations
                .Distinct(new RegistrationComparer())
                .Where(reg => reg.Role == targetRole)
                .Where(reg => reg.Type == targetTypeConst ||
                    reg.Type == targetTypeTemp ||
                    reg.Type == targetTypeHotel)
                .Where(reg => IsFreshPeriod(reg))
                .ToList();
            return registrations;
        }

        public static bool IsChild(DateTime bDate)
        {
            var age = GetAge(bDate);
            return age < Consts.CHILD_AGE_TOP_BORDER;
        }

        public static int GetAge(DateTime bDate)
        {
            var now = DateTime.Today;
            int age = now.Year - bDate.Year;
            if (bDate > now.AddYears(-age)) age--;
            return age;
        }

        public static List<(int, Dossier)> GetRowNumbersAndPersonsFromResultTable(IList<IWebElement> list)
        {
            var result = new List<(int, Dossier)>();
            for (var i = 0; i < list.Count; i++)
            {
                var personArr = list[i].Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //7 - количество колонок таблицы
                if (personArr.Length < 7)
                    continue;
                Dossier person = new Dossier();
                person.Fullname = personArr[0];
                DateTime.TryParse(personArr[1], out DateTime bDate);
                person.Bdate = bDate;
                person.Nationality = personArr[2];
                person.Documents.Add(new Document
                {
                    Type = personArr[3],
                    SeriesNumber = personArr[4],
                    Status = personArr[6]
                });
                result.Add((i, person));
            }
            return result;
        }

        public static string FirstDateOfActualYear()
        {
            var result = "18.02.2022";
            var today = DateTime.Today.ToShortDateString();
            var arr = today.Split('.');
            if(arr.Length == 3)
            {
                result = "01.01." + arr[2];
            }
            return result;
        }

        public static string GetPeriodFromDates(Registration registration)
        {
            if (registration.DateIn == null || registration.DateOut == null)
            {
                return String.Empty;
            }
            string dateout = string.Empty;
            if (registration.DateOut == Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT)
            {
                dateout = Consts.ESFL_REG_UNTIL_NOW;
            }
            else
            {
                dateout = registration.DateOut.ToShortDateString();
            }
            var result = $"{registration.DateIn.ToShortDateString()} - {dateout}";
            return result;
        }

        public static int ParseStartRow(string currentRowNumber)
        {
            var isParsed = int.TryParse(currentRowNumber, out int startRow);
            if (!isParsed || startRow == 0)
                startRow = Consts.DEFAULT_START_ROW;
            return startRow;
        }

        public static int ParseStartRow(int currentRowNumber)
        {
            if (currentRowNumber == 0)
                currentRowNumber = Consts.DEFAULT_START_ROW;
            return currentRowNumber;
        }

        public static void KillProcess(int pid)
        {
            var process = Process.GetProcessById(pid);
            if (!process.HasExited)
                process.Kill();
        }

        public static int GetParentProcess(int Id)
        {
            int parentId = 0;
            using (ManagementObject mo = new ManagementObject($"win32_process.handle='{Id}'"))
            {
                try
                {
                    mo.Get();
                    parentId = Convert.ToInt32(mo["ParentProcessId"]);
                }
                catch (Exception)
                {
                    parentId = -1;
                }
            }
            return parentId;
        }

        public static void KillChildBrowserProcesses(int pid, string browserProcessName)
        {
            var browserProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == browserProcessName);

            foreach (var browserProcess in browserProcesses)
            {
                var parentId = GetParentProcess(browserProcess.Id);
                if (parentId == pid)
                {
                    browserProcess.Kill();
                }
            }
        }

        public static void KillChildDriverProcesses(int pid, string driverProcessName)
        {
            var driverProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == driverProcessName);

            foreach (var driverProcess in driverProcesses)
            {
                var parentId = GetParentProcess(driverProcess.Id);
                if (parentId == pid)
                {
                    driverProcess.Kill();
                }
            }
        }

        public static int GetCurrentProcess()
        {
            var result = Process.GetCurrentProcess().Id;
            return result;
        }
    }
}