using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Windows.Forms;
using WiiUCafeSDKDeluxe.SetupWindows;
using WinLogs;

namespace WiiUCafeSDKDeluxe
{
	internal static class Program
	{
		public static Dictionary<string, string> Paths = new Dictionary<string, string>();
		public static WinLogsManager logsManager = new WinLogsManager();
		public static Home home;

		[STAThread]
		private static void Main()
		{
			logsManager.InitializeLogSystem();
            SettingsManager.LoadSetings();

            InitPaths();
			CheckFolders();
			CheckPaths();
			//AboutWindow.CheckForUpdates(isAutomatic: true); -- Bugged. I hate make auto-updaters. -- Issue with if local minor is > from server. Ex local 1.2.0 detect update if server is 1.1.1...
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			if (CheckForProject())
			{
				logsManager.LogSucess("Project found. Loading Editor...");
				Application.Run(new ProjectLoadConfirm());
				return;
			}
			logsManager.LogWarning("Not Project found. Loading Creator...");
			if (MessageBox.Show("WiiU Deluxe Tool didn't found any project in parent directory. Create new one ?", "No WiiU Deluxe Tool Project found", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Application.Run(new CreateProject());
			}
			else
			{
				Application.Exit();
			}
		}

		private static void InitPaths()
		{
			Paths["CDecrypt"] = Path.Combine(Environment.CurrentDirectory, "tools", "CDecrypt.exe");
			Paths["NUSPacker"] = Path.Combine(Environment.CurrentDirectory, "tools", "NUSPacker.jar");
			Paths["KiiU"] = Path.Combine(Environment.CurrentDirectory, "keygen.py");
		}

		private static void CheckPaths()
		{
			logsManager.Log("[AppCheck] AppCheck is going to check all tools paths");
            if (File.Exists(Paths["NUSPacker"]))
            {
                logsManager.LogSucess("[AppCheck] NUSPacker.jar exists at " + Paths["NUSPacker"]);
            }
            else
            {
                logsManager.LogError("[AppCheck] NUSPacker.jar don't exists at " + Paths["NUSPacker"]);
                MessageBox.Show("NUSPacker.jar is not located. Please install it at " + Paths["NUSPacker"] + ", or reinstall Wii U CafeSDK Deluxe. The Tool will exit", "AppCheck Fatal Error", MessageBoxButtons.OK);
                Application.Exit();
            }
            if (File.Exists(Paths["CDecrypt"]))
			{
				logsManager.LogSucess("[AppCheck] CDecrypt.exe exists at " + Paths["CDecrypt"]);
			}
			else
			{
				logsManager.LogError("[AppCheck] CDecrypt.exe don't exists at " + Paths["CDecrypt"]);
				MessageBox.Show("CDecrypt.exe is not located. Please install it at " + Paths["CDecrypt"] + ", or reinstall Wii U CafeSDK Deluxe. The Tool will exit", "AppCheck Fatal Error", MessageBoxButtons.OK);
				Application.Exit();
			}
			if (File.Exists(Paths["KiiU"]))
			{
				logsManager.LogSucess("[AppCheck] keygen.py exists at " + Paths["KiiU"]);
			}
			else
			{
				logsManager.LogError("[AppCheck] keygen.py don't exists at " + Paths["KiiU"]);
				MessageBox.Show("keygen.py is not located. Please reinstall Wii U CafeSDK Deluxe or the file 'keygen.py' form GitHub and place it at '" + Paths["KiiU"] + "'. The Tool will exit", "AppCheck Fatal Error", MessageBoxButtons.OK);
				Application.Exit();
			}
			logsManager.Log("[AppCheck] AppCheck terminated without any error(s)");
		}

		private static bool CheckForProject()
		{
			logsManager.Log("Checking for wiiudeluxeproj.json...");
			return File.Exists(Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName, "wiiudeluxeproj.json"));
		}

		public static string GetProjectDataFilePath()
		{
			return Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName, "wiiudeluxeproj.json");
		}

		public static string GetMasteringDataFilePath()
		{
			return Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName, "mastering.json");
		}

		public static string GetProjectDataPath()
		{
			return new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName;
		}

		public static ProjectData GetProjectData()
		{
			StreamReader streamReader = new StreamReader(GetProjectDataFilePath());
			ProjectData projectData = JsonSerializer.Deserialize<ProjectData>(streamReader.ReadToEnd());
			streamReader.Close();
			return projectData;
		}

		public static Image GetImageDromBase64String(string base64Image)
		{
			using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64Image)))
			{
				return Image.FromStream(ms);
			}
		}

		public static string GetBase64StringFromImage(Image image)
		{
			using (MemoryStream i = new MemoryStream())
			{
				image.Save(i, image.RawFormat);
				return Convert.ToBase64String(i.ToArray());
			}
		}

		public static void OpenHome()
		{
			home = new Home();
			home.ShowDialog();
		}

		public static Image ResizeImage(Image imgToResize, Size size)
		{
			return new Bitmap(imgToResize, size);
		}

		public static void CheckFolders()
		{
			logsManager.Log("Checking folders...");
			if (!Directory.Exists(GetRoamingFolder()))
			{
				logsManager.LogWarning("Application's Roaming Folder don't exists, creating one");
				Directory.CreateDirectory(GetRoamingFolder());
			}
			if (!File.Exists(GetSettingsFilePath()))
			{
				logsManager.LogWarning("Application's settings file don't exists, creating one with default settings");
				SettingsManager.SaveDefaultSettings();
			}
			if (!File.Exists(GetMasteringDataFilePath()))
			{
				logsManager.LogWarning("Application's mastering file don't exists, creating one with default settings");
				MasteringData masteringData = new MasteringData();
				masteringData.WiIUProjectSettings = WiIUProjectSettings.GenerateDefault();
				masteringData.WiiUMasterSettings = new WiiUMasterSettings();
				StreamWriter streamWriter = new StreamWriter(GetMasteringDataFilePath());
				string dataJson = JsonSerializer.Serialize(masteringData);
				streamWriter.Write(dataJson);
				streamWriter.Close();
			}
		}

		public static string GetSettingsFilePath()
		{
			return Path.Combine(GetRoamingFolder(), "settings.json");
		}

		public static string GetRoamingFolder()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WiiUCafeSDKDeluxe");
		}

		public static long DirSize(DirectoryInfo d)
		{
			long size = 0L;
			FileInfo[] files = d.GetFiles();
			foreach (FileInfo fi in files)
			{
				size += fi.Length;
			}
			DirectoryInfo[] directories = d.GetDirectories();
			foreach (DirectoryInfo di in directories)
			{
				size += DirSize(di);
			}
			return size;
		}

		public static string FormatBytes(long bytes)
		{
			string[] orders = new string[4] { "GB", "MB", "KB", "Bytes" };
			long max = (long)Math.Pow(1024.0, orders.Length - 1);
			string[] array = orders;
			foreach (string order in array)
			{
				if (bytes > max)
				{
					return $"{decimal.Divide(bytes, max):##.##} {order}";
				}
				max /= 1024;
			}
			return "0 Bytes";
		}

		public static string GetSHA256(string filePath)
		{
			using (SHA256 SHA256 = SHA256.Create())
			{
				using (FileStream fileStream = File.OpenRead(filePath))
				{
					return BitConverter.ToString(SHA256.ComputeHash(fileStream)).Replace("-", string.Empty).ToLower();
				}
			}
		}
	}
}
