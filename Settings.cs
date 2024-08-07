using Newtonsoft.Json;
using System.IO;

namespace ListMaster
{
    public class Settings
    {
        public string ScriptName { get; set; }
        public BrowserType BrowserType { get; set; }
        public string URL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int Fullname { get; set; }
        public int Lastname { get; set; }
        public int Firstname { get; set; }
        public int Othername { get; set; }
        public int Bdate { get; set; }
        public int DocumentSeries { get; set; }
        public int DocumentNumber { get; set; }
        public int Registration { get; set; }
        public int DocumentSeriesNumber { get; set; }
        public int Nationality { get; set; }
        public int TargetColumn { get; set; }
        public int StartRow { get; set; }

        public static Settings DeserializeSettings(string file)
        {
            if (file == string.Empty)
            {
                return new Settings();
            }
            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(file));
            return settings;
        }

        public void SerializeSettings(string file)
        {
            if (!File.Exists(file))
            {
                var fileStream = File.Create(file);
                fileStream.Dispose();
            }
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
