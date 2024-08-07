using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ListMaster.gismu.esfl
{
    class DossierPage
    {
        private readonly Browser _browser;
        private readonly By _docTypeSeriesNumber = By.CssSelector("div.ant-col-18:nth-child(1) > div:nth-child(1) > div:nth-child(2) > div:nth-child(1) > span:nth-child(1) > span");
        private readonly By _docDate = By.CssSelector("div.ant-col-6:nth-child(2) > div:nth-child(1) > div:nth-child(2) > div:nth-child(1) > span:nth-child(1) > div:nth-child(1) > span:nth-child(1)");
        private readonly By _docDepNameCode = By.CssSelector("div.ml20:nth-child(3) > div:nth-child(2) > div:nth-child(1) > div:nth-child(2) > div:nth-child(1) > span:nth-child(1) > span:nth-child(1)");
        private readonly By _deathCert = By.XPath("//span[contains(text(),'Свидетельство о смерти')]");
        private readonly By _deathSignal = By.XPath("//td[contains(text(),'Смерть или признание судом умершим')]");
        private readonly By _birthPlaceWrapper = By.XPath("//*[@id=\"print\"]/div[2]/div[2]/div[2]/div/div/div[1]/form/div[1]/div[1]/div[1]/form/div[6]/div/div[2]/div/span");
        private readonly By _nationalityWraper = By.XPath("//*[@id=\"print\"]/div[2]/div[2]/div[2]/div/div/div[1]/form/div[1]/div[1]/div[2]/div/div/div[2]/div/span");
        private readonly By _addressesButton = By.XPath("//div[contains(text(),'Адреса')]");
        private readonly By _addressesBlock = By.CssSelector("div.ant-collapse-item-active:nth-child(3)");
        private readonly By _table = By.TagName("tbody");
        private readonly By _404 = By.XPath("//div[contains(text(),'Запрашиваемой страницы не существует')]");
        private readonly string _urlPattern = @"http://esfl.gismu.it.mvd.ru/persons/\d+";
        private readonly string _url404 = @"http://esfl.gismu.it.mvd.ru/persons/undefined/dossier";

        private readonly By _addressTableBodyCells = By.CssSelector("td");

        public DossierPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.CloseAllAnotherTabs();
            _browser.SwitchToMainTab();
            return new SearchPage(_browser);
        }

        public string GetBirthPlace()
        {
            var wrapper = _browser.GetElement(_birthPlaceWrapper);
            var elements = wrapper?.FindElements(By.CssSelector("div span"));
            if (elements == null)
                return string.Empty;
            var result = new StringBuilder();
            var counter = 0;
            foreach(var element in elements)
            {
                result.Append(element.Text);
                counter++;
                if(counter < elements.Count)
                    result.Append(";");
            }
            return result.ToString();
        }

        public string GetNationality()
        {
            var wraper = _browser.GetElement(_nationalityWraper);
            var innerWrappers = wraper?.FindElements(By.CssSelector("div"));
            if (innerWrappers == null)
                return string.Empty;
            var result = new StringBuilder();
            var counter = 0;

            foreach (var innerWraper in innerWrappers)
            {
                var element = innerWraper.FindElement(By.CssSelector("span span"));
                result.Append(element.Text);
                counter++;
                if (counter < innerWrappers.Count)
                    result.Append(";");
            }

            return result.ToString();
        }

        public Document GetRussianPassport()
        {
            Document doc = null;
            if (_browser.ExistElement(_docTypeSeriesNumber))
            {
                doc = new Document();
                var docTypeSeriesNumber = _browser.Driver.FindElement(_docTypeSeriesNumber).Text;
                var docTypeSeriesNumberTuple = WebParser.DocTypeSeriesNumberEsfl(docTypeSeriesNumber);
                doc.Type = docTypeSeriesNumberTuple.Item1;
                doc.Series = docTypeSeriesNumberTuple.Item2;
                doc.Number = docTypeSeriesNumberTuple.Item3;

                doc.Date = _browser.Driver.FindElement(_docDate).Text;
                var depCodeName = _browser.Driver.FindElement(_docDepNameCode).Text;
                var depCodeNameTuple = WebParser.DepCodeAndNameEsfl(depCodeName);
                doc.DepartmentCode = depCodeNameTuple.Item1;
                doc.DepartmentName = depCodeNameTuple.Item2;
            }
            return doc;
        }

        public string GetDeathSignal()
        {
            string result = string.Empty;
            if(_browser.ExistElement(_deathSignal))
                result = Consts.DEATH_SIGNAL;
            return result;
        }

        public string GetDeathCert()
        {
            string result = string.Empty;
            if (_browser.ExistElement(_deathCert))
                result = _browser.GetElement(_deathCert)?.Text;
            return result;
        }

        public List<Registration> GetActualRegistrations()
        {
            List<Registration> addresses = new List<Registration> { };
            if (_browser.ExistElement(_404)
                || !_browser.IsElementClickableByIndex(_addressesButton))
            {
                return addresses;
            }

            _browser.ExpandBlock(_addressesButton, _addressesBlock);

            IList<IWebElement> webTableAddresses = GetAddressesCells();

            if(webTableAddresses == null
                || webTableAddresses.Count < Consts.ESFL_REG_ADDRESSES_TABLE_COLUMNS)
            {
                return addresses;
            }
            
            int colsCount = Consts.ESFL_REG_ADDRESSES_TABLE_COLUMNS;
            for (var i = 0; i < webTableAddresses.Count; i += colsCount)
            {
                var address = new Registration();
                address.Type = webTableAddresses[i].Text;
                address.DateIn = Utils.StringToDate(webTableAddresses[i + 1].Text);
                address.DateOut = Utils.StringToDate(webTableAddresses[i + 2].Text);
                address.Address = webTableAddresses[i + 3].Text;
                address.Role = webTableAddresses[i + 4].Text;
                address.Source = webTableAddresses[i + 5].Text;
                address.Period = Utils.GetPeriodFromDates(address);
                if (address.DateOut == Consts.ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT
                    || address.DateOut > System.DateTime.Today)
                {
                    addresses.Add(address); 
                }
            }
            return addresses;
        }

        public IList<IWebElement> GetAddressesCells()
        {
            var addressesBlock = _browser.GetElement(_addressesBlock);
            var firstTable = addressesBlock.FindElement(_table);
            var cells = firstTable.FindElements(_addressTableBodyCells);
            return cells;
        }

        public bool IamHere()
        {
            if (!_browser.CanDriveBrowser())
                throw new System.Exception(Consts.ERROR_LOST_BROWSER);
            var url = _browser.Driver.Url;
            var match = Regex.Match(url, _urlPattern);
            var result = match.Success || url == _url404;
            return result;
        }
    }
}