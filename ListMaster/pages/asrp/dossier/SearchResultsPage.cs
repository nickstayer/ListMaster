using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ListMaster.asrp.dossier
{
    class SearchResultsPage
    {
        private Browser _browser;
        private readonly By _resultsTable = By.Id("dossier_form:j_id116:tbl_results:tb");
        private readonly By _resultsTableRows = By.TagName("tr");
        private readonly By _clearifyButton = By.XPath("//input[@value='Уточнить запрос']");

        public SearchResultsPage(Browser browser)
        {
            _browser = browser;
        }

        public bool ArePersonFinded()
        {
            return _browser.ExistElement(_resultsTable);
        }

        public DossierPage OpenDossier(By locator)
        {
            do
            {
                _browser.FindAndClick(locator);
                Thread.Sleep(1000);
            }
            while (_browser.ExistElement(locator));
            return new DossierPage(_browser);
        }

        public List<By> GetActualDocLocators()
        {
            var result = GetLocators(":j_id156")
                .Where(loc => _browser.GetElement(loc).Text == Consts.ASRP_DOC_STATUS_VALID)
                .ToList();
            return result;
        }

        private List<By> GetLocators(string columnId)
        {
            List<By> links = new List<By>();
            for (var i = 0; i < GetResultsRows().Count; i++)
            {
                var locator = By.Id($"dossier_form:j_id116:tbl_results:{i}{columnId}");
                links.Add(locator);
            }
            return links;
        }

        private IList<IWebElement> GetResultsRows()
        {
            var table = _browser.Driver.FindElement(_resultsTable);
            var rows = table.FindElements(_resultsTableRows);
            return rows;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_clearifyButton);
            return new SearchPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_clearifyButton);
        }
    }
}
