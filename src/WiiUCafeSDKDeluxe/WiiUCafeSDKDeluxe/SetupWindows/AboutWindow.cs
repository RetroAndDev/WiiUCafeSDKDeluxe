using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe.SetupWindows
{
	public partial class AboutWindow : Form
	{
		public static UpdateInfo update = new UpdateInfo
		{
			versionMajor = 1,
			versionMinor = 2,
			versionPath = 0,
            versionBuild = 1,
			DirectDownload = "https://github.com/RetroAndDev/WiiUCafeSDKDeluxe/releases"
        };

		public const string updateFile = "https://raw.githubusercontent.com/RetroAndDev/WiiUCafeSDKDeluxe/main/Versions/update.json";

		public AboutWindow()
		{
			InitializeComponent();
			label3.Text = "v" + update.versionMajor + "." + update.versionMinor + "." + update.versionPath;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/RetroAndDev/WiiUCafeSDKDeluxe");
		}

		private void button1_Click(object sender, EventArgs e)
		{
            Process.Start("https://github.com/RetroAndDev/WiiUCafeSDKDeluxe/releases");
        }

		public static void CheckForUpdates(bool isAutomatic = false)
		{
			UpdateInfo remoteVersion = JsonSerializer.Deserialize<UpdateInfo>(new WebClient().DownloadString("https://raw.githubusercontent.com/RetroAndDev/WiiUCafeSDKDeluxe/main/Versions/update.json"));
			Program.logsManager.LogSucess("Remote version : " + remoteVersion.versionMajor + "." + remoteVersion.versionMinor + "." + remoteVersion.versionPath + " local : " + update.versionMajor + "." + update.versionMinor + "." + update.versionPath);
			if (!UpdateInfo.IsUpToDate(update, remoteVersion))
			{
				if (MessageBox.Show("An update is available, do you want to download it now ?", "Update found", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Process.Start("https://github.com/RetroAndDev/WiiUCafeSDKDeluxe/releases");
				}
			}
			else if (!isAutomatic)
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
		public int versionBuild { get; set; } = 0;
        public string DirectDownload { get; set; }

        public static bool IsUpToDate(UpdateInfo applicationUpdate, UpdateInfo lasetUpdate)
        {
            bool isUpToDate = true;

            //Check major version
            if (applicationUpdate.versionMajor >= lasetUpdate.versionMajor)
            {
                Program.logsManager.Log("[Updater] Local Major >= remote Major");

                //Check minor version
                if (applicationUpdate.versionMinor >= lasetUpdate.versionMinor)
                {
                    Program.logsManager.Log("[Updater] Local Minor >= remote Minor");

                    //Check patch version
                    if (applicationUpdate.versionPath >= lasetUpdate.versionPath)
                    {
                        Program.logsManager.Log("[Updater] Local Patch >= remote Pacth");

                        //Check build version
                        if (applicationUpdate.versionBuild >= lasetUpdate.versionBuild)
                        {
                            //application BUILD is UP or EQUAL to laset remote
                            Program.logsManager.Log("[Updater] Local Build >= remote Build");
                            isUpToDate = true;
                        }
                        else
                        {
                            //HERE IS AN UPDATE
                            Program.logsManager.Log("[Updater] Local Build < remote Build");
                            isUpToDate = false;
                        }
                    }
                    else
                    {
                        //HERE IS AN UPDATE
                        Program.logsManager.Log("[Updater] Local Patch < remote Patch");
                        isUpToDate = false;
                    }
                }
                else
                {
                    //HERE IS AN UPDATE
                    Program.logsManager.Log("[Updater] Local Minor < remote Minor");
                    isUpToDate = false;
                }
            }
            else
            {
                //HERE IS AN UPDATE
                Program.logsManager.Log("[Updater] Local Major < remote Major");
                isUpToDate = false;
            }

            return isUpToDate;
        }
    }
}
