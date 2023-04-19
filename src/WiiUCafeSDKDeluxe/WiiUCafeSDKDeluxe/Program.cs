using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using WinLogs;

namespace WiiUCafeSDKDeluxe
{
    internal static class Program
    {
        public static WinLogsManager logsManager = new WinLogsManager();
        public static Home home;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            logsManager.InitializeLogSystem();

            CheckFolders();

            SettingsManager.LoadSetings();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (CheckForProject())
            {
                logsManager.LogSucess("Project found. Loading Editor...");
                Application.Run(new ProjectLoadConfirm());
            }
            else
            {
                logsManager.LogWarning("Not Project found. Loading Creator...");
                DialogResult r = MessageBox.Show("WiiU Deluxe Tool didn't found any project in parent directory. Create new one ?", "No WiiU Deluxe Tool Project found", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    Application.Run(new CreateProject());
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        static bool CheckForProject()
        {
            //locate wiiudeluxeproj.json
            logsManager.Log("Checking for wiiudeluxeproj.json...");
            string parentDirPath = (new DirectoryInfo(Environment.CurrentDirectory)).Parent.FullName;
            string projFilePath = Path.Combine(parentDirPath, "wiiudeluxeproj.json");

            return File.Exists(projFilePath);
        }

        public static string GetProjectDataFilePath()
        {
            //locate wiiudeluxeproj.json
            string parentDirPath = (new DirectoryInfo(Environment.CurrentDirectory)).Parent.FullName;

            return Path.Combine(parentDirPath, "wiiudeluxeproj.json");
        }

        public static string GetMasteringDataFilePath()
        {
            //locate wiiudeluxeproj.json
            string parentDirPath = (new DirectoryInfo(Environment.CurrentDirectory)).Parent.FullName;

            return Path.Combine(parentDirPath, "mastering.json");
        }

        public static string GetProjectDataPath()
        {
            //locate wiiudeluxeproj.json
            string parentDirPath = (new DirectoryInfo(Environment.CurrentDirectory)).Parent.FullName;

            return parentDirPath;
        }

        public static ProjectData GetProjectData()
        {
            string dataPath = GetProjectDataFilePath();

            StreamReader dataReader = new StreamReader(dataPath);

            ProjectData projectData = JsonSerializer.Deserialize<ProjectData>(dataReader.ReadToEnd());

            dataReader.Close();
            return projectData;
        }

        public static Image GetImageDromBase64String(string base64Image)
        {
            byte[] bytes = Convert.FromBase64String(base64Image);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        public static string GetBase64StringFromImage(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static void OpenHome()
        {
            home = new Home();
            home.ShowDialog();
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static void CheckFolders()
        {
            logsManager.Log("Checking folders...");
            if (Directory.Exists(GetRoamingFolder()) == false)
            {
                logsManager.LogWarning("Application's Roaming Folder don't exists, creating one");
                Directory.CreateDirectory(GetRoamingFolder());
            }

            if (!File.Exists(GetSettingsFilePath()))
            {
                //Create Default settings file
                logsManager.LogWarning("Application's settings file don't exists, creating one with default settings");
                SettingsManager.SaveDefaultSettings();
            }

            if (!File.Exists(GetMasteringDataFilePath()))
            {
                //Create Default settings file
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
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        public static string FormatBytes(long bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

                max /= scale;
            }
            return "0 Bytes";
        }
    }
}