using OpenQA.Selenium;

namespace ListMaster.asrp
{
    class LoginFormPage
    {
        private readonly Browser _browser;
        private readonly Settings _settings;

        private readonly string _mainFrame = "main";
        private readonly By _usernameInput = By.Id("j_username");
        private readonly By _passwordInput = By.Id("j_password_clear");
        private readonly By _submitButton = By.XPath("//input[@value='Войти']");

        public LoginFormPage(Browser browser, Settings settings)
        {
            _browser = browser;
            _settings = settings;
            _browser.SwitchToMainFrame(_mainFrame);
        }

        public MainMenuPage LogIn()
        {
            var usernameInput = _browser.FindAndClick(_usernameInput);
            usernameInput.SendKeys(_settings.Username);
            var passwordInput = _browser.FindAndClick(_passwordInput);
            passwordInput.SendKeys(_settings.Password);
            _browser.FindAndClick(_submitButton);
            return new MainMenuPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_submitButton);
        }
    }
}
