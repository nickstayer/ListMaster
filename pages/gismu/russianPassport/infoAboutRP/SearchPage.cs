using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System;

namespace ListMaster.gismu.russianPassport
{
    class SearchPage
    {
        private readonly Browser _browser;
        private readonly By _resetButton = By.XPath("//*[@id=\"app\"]/div/div/div[2]/div/div/form/div/div[2]/div[2]/button");

        private readonly By _submitButton = By.CssSelector("div.ant-form-grid-justify-end button.ant-btn-default");

        private readonly By _passportSeriesInput = By.XPath("//input[@placeholder='Серия']");
        private readonly By _passportNumberInput = By.XPath("//input[@placeholder='Номер']");

        private readonly By _searchResultTable = By.CssSelector("tbody.ant-table-tbody");
        private readonly By _noData = By.XPath("//p[contains(text(),'Нет данных')]");
        private readonly By _resultTableRows = By.CssSelector("tbody.ant-table-tbody tr");
        private readonly By _resultTableColumns = By.CssSelector("tbody.ant-table-tbody tr td");// 6
        private readonly By _column = By.CssSelector("td");
        private readonly By _lastNameInput = By.Id("A_lastName");
        private readonly By _firstNameInput = By.Id("A_firstName");
        private readonly By _middleNameInput = By.Id("A_middleName");
        private readonly By _bDateCalendar = By.CssSelector("#A_birthdayDt input");
        private readonly By _bDateInput = By.CssSelector(".ant-calendar-picker-container input");

        public SearchPage(Browser browser)
        {
            _browser = browser;
        }

        public void EnterData(Dossier person)
        {
            if (person.Documents.Count > 0)
            {
                var passField = _browser.FindAndClick(_passportSeriesInput);
                passField.SendKeys(person.Documents.FirstOrDefault().Series);
                var seriesField = _browser.FindAndClick(_passportNumberInput);
                seriesField.SendKeys(person.Documents.FirstOrDefault().Number); 
            }
            else
            {
                var lastNameField = _browser.FindAndClick(_lastNameInput);
                lastNameField.SendKeys(person.Lastname);
                var firstNameField = _browser.FindAndClick(_firstNameInput);
                firstNameField.SendKeys(person.Firstname);
                if (!string.IsNullOrEmpty(person.Othername))
                {
                    var middleNameField = _browser.FindAndClick(_middleNameInput);
                    middleNameField.SendKeys(person.Othername);
                }
                _browser.FindAndClick(_bDateCalendar);
                var bDateField = _browser.FindAndClick(_bDateInput);
                bDateField.SendKeys(person.Bdate.ToShortDateString());
            }
        }

        public void ClearForm()
        {
            _browser.FindAndClick(_resetButton);
        }

        public void ClickSearchButton()
        {
            _browser.FindAndClick(_submitButton);
            var searchResultTable = _browser.GetElement(_searchResultTable);
            if (searchResultTable == null)
                throw new Exception(Consts.ERROR_CANT_GET_SEARCH_RESULT_TABLE);
        }

        public List<Document> GetDocuments()
        {
            var result = new List<Document>();
            IList<IWebElement> rows = GetResultRows();
            foreach (IWebElement row in rows)
            {
                var document = new Document();
                var columns = row.FindElements(_column);
                if (columns.Count == 6)// количество колонок в таблице выдачи
                {
                    document.Series = columns[2].Text;
                    document.Number = columns[3].Text;
                    document.Date = columns[4].Text;
                    document.Status = columns[5].Text;
                    result.Add(document);
                } 
            }
            return result;
        }

        public PassportPage ViewPassport(IWebElement resultRow)
        {
            Actions action = new Actions(_browser.Driver);
            action.MoveToElement(resultRow).DoubleClick().Perform();
            return new PassportPage(_browser);
        }
        

        public IList<IWebElement> GetResultRows()
        {
            IList<IWebElement> rows = new List<IWebElement>();
            double counter = 0;
            while (counter < Consts.WAIT_30_SEC)
            {
                var noData = _browser.ExistElement(_noData);
                if (noData)
                    break;
                rows = _browser.Driver.FindElements(_resultTableRows);
                if (rows.Count > 0)
                    break;
                Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                counter += Consts.WAIT_3_SEC;
            }
            return rows;
        }

        public bool HaveResults()
        {
            var result = _browser.Driver.FindElements(_resultTableColumns).Count > 1;
            return result;
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_passportSeriesInput);
        }
    }
}