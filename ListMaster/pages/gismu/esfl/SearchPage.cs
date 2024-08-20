using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System;

namespace ListMaster.gismu.esfl
{
    class SearchPage
    {
        private readonly Browser _browser;
        private readonly By _resetButton = By.XPath("//button[@type='reset']");
        private readonly By _passportSeriesInput = By.XPath("//input[@id='docSeries']");
        private readonly By _passportNumberInput = By.XPath("//input[@id='docNumber']");
        private readonly By _lastNameInput = By.XPath("//input[@id='lastName']");
        private readonly By _firstNameInput = By.XPath("//input[@id='firstName']");
        private readonly By _middleNameInput = By.XPath("//input[@id='middleName']");
        private readonly By _bDateCalendar = By.CssSelector("#birthDate input"); 
        private readonly By _bDateInput = By.CssSelector(".ant-calendar-picker-container input");
        private readonly By _submitButton = By.XPath("//button[@type='submit']");
        private readonly By _noData = By.XPath("//p[contains(text(),'Нет данных')]");
        private readonly By _searchResultTable = By.Id("search-result-table");

        private readonly By _dossierLinks = By.CssSelector(".ant-table-fixed-right table tbody tr");
        private readonly By _threeVerticalDots = By.TagName("a");
        private readonly By _viewDossier = By.XPath("//span[contains(text(),'Посмотреть досье ФЛ')]");
        private readonly By _paginatorDiv = By.CssSelector(".ant-pagination-options-size-changer > div");
        private readonly By _paginatorSelector = By.XPath("//span[contains(text(),'Показать все')]");
        private readonly By _pageSelectorSeveralPageMarker = By.CssSelector("li.ant-pagination-item:nth-child(4) > a");

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
            var seriesField = _browser.Driver.FindElement(_passportSeriesInput).GetAttribute("value");
            var numberField = _browser.Driver.FindElement(_passportNumberInput).GetAttribute("value");
            var lastNameField = _browser.Driver.FindElement(_lastNameInput).GetAttribute("value");
            var firstNameField = _browser.Driver.FindElement(_firstNameInput).GetAttribute("value");

            while (!string.IsNullOrEmpty(seriesField)
                || !string.IsNullOrEmpty(numberField)
                || !string.IsNullOrEmpty(lastNameField)
                || !string.IsNullOrEmpty(firstNameField))
            {
                _browser.FindAndClick(_resetButton);
                seriesField = _browser.Driver.FindElement(_passportSeriesInput).GetAttribute("value");
                numberField = _browser.Driver.FindElement(_passportNumberInput).GetAttribute("value");
                lastNameField = _browser.Driver.FindElement(_lastNameInput).GetAttribute("value");
                firstNameField = _browser.Driver.FindElement(_firstNameInput).GetAttribute("value");
            }
        }

        public void ClickSearchButton()
        {
            _browser.FindAndClick(_submitButton);
            var searchResultTable = _browser.GetElement(_searchResultTable);
            if (searchResultTable == null)
                throw new Exception(Consts.ERROR_CANT_GET_SEARCH_RESULT_TABLE);
        }

        public void ShowAllDossierLinksPages()
        {
            if (HaveSeveralPages())
            {
                IWebElement pageSelector = null;
                while (pageSelector == null)
                {
                    _browser.FindAndClick(_paginatorDiv);
                    pageSelector = _browser.FindAndClick(_paginatorSelector);
                }
                _browser.WaitWhileElementDisappear(GismuMenuPage.LoadingElement); 
            }
        }

        public bool HaveSeveralPages()
        {
            return _browser.ExistElement(_pageSelectorSeveralPageMarker);
        }

        public IList<IWebElement> GetDossierLinks()
        {
            IList<IWebElement> dossierLinks = new List<IWebElement>();
            double counter = 0;
            while (counter < Consts.WAIT_30_SEC)
            {
                var noData = _browser.ExistElement(_noData);
                if (noData)
                    break;
                dossierLinks = _browser.Driver.FindElements(_dossierLinks);
                int dossierLinksCount = dossierLinks.Count;
                if (dossierLinksCount > 0)
                    break;
                Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                counter += Consts.WAIT_3_SEC;
            }
            return dossierLinks;
        }

        public DossierPage OpenDossier(IWebElement row)
        {
            double counter = 0;
            while (counter < Consts.WAIT_30_SEC)
            {
                var more = row.FindElement(_threeVerticalDots);
                more.Click();
                Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));

                var viewDossierButtons = row.FindElements(_viewDossier);
                var buttons = viewDossierButtons.Where(el => el.Displayed).ToList();
                if (buttons.Count == 0)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                    counter += Consts.WAIT_3_SEC;
                    continue;
                }
                var button = buttons.ElementAt(0);
                button.Click();
                break;
            }

            _browser.WaitForTheNewTab();
            _browser.SwitchToAnotherTab();
            _browser.WaitWhileElementDisappear(GismuMenuPage.LoadingElement);
            return new DossierPage(_browser);
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_passportSeriesInput);
        }
    }
}