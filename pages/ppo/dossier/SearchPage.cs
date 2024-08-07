using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListMaster.ppo.dossier
{
    class SearchPage
    {
        private readonly Browser _browser;
        private readonly By _searchDossier_PassportInput = By.XPath("//input[@id='docSeriesSearchField:text']");
        private readonly By _searchDossierh_FIOInput = By.XPath("//input[@id='persFIOSearchField:text']");
        private readonly By _search_BDateInput = By.XPath("//input[@id='persBirthDateSearchField:date']");
        private readonly By _search_ResetButton = By.XPath("//button[@id='clear_search_panel']");
        private readonly By _search_SubmitButton = By.XPath("//button[@id='search_button_submit']");
        private readonly By _search_ResultTableRows = By.CssSelector("#table_data tr");
        private readonly By _search_ResultTableCells = By.CssSelector("#table_data tr td div");
        private readonly By _search_ResultCell = By.CssSelector("td div");
        private readonly By _searchDossier_ResultTableCells = By.CssSelector("#table_data tr td div");
        private readonly By _searchDossier_viewDossierButton = By.XPath("//a[@id='viewDossierButton']");
        private readonly By LoadingElement = By.XPath("//div[@id='statusDialog' and @style='display: none;']");
        private readonly By SearchDossier_mainPageRef = By.XPath("//a[text()='Главная']");

        public SearchPage(Browser browser)
        {
            _browser = browser;
        }

        public void EnterData(Dossier person)
        {
            Clear();
            if (person.Documents.Count > 0)
            {
                var passField = _browser.FindAndClick(_searchDossier_PassportInput);
                passField.SendKeys(person.Documents.FirstOrDefault().SeriesNumber);
            }
            else
            {
                var fioField = _browser.FindAndClick(_searchDossierh_FIOInput);
                fioField.SendKeys(person.Fullname);
                var bDateField = _browser.FindAndClick(_search_BDateInput);
                bDateField.SendKeys(person.Bdate.ToShortDateString());
            }
            Search();
        }

        public void Clear()
        {
            _browser.FindAndClick(_search_ResetButton);
        }

        public void Search()
        {
            _browser.FindAndClick(_search_SubmitButton);
            _browser.WaitElement(LoadingElement);
        }

        public bool HaveResults()
        {
            var resultFirstCell = _browser.Driver.FindElement(_searchDossier_ResultTableCells);
            var result = !resultFirstCell.Text.Contains(Consts.MESSAGE_NO_ENTRIES);
            return result;
        }

        public IList<IWebElement> GetResultsRows()
        {
            var rows = _browser.Driver.FindElements(_search_ResultTableRows);
            return rows;
        }

        public DossierPage OpenDossier()
        {
            _browser.FindAndClick(_searchDossier_viewDossierButton);
            return new DossierPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_searchDossier_PassportInput);
        }

        public MainMenuPage ToMainPage()
        {
            if (_browser.ExistElement(SearchDossier_mainPageRef))
            {
                _browser.FindAndClick(SearchDossier_mainPageRef);
            }
            return new MainMenuPage(_browser);
        }

        public List<string> GetCellsValueFromWebRow(IWebElement row)
        {
            var result = new List<string>();
            var cells = row.FindElements(_search_ResultCell);
            foreach (IWebElement cell in cells)
            {
                result.Add(cell.Text);
            }
            return result;
        }

    }
}
