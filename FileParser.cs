using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExceLib;
using System.Text;

namespace ListMaster
{
    public class FileParser
    {
        private readonly ExcelApp _excelApp;
        private readonly Settings _settings;
        public FileParser(ExcelApp excelApp, Settings settings)
        {
            _excelApp = excelApp;
            _settings = settings;
        }

        public FileParser()
        {

        }

        public IEnumerable<Dossier> Read()
        {
            while (_excelApp.CurrentRow <= _excelApp.LastRow)
            {
                if (_excelApp.IsRowHidden(_excelApp.CurrentRow))
                {
                    _excelApp.IncreaseRow();
                    continue;
                }

                var person = new Dossier();
                AddName(person, GetName());
                AddBdate(person, GetBdate());
                AddDocument(person, GetDocument());
                AddRegistration(person, GetRegistration());
                yield return person;
                _excelApp.IncreaseRow();
            }
        }

        #region Name

        private Dossier GetName()
        {
            var person = new Dossier();
            person.Lastname = ParseName(_excelApp.GetText(_settings.Lastname));
            person.Firstname = ParseName(_excelApp.GetText(_settings.Firstname));
            person.Othername = ParseName(_excelApp.GetText(_settings.Othername));
            person.Fullname = ParseName(_excelApp.GetText(_settings.Fullname));
            CompleteNameFields(person);
            return person;
        }

        // for complite-Test (ParseFullNameWithFourPartTest)
        public Dossier GetName(string fullname)
        {
            var person = new Dossier();
            person.Fullname = ParseName(fullname);
            CompleteNameFields(person);
            return person;
        }

        public static string ParseName(string name)
        {
            var nameArr = name.Trim().Split().ToArray();
            var nameParts = new StringBuilder();
            foreach (var namePart in nameArr)
            {
                if (namePart == string.Empty)
                {
                    return string.Empty;
                }
                if (namePart.StartsWith(Consts.NAME_PARSE_EXTRA_SYMBOL))
                {
                    continue;
                }
                nameParts.Append(namePart);
                nameParts.Append(Consts.NAME_PARSE_DELIMITER);
            }
            var result = nameParts.ToString().Trim();
            return result;
        }

        public void CompleteNameFields(Dossier person)
        {
            if (string.IsNullOrEmpty(person.Lastname)
                && !string.IsNullOrEmpty(person.Fullname))
            {
               CompleteNameParts(person);
            }
            else if (!string.IsNullOrEmpty(person.Lastname)
                && string.IsNullOrEmpty(person.Fullname))
            {
                CompleteFullName(person);
            }
        }

        public void CompleteNameParts(Dossier person)
        {
            var fioArr = person.Fullname.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray();
            string othername = null;
            for(var i = 0; i<fioArr.Length; i++)
            {
                if(i == 0)
                {
                    person.Lastname = ParseName(fioArr[i]);
                }
                if (i == 1)
                {
                    person.Firstname = ParseName(fioArr[i]);
                }
                if (i > 1)
                {
                    othername += ParseName(fioArr[i]) + Consts.NAME_PARSE_DELIMITER;
                }
            }
            person.Othername = othername?.Trim();
        }

        public void CompleteFullName(Dossier person)
        {
            person.Fullname = $"{person.Lastname} {person.Firstname} {person.Othername}".Trim();
        }

        public void AddName(Dossier person, Dossier personName)
        {
            person.Lastname = personName.Lastname;
            person.Firstname = personName.Firstname;
            person.Othername = personName.Othername;
            person.Fullname = personName.Fullname;
        }
        #endregion

        #region bDate
        private Dossier GetBdate()
        {
            var person = new Dossier();
            person.Bdate = _excelApp.GetDate(_settings.Bdate);
            return person;
        }

        public void AddBdate(Dossier person, Dossier personBdate)
        {
            person.Bdate = personBdate.Bdate;
        } 
        #endregion

        #region Document

        private Dossier GetDocument()
        {
            var person = new Dossier();
            if (_settings.DocumentNumber != 0 || _settings.DocumentSeriesNumber != 0)
            {
                var series = ParseSeries(_excelApp.GetText(_settings.DocumentSeries));
                var number = ParseNumber(_excelApp.GetText(_settings.DocumentNumber));
                var seriesNumber = ParseSeriesNumber(_excelApp.GetText(_settings.DocumentSeriesNumber));

                if (!string.IsNullOrWhiteSpace(series)
                    || !string.IsNullOrWhiteSpace(number)
                    || !string.IsNullOrWhiteSpace(seriesNumber))
                {
                    person.Documents.Add(new Document());

                    person.Documents.FirstOrDefault().Series
                        = ParseSeries(_excelApp.GetText(_settings.DocumentSeries));
                    person.Documents.FirstOrDefault().Number
                        = ParseNumber(_excelApp.GetText(_settings.DocumentNumber));
                    person.Documents.FirstOrDefault().SeriesNumber
                        = ParseSeriesNumber(_excelApp.GetText(_settings.DocumentSeriesNumber));
                    CompleteDocumentFields(person); 
                }
            }
            return person;
        }

        public static string ParseSeries(string series)
        {
            string pattern1 = @"\d{4}";
            string pattern2 = @"\d+";
            if (Regex.IsMatch(series, pattern1))
            {
                return series;
            }
            else if (Regex.IsMatch(series, pattern2))
            {
                return Normalize(series, 4);
            }
            return string.Empty;
        }

        public static string ParseNumber(string number)
        {
            string pattern1 = @"\d{6}";
            string pattern2 = @"\d+";
            if (Regex.IsMatch(number, pattern1))
            {
                return number;
            }
            else if (Regex.IsMatch(number, pattern2))
            {
                return Normalize(number, 6);
            }
            return string.Empty;
        }

