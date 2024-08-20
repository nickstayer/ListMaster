using OpenQA.Selenium;

namespace ListMaster.ppo
{
    class BadRequestPage
    {
        private readonly Browser _browser;
        private readonly Settings _settings;
        private readonly By _errorRequest1 = By.XPath("//div[contains(text(),'Истекло время бездействия')]");
        private readonly By _enter = By.XPath("//a[contains(text(),'вход')]");

        public BadRequestPage(Browser browser, Settings settings)
        {
            _browser = browser;
            _settings = settings;   
        }

        public LoginFormPage GoToLoginFormPage()
        {
            _browser.FindAndClick(_enter);
            return new LoginFormPage(_browser, _settings);
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new System.Exception(Consts.ERROR_LOST_BROWSER);
            var result = _browser.ExistElement(_errorRequest1);
            return result;
        }
    }
}
//TODO: добавить в GetSearchPage