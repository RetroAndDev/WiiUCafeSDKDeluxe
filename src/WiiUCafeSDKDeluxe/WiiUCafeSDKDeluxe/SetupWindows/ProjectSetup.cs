using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe.SetupWindows
{
    public partial class ProjectSetup : Form
    {
        ProjectData projectData;
        bool isSaving = false;

        public ProjectSetup()
        {
            InitializeComponent();

            foreach (string unityVersion in ApplicationOptions.supportedUnityVersions)
            {
                unitySelection.Items.Add(unityVersion);
            }

            LoadProjectInfo();
        }

        void LoadProjectInfo()
        {
            projectData = Program.GetProjectData();

            nameInput.Text = projectData.projectName;
            devInput.Text = projectData.projectDeveloppers;
            cafeSdkInput.Text = projectData.cafeSdkLocation;
            unitySelection.Text = projectData.unityVersion;

            if (File.Exists(Path.Combine(Program.GetProjectDataPath(), "BootScreens", "gamepad_splach.png")))
            {
                gamepadBox.Image = Image.FromFile(Path.Combine(Program.GetProjectDataPath(), "BootScreens", "gamepad_splach.png"));
            }

            if (File.Exists(Path.Combine(Program.GetProjectDataPath(), "BootScreens", "tv_splach.png")))
            {
                tvBox.Image = Image.FromFile(Path.Combine(Program.GetProjectDataPath(), "BootScreens", "tv_splach.png"));
            }
        }

        private void autoDetectCafe_Click(object sender, EventArgs e)
        {
            string cafeSdk = Environment.GetEnvironmentVariable("CAFE_ROOT");

            DialogResult r = MessageBox.Show("This is you Cafe SDK install ? - " + cafeSdk, "Cafe SDK Path", MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                cafeSdkInput.Text = cafeSdk;
            }
        }

        private void autoDetectUnityBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = false;
            folderDlg.RootFolder = Environment.SpecialFolder.Desktop;
            folderDlg.Description = "Locate Unity Project Folder";
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Check if Unity Project Folder
                string assetsDir = Path.Combine(folderDlg.SelectedPath, "Assets");
                string libDir = Path.Combine(folderDlg.SelectedPath, "Library");
                string projSettingsDir = Path.Combine(folderDlg.SelectedPath, "ProjectSettings");
                if (Directory.Exists(assetsDir) && Directory.Exists(libDir) && Directory.Exists(projSettingsDir))
                {
                    StreamReader projVersionReader = new StreamReader(Path.Combine(projSettingsDir, "ProjectVersion.txt"));
                    string versionContent = projVersionReader.ReadToEnd();
                    projVersionReader.Close();
                    string unityVersion = versionContent.Substring(17, 10);
                    DialogResult rr = MessageBox.Show("This is your Unity Project Version : " + unityVersion + ". This is correct ?", "Unity Version", MessageBoxButtons.YesNo);

                    if (rr == DialogResult.Yes)
                    {
                        if (ApplicationOptions.supportedUnityVersions.Contains(unityVersion))
                        {
                            //Select it
                            unitySelection.SelectedItem = unityVersion;
                        }
                        else
                        {
                            MessageBox.Show("Your Unity Project Verion is not compatible with WiiU Cafe SDK Deluxe. You can select 'Other version' to use Unity " + unityVersion + ". But It not sure that it will works", "Unsupported Unity Version", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter manualy your Unity Version", "Enter manualy", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void ProjectSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaving == false)
            {
                SaveProjectData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveProjectData();
        }

        void SaveProjectData()
        {
            ProjectData newData = new ProjectData()
            {
                projectName = nameInput.Text,
                projectDeveloppers = devInput.Text,
                cafeSdkLocation = cafeSdkInput.Text,
                unityVersion = unitySelection.SelectedText,
                projectIcon = Program.GetBase64StringFromImage(iconBox.Image),
            };

            if(newData != projectData)
            {
                isSaving = true;

                /*if (resided.Size.Width > 128 && resided.Height > 128)
                {
                    resided = Program.ResizeImage(iconBox.Image, new Size(128, 128));
                }*/

                projectData.projectName = nameInput.Text;
                projectData.projectDeveloppers = devInput.Text;
                projectData.cafeSdkLocation = cafeSdkInput.Text;
                projectData.unityVersion = unitySelection.SelectedText;
                projectData.projectIcon = Program.GetBase64StringFromImage(iconBox.Image);

                StreamWriter streamWriter = new StreamWriter(Program.GetProjectDataFilePath());

                string jsonContent = JsonSerializer.Serialize<ProjectData>(projectData);

                streamWriter.Write(jsonContent);
                streamWriter.Close();

                MessageBox.Show("The project data file as been saved in the parent directory. The app will restart", "Project Saved", MessageBoxButtons.OK);
                Application.Restart();
            }
        }

        private void browseMenuBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select Wii U Application Icon";
            open.Filter = "PNG Files(*.png)|*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                if (img.Size.Width > 128 && img.Height > 128)
                {
                    MessageBox.Show("Your Wii U Menu Icon must be 128x128px. Resize It or add it later.", "Menu Icon too big", MessageBoxButtons.OK);
                }
                else
                {
                    iconBox.Image = img;
                }
            }
        }

        private void browseGpBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select Wii U GamePad Startup Screen";
            open.Filter = "PNG Files(*.png)|*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                if (img.Size.Width > 854 && img.Height > 480)
                {
                    MessageBox.Show("Your Wii U GamePad Startup screen must be 854x480. Resize It or add it later.", "Startup image too big", MessageBoxButtons.OK);
                }
                else
                {
                    gamepadBox.Image = img;
                    if (!Directory.Exists(Path.Combine(Program.GetProjectDataPath(), "BootScreens")))
                    {
                        Directory.CreateDirectory(Path.Combine(Program.GetProjectDataPath(), "BootScreens"));
                    }
                    img.Save(Path.Combine(Program.GetProjectDataPath(), "BootScreens", "gamepad_splach.png"));
                }
            }
        }

        private void browseTvBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select Wii U TV Startup Screen";
            open.Filter = "PNG Files(*.png)|*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                if (img.Size.Width > 1920 && img.Height > 720)
                {
                    MessageBox.Show("Your Wii U TV Startup screen must be 1920x720. Resize It or add it later.", "Startup image too big", MessageBoxButtons.OK);
                }
                else
                {
                    tvBox.Image = img;
                    if (!Directory.Exists(Path.Combine(Program.GetProjectDataPath(), "BootScreens")))
                    {
                        Directory.CreateDirectory(Path.Combine(Program.GetProjectDataPath(), "BootScreens"));
                    }
                    img.Save(Path.Combine(Program.GetProjectDataPath(), "BootScreens", "tv_splach.png"));
                }
            }
        }
    }
}
