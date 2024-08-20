using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace ListMaster
{
    public partial class FormMain : Form
    {
        private void UpdateCombobox()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string file in files)
            {
                if (file.Contains(Consts.JSON_EXTENSION))
                {
                    UpdateCache(file);
                }
            }
            UpdateComboFromCache();
        }

        private void UpdateComboFromCache()
        {
            foreach(var pair in Consts.CacheDict)
            {
                if (!pair.Key.Contains(Consts.JSON_EXTENSION))
                {
                    comboBoxOptions.Items.Add(pair.Key); 
                }
            }
        }

        private static void UpdateCache(string file)
        {
            Settings? settings;
            try
            {
                settings = Settings.DeserializeSettings(file);
                if (!Consts.CacheDict.ContainsKey(file)
                    && !Consts.CacheDict.ContainsKey(settings.ScriptName))
                {
                    Consts.CacheDict.Add(file, settings.ScriptName);
                    Consts.CacheDict.Add(settings.ScriptName, file);
                }
            }
            catch
            {

            }
        }

        private void DefaultForm()
        {
            textBoxStatus.Clear();
            lbTotalRows.Text = "";
            lbRamainsRows.Text = "";
            labelFileName.Text = "";
        }

        private Settings GetSettings(string scriptName)
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Consts.CacheDict[scriptName]));
                return settings;
            }
            catch (Exception ex)
            {
                Text = Consts.MESSAGE_CONFIG_ERROR;
                SetStatus(ex.Message);
                throw ex;
            }
        }

        private void InitFile(string title, string filter)
        {
            string directory = Directory.GetCurrentDirectory();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.PathToList))
            {
                directory = GetPathFromFile(Properties.Settings.Default.PathToList);
            }
            OpenFileDialog myFile = new OpenFileDialog
            {
                InitialDirectory = directory,
                Title = title,
                Filter = filter
            };
            if (myFile.ShowDialog() == DialogResult.OK)
            {
                _file = myFile.FileName;
                Properties.Settings.Default.PathToList = _file;
                Properties.Settings.Default.Save();
            }
        }

        private string GetPathFromFile(string pathToFile)
        {
            var arr = pathToFile.Split('\\');
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < arr.Length - 1; i++)
            {
                sb.Append(arr[i]);
                sb.Append('\\');
            }
            return sb.ToString();
        }

        private void OutputFileNameToForm(string file)
        {
            var fileInfo = new FileInfo(file);
            var fileName = fileInfo.Name;
            var fileNameLength = fileName.Length;
            if (fileNameLength <= Consts.MAX_FILENAME_LENGTH)
            {
                labelFileName.Text = fileName;
            }
            else
            {
                int firstCharIndex = fileNameLength
                    - Consts.MAX_FILENAME_LENGTH;
                var result = fileName.Substring(firstCharIndex);
                labelFileName.Text = result;
            }
        }

        private void Ready()
        {
            CheckChoosedScript();
            CheckChoosedFile();
        }

        private void CheckChoosedScript()
        {
            if (string.IsNullOrEmpty(comboBoxOptions.Text)
                || comboBoxOptions.Text == Consts.CHOOSE_SCRIPT)
                throw new Exception(Consts.MESSAGE_NO_SCRIPT);
        }

        private void CheckChoosedFile()
        {
            if (_excelApp is null)
                throw new Exception(Consts.MESSAGE_NO_FILE);
        }
    }
}