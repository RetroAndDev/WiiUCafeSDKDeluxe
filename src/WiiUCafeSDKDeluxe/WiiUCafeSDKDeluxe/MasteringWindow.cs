using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WiiUCafeSDKDeluxe.SetupWindows;

namespace WiiUCafeSDKDeluxe
{
	public partial class MasteringWindow : Form
	{
		private WiiUMasterSettings masterSettings;

		private WiIUProjectSettings projectSettings;

		private WiiUMasterSettings.MasterProcessMode processMode;

		private string workDir;

		private string sourceFilesDir;

		private string versionDataFolder;

		private string nusWorkDir;

		private bool isProcessing;

		public MasteringWindow(WiiUMasterSettings.MasterProcessMode processMode, string versionDataFolder, WiiUMasterSettings masterSettings, WiIUProjectSettings projectSettings)
		{
			InitializeComponent();
			this.masterSettings = masterSettings;
			this.projectSettings = projectSettings;
			this.processMode = processMode;
			this.versionDataFolder = versionDataFolder;
			workDir = Path.Combine(Program.GetProjectDataPath(), "tempMastering");
			sourceFilesDir = Path.Combine(workDir, "sourceFiles");
			nusWorkDir = Path.Combine(workDir, "NusPackerIn");
		}

		private void ProcessDownloadImage(string dataFolder)
		{
			string copyToSdFolder = Path.Combine(dataFolder, "copyToSDCard");
			string[] subVersions = Directory.GetDirectories(copyToSdFolder);
			currentStepLabel.Text = "Preparating copy to working dir...";
			currentFileLabel.Text = "";
			sizeLabel.Text = "";
			if (subVersions.Length != 0)
			{
				if (subVersions.Length > 1)
				{
					List<string> versionsNames = new List<string>();
					string[] array = subVersions;
					foreach (string subVersionPath in array)
					{
						versionsNames.Add(new DirectoryInfo(subVersionPath).Name);
					}
					SelectSubVersionWindow selectSubVersionWindow = new SelectSubVersionWindow(versionsNames.ToArray());
					selectSubVersionWindow.ShowDialog();
					int selectVersion = selectSubVersionWindow.GetResult();
					long size2 = Program.DirSize(new DirectoryInfo(subVersions[selectVersion]));
					sizeLabel.Text = "Copy Size: " + Program.FormatBytes(size2);
					CopyVersionToWorkFolder(subVersions[selectVersion]);
				}
				else
				{
					long size = Program.DirSize(new DirectoryInfo(subVersions[0]));
					sizeLabel.Text = "Copy Size: " + Program.FormatBytes(size);
					CopyVersionToWorkFolder(subVersions[0]);
				}
			}
			else
			{
				MessageBox.Show("No data to process in " + copyToSdFolder + " Check your Unity Build and Console Logs", "No data", MessageBoxButtons.OK);
				Close();
			}
		}

		private void CopyVersionToWorkFolder(string versionFolder)
		{
			currentFileLabel.Text = "Copying files to work folder...";
			currentFileLabel.Text = "";
			sizeLabel.Text = "";
			if (!Directory.Exists(workDir))
			{
				Directory.CreateDirectory(workDir);
			}
			if (!Directory.Exists(sourceFilesDir))
			{
				Directory.CreateDirectory(sourceFilesDir);
			}
			CustomFileCopier customFileCopier = new CustomFileCopier(versionFolder + "\\", sourceFilesDir);
			customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged;
			customFileCopier.OnComplete += AfterCopyToWorkDir;
			customFileCopier.Copy();
		}

		private void AfterCopyToWorkDir()
		{
			DecryptSourceFileDir();
		}

		private void CustomFileCopier_OnProgressChanged(double Percentage, ref bool Cancel)
		{
			progressBar.Value = (int)Percentage;
		}

		public void StartProcess()
		{
			isProcessing = true;
			currentStepLabel.Text = "Preparating...";
			currentFileLabel.Text = "";
			sizeLabel.Text = "";
			label1.Text = "Mastering for Retail Wii U";
			if (Directory.Exists(workDir))
			{
				Directory.Delete(workDir, recursive: true);
			}
			if (processMode == WiiUMasterSettings.MasterProcessMode.ProcessFromDownloadImage)
			{
				ProcessDownloadImage(versionDataFolder);
			}
			if (processMode == WiiUMasterSettings.MasterProcessMode.ProcessFromWumad)
			{
				MessageBox.Show("Mastering from Wumad files is not implemented yet.", "Missing feature", MessageBoxButtons.OK);
				Close();
			}
			if (processMode == WiiUMasterSettings.MasterProcessMode.ProcessFromRaw)
			{
				MessageBox.Show("Mastering from RAW files is not implemented yet.", "Missing feature", MessageBoxButtons.OK);
				Close();
			}
		}

