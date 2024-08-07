using OpenQA.Selenium;
using System;
using System.Threading;

namespace ListMaster.gismu.esfl
{
    class EsflMenuPage
    {
        private readonly Browser _browser;
        private readonly By _esflSearch = By.XPath("//p[contains(text(),'Поиск физических лиц')]");

        public EsflMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.PersistentClick(_esflSearch);
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