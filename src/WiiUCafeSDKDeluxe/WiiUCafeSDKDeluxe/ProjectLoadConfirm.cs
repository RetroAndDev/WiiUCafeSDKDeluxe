using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WiiUCafeSDKDeluxe
{
    public partial class ProjectLoadConfirm : Form
    {
        public ProjectLoadConfirm()
        {
            InitializeComponent();

            LoadProjectInfos();
        }

        void LoadProjectInfos()
        {
            ProjectData projData = Program.GetProjectData();

            iconBox.Image = Program.GetImageDromBase64String(projData.projectIcon);
            label1.Text = projData.projectName;
            label2.Text = projData.projectDeveloppers;
            this.Text = this.Text.Replace("##ProjName##", projData.projectName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();

            Program.OpenHome();
        }

        private void ProjectLoadConfirm_Load(object sender, EventArgs e)
        {

        }
    }
}