namespace WiiUCafeSDKDeluxe
{
    partial class CreateProject
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
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.browseIconBtn = new System.Windows.Forms.Button();
            this.nameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.devInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cafeSdkInput = new System.Windows.Forms.TextBox();
            this.autoDetectCafe = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.autoDetectUnityBtn = new System.Windows.Forms.Button();
            this.unitySelection = new System.Windows.Forms.ComboBox();
            this.validateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // iconBox
            // 
            this.iconBox.Image = global::WiiUCafeSDKDeluxe.ApplicationResources.dummy_icon;
            this.iconBox.Location = new System.Drawing.Point(13, 13);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(128, 128);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconBox.TabIndex = 0;
            this.iconBox.TabStop = false;
            // 
            // browseIconBtn
            // 
            this.browseIconBtn.Location = new System.Drawing.Point(13, 148);
            this.browseIconBtn.Name = "browseIconBtn";
            this.browseIconBtn.Size = new System.Drawing.Size(128, 30);
            this.browseIconBtn.TabIndex = 1;
            this.browseIconBtn.Text = "Browse...";
            this.browseIconBtn.UseVisualStyleBackColor = true;
            this.browseIconBtn.Click += new System.EventHandler(this.browseIconBtn_Click);
            // 
            // nameInput
            // 
            this.nameInput.Location = new System.Drawing.Point(175, 41);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(308, 22);
            this.nameInput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(165, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(165, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Project Developpers :";
            // 
            // devInput
            // 
            this.devInput.Location = new System.Drawing.Point(175, 109);
            this.devInput.Name = "devInput";
            this.devInput.Size = new System.Drawing.Size(308, 22);
            this.devInput.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cafe SDK Location :";
            // 
            // cafeSdkInput
            // 
            this.cafeSdkInput.Location = new System.Drawing.Point(175, 203);
            this.cafeSdkInput.Name = "cafeSdkInput";
            this.cafeSdkInput.Size = new System.Drawing.Size(308, 22);
            this.cafeSdkInput.TabIndex = 6;
            // 
            // autoDetectCafe
            // 
            this.autoDetectCafe.Location = new System.Drawing.Point(489, 202);
            this.autoDetectCafe.Name = "autoDetectCafe";
            this.autoDetectCafe.Size = new System.Drawing.Size(93, 23);
            this.autoDetectCafe.TabIndex = 8;
            this.autoDetectCafe.Text = "Auto Detect";
            this.autoDetectCafe.UseVisualStyleBackColor = true;
            this.autoDetectCafe.Click += new System.EventHandler(this.autoDetectCafe_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(165, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Unity Engine Version :";
            // 
            // autoDetectUnityBtn
            // 
            this.autoDetectUnityBtn.Location = new System.Drawing.Point(489, 267);
            this.autoDetectUnityBtn.Name = "autoDetectUnityBtn";
            this.autoDetectUnityBtn.Size = new System.Drawing.Size(93, 23);
            this.autoDetectUnityBtn.TabIndex = 11;
            this.autoDetectUnityBtn.Text = "Auto Detect";
            this.autoDetectUnityBtn.UseVisualStyleBackColor = true;
            this.autoDetectUnityBtn.Click += new System.EventHandler(this.autoDetectUnityBtn_Click);
            // 
            // unitySelection
            // 
            this.unitySelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitySelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unitySelection.FormattingEnabled = true;
            this.unitySelection.Items.AddRange(new object[] {
            "2017.1.2p3"});
            this.unitySelection.Location = new System.Drawing.Point(175, 268);
            this.unitySelection.Name = "unitySelection";
            this.unitySelection.Size = new System.Drawing.Size(308, 24);
            this.unitySelection.TabIndex = 12;
            // 
            // validateBtn
            // 
            this.validateBtn.Location = new System.Drawing.Point(520, 304);
            this.validateBtn.Name = "validateBtn";
            this.validateBtn.Size = new System.Drawing.Size(125, 35);
            this.validateBtn.TabIndex = 13;
            this.validateBtn.Text = "Create Project";
            this.validateBtn.UseVisualStyleBackColor = true;
            this.validateBtn.Click += new System.EventHandler(this.validateBtn_Click);
            // 
            // CreateProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 351);
            this.Controls.Add(this.validateBtn);
            this.Controls.Add(this.unitySelection);
            this.Controls.Add(this.autoDetectUnityBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.autoDetectCafe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cafeSdkInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.devInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameInput);
            this.Controls.Add(this.browseIconBtn);
            this.Controls.Add(this.iconBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "CreateProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wii U Deluxe Tool - Create New Project";
            this.Load += new System.EventHandler(this.CreateProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.Button browseIconBtn;
        private System.Windows.Forms.TextBox nameInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox devInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cafeSdkInput;
        private System.Windows.Forms.Button autoDetectCafe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button autoDetectUnityBtn;
        private System.Windows.Forms.ComboBox unitySelection;
        private System.Windows.Forms.Button validateBtn;
    }
}