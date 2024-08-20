using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace ListMaster.asrp
{
    class MainMenuPage
    {
        private Browser _browser;

        private readonly By _menu_passport = By.Id("f_menu:j_id81_span");
        private readonly By _menu_passport_search = By.Id("f_menu:j_id82:anchor");

        private readonly By _menu_dossier = By.Id("f_menu:j_id101_span");
        private readonly By _menu_dossier_search = By.Id("f_menu:j_id102:anchor");

        public MainMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public passports.SearchPage GoToPassportSearchPage()
        {
            Actions action = new Actions(_browser.Driver);
            _browser.WaitElement(_menu_passport);
            IWebElement menuButton = _browser.Driver.FindElement(_menu_passport);
            action.MoveToElement(menuButton).Build().Perform();
            action.MoveToElement(_browser.Driver.FindElement(_menu_passport_search)).Build().Perform();
            do
            {
                action.Click().Build().Perform();
                Thread.Sleep(1000);
            }
            while (_browser.ExistElement(_menu_passport_search));
            return new passports.SearchPage(_browser);
        }

        public dossier.SearchPage GoToDossierSearchPage()
        {
            Actions action = new Actions(_browser.Driver);
            _browser.WaitElement(_menu_dossier);
            IWebElement menuButton = _browser.Driver.FindElement(_menu_dossier);
            action.MoveToElement(menuButton).Build().Perform();
            action.MoveToElement(_browser.Driver.FindElement(_menu_dossier_search)).Build().Perform();
            do
            {
                action.Click().Build().Perform();
                Thread.Sleep(1000);
            }
            while (_browser.ExistElement(_menu_dossier_search));
            return new dossier.SearchPage(_browser);
        }
    }
}
