using OpenQA.Selenium;
using System;

namespace ListMaster.ppo
{
    class MainMenuPage
    {
        private readonly Browser _browser;
        private readonly By _dossierLink = By.XPath("//a[text()='Досье']");
        private readonly By _asrLink = By.XPath("//a[text()='Адресно-справочная работа']");
        private readonly By _logDialogDiv = By.XPath("//div[@id='aboutDlg' and contains(@style, 'display: block')]");
        private readonly By _closeLogButton = By.Id("closeLogLink");

        public MainMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public dossier.DossierMenuPage GoToDossierMenuPage()
        {
            if (_browser.ExistElement(_logDialogDiv))
                _browser.FindAndClick(_closeLogButton);
            _browser.FindAndClick(_dossierLink);
            return new dossier.DossierMenuPage(_browser);
        }

        public asr.form10.AsrMenuPage GoToAsrMenuPage()
        {
            if (_browser.ExistElement(_logDialogDiv))
                _browser.FindAndClick(_closeLogButton);
            _browser.FindAndClick(_asrLink);
            return new asr.form10.AsrMenuPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_dossierLink);
        }
    }
}
