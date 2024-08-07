using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace ListMaster.ppo.dossier
{
    class DossierPage
    {
        private readonly Browser _browser;
        private readonly By _dossier_AddressTableColumns = By.CssSelector("#personDossieraddressesTable th");
        private readonly By _dossier_AddressTableCells = By.CssSelector("#personDossieraddressesTable_data tr td div");
        private readonly By _dossier_PaginatorSelect = By.CssSelector("#personDossieraddressesTable select");
        private readonly By _dossier_ExitButton = By.Id("exitButton:jsButton");
        private readonly By _dossier_DocType = By.XPath("//span[@id='declarantPerson:viewDocumentType']");
        private readonly By _dossier_DocSeries = By.XPath("//span[@id='declarantPerson:viewSeries']");
        private readonly By _dossier_DocNumber = By.XPath("//span[@id='declarantPerson:viewNumber']");
        private readonly By _dossier_DocDepartmentData = By.XPath("//span[contains(@id,'declarantPerson:viewAuthority')]");
        private readonly By _dossier_DocDate = By.XPath("//span[@id='declarantPerson:viewDateIssued']");
        private readonly By _loadingElement = By.XPath("//div[@id='statusDialog' and @style='display: none;']");
        private readonly By _openedAddresses = By.XPath("//fieldset[@id='personDossieraddresses' and @class='collapsible']");
        private readonly By _closedAddresses = By.XPath("//fieldset[@id='personDossieraddresses' and @class='collapsible collapsed']");
        private readonly By _showAddresesLink = By.CssSelector("#personDossieraddresses a");

        public DossierPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_dossier_ExitButton);
            return new SearchPage(_browser);
        }

        public IList<IWebElement> GetAddressesCells()
        {
            var cells = _browser.Driver.FindElements(_dossier_AddressTableCells);
            return cells;
        }

        public void ChangePaginator()
        {
            _browser.Driver.FindElement(_dossier_PaginatorSelect).SendKeys(Consts.DOSSIER_PAGINATOR_MAX_VALUE + Keys.Enter);
            _browser.WaitElement(_loadingElement);
        }

        public int GetAddressesTableColumnsCount()
        {
            return _browser.Driver.FindElements(_dossier_AddressTableColumns).Count;
        }

        public Document GetDocument()
        {
            var doc = new Document();
            doc.Series = _browser.TryFindElement(_dossier_DocSeries)?.Text;
            doc.Number = _browser.TryFindElement(_dossier_DocNumber)?.Text;
            doc.Date = _browser.TryFindElement(_dossier_DocDate)?.Text;
            doc.DepartmentCodeAndName = _browser.TryFindElement(_dossier_DocDepartmentData)?.Text;
            doc.DepartmentCode = DepCodeAndName(doc.DepartmentCodeAndName).Item1;
            doc.DepartmentName = DepCodeAndName(doc.DepartmentCodeAndName).Item2;
            doc.Type = _browser.TryFindElement(_dossier_DocType)?.Text;
            return doc;
        }

        public (string, string) DepCodeAndName(string depCodeAndName)
        {
            string code = null;
            string name = null;
            if (!string.IsNullOrWhiteSpace(depCodeAndName))
            {
                var arr = depCodeAndName.Split(',');
                if(arr.Length == 2)
                {
                    code = arr[0].Trim();
                    name = arr[1].Trim();
                }
            }
            return (code, name);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_dossier_AddressTableColumns);
        }

        public List<Registration> GetRegistrations(Dossier person)
        {
            var searchPage = new SearchPage(_browser);
            searchPage.EnterData(person);
            if (!searchPage.HaveResults())
                return new List<Registration>();
            List<Registration> regisrations = new List<Registration>();

            var resultRows = searchPage.GetResultsRows();
            var rows = GetTargetRows(resultRows, Consts.DOC_TYPE_PASSPORT_RF);
            for (var i = 0; i < resultRows.Count; i++)
            {
                resultRows = searchPage.GetResultsRows();
                if (rows.Contains(i))
                {
                    resultRows[i].Click();
                    Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                    var dossierPage = searchPage.OpenDossier();
                    regisrations.AddRange(ExtractRegisrationsFromDossier());
                    dossierPage
                        .GoToSearchPage()
                        .Search();
                }
            }
            return regisrations;
        }

        private List<int> GetTargetRows(IList<IWebElement> list, string targetDocType)
        {
            var results = new List<int>();
            var listForFilter = Utils.GetRowNumbersAndPersonsFromResultTable(list);
            foreach(var (row, person) in listForFilter)
            {
                if (Utils.IsChild(person.Bdate))
                {
                    results.Add(row);
                }
                else
                {
                    if (person.Documents.FirstOrDefault().Type == targetDocType)
                    {
                        results.Add(row);
                    }
                }
            }
            return results;
        }

        public List<Registration> ExtractRegisrationsFromDossier()
        {
            var isOpenAddresses = _browser.ExistElement(_openedAddresses);
            if (!isOpenAddresses)
            {
                _browser.FindAndClick(_showAddresesLink);
            }
            IList<IWebElement> webTableAddresses = GetAddressesCells();
            if (webTableAddresses.Count == 1)
                return new List<Registration>();
            if (webTableAddresses.Count > 1)
                ChangePaginator();
            webTableAddresses = GetAddressesCells();
            List<Registration> addresses = new List<Registration> { };
            int colsCount = GetAddressesTableColumnsCount();
            for (var i = 0; i < webTableAddresses.Count; i += colsCount)
            {
                var address = new Registration();
                address.Type = webTableAddresses[i + 0].Text;
                address.Period = webTableAddresses[i + 1].Text;
                address.Address = webTableAddresses[i + 2].Text;
                address.Role = webTableAddresses[i + 3].Text;
                FileParser.CompleteRegistrationFields(address);
                addresses.Add(address);
            }
            return addresses;
        }
    }
}
