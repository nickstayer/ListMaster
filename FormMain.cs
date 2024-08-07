using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using ExceLib;

namespace ListMaster
{
    public partial class FormMain : Form
    {
        public static Settings settings;
        private ExcelApp _excelApp;
        private Operator _operator;
        private string _file;
        private Browser browser;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateCombobox();
            comboBoxOptions.Text = Consts.CHOOSE_SCRIPT;
            labelFileName.Text = "";
            lbTotalRows.Text = "";
            lbRamainsRows.Text = "";
            textBoxStartRowNumber.Text = Consts.DEFAULT_START_ROW.ToString();
        }

        private void ComboBoxOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultForm();
            settings = GetSettings(comboBoxOptions.Text);
            if (settings != null)
            {
                Text = $"{settings.ScriptName}: {settings.Username}";
            }
        }

        private void Button_ChooseFile_Click(object sender, EventArgs e)
        {
            try
            {
                DefaultForm();
                CheckChoosedScript();
                if (_file != null && _excelApp != null)
                {
                    _excelApp.CloseBook();
                    _excelApp.Quit();
                    _file = null;
                }
                InitFile(Consts.CHOOSE_FILE_TITLE, Consts.CHOOSE_FILE_EXCEL_TYPES);
                if (_file != null)
                {
                    OutputFileNameToForm(_file);
                    _excelApp = new ExcelApp(true);
                    _excelApp.OpenDoc(_file);
                    //_excelApp.SetExcelWindowHalfScreenRight();
                }
            }
            catch(Exception ex)
            {
                textBoxStatus.Text = ex.Message;
            }
        }

        private void SetStatus(string status)
        {
            if (textBoxStatus.TextLength + status.Length > textBoxStatus.MaxLength)
            {
                textBoxStatus.Clear();
            }
            if (!string.IsNullOrEmpty(textBoxStatus.Text))
            {
                textBoxStatus.Text += "\r\n";
            }
            textBoxStatus.Text += status;
            textBoxStatus.SelectionStart = textBoxStatus.TextLength;
            textBoxStatus.ScrollToCaret();
        }

        private async void Button_StartWork_Click(object sender, EventArgs e)
        {
            if (textBoxStatus.Text == Consts.MESSAGE_STOPING)
                return;

            try
            {
                Ready();
                SetStatus(Consts.MESSAGE_WORKING);

                IProgress<int> rowsCountProgress = new Progress<int>(s => lbTotalRows.Text = s.ToString());
                await Task.Run(() =>
                {
                    _excelApp.CalculateVisibleRows(rowsCountProgress, Utils.ParseStartRow(textBoxStartRowNumber.Text));
                });
                
                var execProgress = new Progress<string>(s => SetStatus(s));
                var rowProgress = new Progress<int>(s => textBoxStartRowNumber.Text = s.ToString());
                var browserProgress = new Progress<Browser>(s => browser = s);
                var ramainsRowsCount = new Progress<int>(s => lbRamainsRows.Text = s.ToString());
                var enviroment = new Enviroment
                {
                    StartRow = Utils.ParseStartRow(textBoxStartRowNumber.Text),
                    ExcelApp = _excelApp,
                    RowProgress = rowProgress,
                    ExecProgress = execProgress,
                    RamainsRowsProgress = ramainsRowsCount,
                    FileParser = new FileParser(_excelApp, settings),
                    Browser = browser,
                    BrowserProgress = browserProgress,
                };
                _operator = Operator.GetOperator(enviroment, settings);
                _operator.stop = false;
                await Task.Run(() =>
                {
                    _operator.Work();
                });
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            WorkStop();
        }

        private void WorkStop()
        {
            if (_operator != null)
            {
                if (!_operator.stop)
                {
                    _operator.stop = true;
                    textBoxStatus.Clear();
                    SetStatus(Consts.MESSAGE_STOPING);
                }
            }
        }

        private void labelSettings_Click(object sender, EventArgs e)
        {
            FormSettings form = new FormSettings(comboBoxOptions.Text);
            form.Show();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settings != null)
            {
                var currentID = Utils.GetCurrentProcess();
                var driverName = Consts.BrowserDriver[settings.BrowserType];
                Utils.KillChildDriverProcesses(currentID, driverName); 
            }
        }
    }
}