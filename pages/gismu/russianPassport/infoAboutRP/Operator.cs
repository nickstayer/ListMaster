using System;

namespace ListMaster.gismu.russianPassport
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

            var passportPage = new PassportPage(browser);
            if (passportPage.IamHere())
            {
                return passportPage.
                    GoToSearchPage();
            }

            var searchPage = new SearchPage(browser);
            if (searchPage.IamHere())
            {
                return searchPage;
            }
            var russianPassportMenuPage = new RussianPassportMenuPage(browser);
            if (russianPassportMenuPage.IamHere())
            {
                return russianPassportMenuPage
                    .GoToSearchPage();
            }

            var mainMenuPage = new GismuMenuPage(browser);
            if (mainMenuPage.IamHere())
            {
                return mainMenuPage
                    .GoToRussianPassportPage()
                    .GoToSearchPage();
            }
            var loginPage = new LoginFormPage(browser, settings);
            if (loginPage.IamHere())
            {
                return loginPage
                    .LogIn()
                    .GoToRussianPassportPage()
                    .GoToSearchPage();
            }
            var changePasswordPage = new ChangePasswordPage(browser);
            if (changePasswordPage.IamHere())
            {
                return changePasswordPage
                    .GoToGismuMenuPage()
                    .GoToRussianPassportPage()
                    .GoToSearchPage();
            }
            throw new Exception(Consts.ERROR_SERVER);
        }
    }
}