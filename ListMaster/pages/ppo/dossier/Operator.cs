using System;
using System.Threading;

namespace ListMaster.ppo.dossier
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
            var confirmPage = new ConfirmPage(browser);
            if (confirmPage.IamHere())
            {
                return confirmPage
                    .GoToDossierOldSearchPage();
            }
            var dossierMenuPage = new DossierMenuPage(browser);
            if (dossierMenuPage.IamHere())
            {
                return dossierMenuPage
                    .GoToDossierOldConfirmPage()
                    .GoToDossierOldSearchPage();
            }
            var mainMenuPage = new MainMenuPage(browser);
            if (mainMenuPage.IamHere())
            {
                return mainMenuPage
                    .GoToDossierMenuPage()
                    .GoToDossierOldConfirmPage()
                    .GoToDossierOldSearchPage();
            }
            var loginPage = new LoginFormPage(browser, settings);
            if (loginPage.IamHere())
            {
                return loginPage
                    .LogIn()
                    .GoToDossierMenuPage()
                    .GoToDossierOldConfirmPage()
                    .GoToDossierOldSearchPage();
            }
            var badRequestPage = new BadRequestPage(browser, settings);
            if (badRequestPage.IamHere())
            {
                return badRequestPage
                    .GoToLoginFormPage()
                    .LogIn()
                    .GoToDossierMenuPage()
                    .GoToDossierOldConfirmPage()
                    .GoToDossierOldSearchPage();
            }
            throw new Exception(Consts.ERROR_NO_CONNECTION);
        }
    }
}