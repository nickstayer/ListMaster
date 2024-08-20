using OpenQA.Selenium;

namespace ListMaster.asrp.dossier
{
    class DossierPage
    {
        private Browser _browser;
        private readonly By _dossier_DocDataTab = By.Id("passport_view_form:tab_other_lbl");
        private readonly By _dossier_seriesField = By.Id("passport_view_form:j_id211:v_passportSeries");
        private readonly By _dossier_numberField = By.Id("passport_view_form:j_id211:v_passportNumber");
        private readonly By _dossier_dateField = By.Id("passport_view_form:j_id211:v_passportDeliveryDateInputDate");
        private readonly By _dossier_depCodeField = By.Id("passport_view_form:j_id211:v_passportDepCode");
        private readonly By _dossier_depNameField = By.Id("passport_view_form:j_id211:v_passportDepartment");
        private readonly By _dossier_issueDepNameField = By.Id("passport_view_form:j_id211:v_passportIssueDepartment");
        private By _dossier_ToSearchPageButton = By.XPath("//input[@value='Досье']");
        public DossierPage(Browser browser)
        {
            _browser = browser;
        }

        public void SwitchToDocDataTab()
        {
            _browser.FindAndClick(_dossier_DocDataTab);
        }

        public Document GetDocument()
        {
            var document = new Document();
            document.Series = _browser.GetElement(_dossier_seriesField).GetAttribute("value");
            document.Number = _browser.GetElement(_dossier_numberField).GetAttribute("value");
            document.Date = _browser.GetElement(_dossier_dateField).GetAttribute("value");
            document.DepartmentCode = FileParser.ParseDepartmentCode(_browser.GetElement(_dossier_depCodeField).GetAttribute("value"));
            var depName = _browser.GetElement(_dossier_issueDepNameField).GetAttribute("value") == ""
                ? _browser.GetElement(_dossier_depNameField).GetAttribute("value")
                : _browser.GetElement(_dossier_issueDepNameField).GetAttribute("value");
            document.DepartmentName = depName;
            return document;
        }

        public SearchPage GoToSearchPage()
        {
            _browser.FindAndClick(_dossier_ToSearchPageButton);
            return new SearchPage(_browser);
        }

        public bool IamHere()
        {
            return _browser.ExistElement(_dossier_ToSearchPageButton);
        }
    }
}
