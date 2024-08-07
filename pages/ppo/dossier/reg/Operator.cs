//АРХИВНЫЙ
using System;
using System.Collections.Generic;
using System.Threading;

namespace ListMaster.ppo.dossier.reg
{
    class Operator : ListMaster.Operator
    {
        public Operator(Enviroment enviroment, Settings settings) : base(enviroment, settings)
        {

        }

        public override void Work()
        {
            foreach (Dossier person in fileParser.Read())
            {
                try
                {
                    GetSearchPage();
                    if (string.IsNullOrWhiteSpace(person.Fullname) || person.Bdate == default)
                    {
                        Report(Consts.MESSAGE_WRONG_FORMAT);
                        continue;
                    }
                    if (stop)
                    {
                        Report(Consts.MESSAGE_STOPED);
                        return;
                    }
                    Report(person.Fullname);
                    var registrations = new DossierPage(browser).GetRegistrations(person);
                    var filteredRegistrations = new List<Registration>();
                    if (registrations.Count == 0)
                    {
                        WriteData(Consts.MESSAGE_NO_ADDRESSES_IN_PPO);
                    }
                    else
                    {
                        filteredRegistrations = Utils.FilterDossierRegistrations(registrations,
                            Consts.REG_ROLE_PERSON_ADDRESS,
                            Consts.DOSSIER_REGISTRATION_TYPE_CONSTANT_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_TEMP_RF,
                            Consts.DOSSIER_REGISTRATION_TYPE_HOTEL_RF
                            );
                        if (filteredRegistrations.Count == 0)
                        {
                            WriteData(Consts.MESSAGE_NO_ACTUAL_REGISTRATION);
                        }
                    }
                    foreach (Registration regisration in filteredRegistrations)
                    {
                        if (Utils.IsNewRegistration(regisration, person))
                        {
                            var value = $"{regisration.Type};{regisration.Period};{regisration.Address}";
                            WriteData(value);
                        }
                        else
                        {
                            WriteData(Consts.MESSAGE_SAME_ADDRESS);
                        }
                    }
                }
                catch (Exception ex)
                {
                    needRestartBrowser = true;
                    WriteData($"{Consts.ERROR_CANT_GET_DATA}.{ex.Message}.{settings.ScriptName}:{DateTime.Now}");
                    Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_60_SEC));
                    continue;
                }
            }
            browser?.Quit();
            excelApp.SaveBook();
            excelApp.Quit();
            Report(Consts.MESSAGE_WORK_FINISHED);
        }

        private SearchPage GetSearchPage()
        {
            if (needRestartBrowser
                || browser == null
                || !browser.CanDriveBrowser())
            {
                RestartBrowser();
                needRestartBrowser = false;
            }

            var tryCount = 0;

            while (tryCount < Consts.DEFAULT_TRY_RECOVERY_COUNT)
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
                if (tryCount == Consts.DEFAULT_TRY_RECOVERY_COUNT - 1)
                    RestartBrowser();
                tryCount++;
                Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_60_SEC));
            }
            throw new Exception(Consts.ERROR_NO_CONNECTION);
        }

        private void RestartBrowser()
        {
            browser?.Quit();
            browser = new Browser(settings);
            browser.Init();
        }
    }
}
