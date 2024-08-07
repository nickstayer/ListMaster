using OpenQA.Selenium;

namespace ListMaster.ppo.dossier
{
    class DossierMenuPage
    {
        private readonly Browser _browser;
        private readonly By _dossierOldButton = By.XPath("//input[@value='Досье (старая версия)']");

        public DossierMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public ConfirmPage GoToDossierOldConfirmPage()
        {
            _browser.FindAndClick(_dossierOldButton);
            return new ConfirmPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_dossierOldButton);
        }
    }
}
