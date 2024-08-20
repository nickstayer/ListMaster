using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ListMaster.ppo.asr.form10
{
    class DossierPage
    {
        private readonly Browser _browser;
        private static readonly string _addressSpanId = "j_idt135:value";
        private readonly By _addressSpan = By.XPath($"//span[@id='{_addressSpanId}']");
        private readonly By _dateInSpan = By.XPath("//span[@id='j_idt150:value']");
        private readonly By _dateOutSpan = By.XPath("//span[@id='j_idt151:value']");
        private readonly By _dossier_ExitButton = By.Id("exitButton:ajaxButton");

        public DossierPage(Browser browser)
        {
            _browser = browser;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_dossier_ExitButton);
            return new SearchPage(_browser);
        }

        public List<Registration> GetRegistrations(Dossier person)
        {
            var searchPage = new SearchPage(_browser);
            searchPage.EnterData(person);
            List<Registration> registrations = new List<Registration>();
            if (!searchPage.ExistSearchResultsByLinksCount())
            {
                return registrations;
            }
            var resultRowsCount = searchPage.GetResultsRows().Count;
            for (var i = 0; i < resultRowsCount; i++)
            {
                if (i > 0)
                {
                    searchPage.EnterData(person);
                }
                var resultRows = searchPage.GetResultsRows();
                resultRows[i].Click();
                Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                var registration = GetRegistration();
                registrations.Add(registration);
                GoToSearchPage();
            }
            return registrations;
        }

        public Registration GetRegistration()
        {
            var spanAddress = _browser.Driver.FindElement(_addressSpan);
            var spanDateIn = _browser.Driver.FindElement(_dateInSpan);
            var spanDateOut = _browser.Driver.FindElement(_dateOutSpan);
            var result = new Registration()
            {
                Type = Consts.FORM10_REGISTRATION_TYPE,
                Address = spanAddress.Text,
                DateIn = Utils.StringToDate(spanDateIn.Text),
                DateOut = Utils.StringToDate(spanDateOut.Text),
            };
            FileParser.CompleteRegistrationFields(result);
            return result;
        }

        public bool IamHere()
        {
            return _browser.ExistElementInPageSource(_addressSpanId);
        }
    }
}
