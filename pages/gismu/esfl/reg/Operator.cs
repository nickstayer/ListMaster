using System;
using System.Collections.Generic;
using System.Threading;

namespace ListMaster.gismu.esfl.reg
{
    class Operator : esfl.Operator
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
                            return;
                        }

                        Report(person.Fullname);
                        var regs = GetRegistrations(person);

                        if (regs == null)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ENTRIES}");
                            break;
                        }

                        else if (regs.Count == 0)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ACTUAL_REGISTRATION}");
                            break;
                        }

                        else
                        {
                            for (int i = 0; i < regs.Count; i++)
                            {
                                var value = $"{regs[i].Source};{regs[i].Type};{regs[i].Period};{regs[i].Address};{regs[i].Role}";
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

        public List<Registration> GetRegistrations(Dossier person)
        {
            var searchPage = new SearchPage(browser);
            searchPage.ClearForm();
            searchPage.EnterData(person);
            searchPage.ClickSearchButton();
            var rows = searchPage.GetDossierLinks();
            if (rows.Count == 0)
            {
                return null;
            }
            searchPage.ShowAllDossierLinksPages();
            rows = searchPage.GetDossierLinks();
            List<Registration> regsAll = new List<Registration>();

            foreach (var row in rows)
            {
                var dossierPage = searchPage.OpenDossier(row);
                var regs = dossierPage.GetActualRegistrations();
                if (regs != null && regs.Count != 0)
                {
                    regsAll.AddRange(regs);
                }
                GetSearchPage(); 
            }
            regsAll = Utils.FilterEsflRegUchetRegistrations(regsAll);
            return regsAll;
        }
    }
}