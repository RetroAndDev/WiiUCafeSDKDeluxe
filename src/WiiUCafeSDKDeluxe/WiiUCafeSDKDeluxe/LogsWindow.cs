using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinLogs;

namespace WiiUCafeSDKDeluxe
{
    public partial class LogsWindow : Form
    {
        List<Log> backLogs = new List<Log>();

        public LogsWindow()
        {
            InitializeComponent();

            LoadLogs();
        }

        private void updateLogs_Tick(object sender, EventArgs e)
        {
            LoadLogs();
        }

        void LoadLogs()
        {
            if (backLogs != Program.logsManager.logs)
            {
                logsBox.ResetText();
                backLogs = Program.logsManager.logs;

                int logId = 0;
                foreach (Log log in Program.logsManager.logs)
                {
                    string logString = log.GetLogAsString();

                    if(backLogs.Count != logId + 1)
                    {
                        logString += Environment.NewLine;
                    }

                    logsBox.AppendText(logString, WinLogsManager.GetPrefixColor(log.logType));

                    logId++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Log File | *.log";
            saveFileDialog.Title = "Save log to file";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.logsManager.SaveLogs(saveFileDialog.FileName);
            }
        }

        private void LogsWindow_Load(object sender, EventArgs e)
        {
            updateLogs.Start();
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
