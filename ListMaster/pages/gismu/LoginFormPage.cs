using OpenQA.Selenium;

namespace ListMaster.gismu
{
    class LoginFormPage
    {
        private readonly Browser _browser;
        private readonly Settings _settings;
        private readonly By _usernameInput = By.CssSelector("#username input");
        private readonly By _passwordInput = By.CssSelector("#password input");
        private readonly By _submitSpan = By.CssSelector(".el-button--primary");
        private readonly By _errorLogin = By.XPath("//p[contains(text(),'Неверная пара логин/пароль.')]");

        public LoginFormPage(Browser browser, Settings settings)
        {
            _browser = browser;
            _settings = settings;
        }

        public GismuMenuPage LogIn()
        {
            var usernameInput = _browser.FindAndClick(_usernameInput);
            usernameInput.SendKeys(_settings.Username);
            var passwordInput = _browser.FindAndClick(_passwordInput);
            passwordInput.SendKeys(_settings.Password);
            _browser.FindAndClick(_submitSpan);
            int counter = 0;

            while (_browser.ExistElement(_errorLogin) 
                && counter < Consts.DEFAULT_TRY_REPEAT_LOGIN_COUNT)
            {
                _browser.FindAndClick(_submitSpan);
                counter++;
            }
            return new GismuMenuPage(_browser);
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new System.Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_usernameInput);
        }
    }
}