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
    public partial class SelectSubVersionWindow : Form
    {
        int SelectedIndex = -1;

        public SelectSubVersionWindow(string[] subVersionsNames)
        {
            InitializeComponent();

            foreach(String versionName in subVersionsNames)
            {
                comboBox1.Items.Add(versionName);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedIndex = comboBox1.SelectedIndex;
            this.Close();
        }

        public int GetResult()
        {
            return SelectedIndex;
        }
    }
}
