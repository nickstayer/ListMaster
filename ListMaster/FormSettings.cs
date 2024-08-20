using System;
using System.Windows.Forms;
using System.IO;

namespace ListMaster
{
    public partial class FormSettings : Form
    {
        public FormSettings(string scriptName)
        {
            try
            {
                InitializeComponent();
                string file = GetFile(scriptName);
                var settings = Settings.DeserializeSettings(file);
                FillForm(settings, file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFile(string scriptName)
        {
            string file;
            if (Consts.CacheDict.ContainsKey(scriptName))
            {
                file = Path.Combine(Directory.GetCurrentDirectory(), Consts.CacheDict[scriptName]);
                return file; 
            }
            if(scriptName == Consts.CHOOSE_SCRIPT || tbFileName.Text == string.Empty)
            {
                return string.Empty;
            }
            file = Path.Combine(Directory.GetCurrentDirectory(), tbFileName.Text);
            return file;
        }

        private void FillForm(Settings settings, string file)
        {
            FillCombobox();
            tbScriptName.Text = settings.ScriptName;
            cbBrowserType.Text = settings.BrowserType.ToString();
            tbURL.Text = settings.URL;

            tbUsername.Text = settings.Username;
            tbPassword.Text = settings.Password;
            tbFullname.Text = settings.Fullname.ToString();
            tbLastname.Text = settings.Lastname.ToString();
            tbFirstname.Text = settings.Firstname.ToString();
            tbOthername.Text = settings.Othername.ToString();
            tbBdate.Text = settings.Bdate.ToString();
            tbDocumentSeries.Text = settings.DocumentSeries.ToString();
            tbDocumentNumber.Text = settings.DocumentNumber.ToString();
            tbDocumentSeriesNumber.Text = settings.DocumentSeriesNumber.ToString();
            tbTargetColumn.Text = settings.TargetColumn.ToString();
            tbFileName.Text = file;
        }

        private void FillCombobox()
        {
            foreach (var browser in Enum.GetValues(typeof(BrowserType)))
            {
                cbBrowserType.Items.Add(browser.ToString());
            }
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain.settings = GetSettingsFromForm();
                string file = GetFile(FormMain.settings.ScriptName);
                FormMain.settings.SerializeSettings(file);
                MessageBox.Show(Consts.MESSAGE_SETTINGS_SAVED);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Settings GetSettingsFromForm()
        {
            var settings = new Settings();
            settings.ScriptName = tbScriptName.Text;
            settings.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), cbBrowserType.Text);
            settings.URL = tbURL.Text;

            settings.Username = tbUsername.Text;
            settings.Password = tbPassword.Text;
            settings.Fullname = int.Parse(tbFullname.Text);
            settings.Lastname = int.Parse(tbLastname.Text);
            settings.Firstname = int.Parse(tbFirstname.Text);
            settings.Othername = int.Parse(tbOthername.Text);
            settings.Bdate = int.Parse(tbBdate.Text);
            settings.DocumentSeries = int.Parse(tbDocumentSeries.Text);
            settings.DocumentNumber = int.Parse(tbDocumentNumber.Text);
            settings.DocumentSeriesNumber = int.Parse(tbDocumentSeriesNumber.Text);
            settings.TargetColumn = int.Parse(tbTargetColumn.Text);
            return settings;
        }
    }
}