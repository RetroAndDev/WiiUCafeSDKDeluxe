using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiiUCafeSDKDeluxe.SetupWindows
{
    public partial class CreateVersion : Form
    {
        Home home;
        public CreateVersion(Home home)
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 1;
            this.home = home;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameVersion version = new GameVersion()
            {
                VersionName = textBox1.Text,
                UnityBuildMode = comboBox1.SelectedIndex,
            };

            string jsonVersion = JsonSerializer.Serialize(version);

            string versionDir = Path.Combine(Program.GetProjectDataPath(), "Versions");
            string versionFile = Path.Combine(Program.GetProjectDataPath(), "Versions", textBox1.Text + ".json");
            if (!Directory.Exists(versionDir))
            {
                Directory.CreateDirectory(versionDir);
            }

            StreamWriter sw = new StreamWriter(versionFile);
            sw.Write(jsonVersion);
            sw.Close();

            home.LoadVersions();
            Close();
        }
    }
}
