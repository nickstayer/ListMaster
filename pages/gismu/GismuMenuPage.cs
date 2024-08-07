using OpenQA.Selenium;
using System;

namespace ListMaster.gismu
{
    class GismuMenuPage
    {
        private readonly Browser _browser;
        private readonly By _esflP = By.XPath("//p[contains(text(),'ЕСФЛ')]");
        private readonly By _russianPassport = By.XPath("//p[contains(text(),'Российский паспорт')]");
        public static readonly By LoadingElement = By.CssSelector(".ant-spin-dot-item");

        public GismuMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public esfl.EsflMenuPage EsflMenuPage()
        {
            _browser.PersistentClick(_esflP);
            return new esfl.EsflMenuPage(_browser);
        }

        public russianPassport.RussianPassportMenuPage GoToRussianPassportPage()
        {
            _browser.PersistentClick(_russianPassport);
            return new russianPassport.RussianPassportMenuPage(_browser);
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_esflP);
        }
    }
}