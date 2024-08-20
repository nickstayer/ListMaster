using OpenQA.Selenium;
using System;

namespace ListMaster.gismu.russianPassport
{
    class PassportPage
    {
        private readonly Browser _browser;
        private readonly By _exitButtonSpan = By.XPath("//span[contains(text(),'Выход')]");
        private readonly By _formOneTitle = By.XPath("//span[contains(text(),'Форма № 1')]");
        private readonly By _passportPageTitle = By.XPath("//h1[contains(text(),'Сведения о паспорте')]");
        private readonly By _digitalDocsBlockMarker = By.XPath("//span[contains(text(),'Тип')]");
        private readonly By _digitalDocsButton = By.XPath("//div[contains(text(),'Электронные копии документов')]");

        public PassportPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToSearchPage()
        {
            var exitButtonSpan = _browser.GetElement(_exitButtonSpan);
            var exitButton = _browser.GetParentElement(exitButtonSpan);
            while (IamHere())
            {
                exitButton.Click();  
            }
            _browser.WaitFor(Consts.WAIT_3_SEC);
            return new SearchPage(_browser);
        }

        public bool HasFormOne()
        {
            _browser.ExpandBlock(_digitalDocsButton, _digitalDocsBlockMarker);
            var result = _browser.ExistElement(_formOneTitle);
            return result;
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new Exception(Consts.ERROR_LOST_BROWSER);
            return _browser.ExistElement(_passportPageTitle);
        }
    }
}