using OpenQA.Selenium;
using System;

namespace ListMaster.ppo.asr.form10
{
    class AsrMenuPage
    {
        private readonly Browser _browser;
        private readonly By _formTenJournalButton = By.XPath("//input[@value='Журнал Форм №10']");

        public AsrMenuPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_formTenJournalButton);
            return new SearchPage(_browser);
        }

        internal bool IamHere()
        {
            return _browser.ExistElement(_formTenJournalButton);
        }
    }
}
