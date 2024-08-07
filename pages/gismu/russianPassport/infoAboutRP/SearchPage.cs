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

        public SearchPage(Browser browser)
        {
            _browser = browser;
        }

        public void EnterData(Dossier person)
        {
            var passField = _browser.FindAndClick(_passportSeriesInput);
            passField.SendKeys(person.Documents.FirstOrDefault().Series);
            var seriesField = _browser.FindAndClick(_passportNumberInput);
            seriesField.SendKeys(person.Documents.FirstOrDefault().Number);
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

        public List<string> GetDocumentStatus()
        {
            var result = new List<string>();
            IList<IWebElement> rows = GetResultRows();
            foreach (IWebElement row in rows)
            {
                var columns = row.FindElements(_column);
                if (columns.Count == 6)//магическое
                {
                    result.Add(columns[5].Text); 
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