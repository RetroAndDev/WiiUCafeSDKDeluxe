using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe.SetupWindows
{
    public partial class AboutWindow : Form
    {
        UpdateInfo update;
        string updateFile = "https://raw.githubusercontent.com/RetroAndDev/WiiUCafeSDKDeluxe/main/Versions/update.json";

        public AboutWindow()
        {
            InitializeComponent();

            update = new UpdateInfo()
            {
                versionMajor = 1,
                versionMinor = 0,
                versionPath = 2,
                DirectDownload = "https://github.com/ArtOS-Developper/WiiUCafeSDKDeluxe/releases",
            };

            label3.Text = "v" + update.versionMajor + "." + +update.versionMinor + "." + update.versionPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/RetroAndDev/WiiUCafeSDKDeluxe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string updateRemoteJson = client.DownloadString(updateFile);
            UpdateInfo remoteVersion = JsonSerializer.Deserialize<UpdateInfo>(updateRemoteJson);

            bool hereIsAnUpdate = false;
            Program.logsManager.LogSucess("Remote version : " + remoteVersion.versionMajor + "." + remoteVersion.versionMinor + "." + remoteVersion.versionPath + " local : " + update.versionMajor + "." + update.versionMinor + "." + update.versionPath);
            if (remoteVersion.versionMajor >= update.versionMajor)
            {
                Program.logsManager.LogSucess("Remote Major > or = to local Major");
                if (remoteVersion.versionMinor >= update.versionMinor)
                {
                    Program.logsManager.LogSucess("Remote Minor > or = to local Minor");
                    if (remoteVersion.versionPath > update.versionPath)
                    {
                        Program.logsManager.LogSucess("Remote Patch > or = to local Patch");
                        hereIsAnUpdate = true;
                    }
                    else
                    {
                        hereIsAnUpdate = false;
                        Program.logsManager.LogSucess("Remote Patch < or = to local Patch");
                    }
                }
                else
                {
                    hereIsAnUpdate = false;
                    Program.logsManager.LogSucess("Remote Minor < or = to local Minor");
                }
            }
            else
            {
                hereIsAnUpdate = false;
                Program.logsManager.LogSucess("Remote Major < or = to local Major");
            }

            if (hereIsAnUpdate)
            {
                if (MessageBox.Show("An update is available, do you want to download it now ?", "Update found", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("https://github.com/RetroAndDev/WiiUCafeSDKDeluxe/releases");
                }
            }
            else
            {
                MessageBox.Show("No update found", "No update found", MessageBoxButtons.OK);
            }
        }
    }

    public class UpdateInfo
    {
        public int versionMajor { get; set; }
        public int versionMinor { get; set; }
        public int versionPath { get; set; }
        public string DirectDownload { get; set; }
    }
}
