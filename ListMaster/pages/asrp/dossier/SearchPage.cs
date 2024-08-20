using OpenQA.Selenium;

namespace ListMaster.asrp.dossier
{
    class SearchPage
    {
        private Browser _browser;
        private readonly string _search_DossierLastNameInputId = "search_form:j_id96:dossier_params_lastName";
        
        private By _search_DossierLastNameInput { get {return By.Id(_search_DossierLastNameInputId); } }        
        private readonly By _search_DossierFirstNameInput = By.Id("search_form:j_id96:dossier_params_firstName");
        private readonly By _search_DossierOtherNameInput = By.Id("search_form:j_id96:dossier_params_middleName");
        private readonly By _search_DossierBDateInput = By.Id("search_form:j_id96:dossier_params_birthDateInputDate");
        private readonly By _search_SearchButton = By.XPath("//input[@value='Искать']");
        private readonly By _search_ClearButton = By.XPath("//input[@value='Очистить']");
        private readonly By _loadingElement = By.XPath("//div[contains(@id, ':ajaxLoadingModalBoxCDiv') and @fire]");
       
        public SearchPage(Browser browser)
        {
            _browser = browser;
        }

        public void EnterData(Dossier person)
        {
            var lastnameField = _browser.FindAndClick(_search_DossierLastNameInput);
            lastnameField.SendKeys(person.Lastname);
            var firstnameField = _browser.FindAndClick(_search_DossierFirstNameInput);
            firstnameField.SendKeys(person.Firstname);
            if(person.Othername != null)
            {
                var othernameField = _browser.FindAndClick(_search_DossierOtherNameInput);
                othernameField.SendKeys(person.Othername);
            }
            var bDateField = _browser.FindAndClick(_search_DossierBDateInput);
            bDateField.SendKeys(person.Bdate.ToShortDateString());
        }

        public SearchResultsPage Search()
        {
            _browser.FindAndClick(_search_SearchButton);
            _browser.WaitWhileElementDisappear(_loadingElement);
            return new SearchResultsPage(_browser);
        }

        public void ClearForm()
        {
            _browser.FindAndClick(_search_ClearButton);
        }

        public bool IamHere()
        {
            return _browser.ExistElementInPageSource(_search_DossierLastNameInputId);
        }
    }
}