using System;
using System.Threading;

namespace ListMaster.gismu.esfl
{
    class Operator : ListMaster.Operator
    {
        public Operator(Enviroment enviroment, Settings settings) : base(enviroment, settings)
        {

        }

        public override void Work()
        {

        }

        public SearchPage GetSearchPage()
        {
            if (needRestartBrowser
                || browser == null
                || !browser.CanDriveBrowser())
            {
                RestartBrowser();
            }

            browser.CloseAllAnotherTabs();

            var searchPage = new SearchPage(browser);
            if (searchPage.IamHere())
            {
                return searchPage;
            }
            var dossierPage = new DossierPage(browser);
            if (dossierPage.IamHere())
            {
                return dossierPage
                    .GoToSearchPage();
            }
            var esflMenuPage = new EsflMenuPage(browser);
            if (esflMenuPage.IamHere())
            {
                return esflMenuPage
                    .GoToSearchPage();
            }
            var mainMenuPage = new GismuMenuPage(browser);
            if (mainMenuPage.IamHere())
            {
                return mainMenuPage
                    .EsflMenuPage()
                    .GoToSearchPage();
            }
            var loginPage = new LoginFormPage(browser, settings);
            if (loginPage.IamHere())
            {
                return loginPage
                    .LogIn()
                    .EsflMenuPage()
                    .GoToSearchPage();
            }
            var changePasswordPage = new ChangePasswordPage(browser);
            if (changePasswordPage.IamHere())
            {
                return changePasswordPage
                    .GoToGismuMenuPage()
                    .EsflMenuPage()
                    .GoToSearchPage();
            }
            throw new Exception(Consts.ERROR_SERVER);
        }
    }
}