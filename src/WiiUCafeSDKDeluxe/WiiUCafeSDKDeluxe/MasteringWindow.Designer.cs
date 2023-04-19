namespace WiiUCafeSDKDeluxe
{
    partial class MasteringWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.currentStepLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.currentFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(18, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(527, 40);
            this.progressBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mastering files for Retail Wii U";
            // 
            // currentStepLabel
            // 
            this.currentStepLabel.AutoSize = true;
            this.currentStepLabel.Location = new System.Drawing.Point(15, 84);
            this.currentStepLabel.Name = "currentStepLabel";
            this.currentStepLabel.Size = new System.Drawing.Size(75, 16);
            this.currentStepLabel.TabIndex = 2;
            this.currentStepLabel.Text = "Actual Step";
            // 
            // sizeLabel
            // 
            this.sizeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sizeLabel.Location = new System.Drawing.Point(412, 84);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(133, 16);
            this.sizeLabel.TabIndex = 3;
            this.sizeLabel.Text = "1024Go/1024Go";
            this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentFileLabel
            // 
            this.currentFileLabel.Location = new System.Drawing.Point(13, 104);
            this.currentFileLabel.Name = "currentFileLabel";
            this.currentFileLabel.Size = new System.Drawing.Size(426, 23);
            this.currentFileLabel.TabIndex = 4;
            this.currentFileLabel.Text = "Copying File : FILENAME";
            this.currentFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MasteringWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 141);
            this.ControlBox = false;
            this.Controls.Add(this.currentFileLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.currentStepLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasteringWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mastering - Wii U Cafe SDK Deluxe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasteringWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentStepLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label currentFileLabel;
    }
}