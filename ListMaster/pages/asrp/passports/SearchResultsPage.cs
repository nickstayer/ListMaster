using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ListMaster.asrp.passports
{
    class SearchResultsPage
    {
        private Browser _browser;
        private readonly By _resultsTableRows = By.Id("passp_search_results_form:tbl_results:0:datarow_0");
        private readonly By _clearifyButton = By.XPath("//input[@value='Уточнить запрос']");

        public SearchResultsPage(Browser browser)
        {
            _browser = browser;
        }

        public DossierPage OpenDossier()
        {
            do
            {
                _browser.FindAndClick(_resultsTableRows);
                Thread.Sleep(1000);
            }
            while (_browser.ExistElement(_resultsTableRows));
            return new DossierPage(_browser);
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_clearifyButton);
            return new SearchPage(_browser);
        }

        internal bool IamHere()
        {
            return _browser.ExistElement(_clearifyButton);
        }
    }
}
