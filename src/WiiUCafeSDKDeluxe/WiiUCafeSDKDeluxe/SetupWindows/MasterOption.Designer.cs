namespace WiiUCafeSDKDeluxe.SetupWindows
{
    partial class MasterOption
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
            this.uniqueIdInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.titleIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.variationInput = new System.Windows.Forms.TextBox();
            this.generateTitleKeyBtn = new System.Windows.Forms.Button();
            this.ressetTitleIdBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.copyTitleIdBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.copyUnencryptedTitleKeyBtn = new System.Windows.Forms.Button();
            this.unencryptedKeyInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.encryptedKeyInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.copyEncryptedTitleKeyBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.copySdCheck = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browseSDPathBtn = new System.Windows.Forms.Button();
            this.processModeComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cdecrypt_ImportTikBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.nusSkipAppXmlCheck = new System.Windows.Forms.CheckBox();
            this.labelTikSys = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // uniqueIdInput
            // 
            this.uniqueIdInput.Location = new System.Drawing.Point(90, 24);
            this.uniqueIdInput.MaxLength = 7;
            this.uniqueIdInput.Name = "uniqueIdInput";
            this.uniqueIdInput.Size = new System.Drawing.Size(79, 25);
            this.uniqueIdInput.TabIndex = 0;
            this.uniqueIdInput.Text = "0x10000";
            this.uniqueIdInput.TextChanged += new System.EventHandler(this.uniqueIdInput_TextChanged);
            this.uniqueIdInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uniqueIdInput_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "UniqueID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.copyTitleIdBtn);
            this.groupBox1.Controls.Add(this.ressetTitleIdBtn);
            this.groupBox1.Controls.Add(this.generateTitleKeyBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.variationInput);
            this.groupBox1.Controls.Add(this.titleIdTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.uniqueIdInput);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.753846F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(478, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 161);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TitleID Generation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "TitleID";
            // 
            // titleIdTextBox
            // 
            this.titleIdTextBox.Enabled = false;
            this.titleIdTextBox.Location = new System.Drawing.Point(71, 86);
            this.titleIdTextBox.MaxLength = 16;
            this.titleIdTextBox.Name = "titleIdTextBox";
            this.titleIdTextBox.Size = new System.Drawing.Size(233, 25);
            this.titleIdTextBox.TabIndex = 3;
            this.titleIdTextBox.Text = "0005000011000000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Variation";
            // 
            // variationInput
            // 
            this.variationInput.Location = new System.Drawing.Point(90, 55);
            this.variationInput.MaxLength = 4;
            this.variationInput.Name = "variationInput";
            this.variationInput.Size = new System.Drawing.Size(79, 25);
            this.variationInput.TabIndex = 4;
            this.variationInput.Text = "0x00";
            this.variationInput.TextChanged += new System.EventHandler(this.variationInput_TextChanged);
            this.variationInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.variationInput_KeyPress);
            // 
            // generateTitleKeyBtn
            // 
            this.generateTitleKeyBtn.Location = new System.Drawing.Point(21, 117);
            this.generateTitleKeyBtn.Name = "generateTitleKeyBtn";
            this.generateTitleKeyBtn.Size = new System.Drawing.Size(170, 30);
            this.generateTitleKeyBtn.TabIndex = 6;
            this.generateTitleKeyBtn.Text = "Generate Title Keys";
            this.generateTitleKeyBtn.UseVisualStyleBackColor = true;
            this.generateTitleKeyBtn.Click += new System.EventHandler(this.generateTitleKeyBtn_Click);
            // 
            // ressetTitleIdBtn
            // 
            this.ressetTitleIdBtn.Location = new System.Drawing.Point(201, 34);
            this.ressetTitleIdBtn.Name = "ressetTitleIdBtn";
            this.ressetTitleIdBtn.Size = new System.Drawing.Size(75, 35);
            this.ressetTitleIdBtn.TabIndex = 7;
            this.ressetTitleIdBtn.Text = "Resset";
            this.ressetTitleIdBtn.UseVisualStyleBackColor = true;
            this.ressetTitleIdBtn.Click += new System.EventHandler(this.ressetTitleIdBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(713, 350);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 30);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // copyTitleIdBtn
            // 
            this.copyTitleIdBtn.Location = new System.Drawing.Point(197, 117);
            this.copyTitleIdBtn.Name = "copyTitleIdBtn";
            this.copyTitleIdBtn.Size = new System.Drawing.Size(107, 30);
            this.copyTitleIdBtn.TabIndex = 8;
            this.copyTitleIdBtn.Text = "Copy";
            this.copyTitleIdBtn.UseVisualStyleBackColor = true;
            this.copyTitleIdBtn.Click += new System.EventHandler(this.copyTitleIdBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.encryptedKeyInput);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.copyEncryptedTitleKeyBtn);
            this.groupBox2.Controls.Add(this.unencryptedKeyInput);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.copyUnencryptedTitleKeyBtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.753846F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(478, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 161);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TitleKeys";
            // 
            // copyUnencryptedTitleKeyBtn
            // 
            this.copyUnencryptedTitleKeyBtn.Location = new System.Drawing.Point(239, 51);
            this.copyUnencryptedTitleKeyBtn.Name = "copyUnencryptedTitleKeyBtn";
            this.copyUnencryptedTitleKeyBtn.Size = new System.Drawing.Size(65, 25);
            this.copyUnencryptedTitleKeyBtn.TabIndex = 8;
            this.copyUnencryptedTitleKeyBtn.Text = "Copy";
            this.copyUnencryptedTitleKeyBtn.UseVisualStyleBackColor = true;
            this.copyUnencryptedTitleKeyBtn.Click += new System.EventHandler(this.copyUnencryptedTitleKeyBtn_Click);
            // 
            // unencryptedKeyInput
            // 
            this.unencryptedKeyInput.Enabled = false;
            this.unencryptedKeyInput.Location = new System.Drawing.Point(31, 51);
            this.unencryptedKeyInput.MaxLength = 16;
            this.unencryptedKeyInput.Name = "unencryptedKeyInput";
            this.unencryptedKeyInput.Size = new System.Drawing.Size(202, 25);
            this.unencryptedKeyInput.TabIndex = 10;
            this.unencryptedKeyInput.Text = "0005000011000000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Unencrypted Title Key";
            // 
            // encryptedKeyInput
            // 
            this.encryptedKeyInput.Enabled = false;
            this.encryptedKeyInput.Location = new System.Drawing.Point(31, 109);
            this.encryptedKeyInput.MaxLength = 16;
            this.encryptedKeyInput.Name = "encryptedKeyInput";
            this.encryptedKeyInput.Size = new System.Drawing.Size(202, 25);
            this.encryptedKeyInput.TabIndex = 13;
            this.encryptedKeyInput.Text = "0005000011000000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "Encrypted Title Key";
            // 
            // copyEncryptedTitleKeyBtn
            // 
            this.copyEncryptedTitleKeyBtn.Location = new System.Drawing.Point(239, 109);
            this.copyEncryptedTitleKeyBtn.Name = "copyEncryptedTitleKeyBtn";
            this.copyEncryptedTitleKeyBtn.Size = new System.Drawing.Size(65, 25);
            this.copyEncryptedTitleKeyBtn.TabIndex = 11;
            this.copyEncryptedTitleKeyBtn.Text = "Copy";
            this.copyEncryptedTitleKeyBtn.UseVisualStyleBackColor = true;
            this.copyEncryptedTitleKeyBtn.Click += new System.EventHandler(this.copyEncryptedTitleKeyBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.processModeComboBox);
            this.groupBox3.Controls.Add(this.browseSDPathBtn);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.copySdCheck);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.753846F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 328);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mastering Options";
            // 
            // copySdCheck
            // 
            this.copySdCheck.AutoSize = true;
            this.copySdCheck.Location = new System.Drawing.Point(10, 26);
            this.copySdCheck.Name = "copySdCheck";
            this.copySdCheck.Size = new System.Drawing.Size(276, 24);
            this.copySdCheck.TabIndex = 0;
            this.copySdCheck.Text = "Automaticaly Copy Install Files to SD";
            this.copySdCheck.UseVisualStyleBackColor = true;
            this.copySdCheck.CheckedChanged += new System.EventHandler(this.copySdCheck_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(23, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(347, 25);
            this.textBox1.TabIndex = 1;
            // 
            // browseSDPathBtn
            // 
            this.browseSDPathBtn.Location = new System.Drawing.Point(377, 54);
            this.browseSDPathBtn.Name = "browseSDPathBtn";
            this.browseSDPathBtn.Size = new System.Drawing.Size(75, 26);
            this.browseSDPathBtn.TabIndex = 2;
            this.browseSDPathBtn.Text = "Browse";
            this.browseSDPathBtn.UseVisualStyleBackColor = true;
            this.browseSDPathBtn.Click += new System.EventHandler(this.browseSDPathBtn_Click);
            // 
            // processModeComboBox
            // 
            this.processModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.processModeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processModeComboBox.FormattingEnabled = true;
            this.processModeComboBox.Items.AddRange(new object[] {
            "Process From Download Image",
            "Process From Wumad",
            "Process From Raw"});
            this.processModeComboBox.Location = new System.Drawing.Point(23, 114);
            this.processModeComboBox.Name = "processModeComboBox";
            this.processModeComboBox.Size = new System.Drawing.Size(347, 28);
            this.processModeComboBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.753846F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Master Process Mode";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelTikSys);
            this.groupBox4.Controls.Add(this.cdecrypt_ImportTikBtn);
            this.groupBox4.Location = new System.Drawing.Point(10, 160);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(442, 76);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CDecrypt Options";
            // 
            // cdecrypt_ImportTikBtn
            // 
            this.cdecrypt_ImportTikBtn.Location = new System.Drawing.Point(13, 28);
            this.cdecrypt_ImportTikBtn.Name = "cdecrypt_ImportTikBtn";
            this.cdecrypt_ImportTikBtn.Size = new System.Drawing.Size(170, 30);
            this.cdecrypt_ImportTikBtn.TabIndex = 11;
            this.cdecrypt_ImportTikBtn.Text = "Import tik_sys.bin";
            this.cdecrypt_ImportTikBtn.UseVisualStyleBackColor = true;
            this.cdecrypt_ImportTikBtn.Click += new System.EventHandler(this.cdecrypt_ImportTikBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.nusSkipAppXmlCheck);
            this.groupBox5.Location = new System.Drawing.Point(10, 243);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(442, 76);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "NUSPacker Options";
            // 
            // nusSkipAppXmlCheck
            // 
            this.nusSkipAppXmlCheck.AutoSize = true;
            this.nusSkipAppXmlCheck.Location = new System.Drawing.Point(13, 32);
            this.nusSkipAppXmlCheck.Name = "nusSkipAppXmlCheck";
            this.nusSkipAppXmlCheck.Size = new System.Drawing.Size(172, 24);
            this.nusSkipAppXmlCheck.TabIndex = 13;
            this.nusSkipAppXmlCheck.Text = "Skip AppXml Parsing";
            this.nusSkipAppXmlCheck.UseVisualStyleBackColor = true;
            this.nusSkipAppXmlCheck.CheckedChanged += new System.EventHandler(this.nusSkipAppXmlCheck_CheckedChanged);
            // 
            // labelTikSys
            // 
            this.labelTikSys.Location = new System.Drawing.Point(190, 28);
            this.labelTikSys.Name = "labelTikSys";
            this.labelTikSys.Size = new System.Drawing.Size(246, 30);
            this.labelTikSys.TabIndex = 12;
            this.labelTikSys.Text = "No tik_sys.bin imported";
            this.labelTikSys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MasterOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 392);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Options - Wii U Cafe SDK Deluxe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterOption_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox uniqueIdInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button generateTitleKeyBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox variationInput;
        private System.Windows.Forms.TextBox titleIdTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ressetTitleIdBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button copyTitleIdBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox encryptedKeyInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button copyEncryptedTitleKeyBtn;
        private System.Windows.Forms.TextBox unencryptedKeyInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button copyUnencryptedTitleKeyBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button browseSDPathBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox copySdCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox processModeComboBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button cdecrypt_ImportTikBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox nusSkipAppXmlCheck;
        private System.Windows.Forms.Label labelTikSys;
    }
}