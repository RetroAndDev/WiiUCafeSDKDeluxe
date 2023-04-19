using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe
{
    public static class SettingsManager
    {
        public static ApplicationSettings applicationSettings;

        public static void LoadSetings()
        {
            Program.logsManager.Log("Loading settings.json file from application's roaming folder");
            StreamReader streamReader = new StreamReader(Program.GetSettingsFilePath());
            string jsonSettings = streamReader.ReadToEnd();
            streamReader.Close();

            applicationSettings = JsonSerializer.Deserialize<ApplicationSettings>(jsonSettings);
        }

        public static void SaveSettings()
        {
            Program.logsManager.Log("Saving settings.json file to application's roaming folder");
            StreamWriter streamWriter = new StreamWriter(Program.GetSettingsFilePath());
            string jsonSettings = JsonSerializer.Serialize<ApplicationSettings>(applicationSettings);
            streamWriter.Write(jsonSettings);
            streamWriter.Close();
        }

        public static void SaveDefaultSettings()
        {
            Program.logsManager.Log("Creating and saving default settings.json file to application's roaming folder");
            applicationSettings = new ApplicationSettings();

            SaveSettings();
        }

        public static void CheckSettingsBeforeClose()
        {
            /*StreamReader streamReader = new StreamReader(Program.GetSettingsFilePath());
            string jsonSettings = streamReader.ReadToEnd();
            streamReader.Close();

            ApplicationSettings savedSettings = JsonSerializer.Deserialize<ApplicationSettings>(jsonSettings);

            if(savedSettings != applicationSettings)
            {
                DialogResult r = MessageBox.Show("Some settings are not saved. Save it before exiting ?", "Save settings ?", MessageBoxButtons.YesNo);

                if(r == DialogResult.Yes)
                {
                    SaveSettings();
                }
            }*/
        }
    }

    public class ApplicationSettings
    {
        public WiiUHardware wiiUHardware { get; set; } = new WiiUHardware();
        public string javaPath { get; set; } = ApplicationOptions.defaultJavaPath;
        public string pythonPath { get; set; } = ApplicationOptions.defaultPythonPath;
        public string parentCafeSdk { get; set; } = @"C:\Nintendo\WiiUSDK\";
    }
}
