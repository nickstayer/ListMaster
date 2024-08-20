using OpenQA.Selenium;

namespace ListMaster.ppo
{
    class LoginFormPage
    {
        private readonly Browser _browser;
        private readonly Settings _settings;
        private readonly By _usernameInput = By.XPath("//input[@id='usernameInput:username']");
        private readonly By _passwordInput = By.XPath("//input[@id='passwordInput:password']");
        private readonly By _submitButton = By.XPath("//button[@id='doLogin']");

        public LoginFormPage(Browser browser, Settings settings)
        {
            _browser = browser;
            _settings = settings;
        }

        public MainMenuPage LogIn()
        {
            do
            {
                var usernameInput = _browser.FindAndClick(_usernameInput);
                usernameInput.SendKeys(_settings.Username);
                var passwordInput = _browser.FindAndClick(_passwordInput);
                passwordInput.SendKeys(_settings.Password);
                _browser.FindAndClick(_submitButton);
            }
            while (CheckBadRequest());

            return new MainMenuPage(_browser);
        }

        public bool CheckBadRequest()
        {
            var badRequestPage = new BadRequestPage(_browser, _settings);
            if (badRequestPage.IamHere())
            {
                badRequestPage.GoToLoginFormPage();
                return true;
            }
            return badRequestPage.IamHere();
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_submitButton);
        }
    }
}
