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
			versionMinor = 1,
			versionPath = 1,
			DirectDownload = "https://github.com/ArtOS-Developper/WiiUCafeSDKDeluxe/releases"
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
		}

		public static void CheckForUpdates(bool isAutomatic = false)
		{
			UpdateInfo remoteVersion = JsonSerializer.Deserialize<UpdateInfo>(new WebClient().DownloadString("https://raw.githubusercontent.com/RetroAndDev/WiiUCafeSDKDeluxe/main/Versions/update.json"));
			bool hereIsAnUpdate = false;
			Program.logsManager.LogSucess("Remote version : " + remoteVersion.versionMajor + "." + remoteVersion.versionMinor + "." + remoteVersion.versionPath + " local : " + update.versionMajor + "." + update.versionMinor + "." + update.versionPath);
			if (update.versionMajor >= remoteVersion.versionMajor)
			{
				Program.logsManager.LogSucess("Local Major > or = to Remote Major");
				if (update.versionMinor >= remoteVersion.versionMinor)
				{
					Program.logsManager.LogSucess("Local Minor > or = to Local Minor");
					if (update.versionPath >= remoteVersion.versionPath)
					{
						Program.logsManager.LogSucess("Local Patch > or = to Local Patch");
						hereIsAnUpdate = false;
					}
					else
					{
						hereIsAnUpdate = true;
						Program.logsManager.LogSucess("Remote Patch < or = to local Patch");
					}
				}
				else
				{
					hereIsAnUpdate = true;
					Program.logsManager.LogSucess("Remote Minor < or = to local Minor");
				}
			}
			else
			{
				hereIsAnUpdate = true;
				Program.logsManager.LogSucess("Remote Major < or = to local Major");
			}
			if (hereIsAnUpdate)
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
        public string DirectDownload { get; set; }
    }
}
