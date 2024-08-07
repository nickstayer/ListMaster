using OpenQA.Selenium;

namespace ListMaster.gismu
{
    class ChangePasswordPage
    {
        private readonly Browser _browser;
        private readonly By _personalCabinet = By.XPath("//h1[contains(text(),'Личный кабинет пользователя')]");

        public ChangePasswordPage(Browser browser)
        {
            _browser = browser;
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new System.Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_personalCabinet);
        }

        public GismuMenuPage GoToGismuMenuPage()
        {
            _browser.GoTo();
            return new GismuMenuPage(_browser);
        }
    }
}