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
        string updateFile = "https://raw.githubusercontent.com/ArtOS-Developper/WiiUCafeSDKDeluxe/main/Versions/update.json";

        public AboutWindow()
        {
            InitializeComponent();

            update = new UpdateInfo()
            {
                VersionMajor = 1,
                VersionMinor = 0,
                VersionPath = 0,
                DirectDownload = "https://github.com/ArtOS-Developper/WiiUCafeSDKDeluxe/releases",
            };

            label3.Text = "v" + update.VersionMajor + "." + +update.VersionMinor + "." + update.VersionPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ArtOS-Developper/WiiUCafeSDKDeluxe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string updateRemoteJson = client.DownloadString(updateFile);
            UpdateInfo remoteVersion = JsonSerializer.Deserialize<UpdateInfo>(updateRemoteJson);

            bool hereIsAnUpdate = false;
            if (remoteVersion.VersionMajor > update.VersionMajor)
            {
                if (remoteVersion.VersionMinor > update.VersionMinor)
                {
                    if (remoteVersion.VersionPath > update.VersionPath)
                    {
                        hereIsAnUpdate = true;
                    }
                    else
                    {
                        hereIsAnUpdate = false;
                    }
                }
                else
                {
                    hereIsAnUpdate = false;
                }
            }
            else
            {
                hereIsAnUpdate = false;
            }

            if (hereIsAnUpdate)
            {
                if(MessageBox.Show("An update is available, do you want to download it now ?", "Update found", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(remoteVersion.DirectDownload);
                }
            }
            else
            {
                MessageBox.Show("No update found", "No update found", MessageBoxButtons.OK);
            }
        }
    }

    public partial class UpdateInfo
    {
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionPath { get; set; }
        public string DirectDownload { get; set; }
    }
}