		private void DecryptSourceFileDir()
		{
			string tikFile = Path.Combine(sourceFilesDir, "title.tik");
			string tmdFile = Path.Combine(sourceFilesDir, "title.tmd");
			string wiiuFile = Path.Combine(Environment.CurrentDirectory, "tools", "tik_sys.bin");
			Process process = new Process();
			process.StartInfo.FileName = Program.Paths["CDecrypt"];
			process.StartInfo.Arguments = "\"" + tikFile + "\" \"" + tmdFile + "\" \"" + wiiuFile + "\"";
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			currentStepLabel.Text = "Decrypting with CDecrypt...";
			sizeLabel.Text = "";
			currentFileLabel.Text = "";
			Program.logsManager.Log("Running CDecrypt at path (" + Program.Paths["CDecrypt"] + ") with arg :" + process.StartInfo.Arguments);
			process.Start();
			File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "tools", "cdecrypt_debug.txt"), process.StandardOutput.ReadToEnd());
			process.WaitForExit();
			CopyRawToNus();
		}

		private void CopyRawToNus()
		{
			DirectoryInfo metaDir = new DirectoryInfo(Path.Combine(sourceFilesDir, "meta"));
			DirectoryInfo codeDir = new DirectoryInfo(Path.Combine(sourceFilesDir, "code"));
			DirectoryInfo directory = new DirectoryInfo(Path.Combine(sourceFilesDir, "content"));
			if (!Directory.Exists(nusWorkDir))
			{
				Directory.CreateDirectory(nusWorkDir);
			}
			string metaOut = Path.Combine(nusWorkDir, "meta");
			string codeOut = Path.Combine(nusWorkDir, "code");
			string contentOut = Path.Combine(nusWorkDir, "content");
			if (Directory.Exists(metaOut))
			{
				Directory.Delete(metaOut, recursive: true);
			}
			if (Directory.Exists(codeOut))
			{
				Directory.Delete(codeOut, recursive: true);
			}
			if (Directory.Exists(contentOut))
			{
				Directory.Delete(contentOut, recursive: true);
			}
			currentStepLabel.Text = "Copying files to NUSPacker dir...";
			currentFileLabel.Text = "";
			sizeLabel.Text = "";
			Directory.CreateDirectory(metaOut);
			Directory.CreateDirectory(codeOut);
			Directory.CreateDirectory(contentOut);
			metaDir.DeepCopy(metaOut);
			codeDir.DeepCopy(codeOut);
			directory.DeepCopy(contentOut);
			RepackContentWithNus();
		}

		private void MasteringWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (isProcessing)
			{
				e.Cancel = true;
			}
		}

		private void RepackContentWithNus()
		{
			string nusOut = Path.Combine(Program.GetProjectDataPath(), "copyToYourWiiUSD");
			if (!Directory.Exists(nusOut))
			{
				Directory.CreateDirectory(nusOut);
			}
			string nusCommand = " -jar " + Program.Paths["NUSPacker"] + " -in \"" + nusWorkDir + "\" -out \"" + nusOut + "\" -encryptWith \"" + projectSettings.unencrypted_TitleKey + "\" -encryptKeyWith D7B00402659BA2ABD2CB0DB27FA2B656  -tID \"" + projectSettings.TitleId + "\"";
			if (masterSettings.nus_skipappxmlparsing)
			{
				nusCommand += " -skipXMLParsing";
			}
			Process process = new Process();
			process.StartInfo.FileName = SettingsManager.applicationSettings.javaPath;
			process.StartInfo.Arguments = nusCommand;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			currentStepLabel.Text = "Repacking with NUSPacker...";
			sizeLabel.Text = "";
			currentFileLabel.Text = "";
			Program.logsManager.Log("Running NUSPacker at path (" + Program.Paths["NUSPacker"] + " )with arg :" + process.StartInfo.Arguments);
			process.Start();
			File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "tools", "nuspacker_debug.txt"), process.StandardOutput.ReadToEnd());
			process.WaitForExit();
			if (masterSettings.autoCopyToSd)
			{
				NeedCopyToSd(nusOut);
			}
			Directory.Delete(workDir, recursive: true);
			MessageBox.Show("Your Wii U Retail Installable files are located in the folder 'copyToYourWiiUSD'", "Mastering finised", MessageBoxButtons.OK);
			isProcessing = false;
			Close();
		}

		private void NeedCopyToSd(string nusOutDir)
		{
			if (Directory.Exists(masterSettings.sdCardPath))
			{
				if (Directory.Exists(Path.Combine(masterSettings.sdCardPath, Program.GetProjectData().projectName)))
				{
					Directory.Delete(Path.Combine(masterSettings.sdCardPath, Program.GetProjectData().projectName), recursive: true);
				}
				Directory.CreateDirectory(Path.Combine(masterSettings.sdCardPath, Program.GetProjectData().projectName));
				CustomFileCopier customFileCopier = new CustomFileCopier(nusOutDir, Path.Combine(masterSettings.sdCardPath, Program.GetProjectData().projectName));
				customFileCopier.OnProgressChanged += CustomFileCopier_OnProgressChanged;
				customFileCopier.OnComplete += OnFinishCopyToSd;
				customFileCopier.Copy();
			}
			else
			{
				MessageBox.Show("Can't automaticaly copy files to SD, folder does not exists", "Failed to copy to SD", MessageBoxButtons.OK);
			}
		}

		private void OnFinishCopyToSd()
		{
			MessageBox.Show("Install Files for Retail Wii U are available on the Wii U SD", "Copy to SD finished", MessageBoxButtons.OK);
		}
	}

    public static class DirectoryInfoExtensions
    {
        public static void DeepCopy(this DirectoryInfo directory, string destinationDir)
        {
            foreach (string dir in Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories))
            {
                string dirToCreate = dir.Replace(directory.FullName, destinationDir);
                Directory.CreateDirectory(dirToCreate);
            }

            foreach (string newPath in Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(directory.FullName, destinationDir), true);
            }
        }
    }
}
