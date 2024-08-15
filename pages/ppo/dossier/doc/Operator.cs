//АРХИВНЫЙ
using System;
using System.Collections.Generic;
using System.Threading;

namespace ListMaster.ppo.dossier.doc
{
    class Operator : dossier.Operator
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
                        if (string.IsNullOrWhiteSpace(person.Fullname) || person.Bdate == default)
                        {
                            Report(Consts.MESSAGE_WRONG_FORMAT);
                            break;
                        }

                        if (stop)
                        {
                            Report(Consts.MESSAGE_STOPED);
                            return;
                        }

                        Report(person.Fullname);
                        var docs = GetDocuments(person);

                        if (docs == null)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ENTRIES}");
                            break;
                        }

                        else if (docs.Count == 0)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ACTUAL_DOCS}");
                            break;
                        }

                        else
                        {
                            for (int i = 0; i < docs.Count; i++)
                            {
                                string value = string.Empty;

                                if (docs[i].Type == Consts.DOC_TYPE_PASSPORT_RF)
                                {
                                    value = $"{docs[i].Series};{docs[i].Number};{docs[i].Date};{docs[i].DepartmentCode};{docs[i].DepartmentName}";
                                }

                                else if (docs[i].Type == Consts.DOC_TYPE_DEATH_CERT)
                                {
                                    value = $"{docs[i].Comment}";
                                }

                                else if (docs[i].Type == Consts.DOC_TYPE_SIGNAL)
                                {
                                    value = $"{Consts.DOC_TYPE_SIGNAL}:{docs[i].Comment}";
                                }
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
                            Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_60_SEC));
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

        public List<Document> GetDocuments(Dossier person)
        {
            var searchPage = new SearchPage(browser);
            searchPage.EnterData(person);
            if (!searchPage.HaveResults())
            {
                return null;
            }
            List<Document> docs = new List<Document>();
            var resultRowsCount = searchPage.GetResultsRows().Count;
            for (var i = 0; i < resultRowsCount; i++)
            {
                var resultRows = searchPage.GetResultsRows();
                var cellsValues = searchPage.GetCellsValueFromWebRow(resultRows[i]);
                if ((cellsValues[Consts.WEB_TABLE_DOC_STATUS_COLUMN] == Consts.DOSSIER_DOC_STATUS_VALID ||
                    cellsValues[Consts.WEB_TABLE_DOC_STATUS_COLUMN] == Consts.DOC_STATUS_FOR_ISSUE) &&
                    cellsValues[Consts.WEB_TABLE_DOC_TYPE_COLUMN] == Consts.DOC_TYPE_PASSPORT_RF)
                {
                    resultRows[i].Click();
                    Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                    var document = searchPage.OpenDossier().GetDocument();
                    docs.Add(document);
                    GetSearchPage().Search();
                }
                else continue;
            }
            return docs;
        }
    }
}
