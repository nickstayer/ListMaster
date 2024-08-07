using System;
using System.Collections.Generic;
using System.Threading;

namespace ListMaster.ppo.asr.form10
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
                        WriteData(Consts.MESSAGE_NO_IN_FORM_TEN);
                    }
                    else
                    {
                        filteredRegistrations = Utils.FilterForm10Registrations(registrations);
                        if (filteredRegistrations.Count == 0)
                        {
                            WriteData(Consts.MESSAGE_NO_ACTUAL_ADDRESSES_IN_FORM_TEN);
                        }
                    }
                    foreach (Registration registration in filteredRegistrations)
                    {
                        if (Utils.IsNewRegistration(registration, person))
                        {
                            var value = $"{registration.Type}; {registration.DateIn.ToShortDateString()} - "
                                + $"{registration.DateOut.ToShortDateString()}; {registration.Address}";
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
                var dossierFormTenPage = new DossierPage(browser);
                if (dossierFormTenPage.IamHere())
                {
                    return dossierFormTenPage.GoToSearchPage();
                }
                var asrMenuPage = new AsrMenuPage(browser);
                if (asrMenuPage.IamHere())
                {
                    return asrMenuPage.GoToSearchPage();
                }
                var mainMenuPage = new MainMenuPage(browser);
                if (mainMenuPage.IamHere())
                {
                    asrMenuPage = mainMenuPage.GoToAsrMenuPage();
                    return asrMenuPage.GoToSearchPage();
                }
                var loginPage = new LoginFormPage(browser, settings);
                if (loginPage.IamHere())
                {
                    mainMenuPage = loginPage.LogIn();
                    asrMenuPage = mainMenuPage.GoToAsrMenuPage();
                    return asrMenuPage.GoToSearchPage();
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
