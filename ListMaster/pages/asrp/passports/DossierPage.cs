using OpenQA.Selenium;

namespace ListMaster.asrp.passports
{
    class DossierPage
    {
        private Browser _browser;
        private readonly By _dossier_FormOneButton = By.Id("passport_view_form:j_id120:j_id206");
        private readonly By _dossier_MenuSearchLink = By.Id("f_menu:j_id86_span");
        public DossierPage(Browser browser)
        {
            _browser = browser;
        }
        public bool IsFormOneUploaded()
        {
            _browser.WaitElement(_dossier_FormOneButton);
            return _browser.Driver.FindElement(_dossier_FormOneButton).Enabled;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_dossier_MenuSearchLink);
            return new SearchPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_dossier_MenuSearchLink);
        }
    }
}
