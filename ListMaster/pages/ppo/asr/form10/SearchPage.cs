using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace ListMaster.ppo.asr.form10
{
    class SearchPage
    {
        private readonly Browser _browser;
        private readonly By _lastNameInput = By.XPath($"//input[@id='lastName:text']");
        private readonly By _firstNameInput = By.XPath("//input[@id='firstName:text']");
        private readonly By _middleNameInput = By.XPath("//input[@id='middleName:text']");
        private readonly By _bDateInput = By.XPath("//input[@id='birthDate:date']");
        private readonly By _inDateInput = By.XPath("//input[@id='arrivalPeriod:startDate:date']");
        private readonly By _outDateInput = By.XPath("//input[@id='arrivalPeriod:endDate:date']");
        private readonly By _resetButton = By.Id("clearButton");
        private readonly By _submitButton = By.Id("searchButton");
        private readonly By _resultTableLink = By.CssSelector("#form5SearchResultTable_data a");
        private readonly By LoadingElement = By.XPath("//div[@id='statusDialog' and @style='display: none;']");

        public SearchPage(Browser browser)
        {
            _browser = browser;
        }

        public void EnterData(Dossier person)
        {
            Clear();
            var lastNameField = _browser.FindAndClick(_lastNameInput);
            lastNameField.SendKeys(person.Lastname);
            var firstNameField = _browser.FindAndClick(_firstNameInput);
            firstNameField.SendKeys(person.Firstname);
            if (!string.IsNullOrEmpty(person.Othername))
            {
                var middleNameField = _browser.FindAndClick(_middleNameInput);
                middleNameField.SendKeys(person.Othername);
            }
            var bDateField = _browser.FindAndClick(_bDateInput);
            bDateField.SendKeys(person.Bdate.ToShortDateString());
            var inDateField = _browser.FindAndClick(_inDateInput);
            inDateField.SendKeys(Utils.FirstDateOfActualYear());
            var outDateField = _browser.FindAndClick(_outDateInput);
            var todayYear = int.Parse(DateTime.Today.Year.ToString());
            var lastYearDate = new DateTime(todayYear, 12, 31);
            var outDateDefault = lastYearDate.ToShortDateString();
            outDateField.SendKeys(outDateDefault);
            Search();
        }

        public void Clear()
        {
            _browser.FindAndClick(_resetButton);
        }

        public void Search()
        {
            _browser.FindAndClick(_submitButton);
            _browser.WaitElement(LoadingElement);
        }

        public bool ExistSearchResultsByLinksCount()
        {
            var links = _browser.Driver.FindElements(_resultTableLink);
            var result = links.Count > 0;
            return result;
        }

        public IList<IWebElement> GetResultsRows()
        {
            var rows = _browser.Driver.FindElements(_resultTableLink);
            return rows;
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_lastNameInput);
        }
    }
}
