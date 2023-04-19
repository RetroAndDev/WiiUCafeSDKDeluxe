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
    public partial class MasterOption : Form
    {
        MasteringData masteringData;

        public MasterOption()
        {
            InitializeComponent();

            LoadData();

            CheckTikSys();
        }

        void LoadData()
        {
            StreamReader streamReader = new StreamReader(Program.GetMasteringDataFilePath());
            string masteringJson = streamReader.ReadToEnd();
            streamReader.Close();

            masteringData = JsonSerializer.Deserialize<MasteringData>(masteringJson);

            uniqueIdInput.Text = masteringData.WiIUProjectSettings.UniqueId;
            variationInput.Text = masteringData.WiIUProjectSettings.Variation;

            unencryptedKeyInput.Text = masteringData.WiIUProjectSettings.unencrypted_TitleKey;
            encryptedKeyInput.Text = masteringData.WiIUProjectSettings.encrypted_TitleKey;
            encryptedKeyInput.Text = masteringData.WiIUProjectSettings.encrypted_TitleKey;

            copySdCheck.Checked = masteringData.WiiUMasterSettings.autoCopyToSd;
            nusSkipAppXmlCheck.Checked = masteringData.WiiUMasterSettings.nus_skipappxmlparsing;

            textBox1.Text = masteringData.WiiUMasterSettings.sdCardPath;

            processModeComboBox.SelectedIndex = (int)masteringData.WiiUMasterSettings.masterProcessMode;
            browseSDPathBtn.Enabled = masteringData.WiiUMasterSettings.autoCopyToSd;
        }

        private void ressetTitleIdBtn_Click(object sender, EventArgs e)
        {
            uniqueIdInput.Text = "0x10000";
            variationInput.Text = "0x00";
        }

        private void uniqueIdInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(uniqueIdInput.SelectionStart < 3)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void variationInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (variationInput.SelectionStart < 3)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void uniqueIdInput_TextChanged(object sender, EventArgs e)
        {
            GenerateTitleId();
        }

        void GenerateTitleId()
        {
            string uniqueId = uniqueIdInput.Text;
            string variation = variationInput.Text;

            //fill to correct size
            int missing = 7 - uniqueId.Length;
            int actual = 0;
            while(actual != missing)
            {
                uniqueId += "0";
                actual++;
            }

            missing = 4 - variation.Length;
            actual = 0;
            while (actual != missing)
            {
                variation += "0";
                actual++;
            }

            titleIdTextBox.Text = "000500001" + uniqueId.Substring(2) + variation.Substring(2);
            masteringData.WiIUProjectSettings.TitleId = titleIdTextBox.Text;
            masteringData.WiIUProjectSettings.UniqueId = uniqueId;
            masteringData.WiIUProjectSettings.Variation = variation;
        }

        private void variationInput_TextChanged(object sender, EventArgs e)
        {
            GenerateTitleId();
        }

        private void copyTitleIdBtn_Click(object sender, EventArgs e)
        {
            Program.logsManager.Log("TitleId '" + titleIdTextBox.Text + "' copied to clipboard");
            Clipboard.SetText(titleIdTextBox.Text);
        }

        private void copyUnencryptedTitleKeyBtn_Click(object sender, EventArgs e)
        {
            Program.logsManager.Log("Unencrypted TitleKey '" + unencryptedKeyInput.Text + "' copied to clipboard");
            Clipboard.SetText(unencryptedKeyInput.Text);
        }

        private void copyEncryptedTitleKeyBtn_Click(object sender, EventArgs e)
        {
            Program.logsManager.Log("Encrypted TitleKey '" + encryptedKeyInput.Text + "' copied to clipboard");
            Clipboard.SetText(encryptedKeyInput.Text);
        }

        private void generateTitleKeyBtn_Click(object sender, EventArgs e)
        {
            string unencryptedKey = WiiUTitleKeysGenerator.GetUnencryptedTitleKey(titleIdTextBox.Text, "mypass", "D7B00402659BA2ABD2CB0DB27FA2B656");
            string encryptedKey = WiiUTitleKeysGenerator.GetEncryptedTitleKey(titleIdTextBox.Text, "mypass", "D7B00402659BA2ABD2CB0DB27FA2B656");

            Program.logsManager.LogSucess("[Kii U Generator] Generated Unencrypted TitleKey : " + unencryptedKey);
            Program.logsManager.LogSucess("[Kii U Generator] Generated Encrypted TitleKey : " + encryptedKey);

            unencryptedKeyInput.Text = unencryptedKey;
            encryptedKeyInput.Text = encryptedKey;

            masteringData.WiIUProjectSettings.unencrypted_TitleKey = unencryptedKey;
            masteringData.WiIUProjectSettings.encrypted_TitleKey = encryptedKey;
        }

        void SaveData()
        {
            StreamWriter streamWriter = new StreamWriter(Program.GetMasteringDataFilePath());
            string dataJson = JsonSerializer.Serialize(masteringData);
            streamWriter.Write(dataJson);
            streamWriter.Close();
        }

        private void MasterOption_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void cdecrypt_ImportTikBtn_Click(object sender, EventArgs e)
        {
            string autoPath = Path.Combine(Program.GetProjectData().cafeSdkLocation, "system", "bin", "tool", "mastering", "resources", "makemaster");
            if (File.Exists(Path.Combine(autoPath, "tik_sys.bin")))
            {
                if(MessageBox.Show("Automatic tik_sys.bin detected at " + Path.Combine(autoPath, "tik_sys.bin") + " Import it ?", "tik_sys.bin found", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //import tik file
                    File.Copy(Path.Combine(autoPath, "tik_sys.bin"), Path.Combine(Environment.CurrentDirectory, "tools", "tik_sys.bin"));
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Wii U Tiket File (tik_sys.bin) | tik_sys.bin";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Manualy Browse tik_sys.bin";
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(openFileDialog.FileName, Path.Combine(Environment.CurrentDirectory, "tik_sys.bin"));
                }
            }

            CheckTikSys();
        }

        void CheckTikSys()
        {
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "tools", "tik_sys.bin")))
            {
                labelTikSys.Text = "File 'tik_sys.bin' found";
                labelTikSys.ForeColor = Color.Green;
            }
            else
            {
                labelTikSys.Text = "File 'tik_sys.bin' not found";
                labelTikSys.ForeColor = Color.Red;
            }
        }

        private void copySdCheck_CheckedChanged(object sender, EventArgs e)
        {
            masteringData.WiiUMasterSettings.autoCopyToSd = copySdCheck.Checked;

            browseSDPathBtn.Enabled = masteringData.WiiUMasterSettings.autoCopyToSd;
        }

        private void nusSkipAppXmlCheck_CheckedChanged(object sender, EventArgs e)
        {
            masteringData.WiiUMasterSettings.nus_skipappxmlparsing = nusSkipAppXmlCheck.Checked;
        }

        private void browseSDPathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select automatic copy folder of Wii U's SD card";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                //Add path
                textBox1.Text = folderBrowserDialog.SelectedPath;
                masteringData.WiiUMasterSettings.sdCardPath = folderBrowserDialog.SelectedPath; 
            }
        }
    }
}
