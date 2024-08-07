using OpenQA.Selenium;
using System;
using System.Threading;

namespace ListMaster.gismu.russianPassport
{
    class RussianPassportMenuPage
    {
        private readonly Browser _browser;
        private readonly By _esflSearch = By.XPath("//p[contains(text(),'Поиск физических лиц')]");
        private readonly By _keepingRecordsButton = By.XPath("//div[contains(text(),'Ведение учетов')]");
        private readonly By _infoAboutRussianPassports = By.XPath("//p[contains(text(),'Сведения о российских паспортах')]");

        public RussianPassportMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public void ExpandKeepingRecords()
        {
            _browser.ExpandBlock(_keepingRecordsButton, _infoAboutRussianPassports);
        }

        public SearchPage GoToSearchPage()
        {
            ExpandKeepingRecords();
            _browser.PersistentClick(_infoAboutRussianPassports);
            return new SearchPage(_browser);
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_esflSearch);
        }
    }
}