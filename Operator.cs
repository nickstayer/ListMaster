using System;
using System.Reflection;
using ExceLib;

namespace ListMaster
{
    abstract class Operator
    {
        public Browser browser { get; set; }
        public ExcelApp excelApp { get; set; }
        public IProgress<string> labelExecStatus { get; set; }
        public IProgress<int> textBoxRowNumber { get; set; }
        public IProgress<int> labelRamainsRowsCount { get; set; }
        public IProgress<Browser> browserProgress { get; set; }
        public bool stop { get; set; }
        public FileParser fileParser { get; set; }
        public Settings settings { get; set; }
        public abstract void Work();
        public bool needRestartBrowser = false;

        public Operator(Enviroment enviroment, Settings _settings)
        {
            browser = enviroment.Browser;
            browserProgress = enviroment.BrowserProgress;
            excelApp = enviroment.ExcelApp;
            labelExecStatus = enviroment.ExecProgress;
            labelRamainsRowsCount = enviroment.RamainsRowsProgress;
            textBoxRowNumber = enviroment.RowProgress;
            fileParser = enviroment.FileParser;
            settings = _settings;
            excelApp.CurrentRow = enviroment.StartRow;
        }

        public void Report(string status)
        {
            textBoxRowNumber.Report(excelApp.CurrentRow);
            labelExecStatus.Report(status);
            labelRamainsRowsCount.Report(excelApp.RemainsVisibleRowsCount);
        }

        public void WriteData(string data)
        {
            var targetColumn = settings.TargetColumn;
            excelApp.WriteNote(data, targetColumn);
        }

        public void RestartBrowser()
        {
            browser?.Stop();
            browser = new Browser(settings);
            browser.Init();
            browser.DeleteAllCookies();
            needRestartBrowser = false;
            browser.WaitFor(Consts.WAIT_10_SEC);
        }

        //*новый сценарий*
        public static Operator GetOperator(Enviroment enviroment, Settings settings)
        {
            try
            {
                switch (settings.ScriptName)
                {

                    case Consts.SCRIPT_GISMU_ESFL_DOC:
                        return new gismu.esfl.doc.Operator(enviroment, settings);

                    case Consts.SCRIPT_GISMU_ESFL_REG:
                        return new gismu.esfl.reg.Operator(enviroment, settings);

                    case Consts.SCRIPT_GISMU_RP_DOC_STATUS:
                        return new gismu.russianPassport.docStatus.Operator(enviroment, settings);

                    case Consts.SCRIPT_GISMU_RP_FORM_ONE:
                        return new gismu.russianPassport.formONe.Operator(enviroment, settings);

                    case Consts.SCRIPT_GISMU_ESFL_NATION_BIRTHPLACE:
                        return new gismu.esfl.nationBirthPlace.Operator(enviroment, settings);

                    //АХИВНЫЕ:
                    //case Consts.SCRIPT_ASRP_PASSPORTS_FORM_ONE:
                    //    return new asrp.passports.Operator(enviroment, settings);

                    //case Consts.SCRIPT_ASRP_DOCUMENT:
                    //    return new asrp.dossier.Operator(enviroment, settings);

                    //case Consts.SCRIPT_PPO_DOSSIER_REGISTRATION:
                    //    return new ppo.dossier.reg.Operator(enviroment, settings);
                    //case Consts.SCRIPT_PPO_DOSSIER_DOCUMENT:
                    //    return new ppo.dossier.doc.Operator(enviroment, settings);
                    //case Consts.SCRIPT_PPO_ASR_FORM_10_REGISTRATION:
                    //    return new ppo.asr.form10.Operator(enviroment, settings);

                    default:
                        throw new Exception(Consts.MESSAGE_NO_OPERATOR);
                }
            }
            catch(Exception ex)
            {
                var m = MethodBase.GetCurrentMethod();
                throw new Exception($"{m.ReflectedType.Name}.{m.Name}: {ex.Message}");
            }
        }
    }
}
