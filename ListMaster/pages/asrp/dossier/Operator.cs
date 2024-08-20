//АРХИВНЫЙ
using System;

namespace ListMaster.asrp.dossier
{
    class Operator : ListMaster.Operator
    {
        public Operator(Enviroment enviroment, Settings settings) : base(enviroment, settings)
        {

        }

        public override void Work()
        {
            var searchPage = GetSearchPage();
            foreach (Dossier person in fileParser.Read())
            {
                if (string.IsNullOrWhiteSpace(person.Fullname) || person.Bdate == default)
                {
                    Report(Consts.MESSAGE_WRONG_FORMAT);
                    return;
                }
                try
                {
                    if (stop)
                    {
                        Report(Consts.MESSAGE_STOPED);
                        return;
                    }
                    Report(person.Fullname);
                    searchPage.ClearForm();
                    searchPage.EnterData(person);
                    var searchResultPage = searchPage.Search();
                    if (!searchResultPage.ArePersonFinded())
                    {
                        excelApp.WriteNote(Consts.MESSAGE_NO_IN_ASRP);
                        searchResultPage.GoToSearchPage();
                        continue;
                    }
                    var actualDocLinks = searchResultPage.GetActualDocLocators();
                    if(actualDocLinks.Count == 0)
                    {
                        excelApp.WriteNote(Consts.MESSAGE_NO_ACTUAL_DOCS);
                        searchResultPage.GoToSearchPage();
                        continue;
                    }
                    for (var i = 0; i < actualDocLinks.Count; i++)
                    {
                        var dossierPage = searchResultPage.OpenDossier(actualDocLinks[i]);
                        dossierPage.SwitchToDocDataTab();
                        var doc = dossierPage.GetDocument();
                        var value = $"{doc.Series} {doc.Number}; {doc.Date}; "
                            + $"{doc.DepartmentCode}, {doc.DepartmentName}";
                        excelApp.WriteNote(value);
                        dossierPage.GoToSearchPage();
                        if(i < actualDocLinks.Count - 1)
                        {
                            searchPage.Search();
                        }
                    }
                }
                catch (Exception)
                {
                    Report(Consts.MESSAGE_RECOVERY);
                    GetSearchPage();
                }
            }
            browser?.Quit();
            excelApp.SaveBook();
            //excelApp.Quit();
            Report(Consts.MESSAGE_WORK_FINISHED);
        }

        private SearchPage GetSearchPage()
        {
            var searchPage = new SearchPage(browser);
            if (searchPage.IamHere())
            {
                return searchPage;
            }
            var dossierPage = new DossierPage(browser);
            if (dossierPage.IamHere())
            {
                return dossierPage.GoToSearchPage();
            }
            var searchResultPage = new SearchResultsPage(browser);
            if (searchResultPage.IamHere())
            {
                return searchResultPage.GoToSearchPage();
            }
            var loginPage = new LoginFormPage(browser, settings);
            if (loginPage.IamHere())
            {
                return loginPage
                    .LogIn()
                    .GoToDossierSearchPage();
            }
            throw new Exception(Consts.MESSAGE_DONT_KNOW_CURRENT_PAGE);
        }
    }
}
