//АРХИВНЫЙ
using System;
using System.Linq;

namespace ListMaster.asrp.passports
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
                try
                {
                    if (person.Documents.Count == 0)
                    {
                        Report(Consts.MESSAGE_WRONG_FORMAT);
                        return;
                    }
                    if (stop)
                    {
                        Report(Consts.MESSAGE_STOPED);
                        return;
                    }
                    Report($"{person.Documents.FirstOrDefault().Series} {person.Documents.FirstOrDefault().Number}");
                    searchPage.EnterData(person);
                    var note = searchPage
                        .Search()
                        .OpenDossier()
                        .IsFormOneUploaded() ? Consts.MESSAGE_YES : Consts.MESSAGE_NO;
                    excelApp.WriteNote(note);
                    GetSearchPage();
                }
                catch (Exception)
                {
                    Report(Consts.MESSAGE_RECOVERY);
                    GetSearchPage();
                }
            }
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
                var mainMenuPage = loginPage.LogIn();
                
                return mainMenuPage.GoToPassportSearchPage();
            }
            throw new Exception(Consts.MESSAGE_DONT_KNOW_CURRENT_PAGE);
        }
    }
}
