using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe.SetupWindows
{
    public partial class HardwareSetup : Form
    {
        public HardwareSetup()
        {
            InitializeComponent();

            LoadHardwares();
        }

        void LoadHardwares()
        {
            foreach (string hardware in ApplicationOptions.wiiUHardwares)
            {
                comboBox1.Items.Add(hardware);
            }

            //Select saved hardware
            comboBox1.SelectedIndex = SettingsManager.applicationSettings.wiiUHardware.wiiUSet;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    pictureBox1.Image = ApplicationResources.wiiu_whiteset;
                    break;
                case 2:
                    pictureBox1.Image = ApplicationResources.wiiu_catdev;
                    break;
                case 3:
                    pictureBox1.Image = ApplicationResources.wiiu_catr;
                    break;
                case 4:
                    pictureBox1.Image = ApplicationResources.wiiu_dev;
                    break;
                default:
                    pictureBox1.Image = ApplicationResources.wiiu_blackset;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveHardware();
        }

        void SaveHardware()
        {
            //Save
            SettingsManager.applicationSettings.wiiUHardware.wiiUSet = comboBox1.SelectedIndex;

            SettingsManager.SaveSettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Display hardware specs
        }

        private void HardwareSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveHardware();
        }
    }
}
