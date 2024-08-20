using OpenQA.Selenium;
using System.Linq;

namespace ListMaster.asrp.passports
{
    class SearchPage
    {
        private Browser _browser;
        private readonly string _search_PassportSeriesInputId = "incl_main:search_main_form:passportSeries";
        private By _search_PassportSeriesInput { get {return By.Id(_search_PassportSeriesInputId); } }
        private readonly By _search_SearchButton = By.XPath("//input[@value='Искать']");
        private readonly By _search_ClearButton = By.XPath("//input[@value='Очистить']");
        private readonly By _search_PassportNumberInput = By.Id("incl_main:search_main_form:passportNumber");
        private readonly By _loadingElement = By.XPath("//div[contains(@id, ':ajaxLoadingModalBoxCDiv') and @fire]");

        public SearchPage(Browser browser)
        {
            _browser = browser;
        }

        public void EnterData(Dossier person)
        {
            var seriesField = _browser.FindAndClick(_search_PassportSeriesInput);
            seriesField.SendKeys(person.Documents.FirstOrDefault().Series);
            var numberField = _browser.FindAndClick(_search_PassportNumberInput);
            numberField.SendKeys(person.Documents.FirstOrDefault().Number);
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
            return _browser.ExistElementInPageSource(_search_PassportSeriesInputId);
        }
    }
}
