using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ListMaster.gismu.russianPassport.formONe
{
    class Operator : russianPassport.Operator
    {
        public Operator(Enviroment enviroment, Settings settings) : base(enviroment, settings)
        {

        }

        public override void Work()
        {
            var saveEntriesCount = 0;
            foreach (Dossier person in fileParser.Read())
            {
                saveEntriesCount++;
                if (saveEntriesCount == Consts.SAVE_ENTRIES_COUNT)
                {
                    excelApp.SaveBook();
                    saveEntriesCount = 0;
                }

                int exCounter = 0;
                while (exCounter != Consts.DEFAULT_TRY_RECOVERY_COUNT)
                {
                    try
                    {
                        GetSearchPage();
                        var requredCondition = person.Documents.Count != 0 && !string.IsNullOrWhiteSpace(person.Documents[0].Series) && !string.IsNullOrWhiteSpace(person.Documents[0].Number);
                        if (!requredCondition)
                        {
                            Report(Consts.MESSAGE_WRONG_FORMAT);
                            break;
                        }

                        if (stop)
                        {
                            Report(Consts.MESSAGE_STOPED);
                            browserProgress.Report(browser);
                            return;
                        }

                        Report(person.Documents.FirstOrDefault()?.SeriesNumber);
                        var value = HasFormOne(person);
                        WriteData(value);
                        break;
                    }
                    catch (Exception ex)
                    {
                        exCounter++;

                        if (exCounter == Consts.DEFAULT_TRY_RECOVERY_COUNT)
                        {
                            WriteData($"{Consts.ERROR_CANT_GET_DATA} {DateTime.Now}");
                        }

                        // специфика сервиса
                        if (!browser.CanDriveBrowser()
                            || ex.Message == Consts.ERROR_SERVER
                            || ex.Message == Consts.ERROR_LOST_BROWSER
                            || ex.Message == Consts.ERROR_CANT_GET_SEARCH_RESULT_TABLE)
                        {
                            Report($"{ex.Message}. {Consts.MESSAGE_TRY_CONNECT} {exCounter}");
                            needRestartBrowser = true;
                            continue;
                        }

                        // проблема с ожиданием страницы
                        if (browser.CanDriveBrowser())
                        {
                            Report($"{ex.Message}. {Consts.MESSAGE_TRY_CONNECT} {exCounter}");
                            if (!ex.Message.Contains(Consts.ERORR_SCROLL_KEYWORD))
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_60_SEC)); 
                            }
                            continue;
                        }
                    }
                }
                exCounter = 0;
            }
            Report(Consts.MESSAGE_WORK_FINISHED);
            browser?.Stop();
            excelApp.SaveBook();
        }

        public string HasFormOne(Dossier person)
        {
            var hasFormOne = false;
            var searchPage = new SearchPage(browser);
            searchPage.ClearForm();
            searchPage.EnterData(person);
            searchPage.ClickSearchButton();
            if (!searchPage.HaveResults())
            {
                return "документ не найден";
            }

            IList<IWebElement> rows = searchPage.GetResultRows();
            foreach (IWebElement row in rows)
            {
                var passportPage = searchPage.ViewPassport(row);
                hasFormOne = passportPage.HasFormOne();
                if(hasFormOne) break;
            }
            var result = hasFormOne ? "да" : "нет";
            return result;
        }
    }
}