namespace Crazysoft.OTRRemote
{
    partial class FrmWizard
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizard));
            this.pnlPage1 = new System.Windows.Forms.Panel();
            this.lblPage1Header = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblPage1Text1 = new System.Windows.Forms.Label();
            this.pbPage1Shortcut = new System.Windows.Forms.PictureBox();
            this.pnlSeperator = new System.Windows.Forms.Panel();
            this.pnlPage2 = new System.Windows.Forms.Panel();
            this.pbPage2CapturePlugin = new System.Windows.Forms.PictureBox();
            this.lblPage2Text1 = new System.Windows.Forms.Label();
            this.pnlPage3 = new System.Windows.Forms.Panel();
            this.pbPage3NewDevice = new System.Windows.Forms.PictureBox();
            this.lblPage3Text1 = new System.Windows.Forms.Label();
            this.pnlPage4 = new System.Windows.Forms.Panel();
            this.pnlPage5 = new System.Windows.Forms.Panel();
            this.pbPage5ParameterTab = new System.Windows.Forms.PictureBox();
            this.tbPage5DeleteText = new System.Windows.Forms.TextBox();
            this.tbPage5RecordText = new System.Windows.Forms.TextBox();
            this.lblPage5Delete = new System.Windows.Forms.Label();
            this.lblPage5Record = new System.Windows.Forms.Label();
            this.lblPage5Text1 = new System.Windows.Forms.Label();
            this.pbPage4ApplicationTab = new System.Windows.Forms.PictureBox();
            this.tbPage3Path = new System.Windows.Forms.TextBox();
            this.lblPage4Text1 = new System.Windows.Forms.Label();
            this.pnlPage6 = new System.Windows.Forms.Panel();
            this.pbPage6SettingsTab = new System.Windows.Forms.PictureBox();
            this.lblPage6Text1 = new System.Windows.Forms.Label();
            this.pnlPage7 = new System.Windows.Forms.Panel();
            this.lblPage7Text1 = new System.Windows.Forms.Label();
            this.lblPage7Header = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage1Shortcut)).BeginInit();
            this.pnlPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage2CapturePlugin)).BeginInit();
            this.pnlPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage3NewDevice)).BeginInit();
            this.pnlPage4.SuspendLayout();
            this.pnlPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage5ParameterTab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage4ApplicationTab)).BeginInit();
            this.pnlPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage6SettingsTab)).BeginInit();
            this.pnlPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPage1
            // 
            this.pnlPage1.Location = new System.Drawing.Point(12, 66);
            this.pnlPage1.Name = "pnlPage1";
            this.pnlPage1.Size = new System.Drawing.Size(470, 310);
            this.pnlPage1.TabIndex = 0;
            // 
            // lblPage1Header
            // 
            this.lblPage1Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage1Header.Location = new System.Drawing.Point(12, 66);
            this.lblPage1Header.Name = "lblPage1Header";
            this.lblPage1Header.Size = new System.Drawing.Size(470, 43);
            this.lblPage1Header.TabIndex = 0;
            this.lblPage1Header.Text = "This wizard will now guide you thorugh the configuration of TV-Browser for OTR Re" +
                "mote. Unfortunately, this can\'t be done automatically up to now.";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.pbHeader);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(494, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(65, 23);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(148, 13);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "Step 1: Start TV-Browser";
            // 
            // pbHeader
            // 
            this.pbHeader.Location = new System.Drawing.Point(11, 5);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(48, 48);
            this.pbHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbHeader.TabIndex = 1;
            this.pbHeader.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(407, 389);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(326, 389);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "&Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            this.btnPrevious.Location = new System.Drawing.Point(245, 389);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "< &Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lblPage1Text1
            // 
            this.lblPage1Text1.Location = new System.Drawing.Point(12, 109);
            this.lblPage1Text1.Name = "lblPage1Text1";
            this.lblPage1Text1.Size = new System.Drawing.Size(470, 31);
            this.lblPage1Text1.TabIndex = 1;
            this.lblPage1Text1.Text = "First, you have to start TV-Browser, if you haven\'t done yet. Simply open TV-Brow" +
                "ser as you normally would do.";
            // 
            // pbPage1Shortcut
            // 
            this.pbPage1Shortcut.Location = new System.Drawing.Point(199, 159);
            this.pbPage1Shortcut.Name = "pbPage1Shortcut";
            this.pbPage1Shortcut.Size = new System.Drawing.Size(96, 81);
            this.pbPage1Shortcut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPage1Shortcut.TabIndex = 2;
            this.pbPage1Shortcut.TabStop = false;
            // 
            // pnlSeperator
            // 
            this.pnlSeperator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSeperator.Location = new System.Drawing.Point(0, 382);
            this.pnlSeperator.Name = "pnlSeperator";
            this.pnlSeperator.Size = new System.Drawing.Size(494, 1);
            this.pnlSeperator.TabIndex = 5;
            // 
            // pnlPage2
            // 
            this.pnlPage2.Controls.Add(this.pbPage2CapturePlugin);
            this.pnlPage2.Controls.Add(this.lblPage2Text1);
            this.pnlPage2.Location = new System.Drawing.Point(12, 66);
            this.pnlPage2.Name = "pnlPage2";
            this.pnlPage2.Size = new System.Drawing.Size(470, 310);
            this.pnlPage2.TabIndex = 6;
            // 
            // pbPage2CapturePlugin
            // 
            this.pbPage2CapturePlugin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPage2CapturePlugin.Location = new System.Drawing.Point(110, 50);
            this.pbPage2CapturePlugin.Name = "pbPage2CapturePlugin";
            this.pbPage2CapturePlugin.Size = new System.Drawing.Size(250, 211);
            this.pbPage2CapturePlugin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPage2CapturePlugin.TabIndex = 1;
            this.pbPage2CapturePlugin.TabStop = false;
            // 
            // lblPage2Text1
            // 
            this.lblPage2Text1.Location = new System.Drawing.Point(0, 0);
            this.lblPage2Text1.Name = "lblPage2Text1";
            this.lblPage2Text1.Size = new System.Drawing.Size(470, 29);
            this.lblPage2Text1.TabIndex = 0;
            this.lblPage2Text1.Text = "Now, open the Capture Plugin Settings dialog by clicking on \"Plugins\" in the menu" +
                " bar and then selecting \"Capture Plugin\".";
            // 
            // pnlPage3
            // 
            this.pnlPage3.Controls.Add(this.pnlPage4);
            this.pnlPage3.Controls.Add(this.pbPage3NewDevice);
            this.pnlPage3.Controls.Add(this.lblPage3Text1);
            this.pnlPage3.Location = new System.Drawing.Point(12, 66);
            this.pnlPage3.Name = "pnlPage3";
            this.pnlPage3.Size = new System.Drawing.Size(470, 310);
            this.pnlPage3.TabIndex = 7;
            // 
            // pbPage3NewDevice
            // 
            this.pbPage3NewDevice.Location = new System.Drawing.Point(89, 43);
            this.pbPage3NewDevice.Name = "pbPage3NewDevice";
            this.pbPage3NewDevice.Size = new System.Drawing.Size(293, 264);
            this.pbPage3NewDevice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPage3NewDevice.TabIndex = 1;
            this.pbPage3NewDevice.TabStop = false;
            // 
            // lblPage3Text1
            // 
            this.lblPage3Text1.Location = new System.Drawing.Point(-3, 0);
            this.lblPage3Text1.Name = "lblPage3Text1";
            this.lblPage3Text1.Size = new System.Drawing.Size(473, 40);
            this.lblPage3Text1.TabIndex = 0;
            this.lblPage3Text1.Text = "In the settings dialog, create a new device by changing to the \"Devices\" tab, cli" +
                "cking on \"Add Device\". Enter the name \"OnlineTvRecorder\" and select the Default " +
                "Driver. Click OK.";
            // 
            // pnlPage4
            // 
            this.pnlPage4.Controls.Add(this.pbPage4ApplicationTab);
            this.pnlPage4.Controls.Add(this.tbPage3Path);
            this.pnlPage4.Controls.Add(this.lblPage4Text1);
            this.pnlPage4.Location = new System.Drawing.Point(0, 0);
            this.pnlPage4.Name = "pnlPage4";
            this.pnlPage4.Size = new System.Drawing.Size(470, 310);
            this.pnlPage4.TabIndex = 8;
            // 
            // pnlPage5
            // 
            this.pnlPage5.Controls.Add(this.pnlPage6);
            this.pnlPage5.Controls.Add(this.pbPage5ParameterTab);
            this.pnlPage5.Controls.Add(this.tbPage5DeleteText);
            this.pnlPage5.Controls.Add(this.tbPage5RecordText);
            this.pnlPage5.Controls.Add(this.lblPage5Delete);
            this.pnlPage5.Controls.Add(this.lblPage5Record);
            this.pnlPage5.Controls.Add(this.lblPage5Text1);
            this.pnlPage5.Location = new System.Drawing.Point(12, 66);
            this.pnlPage5.Name = "pnlPage5";
            this.pnlPage5.Size = new System.Drawing.Size(470, 310);
            this.pnlPage5.TabIndex = 9;
            // 
            // pbPage5ParameterTab
            // 
            this.pbPage5ParameterTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPage5ParameterTab.Location = new System.Drawing.Point(12, 101);
            this.pbPage5ParameterTab.Name = "pbPage5ParameterTab";
            this.pbPage5ParameterTab.Size = new System.Drawing.Size(446, 206);
            this.pbPage5ParameterTab.TabIndex = 6;
            this.pbPage5ParameterTab.TabStop = false;
            // 
            // tbPage5DeleteText
            // 
            this.tbPage5DeleteText.BackColor = System.Drawing.SystemColors.Control;
            this.tbPage5DeleteText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPage5DeleteText.Location = new System.Drawing.Point(67, 61);
            this.tbPage5DeleteText.Multiline = true;
            this.tbPage5DeleteText.Name = "tbPage5DeleteText";
            this.tbPage5DeleteText.ReadOnly = true;
            this.tbPage5DeleteText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPage5DeleteText.Size = new System.Drawing.Size(400, 34);
            this.tbPage5DeleteText.TabIndex = 5;
            this.tbPage5DeleteText.Text = resources.GetString("tbPage5DeleteText.Text");
            // 
            // tbPage5RecordText
            // 
            this.tbPage5RecordText.BackColor = System.Drawing.SystemColors.Control;
            this.tbPage5RecordText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPage5RecordText.Location = new System.Drawing.Point(67, 21);
            this.tbPage5RecordText.Multiline = true;
            this.tbPage5RecordText.Name = "tbPage5RecordText";
            this.tbPage5RecordText.ReadOnly = true;
            this.tbPage5RecordText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPage5RecordText.Size = new System.Drawing.Size(400, 34);
            this.tbPage5RecordText.TabIndex = 4;
            this.tbPage5RecordText.Text = resources.GetString("tbPage5RecordText.Text");
            // 
            // lblPage5Delete
            // 
            this.lblPage5Delete.AutoSize = true;
            this.lblPage5Delete.Location = new System.Drawing.Point(3, 63);
            this.lblPage5Delete.Name = "lblPage5Delete";
            this.lblPage5Delete.Size = new System.Drawing.Size(41, 13);
            this.lblPage5Delete.TabIndex = 2;
            this.lblPage5Delete.Text = "Delete:";
            // 
            // lblPage5Record
            // 
            this.lblPage5Record.AutoSize = true;
            this.lblPage5Record.Location = new System.Drawing.Point(3, 23);
            this.lblPage5Record.Name = "lblPage5Record";
            this.lblPage5Record.Size = new System.Drawing.Size(45, 13);
            this.lblPage5Record.TabIndex = 1;
            this.lblPage5Record.Text = "Record:";
            // 
            // lblPage5Text1
            // 
            this.lblPage5Text1.Location = new System.Drawing.Point(-3, 0);
            this.lblPage5Text1.Name = "lblPage5Text1";
            this.lblPage5Text1.Size = new System.Drawing.Size(473, 18);
            this.lblPage5Text1.TabIndex = 0;
            this.lblPage5Text1.Text = "On the Parameter tab, add following text to the Record and the Delete fields:";
            // 
            // pbPage4ApplicationTab
            // 
            this.pbPage4ApplicationTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPage4ApplicationTab.Location = new System.Drawing.Point(40, 61);
            this.pbPage4ApplicationTab.Name = "pbPage4ApplicationTab";
            this.pbPage4ApplicationTab.Size = new System.Drawing.Size(390, 113);
            this.pbPage4ApplicationTab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPage4ApplicationTab.TabIndex = 2;
            this.pbPage4ApplicationTab.TabStop = false;
            // 
            // tbPage3Path
            // 
            this.tbPage3Path.BackColor = System.Drawing.SystemColors.Control;
            this.tbPage3Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPage3Path.Location = new System.Drawing.Point(0, 21);
            this.tbPage3Path.Name = "tbPage3Path";
            this.tbPage3Path.ReadOnly = true;
            this.tbPage3Path.Size = new System.Drawing.Size(470, 20);
            this.tbPage3Path.TabIndex = 1;
            this.tbPage3Path.Text = "C:\\Program Files\\Crazysoft\\OTR Remote\\OTRRemote.exe";
            // 
            // lblPage4Text1
            // 
            this.lblPage4Text1.Location = new System.Drawing.Point(-3, 0);
            this.lblPage4Text1.Name = "lblPage4Text1";
            this.lblPage4Text1.Size = new System.Drawing.Size(473, 18);
            this.lblPage4Text1.TabIndex = 0;
            this.lblPage4Text1.Text = "On the Application tab, copy following text into the Application field:";
            // 
            // pnlPage6
            // 
            this.pnlPage6.Controls.Add(this.pbPage6SettingsTab);
            this.pnlPage6.Controls.Add(this.lblPage6Text1);
            this.pnlPage6.Location = new System.Drawing.Point(0, 0);
            this.pnlPage6.Name = "pnlPage6";
            this.pnlPage6.Size = new System.Drawing.Size(470, 310);
            this.pnlPage6.TabIndex = 9;
            // 
            // pbPage6SettingsTab
            // 
            this.pbPage6SettingsTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPage6SettingsTab.Location = new System.Drawing.Point(14, 56);
            this.pbPage6SettingsTab.Name = "pbPage6SettingsTab";
            this.pbPage6SettingsTab.Size = new System.Drawing.Size(443, 251);
            this.pbPage6SettingsTab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPage6SettingsTab.TabIndex = 1;
            this.pbPage6SettingsTab.TabStop = false;
            // 
            // lblPage6Text1
            // 
            this.lblPage6Text1.Location = new System.Drawing.Point(-3, 0);
            this.lblPage6Text1.Name = "lblPage6Text1";
            this.lblPage6Text1.Size = new System.Drawing.Size(473, 40);
            this.lblPage6Text1.TabIndex = 0;
            this.lblPage6Text1.Text = "On the Settings tab, set the options as shown on the image below. If you know, wh" +
                "at these settings do, you can also set them as they fit best for you.";
            // 
            // pnlPage7
            // 
            this.pnlPage7.Controls.Add(this.lblPage7Text1);
            this.pnlPage7.Controls.Add(this.lblPage7Header);
            this.pnlPage7.Location = new System.Drawing.Point(12, 66);
            this.pnlPage7.Name = "pnlPage7";
            this.pnlPage7.Size = new System.Drawing.Size(470, 310);
            this.pnlPage7.TabIndex = 9;
            // 
            // lblPage7Text1
            // 
            this.lblPage7Text1.Location = new System.Drawing.Point(-3, 40);
            this.lblPage7Text1.Name = "lblPage7Text1";
            this.lblPage7Text1.Size = new System.Drawing.Size(473, 50);
            this.lblPage7Text1.TabIndex = 1;
            this.lblPage7Text1.Text = "You can simply record TV shows by right-clicking them and selecting \"Record\" from" +
                " the context menu. Close this wizard by clicking on \"Close\" in the right bottom " +
                "corner.";
            // 
            // lblPage7Header
            // 
            this.lblPage7Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage7Header.Location = new System.Drawing.Point(-3, 0);
            this.lblPage7Header.Name = "lblPage7Header";
            this.lblPage7Header.Size = new System.Drawing.Size(473, 40);
            this.lblPage7Header.TabIndex = 0;
            this.lblPage7Header.Text = "You have now finished configuring TV-Browser for OTR Remote. After clicking on \"O" +
                "K\" in both windows, you are ready to record!";
            // 
            // FrmWizard
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 424);
            this.Controls.Add(this.pnlPage7);
            this.Controls.Add(this.pnlPage5);
            this.Controls.Add(this.pnlPage3);
            this.Controls.Add(this.pnlPage2);
            this.Controls.Add(this.pnlSeperator);
            this.Controls.Add(this.pbPage1Shortcut);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblPage1Text1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblPage1Header);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlPage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TV-Browser Plugin Installation Wizard";
            this.Load += new System.EventHandler(this.FrmWizard_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage1Shortcut)).EndInit();
            this.pnlPage2.ResumeLayout(false);
            this.pnlPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage2CapturePlugin)).EndInit();
            this.pnlPage3.ResumeLayout(false);
            this.pnlPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage3NewDevice)).EndInit();
            this.pnlPage4.ResumeLayout(false);
            this.pnlPage4.PerformLayout();
            this.pnlPage5.ResumeLayout(false);
            this.pnlPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage5ParameterTab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage4ApplicationTab)).EndInit();
            this.pnlPage6.ResumeLayout(false);
            this.pnlPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage6SettingsTab)).EndInit();
            this.pnlPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPage1;
        private System.Windows.Forms.Label lblPage1Header;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pbHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblPage1Text1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox pbPage1Shortcut;
        private System.Windows.Forms.Panel pnlSeperator;
        private System.Windows.Forms.Panel pnlPage2;
        private System.Windows.Forms.Label lblPage2Text1;
        private System.Windows.Forms.PictureBox pbPage2CapturePlugin;
        private System.Windows.Forms.Panel pnlPage3;
        private System.Windows.Forms.PictureBox pbPage3NewDevice;
        private System.Windows.Forms.Label lblPage3Text1;
        private System.Windows.Forms.Panel pnlPage4;
        private System.Windows.Forms.Label lblPage4Text1;
        private System.Windows.Forms.TextBox tbPage3Path;
        private System.Windows.Forms.PictureBox pbPage4ApplicationTab;
        private System.Windows.Forms.Panel pnlPage5;
        private System.Windows.Forms.Label lblPage5Text1;
        private System.Windows.Forms.TextBox tbPage5RecordText;
        private System.Windows.Forms.Label lblPage5Delete;
        private System.Windows.Forms.Label lblPage5Record;
        private System.Windows.Forms.PictureBox pbPage5ParameterTab;
        private System.Windows.Forms.TextBox tbPage5DeleteText;
        private System.Windows.Forms.Panel pnlPage6;
        private System.Windows.Forms.Label lblPage6Text1;
        private System.Windows.Forms.PictureBox pbPage6SettingsTab;
        private System.Windows.Forms.Panel pnlPage7;
        private System.Windows.Forms.Label lblPage7Text1;
        private System.Windows.Forms.Label lblPage7Header;
    }
}