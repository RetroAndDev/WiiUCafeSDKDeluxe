using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WiiUCafeSDKDeluxe
{
    public partial class CreateProject : Form
    {
        public CreateProject()
        {
            InitializeComponent();

            LoadUnityVersions();
        }

        void LoadUnityVersions()
        {
            unitySelection.Items.Clear();

            foreach(string unityVersion in ApplicationOptions.supportedUnityVersions)
            {
                unitySelection.Items.Add(unityVersion);
            }

            unitySelection.SelectedIndex = 0;
        }

        private void autoDetectCafe_Click(object sender, EventArgs e)
        {
            string cafeSdk = Environment.GetEnvironmentVariable("CAFE_ROOT");

            DialogResult r = MessageBox.Show("This is you Cafe SDK install ? - " + cafeSdk, "Cafe SDK Path", MessageBoxButtons.YesNo);

            if(r == DialogResult.Yes)
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
                TryExtractUnityVersion(folderDlg.SelectedPath);
            }
        }

        void TryExtractUnityVersion(string folder)
        {
            //Check if Unity Project Folder
            string assetsDir = Path.Combine(folder, "Assets");
            string libDir = Path.Combine(folder, "Library");
            string projSettingsDir = Path.Combine(folder, "ProjectSettings");
            if(Directory.Exists(assetsDir) && Directory.Exists(libDir) && Directory.Exists(projSettingsDir))
            {
                StreamReader projVersionReader = new StreamReader(Path.Combine(projSettingsDir, "ProjectVersion.txt"));
                string versionContent = projVersionReader.ReadToEnd();
                projVersionReader.Close();
                string unityVersion = versionContent.Substring(17, 10);
                DialogResult rr = MessageBox.Show("This is your Unity Project Version : " + unityVersion + ". This is correct ?", "Unity Version", MessageBoxButtons.YesNo);

                if (rr == DialogResult.Yes)
                {
                    CheckIfGoodUnityVersion(unityVersion);
                }
                else
                {
                    MessageBox.Show("Please enter manualy your Unity Version", "Enter manualy", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Selected Folder is not an Unity Project or Unity was not correctly setup", "Failed to locate 'ProjectSettings/ProjectVersion.txt'", MessageBoxButtons.OK);
            }
        }

        void CheckIfGoodUnityVersion(string UnityVersion)
        {
            if (ApplicationOptions.supportedUnityVersions.Contains(UnityVersion))
            {
                //Select it
                unitySelection.SelectedItem = UnityVersion;
            }
            else
            {
                MessageBox.Show("Your Unity Project Verion is not compatible with WiiU Cafe SDK Deluxe. You can select 'Other version' to use Unity " + UnityVersion + ". But It not sure that it will works", "Unsupported Unity Version", MessageBoxButtons.OK);
            }
        }

        private void browseIconBtn_Click(object sender, EventArgs e)
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

        private void validateBtn_Click(object sender, EventArgs e)
        {
            Image resided = iconBox.Image;

            /*if (resided.Size.Width > 128 && resided.Height > 128)
            {
                resided = Program.ResizeImage(iconBox.Image, new Size(128, 128));
            }*/

            ProjectData projectData = new ProjectData()
            {
                projectName = nameInput.Text,
                projectDeveloppers = devInput.Text,
                cafeSdkLocation = cafeSdkInput.Text,
                unityVersion = unitySelection.Text,
                projectIcon = Program.GetBase64StringFromImage(resided),
            };

            //Save it
            MessageBox.Show("The project data file will be saved in the parent directory.", "Save Project", MessageBoxButtons.OK);

            StreamWriter streamWriter = new StreamWriter(Program.GetProjectDataFilePath());

            string jsonContent = JsonSerializer.Serialize<ProjectData>(projectData);

            streamWriter.Write(jsonContent);
            streamWriter.Close();

            MessageBox.Show("The project data file as been saved in the parent directory. The app will restart", "Project Saved", MessageBoxButtons.OK);
            Application.Restart();
        }

        private void CreateProject_Load(object sender, EventArgs e)
        {

        }
    }
}
