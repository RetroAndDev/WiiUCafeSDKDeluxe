using System.IO;
using System.Text.Json;
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
			CheckPaths();
		}

		public static void CheckPaths()
		{
			Program.logsManager.Log("[AppCheck] AppCheck is going to check all tools paths");
			if (applicationSettings.javaPath.EndsWith(".exe"))
			{
                if (File.Exists(applicationSettings.javaPath))
                {
                    Program.logsManager.LogSucess("[AppCheck] java.exe exists at " + applicationSettings.javaPath);
                    string sha257 = Program.GetSHA256(applicationSettings.javaPath);
                    if (sha257 == ApplicationOptions.defaultJavaSha256)
                    {
                        Program.logsManager.LogSucess("[AppCheck] Java is JDK 18.0.2.1, and support is confirmed !");
                    }
                    else
                    {
                        Program.logsManager.LogWarning("[AppCheck] Java is UNDIFINED, and support is not confirmed ! Continue at your own risk(s). If you want this message to go, install and specify Java 18.0.2.1 in settings");
                        Program.logsManager.LogWarning("[AppCheck] Java JDK 18.0.2.1 sha256: " + ApplicationOptions.defaultJavaSha256 + ", Installed Java sha256: " + sha257);
                    }
                }
                else
                {
                    Program.logsManager.LogError("[AppCheck] java.exe don't exists at " + applicationSettings.javaPath);
                    MessageBox.Show("java.exe is not located. Please install it and specify Java Path in Settings, or use/check your Java Path in Settings. The Tool will exit", "AppCheck Fatal Error", MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
			else
			{
                Program.logsManager.LogWarning("[AppCheck] Unable to check Java patch. You settings don't point to a valid exe file. Ignore this message if you use 'java' alias as Java Path");
			}
			if (applicationSettings.pythonPath.EndsWith(".exe"))
			{
                if (File.Exists(applicationSettings.pythonPath))
                {
                    Program.logsManager.LogSucess("[AppCheck] python.exe exists at " + applicationSettings.pythonPath);
                    string sha256 = Program.GetSHA256(applicationSettings.pythonPath);
                    if (sha256 == ApplicationOptions.defaultPythonSha256)
                    {
                        Program.logsManager.LogSucess("[AppCheck] Python is Python-3.7.3, and support is confirmed !");
                    }
                    else
                    {
                        Program.logsManager.LogWarning("[AppCheck] Python is UNDIFINED, and support is not confirmed ! Continue at your own risk(s). If you want this message to go, install and specify Python 3.7.3 in settings");
                        Program.logsManager.LogWarning("[AppCheck] Python 3.7.3 sha256: " + ApplicationOptions.defaultPythonSha256 + ", Installed Python sha256: " + sha256);
                    }
                }
                else
                {
                    Program.logsManager.LogError("[AppCheck] python.exe don't exists at " + applicationSettings.pythonPath);
                    MessageBox.Show("python.exe is not located. Please install it and specify Python Path in Settings, or use/check your Python Path in Settings. The Tool will exit", "AppCheck Fatal Error", MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
            else
            {
                Program.logsManager.LogWarning("[AppCheck] Unable to check Python patch. You settings don't point to a valid exe file. Ignore this message if you use 'python'/'py' alias as Python Path");

            }
            Program.logsManager.Log("[AppCheck] AppCheck terminated without any error(s)");
		}

		public static void SaveSettings()
		{
			Program.logsManager.Log("Saving settings.json file to application's roaming folder");
			StreamWriter streamWriter = new StreamWriter(Program.GetSettingsFilePath());
			string jsonSettings = JsonSerializer.Serialize(applicationSettings);
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
		}
	}

    public class ApplicationSettings
    {
        public WiiUHardware wiiUHardware { get; set; } = new WiiUHardware();
        public string javaPath { get; set; } = ApplicationOptions.defaultJavaPath;
        public string pythonPath { get; set; } = ApplicationOptions.defaultPythonPath;
        public string parentCafeSdk { get; set; } = "C:\\Nintendo\\WiiUSDK\\";
    }
}
