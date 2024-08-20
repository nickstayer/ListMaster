using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ListMaster.gismu.russianPassport.docStatus
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
                        var docStatus = GetDocumentStatus(person);

                        if (docStatus.Count == 0)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ENTRIES}");
                            break;
                        }

                        else
                        {
                            for (int i = 0; i < docStatus.Count; i++)
                            {
                                var value = $"{docStatus[i]}";
                                var targetColumn = 0;
                                if (settings.TargetColumn != 0)
                                    targetColumn = settings.TargetColumn + i;
                                excelApp.WriteNote(value, targetColumn);
                            }
                            break;
                        }
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
            browser?.Quit();
            excelApp.SaveBook();
            //excelApp.Quit();
            Report(Consts.MESSAGE_WORK_FINISHED);
        }

        private List<string> GetDocumentStatus(Dossier person)
        {
            var searchPage = new SearchPage(browser);
            searchPage.ClearForm();
            searchPage.EnterData(person);
            searchPage.ClickSearchButton();
            if (!searchPage.HaveResults())
            {
                return new List<string>();
            }
            var result = searchPage.GetDocumentStatus();
            return result;
        }
    }
}