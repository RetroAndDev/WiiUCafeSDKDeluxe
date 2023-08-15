using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe.SetupWindows
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            textBox1.Text = SettingsManager.applicationSettings.javaPath;
            textBox2.Text = SettingsManager.applicationSettings.pythonPath;
            textBox3.Text = SettingsManager.applicationSettings.parentCafeSdk;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            SettingsManager.applicationSettings.pythonPath = textBox2.Text;
            SettingsManager.applicationSettings.javaPath = textBox1.Text;
            SettingsManager.applicationSettings.parentCafeSdk = textBox3.Text;

            SettingsManager.SaveSettings();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select java.exe file";
            fileDialog.Multiselect = false;
            fileDialog.Filter = "java.exe (java.exe) | java.exe";
            fileDialog.InitialDirectory = @"C:\Program Files\";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select python.exe file";
            fileDialog.Multiselect = false;
            fileDialog.Filter = "python.exe (python.exe) | python.exe";
            fileDialog.InitialDirectory = @"C:\Program Files\";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fileDialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Locate the Cafe SDK Parent folder";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                string cafeEnvFile = Path.Combine(dialog.SelectedPath, "SDK", "cafe_sdk", "cafex_env.bat");
                if (File.Exists(cafeEnvFile))
                {
                    textBox3.Text = dialog.SelectedPath;
                }
                else
                {
                    MessageBox.Show("The selected folder does not contain any Wii U Cafe SDK", "No SDK found", MessageBoxButtons.OK);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.logsManager.Log("[Settings] Running Manual Path Checking");
            SettingsManager.CheckPaths();
            MessageBox.Show("Paths Checked ! Check in logs", "Finished", MessageBoxButtons.OK);
        }
    }
}
