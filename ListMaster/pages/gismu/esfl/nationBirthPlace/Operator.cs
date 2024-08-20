using System;
using System.Threading;

namespace ListMaster.gismu.esfl.nationBirthPlace
{
    class Operator : esfl.Operator
    {
        public Operator(Enviroment enviroment, Settings settings) : base(enviroment, settings) {}

        public override void Work()
        {
            var saveEntriesCount = 0;
            foreach (Dossier dossier in fileParser.Read())
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
                        var requredCondition = !string.IsNullOrWhiteSpace(dossier.Fullname) && dossier.Bdate != default;
                        var alterRequredCondition = dossier.Documents.Count != 0 
                            && !string.IsNullOrWhiteSpace(dossier.Documents[0].Series) 
                            && !string.IsNullOrWhiteSpace(dossier.Documents[0].Number);
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

                        Report(dossier.Fullname);
                        var dos = GetDossier(dossier);

                        // проверка наличия результата
                        if (dos == null)
                        {
                            WriteData($"{Consts.MESSAGE_NO_ENTRIES}");
                            break;
                        }

                        else if (string.IsNullOrEmpty(dos.Nationality) && string.IsNullOrEmpty(dos.BirthPlace))
                        {
                            WriteData($"{Consts.MESSAGE_NO_DATA}");
                            break;
                        }

                        else
                        {
                            // есть результат!
                            string nation = string.IsNullOrEmpty(dos.Nationality) ? string.Empty : dos.Nationality;
                            string birthPlace = string.IsNullOrEmpty(dos.BirthPlace) ? string.Empty : dos.BirthPlace;

                            excelApp.WriteNote($"{nation};{birthPlace}");
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

        public Dossier GetDossier(Dossier dossier)
        {
            var searchPage = new SearchPage(browser);
            searchPage.ClearForm();
            searchPage.EnterData(dossier);
            searchPage.ClickSearchButton();
            var dossierLinks = searchPage.GetDossierLinks();
            if (dossierLinks.Count == 0)
            {
                return null;
            }
            searchPage.ShowAllDossierLinksPages();
            dossierLinks = searchPage.GetDossierLinks();

            // обход всех досье
            var counter = 0;
            foreach (var dossierLink in dossierLinks)
            {
                counter++;
                // условие прекращения обхода
                if (!string.IsNullOrEmpty(dossier.BirthPlace)
                    && !string.IsNullOrEmpty(dossier.Nationality))
                    break;

                Report($"Смотрю досье {counter}");

                var dossierPage = searchPage.OpenDossier(dossierLink);

                // получение данных
                var birthPlace = dossierPage.GetBirthPlace();
                if (string.IsNullOrEmpty(dossier.BirthPlace) && !string.IsNullOrEmpty(birthPlace))
                    dossier.BirthPlace = birthPlace;

                var nationality = dossierPage.GetNationality();
                if (string.IsNullOrEmpty(dossier.Nationality) && !string.IsNullOrEmpty(nationality))
                    dossier.Nationality = nationality;

                GetSearchPage(); 
            }
            return dossier;
        }
    }
}