        public static string ParseSeriesNumber(string seriesNumber)
        {
            seriesNumber = seriesNumber.Replace(" ", "");
            string pattern = @"\d{10}";
            if (Regex.IsMatch(seriesNumber, pattern))
            {
                return seriesNumber;
            }
            return string.Empty;
        }

        public static string ParseDepartmentCode(string code)
        {
            string pattern1 = @"\d{3}-\d{3}";
            string pattern2 = @"\d{6}";
            if (Regex.IsMatch(code, pattern1))
            {
                return code;
            }
            if (Regex.IsMatch(code, pattern2))
            {
                var codepart1 = code.Substring(0, 3);
                var codepart2 = code.Substring(3);
                return $"{codepart1}-{codepart2}";
            }
            return string.Empty;
        }

        public static string Normalize(string number, int digitCount)
        {
            while (number.Length != digitCount)
            {
                number = "0" + number;
            }
            return number;
        }

        public void CompleteDocumentFields(Dossier person)
        {
            if(person.Documents.Count == 0)
            {
                return;
            }
            var series = person.Documents.FirstOrDefault().Series;
            var seriesAndNumber = person.Documents.FirstOrDefault().SeriesNumber;
            if (string.IsNullOrEmpty(series)
                && !string.IsNullOrEmpty(seriesAndNumber))
            {
                CompleteSeriesAndNumber(person);
            }
            else if (!string.IsNullOrEmpty(series)
                && string.IsNullOrEmpty(seriesAndNumber))
            {
                CompleteSeriesNumber(person);
            }
        }

        private void CompleteSeriesAndNumber(Dossier person)
        {
            if (person.Documents.Count == 0)
            {
                return;
            }
            var seriesAndNumber = person.Documents.FirstOrDefault().SeriesNumber.Replace(" ", "");
            person.Documents.FirstOrDefault().Series = seriesAndNumber.Substring(0, 4);
            person.Documents.FirstOrDefault().Number = seriesAndNumber.Substring(4, 6); 
        }

        private void CompleteSeriesNumber(Dossier person)
        {
            if (person.Documents.Count == 0)
            {
                return;
            }
            person.Documents.FirstOrDefault().SeriesNumber =
                $"{person.Documents.FirstOrDefault().Series} {person.Documents.FirstOrDefault().Number}";
        }

        public void AddDocument(Dossier person, Dossier personDocument)
        {
            if (personDocument.Documents.Count == 0)
            {
                return;
            }
            person.Documents.Add(new Document());
            person.Documents.FirstOrDefault().Series = personDocument.Documents.FirstOrDefault().Series;
            person.Documents.FirstOrDefault().Number = personDocument.Documents.FirstOrDefault().Number;
            person.Documents.FirstOrDefault().SeriesNumber = personDocument.Documents.FirstOrDefault().SeriesNumber; 
        }
        #endregion

        #region Registration
        private Dossier GetRegistration()
        {
            var person = ParseRegistration(_excelApp.GetText(_settings.Registration));
            if (person.Registrations.Count == 0)
            {
                return person;
            }
            CompleteRegistrationFields(person.Registrations.FirstOrDefault());
            return person;
        }

        public Dossier ParseRegistration(string registration)
        {
            var regArr = registration
                .Split(Consts.SEPARATOR_REGISTRATION)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray();
            var person = new Dossier();
            if (regArr.Length == 3)
            {
                person.Registrations.Add(new Registration
                {
                    Type = regArr[0],
                    Period = regArr[1],
                    Address = regArr[2],
                });
            }
            return person;
        }

        //for tests only (should delete)
        public void CompleteRegistrationFields(Dossier person)
        {
            if (person.Registrations.Count == 0)
            {
                return;
            }
            CompleteRegistrationFields(person.Registrations.FirstOrDefault());
        }

        public static void CompleteRegistrationFields(Registration registration)
        {
            if (registration == null)
            {
                return;
            }
            if (registration.DateIn == default)
            {
                var dateIn = GetDatesFromPeriod(registration.Period).Item1;
                var dateOut = GetDatesFromPeriod(registration.Period).Item2;
                registration.DateIn = dateIn;
                registration.DateOut = dateOut;
            }
            else if (String.IsNullOrWhiteSpace(registration.Period))
            {
                var period = Utils.GetPeriodFromDates(registration);
                registration.Period = period;
            }
        }

        public static (DateTime, DateTime) GetDatesFromPeriod(string period)
        {
            var dates = period.Split(Consts.SEPARATOR_PERIOD).Select(x => x.Trim()).ToArray();
            DateTime dateIn = default;
            DateTime dateOut = default;
            if (dates.Length > 0)
            {
                DateTime.TryParse(dates[0], out dateIn);
                if (dates.Length > 1)
                {
                    DateTime.TryParse(dates[1], out dateOut);
                }
            }
            return (dateIn, dateOut);
        }



        public void AddRegistration(Dossier person, Dossier personReg)
        {
            if (personReg.Registrations.Count == 0)
            {
                return;
            }
            person.Registrations.Add(new Registration
            {
                Type = personReg.Registrations.FirstOrDefault().Type,
                Period = personReg.Registrations.FirstOrDefault().Period,
                Address = personReg.Registrations.FirstOrDefault().Address,
                DateIn = personReg.Registrations.FirstOrDefault().DateIn,
                DateOut = personReg.Registrations.FirstOrDefault().DateOut
            }); 
        }
        #endregion
    }
}
