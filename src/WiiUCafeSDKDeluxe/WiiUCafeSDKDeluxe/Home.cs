using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiiUCafeSDKDeluxe.SetupWindows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WiiUCafeSDKDeluxe
{
    public partial class Home : Form
    {
        public ProjectData ProjectData { get; private set; }
        public List<GameVersion> versions = new List<GameVersion>();

        public Home()
        {
            InitializeComponent();

            LoadProjectInfos();

            LoadVersions();

            LoadCafeTools();
        }

        void LoadCafeTools()
        {
            if (File.Exists(Path.Combine(SettingsManager.applicationSettings.parentCafeSdk, "Tools", "app_configuration_tool", "bin", "appconfig.exe")))
            {
                configToolInstall.Text = "App Config Tool is installed";
                configToolInstall.ForeColor = Color.Green;
                button16.Enabled = true;
            }
            else
            {
                configToolInstall.Text = "App Config Tool is not installed. Please install it with NDI";
                configToolInstall.ForeColor = Color.Red;
                button16.Enabled = false;
            }

            if (File.Exists(Path.Combine(SettingsManager.applicationSettings.parentCafeSdk, "Tools", "master_editor", "bin", "mastereditor.exe")))
            {
                label7.Text = "Master Editor is installed";
                label7.ForeColor = Color.Green;
                button17.Enabled = true;
            }
            else
            {
                label7.Text = "Master Editor is not installed. Please install it with NDI";
                label7.ForeColor = Color.Red;
                button17.Enabled = false;
            }
        }

        void LoadProjectInfos()
        {
            ProjectData = Program.GetProjectData();

            pictureBox1.Image = Program.GetImageDromBase64String(ProjectData.projectIcon);
            label1.Text = ProjectData.projectName;
            label2.Text = ProjectData.projectDeveloppers;

            this.Text = this.Text.Replace("#ProjectName#", ProjectData.projectName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MasterOption masterOption = new MasterOption();
            masterOption.ShowDialog();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingsManager.CheckSettingsBeforeClose();

            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CreateVersion createVersion = new CreateVersion(this);
            createVersion.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HardwareSetup hardware = new HardwareSetup();
            hardware.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LogsWindow logsWindow = new LogsWindow();
            logsWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProjectSetup projectSetup = new ProjectSetup();
            projectSetup.ShowDialog();
        }

        public void LoadVersions()
        {
            versions.Clear();

            string versionDir = Path.Combine(Program.GetProjectDataPath(), "Versions");
            if (!Directory.Exists(versionDir))
            {
                Directory.CreateDirectory(versionDir);
            }

            string[] fileVersions = Directory.GetFiles(versionDir, "*.json", SearchOption.TopDirectoryOnly);
            foreach(string version in fileVersions)
            {
                StreamReader reader = new StreamReader(version);
                string jsonVersion = reader.ReadToEnd();
                reader.Close();
                
                versions.Add(JsonSerializer.Deserialize<GameVersion>(jsonVersion));
            }

            foreach(GameVersion gameVersion in versions)
            {
                comboBox1.Items.Add(gameVersion.VersionName);
            }

            if(comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Visible = true;
            DisplayVersion();
        }

        void DisplayVersion()
        {
            GameVersion version = versions[comboBox1.SelectedIndex];

            label4.Text = version.VersionName;
            CalculateCopyToSdFile(version.VersionName);
        }

        void CalculateCopyToSdFile(string versionName)
        {
            string copyToSdPath = Path.Combine(Program.GetProjectDataPath(), versionName, "copyToSDCard");
            if (Directory.Exists(copyToSdPath))
            {
                long size = Program.DirSize(new DirectoryInfo(copyToSdPath));
                label3.Text = "CopyToSd Size: " + Program.FormatBytes(size);
                Program.logsManager.LogSucess("Calculated copyToSDCard folder in " + versionName);
            }
            else
            {
                Program.logsManager.LogError("Unable to locate copyToSDCard in " + versionName);
                MessageBox.Show("Unable to locate and calculate copyToSDCard folder in version : " + versionName, "Version Data Missing", MessageBoxButtons.OK);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GameVersion version = versions[comboBox1.SelectedIndex];

            string rawFiles = Path.Combine(Program.GetProjectDataPath(), version.VersionName);
            Process.Start(rawFiles);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GameVersion version = versions[comboBox1.SelectedIndex];

            string rawFiles = Path.Combine(Program.GetProjectDataPath(), version.VersionName, "copyToSDCard");
            Process.Start(rawFiles);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string versionFilePath = Path.Combine(Program.GetProjectDataPath(), "Versions", comboBox1.Text + ".json");
            Program.logsManager.Log("Located version file to delete : " + versionFilePath);
            if (File.Exists(versionFilePath))
            {
                GameVersion gameVersion = versions[comboBox1.SelectedIndex];
                string versionDataPath = Path.Combine(Program.GetProjectDataPath(), gameVersion.VersionName);
                DialogResult r = MessageBox.Show("Delete version : " + gameVersion.VersionName + " ?", "Delete Version ?", MessageBoxButtons.YesNo);
                if(r == DialogResult.Yes)
                {
                    File.Delete(versionFilePath);
                    LoadVersions();
                    Program.logsManager.LogSucess("Sucessfully deleted version file at " + versionFilePath);
                    DialogResult r2 = MessageBox.Show("Delete files ?", "Delete Version ?", MessageBoxButtons.YesNo);
                    if(r2 == DialogResult.Yes)
                    {
                        Directory.Delete(versionDataPath, true);
                        Program.logsManager.LogSucess("Sucessfully deleted version data at " + versionDataPath);
                    }
                }

                LoadVersions();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Import Version option import and create a version from the folder who contain the code/meta/content folders or wumad file created from Unity. Depending on the size of your build, the application will freeze more or less time.", "Import details", MessageBoxButtons.OK);

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Folder where import version";
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dirName = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                
                if (!Directory.Exists(Path.Combine(Program.GetProjectDataPath(), dirName.Name)))
                {
                    Directory.CreateDirectory(Path.Combine(Program.GetProjectDataPath(), dirName.Name));
                    string versionFilePath = Path.Combine(Program.GetProjectDataPath(), "Versions", dirName.Name + ".json");
                    GameVersion version = new GameVersion();
                    version.VersionName = dirName.Name;
                    if (Directory.Exists(Path.Combine(folderBrowserDialog.SelectedPath, "copyToSDCard")))
                    {
                        version.UnityBuildMode = 1;
                    }
                    else
                    {
                        if (File.Exists(Path.Combine(folderBrowserDialog.SelectedPath, "Unity-master.wumad")))
                        {
                            version.UnityBuildMode = 2;
                        }
                        else
                        {
                            if (File.Exists(Path.Combine(folderBrowserDialog.SelectedPath, "Unity-relase.wumad")))
                            {
                                version.UnityBuildMode = 2;
                            }
                            else
                            {
                                version.UnityBuildMode = 0;
                            }
                        }
                    }
                    string versionJson = JsonSerializer.Serialize(version);
                    StreamWriter streamWriter = new StreamWriter(versionFilePath);
                    streamWriter.Write(versionJson);
                    streamWriter.Close();

                    Program.logsManager.Log("[ImportVersion] Version Profile Created with parameters : " + version.VersionName + " / " + version.UnityBuildMode);

                    foreach (string dirPath in Directory.GetDirectories(folderBrowserDialog.SelectedPath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(folderBrowserDialog.SelectedPath, Path.Combine(Program.GetProjectDataPath(), dirName.Name)));
                        Program.logsManager.LogSucess("[ImportVersion] Folder copied from '" + folderBrowserDialog.SelectedPath + "' to '" + Path.Combine(Program.GetProjectDataPath(), dirName.Name) + "' succed !");
                    }

                    foreach (string newPath in Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(folderBrowserDialog.SelectedPath, Path.Combine(Program.GetProjectDataPath(), dirName.Name)), true);
                        Program.logsManager.LogSucess("[ImportVersion] File copied from '" + newPath + "' to '" + Path.Combine(Program.GetProjectDataPath(), dirName.Name) + "' succed !");
                    }

                    Program.logsManager.LogSucess("Version " + version.VersionName + " imported");
                }
                else
                {
                    MessageBox.Show("A version with the same name already exists. Change version name in Wii U Cafe SDK Deluxe or the impot folder name");
                    Program.logsManager.LogWarning("The version who user tried to install already exists in Wii U Cafe SDK Deluxe");
                }
            }

            LoadVersions();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GameVersion version = versions[comboBox1.SelectedIndex];

            StreamReader streamReader = new StreamReader(Program.GetMasteringDataFilePath());
            string masteringJson = streamReader.ReadToEnd();
            streamReader.Close();

            MasteringData masteringData = JsonSerializer.Deserialize<MasteringData>(masteringJson);

            MasteringWindow masteringWindow = new MasteringWindow(masteringData.WiiUMasterSettings.masterProcessMode, Path.Combine(Program.GetProjectDataPath() ,version.VersionName), masteringData.WiiUMasterSettings, masteringData.WiIUProjectSettings);
            masteringWindow.Show();
            masteringWindow.StartProcess();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(SettingsManager.applicationSettings.parentCafeSdk, "Tools", "app_configuration_tool", "bin", "appconfig.exe"));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(SettingsManager.applicationSettings.parentCafeSdk, "Tools", "master_editor", "bin", "mastereditor.exe"));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("https://artos-developper.github.io/WiiUCafeSDKDeluxe/");
        }
    }
}
