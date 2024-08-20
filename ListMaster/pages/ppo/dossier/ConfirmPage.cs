using System;
using OpenQA.Selenium;

namespace ListMaster.ppo.dossier
{
    class ConfirmPage
    {
        private readonly Browser _browser;
        private readonly By _dossierOldButton = By.XPath("//button[@id='oldDossierButton']");

        public ConfirmPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToDossierOldSearchPage()
        {
            _browser.FindAndClick(_dossierOldButton);
            return new SearchPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_dossierOldButton);
        }
    }
}
