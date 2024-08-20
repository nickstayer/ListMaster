using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ListMaster.gismu.russianPassport.actualDoc
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
                        var requredCondition = !string.IsNullOrWhiteSpace(person.Fullname) && person.Bdate != default;
                        var alterRequredCondition = person.Documents.Count != 0 && !string.IsNullOrWhiteSpace(person.Documents[0].Series) && !string.IsNullOrWhiteSpace(person.Documents[0].Number);
                        if (!requredCondition && !alterRequredCondition)
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
                        if (person.Documents.Count == 0)
                            Report(person.Fullname);
                        else
                            Report(person.Documents.FirstOrDefault()?.SeriesNumber);
                        var actualDocuments = GetActualDocuments(person);

                        if (actualDocuments.Count == 0)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ENTRIES}");
                            break;
                        }

                        else
                        {
                            for (int i = 0; i < actualDocuments.Count; i++)
                            {
                                var value = $"{actualDocuments[i].Status};{actualDocuments[i].Series};{actualDocuments[i].Number};{actualDocuments[i].Date}";
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

        private List<Document> GetActualDocuments(Dossier person)
        {
            var searchPage = new SearchPage(browser);
            searchPage.ClearForm();
            searchPage.EnterData(person);
            searchPage.ClickSearchButton();
            if (!searchPage.HaveResults())
            {
                return new List<Document>();
            }
            var documents = searchPage.GetDocuments();
            var result = documents.Where(doc => doc.Status == "Действительный").ToList();
            return result;
        }
    }
}