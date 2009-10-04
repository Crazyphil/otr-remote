namespace Crazysoft.OTRRemote
{
    partial class FrmPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreferences));
            this.tcTabs = new System.Windows.Forms.TabControl();
            this.tcTabsProgram = new System.Windows.Forms.TabPage();
            this.cbRetryDelete = new System.Windows.Forms.CheckBox();
            this.cbProgressMethod = new System.Windows.Forms.ComboBox();
            this.lblProgressIndicator = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.cbAutoUpdate = new System.Windows.Forms.CheckBox();
            this.cbRecordFollowing = new System.Windows.Forms.CheckBox();
            this.cbAdjustStartTime = new System.Windows.Forms.CheckBox();
            this.btnEditStations = new System.Windows.Forms.Button();
            this.pnlAutoClose = new System.Windows.Forms.Panel();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.tbCloseSeconds = new System.Windows.Forms.NumericUpDown();
            this.cbAutoClose = new System.Windows.Forms.CheckBox();
            this.tcTabsNetwork = new System.Windows.Forms.TabPage();
            this.pnlProxyOptions = new System.Windows.Forms.Panel();
            this.pnlProxyAuthentication = new System.Windows.Forms.Panel();
            this.tbProxyPassword = new System.Windows.Forms.TextBox();
            this.tbProxyUser = new System.Windows.Forms.TextBox();
            this.lblProxyPassword = new System.Windows.Forms.Label();
            this.lblProxyUser = new System.Windows.Forms.Label();
            this.cbProxyAuthentication = new System.Windows.Forms.CheckBox();
            this.pnlProxyData = new System.Windows.Forms.Panel();
            this.tbProxyAddress = new System.Windows.Forms.TextBox();
            this.tbProxyPort = new System.Windows.Forms.TextBox();
            this.lblProxyPort = new System.Windows.Forms.Label();
            this.lblProxyAddress = new System.Windows.Forms.Label();
            this.rbCustomProxy = new System.Windows.Forms.RadioButton();
            this.rbDefaultProxy = new System.Windows.Forms.RadioButton();
            this.cbProxy = new System.Windows.Forms.CheckBox();
            this.tcTabsAbout = new System.Windows.Forms.TabPage();
            this.llblHomepage = new System.Windows.Forms.LinkLabel();
            this.lblThanks = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.llblUpdate = new System.Windows.Forms.LinkLabel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbRecordingPreview = new System.Windows.Forms.CheckBox();
            this.tcTabs.SuspendLayout();
            this.tcTabsProgram.SuspendLayout();
            this.pnlAutoClose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCloseSeconds)).BeginInit();
            this.tcTabsNetwork.SuspendLayout();
            this.pnlProxyOptions.SuspendLayout();
            this.pnlProxyAuthentication.SuspendLayout();
            this.pnlProxyData.SuspendLayout();
            this.tcTabsAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTabs
            // 
            this.tcTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTabs.Controls.Add(this.tcTabsProgram);
            this.tcTabs.Controls.Add(this.tcTabsNetwork);
            this.tcTabs.Controls.Add(this.tcTabsAbout);
            this.tcTabs.Location = new System.Drawing.Point(12, 12);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(310, 260);
            this.tcTabs.TabIndex = 0;
            // 
            // tcTabsProgram
            // 
            this.tcTabsProgram.Controls.Add(this.cbRecordingPreview);
            this.tcTabsProgram.Controls.Add(this.cbRetryDelete);
            this.tcTabsProgram.Controls.Add(this.cbProgressMethod);
            this.tcTabsProgram.Controls.Add(this.lblProgressIndicator);
            this.tcTabsProgram.Controls.Add(this.lblLastUpdate);
            this.tcTabsProgram.Controls.Add(this.cbAutoUpdate);
            this.tcTabsProgram.Controls.Add(this.cbRecordFollowing);
            this.tcTabsProgram.Controls.Add(this.cbAdjustStartTime);
            this.tcTabsProgram.Controls.Add(this.btnEditStations);
            this.tcTabsProgram.Controls.Add(this.pnlAutoClose);
            this.tcTabsProgram.Location = new System.Drawing.Point(4, 22);
            this.tcTabsProgram.Name = "tcTabsProgram";
            this.tcTabsProgram.Padding = new System.Windows.Forms.Padding(3);
            this.tcTabsProgram.Size = new System.Drawing.Size(302, 234);
            this.tcTabsProgram.TabIndex = 0;
            this.tcTabsProgram.Text = "Program";
            this.tcTabsProgram.UseVisualStyleBackColor = true;
            // 
            // cbRetryDelete
            // 
            this.cbRetryDelete.AutoSize = true;
            this.cbRetryDelete.Location = new System.Drawing.Point(6, 128);
            this.cbRetryDelete.Name = "cbRetryDelete";
            this.cbRetryDelete.Size = new System.Drawing.Size(198, 17);
            this.cbRetryDelete.TabIndex = 11;
            this.cbRetryDelete.Text = "Automatically retry recording deletion";
            this.cbRetryDelete.UseVisualStyleBackColor = true;
            // 
            // cbProgressMethod
            // 
            this.cbProgressMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProgressMethod.FormattingEnabled = true;
            this.cbProgressMethod.Items.AddRange(new object[] {
            "Show Window",
            "Show Systray-Icon",
            "Hide"});
            this.cbProgressMethod.Location = new System.Drawing.Point(108, 6);
            this.cbProgressMethod.Name = "cbProgressMethod";
            this.cbProgressMethod.Size = new System.Drawing.Size(146, 21);
            this.cbProgressMethod.TabIndex = 10;
            this.cbProgressMethod.SelectedIndexChanged += new System.EventHandler(this.cbProgressMethod_SelectedIndexChanged);
            // 
            // lblProgressIndicator
            // 
            this.lblProgressIndicator.AutoSize = true;
            this.lblProgressIndicator.Location = new System.Drawing.Point(3, 9);
            this.lblProgressIndicator.Name = "lblProgressIndicator";
            this.lblProgressIndicator.Size = new System.Drawing.Size(95, 13);
            this.lblProgressIndicator.TabIndex = 9;
            this.lblProgressIndicator.Text = "Progre&ss Indicator:";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Location = new System.Drawing.Point(17, 176);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(99, 13);
            this.lblLastUpdate.TabIndex = 8;
            this.lblLastUpdate.Text = "Last update check:";
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.AutoSize = true;
            this.cbAutoUpdate.Checked = true;
            this.cbAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoUpdate.Location = new System.Drawing.Point(6, 156);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(213, 17);
            this.cbAutoUpdate.TabIndex = 7;
            this.cbAutoUpdate.Text = "Automatically check for updates &weekly";
            this.cbAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // cbRecordFollowing
            // 
            this.cbRecordFollowing.AutoSize = true;
            this.cbRecordFollowing.Location = new System.Drawing.Point(6, 105);
            this.cbRecordFollowing.Name = "cbRecordFollowing";
            this.cbRecordFollowing.Size = new System.Drawing.Size(151, 17);
            this.cbRecordFollowing.TabIndex = 6;
            this.cbRecordFollowing.Text = "Also record following show";
            this.cbRecordFollowing.UseVisualStyleBackColor = true;
            // 
            // cbAdjustStartTime
            // 
            this.cbAdjustStartTime.AutoSize = true;
            this.cbAdjustStartTime.Checked = true;
            this.cbAdjustStartTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAdjustStartTime.Location = new System.Drawing.Point(6, 82);
            this.cbAdjustStartTime.Name = "cbAdjustStartTime";
            this.cbAdjustStartTime.Size = new System.Drawing.Size(198, 17);
            this.cbAdjustStartTime.TabIndex = 5;
            this.cbAdjustStartTime.Text = "Record shows, which already begun";
            this.cbAdjustStartTime.UseVisualStyleBackColor = true;
            // 
            // btnEditStations
            // 
            this.btnEditStations.Location = new System.Drawing.Point(138, 205);
            this.btnEditStations.Name = "btnEditStations";
            this.btnEditStations.Size = new System.Drawing.Size(158, 23);
            this.btnEditStations.TabIndex = 4;
            this.btnEditStations.Text = "E&dit Station Corrections...";
            this.btnEditStations.UseVisualStyleBackColor = true;
            this.btnEditStations.Click += new System.EventHandler(this.btnEditStations_Click);
            // 
            // pnlAutoClose
            // 
            this.pnlAutoClose.Controls.Add(this.lblSeconds);
            this.pnlAutoClose.Controls.Add(this.tbCloseSeconds);
            this.pnlAutoClose.Controls.Add(this.cbAutoClose);
            this.pnlAutoClose.Location = new System.Drawing.Point(20, 33);
            this.pnlAutoClose.Name = "pnlAutoClose";
            this.pnlAutoClose.Size = new System.Drawing.Size(235, 20);
            this.pnlAutoClose.TabIndex = 3;
            // 
            // lblSeconds
            // 
            this.lblSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(190, 3);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 7;
            this.lblSeconds.Text = "seconds";
            this.lblSeconds.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCloseSeconds
            // 
            this.tbCloseSeconds.Location = new System.Drawing.Point(144, 0);
            this.tbCloseSeconds.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.tbCloseSeconds.Name = "tbCloseSeconds";
            this.tbCloseSeconds.Size = new System.Drawing.Size(40, 20);
            this.tbCloseSeconds.TabIndex = 6;
            // 
            // cbAutoClose
            // 
            this.cbAutoClose.AutoSize = true;
            this.cbAutoClose.Checked = true;
            this.cbAutoClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoClose.Location = new System.Drawing.Point(0, 3);
            this.cbAutoClose.Name = "cbAutoClose";
            this.cbAutoClose.Size = new System.Drawing.Size(118, 17);
            this.cbAutoClose.TabIndex = 5;
            this.cbAutoClose.Text = "Hide &indicator after:";
            this.cbAutoClose.UseVisualStyleBackColor = true;
            // 
            // tcTabsNetwork
            // 
            this.tcTabsNetwork.Controls.Add(this.pnlProxyOptions);
            this.tcTabsNetwork.Controls.Add(this.cbProxy);
            this.tcTabsNetwork.Location = new System.Drawing.Point(4, 22);
            this.tcTabsNetwork.Name = "tcTabsNetwork";
            this.tcTabsNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tcTabsNetwork.Size = new System.Drawing.Size(302, 234);
            this.tcTabsNetwork.TabIndex = 1;
            this.tcTabsNetwork.Text = "Network";
            this.tcTabsNetwork.UseVisualStyleBackColor = true;
            // 
            // pnlProxyOptions
            // 
            this.pnlProxyOptions.Controls.Add(this.pnlProxyAuthentication);
            this.pnlProxyOptions.Controls.Add(this.cbProxyAuthentication);
            this.pnlProxyOptions.Controls.Add(this.pnlProxyData);
            this.pnlProxyOptions.Controls.Add(this.rbCustomProxy);
            this.pnlProxyOptions.Controls.Add(this.rbDefaultProxy);
            this.pnlProxyOptions.Location = new System.Drawing.Point(20, 29);
            this.pnlProxyOptions.Name = "pnlProxyOptions";
            this.pnlProxyOptions.Size = new System.Drawing.Size(276, 199);
            this.pnlProxyOptions.TabIndex = 11;
            // 
            // pnlProxyAuthentication
            // 
            this.pnlProxyAuthentication.Controls.Add(this.tbProxyPassword);
            this.pnlProxyAuthentication.Controls.Add(this.tbProxyUser);
            this.pnlProxyAuthentication.Controls.Add(this.lblProxyPassword);
            this.pnlProxyAuthentication.Controls.Add(this.lblProxyUser);
            this.pnlProxyAuthentication.Enabled = false;
            this.pnlProxyAuthentication.Location = new System.Drawing.Point(12, 88);
            this.pnlProxyAuthentication.Name = "pnlProxyAuthentication";
            this.pnlProxyAuthentication.Size = new System.Drawing.Size(261, 51);
            this.pnlProxyAuthentication.TabIndex = 32;
            // 
            // tbProxyPassword
            // 
            this.tbProxyPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyPassword.Location = new System.Drawing.Point(67, 26);
            this.tbProxyPassword.Name = "tbProxyPassword";
            this.tbProxyPassword.Size = new System.Drawing.Size(194, 20);
            this.tbProxyPassword.TabIndex = 32;
            this.tbProxyPassword.UseSystemPasswordChar = true;
            // 
            // tbProxyUser
            // 
            this.tbProxyUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyUser.Location = new System.Drawing.Point(67, 0);
            this.tbProxyUser.Name = "tbProxyUser";
            this.tbProxyUser.Size = new System.Drawing.Size(194, 20);
            this.tbProxyUser.TabIndex = 31;
            // 
            // lblProxyPassword
            // 
            this.lblProxyPassword.AutoSize = true;
            this.lblProxyPassword.Location = new System.Drawing.Point(3, 29);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new System.Drawing.Size(56, 13);
            this.lblProxyPassword.TabIndex = 30;
            this.lblProxyPassword.Text = "Passwor&d:";
            // 
            // lblProxyUser
            // 
            this.lblProxyUser.AutoSize = true;
            this.lblProxyUser.Location = new System.Drawing.Point(3, 3);
            this.lblProxyUser.Name = "lblProxyUser";
            this.lblProxyUser.Size = new System.Drawing.Size(58, 13);
            this.lblProxyUser.TabIndex = 29;
            this.lblProxyUser.Text = "Us&ername:";
            // 
            // cbProxyAuthentication
            // 
            this.cbProxyAuthentication.AutoSize = true;
            this.cbProxyAuthentication.Location = new System.Drawing.Point(0, 69);
            this.cbProxyAuthentication.Name = "cbProxyAuthentication";
            this.cbProxyAuthentication.Size = new System.Drawing.Size(148, 17);
            this.cbProxyAuthentication.TabIndex = 31;
            this.cbProxyAuthentication.Text = "Aut&hentication is required:";
            this.cbProxyAuthentication.UseVisualStyleBackColor = true;
            this.cbProxyAuthentication.CheckedChanged += new System.EventHandler(this.cbProxyAuthentication_CheckedChanged);
            // 
            // pnlProxyData
            // 
            this.pnlProxyData.Controls.Add(this.tbProxyAddress);
            this.pnlProxyData.Controls.Add(this.tbProxyPort);
            this.pnlProxyData.Controls.Add(this.lblProxyPort);
            this.pnlProxyData.Controls.Add(this.lblProxyAddress);
            this.pnlProxyData.Enabled = false;
            this.pnlProxyData.Location = new System.Drawing.Point(13, 42);
            this.pnlProxyData.Name = "pnlProxyData";
            this.pnlProxyData.Size = new System.Drawing.Size(260, 21);
            this.pnlProxyData.TabIndex = 21;
            // 
            // tbProxyAddress
            // 
            this.tbProxyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyAddress.Location = new System.Drawing.Point(60, 0);
            this.tbProxyAddress.Name = "tbProxyAddress";
            this.tbProxyAddress.Size = new System.Drawing.Size(129, 20);
            this.tbProxyAddress.TabIndex = 26;
            // 
            // tbProxyPort
            // 
            this.tbProxyPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyPort.Location = new System.Drawing.Point(230, 0);
            this.tbProxyPort.Name = "tbProxyPort";
            this.tbProxyPort.Size = new System.Drawing.Size(30, 20);
            this.tbProxyPort.TabIndex = 25;
            this.tbProxyPort.Text = "8080";
            // 
            // lblProxyPort
            // 
            this.lblProxyPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProxyPort.AutoSize = true;
            this.lblProxyPort.Location = new System.Drawing.Point(195, 3);
            this.lblProxyPort.Name = "lblProxyPort";
            this.lblProxyPort.Size = new System.Drawing.Size(29, 13);
            this.lblProxyPort.TabIndex = 22;
            this.lblProxyPort.Text = "Po&rt:";
            // 
            // lblProxyAddress
            // 
            this.lblProxyAddress.AutoSize = true;
            this.lblProxyAddress.Location = new System.Drawing.Point(3, 3);
            this.lblProxyAddress.Name = "lblProxyAddress";
            this.lblProxyAddress.Size = new System.Drawing.Size(48, 13);
            this.lblProxyAddress.TabIndex = 21;
            this.lblProxyAddress.Text = "&Address:";
            // 
            // rbCustomProxy
            // 
            this.rbCustomProxy.AutoSize = true;
            this.rbCustomProxy.Location = new System.Drawing.Point(0, 23);
            this.rbCustomProxy.Name = "rbCustomProxy";
            this.rbCustomProxy.Size = new System.Drawing.Size(112, 17);
            this.rbCustomProxy.TabIndex = 12;
            this.rbCustomProxy.Text = "Use custom pro&xy:";
            this.rbCustomProxy.UseVisualStyleBackColor = true;
            this.rbCustomProxy.CheckedChanged += new System.EventHandler(this.rbCustomProxy_CheckedChanged);
            // 
            // rbDefaultProxy
            // 
            this.rbDefaultProxy.AutoSize = true;
            this.rbDefaultProxy.Checked = true;
            this.rbDefaultProxy.Location = new System.Drawing.Point(0, 0);
            this.rbDefaultProxy.Name = "rbDefaultProxy";
            this.rbDefaultProxy.Size = new System.Drawing.Size(159, 17);
            this.rbDefaultProxy.TabIndex = 11;
            this.rbDefaultProxy.TabStop = true;
            this.rbDefaultProxy.Text = "Use IE &default proxy settings";
            this.rbDefaultProxy.UseVisualStyleBackColor = true;
            // 
            // cbProxy
            // 
            this.cbProxy.AutoSize = true;
            this.cbProxy.Checked = true;
            this.cbProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbProxy.Location = new System.Drawing.Point(6, 6);
            this.cbProxy.Name = "cbProxy";
            this.cbProxy.Size = new System.Drawing.Size(105, 17);
            this.cbProxy.TabIndex = 0;
            this.cbProxy.Text = "&Use proxy server";
            this.cbProxy.UseVisualStyleBackColor = true;
            this.cbProxy.CheckedChanged += new System.EventHandler(this.cbProxy_CheckedChanged);
            // 
            // tcTabsAbout
            // 
            this.tcTabsAbout.Controls.Add(this.llblHomepage);
            this.tcTabsAbout.Controls.Add(this.lblThanks);
            this.tcTabsAbout.Controls.Add(this.lblCopyright);
            this.tcTabsAbout.Controls.Add(this.lblVersion);
            this.tcTabsAbout.Controls.Add(this.lblProductName);
            this.tcTabsAbout.Location = new System.Drawing.Point(4, 22);
            this.tcTabsAbout.Name = "tcTabsAbout";
            this.tcTabsAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tcTabsAbout.Size = new System.Drawing.Size(302, 234);
            this.tcTabsAbout.TabIndex = 2;
            this.tcTabsAbout.Text = "About";
            this.tcTabsAbout.UseVisualStyleBackColor = true;
            // 
            // llblHomepage
            // 
            this.llblHomepage.AutoSize = true;
            this.llblHomepage.Location = new System.Drawing.Point(6, 98);
            this.llblHomepage.Name = "llblHomepage";
            this.llblHomepage.Size = new System.Drawing.Size(146, 13);
            this.llblHomepage.TabIndex = 5;
            this.llblHomepage.TabStop = true;
            this.llblHomepage.Text = "http://www.crazysoft.net.ms/";
            this.llblHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblHomepage_LinkClicked);
            // 
            // lblThanks
            // 
            this.lblThanks.AutoEllipsis = true;
            this.lblThanks.Location = new System.Drawing.Point(3, 128);
            this.lblThanks.Name = "lblThanks";
            this.lblThanks.Size = new System.Drawing.Size(293, 103);
            this.lblThanks.TabIndex = 4;
            this.lblThanks.Text = "Thanks to the folks at OnlineTVRecorder.com for their great service.\r\nThanks to t" +
                "he community at CodeProject.com for their coding help.";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(3, 72);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(293, 26);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "Copyright © Crazysoft 2006-2007. All rights reserved.";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(6, 37);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(290, 17);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version 1.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(6, 3);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(290, 39);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Crazysoft OTR Remote";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // llblUpdate
            // 
            this.llblUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llblUpdate.AutoSize = true;
            this.llblUpdate.Location = new System.Drawing.Point(12, 283);
            this.llblUpdate.Name = "llblUpdate";
            this.llblUpdate.Size = new System.Drawing.Size(103, 13);
            this.llblUpdate.TabIndex = 1;
            this.llblUpdate.TabStop = true;
            this.llblUpdate.Text = "Check for updates...";
            this.llblUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblUpdate_LinkClicked);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(166, 278);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(247, 278);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbRecordingPreview
            // 
            this.cbRecordingPreview.AutoSize = true;
            this.cbRecordingPreview.Checked = true;
            this.cbRecordingPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRecordingPreview.Location = new System.Drawing.Point(6, 59);
            this.cbRecordingPreview.Name = "cbRecordingPreview";
            this.cbRecordingPreview.Size = new System.Drawing.Size(230, 17);
            this.cbRecordingPreview.TabIndex = 12;
            this.cbRecordingPreview.Text = "Show recording preview before sending job";
            this.cbRecordingPreview.UseVisualStyleBackColor = true;
            // 
            // FrmPreferences
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(334, 313);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.llblUpdate);
            this.Controls.Add(this.tcTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPreferences";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Program Preferences";
            this.Load += new System.EventHandler(this.FrmPreferences_Load);
            this.tcTabs.ResumeLayout(false);
            this.tcTabsProgram.ResumeLayout(false);
            this.tcTabsProgram.PerformLayout();
            this.pnlAutoClose.ResumeLayout(false);
            this.pnlAutoClose.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCloseSeconds)).EndInit();
            this.tcTabsNetwork.ResumeLayout(false);
            this.tcTabsNetwork.PerformLayout();
            this.pnlProxyOptions.ResumeLayout(false);
            this.pnlProxyOptions.PerformLayout();
            this.pnlProxyAuthentication.ResumeLayout(false);
            this.pnlProxyAuthentication.PerformLayout();
            this.pnlProxyData.ResumeLayout(false);
            this.pnlProxyData.PerformLayout();
            this.tcTabsAbout.ResumeLayout(false);
            this.tcTabsAbout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcTabs;
        private System.Windows.Forms.TabPage tcTabsProgram;
        private System.Windows.Forms.TabPage tcTabsNetwork;
        private System.Windows.Forms.TabPage tcTabsAbout;
        private System.Windows.Forms.LinkLabel llblUpdate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbProxy;
        private System.Windows.Forms.Panel pnlProxyOptions;
        private System.Windows.Forms.Panel pnlProxyData;
        private System.Windows.Forms.RadioButton rbCustomProxy;
        private System.Windows.Forms.RadioButton rbDefaultProxy;
        private System.Windows.Forms.TextBox tbProxyAddress;
        private System.Windows.Forms.TextBox tbProxyPort;
        private System.Windows.Forms.Label lblProxyPort;
        private System.Windows.Forms.Label lblProxyAddress;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblThanks;
        private System.Windows.Forms.Panel pnlAutoClose;
        private System.Windows.Forms.LinkLabel llblHomepage;
        private System.Windows.Forms.Button btnEditStations;
        private System.Windows.Forms.Panel pnlProxyAuthentication;
        private System.Windows.Forms.TextBox tbProxyPassword;
        private System.Windows.Forms.TextBox tbProxyUser;
        private System.Windows.Forms.Label lblProxyPassword;
        private System.Windows.Forms.Label lblProxyUser;
        private System.Windows.Forms.CheckBox cbProxyAuthentication;
        private System.Windows.Forms.CheckBox cbAdjustStartTime;
        private System.Windows.Forms.CheckBox cbRecordFollowing;
        private System.Windows.Forms.NumericUpDown tbCloseSeconds;
        private System.Windows.Forms.CheckBox cbAutoClose;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.CheckBox cbAutoUpdate;
        private System.Windows.Forms.ComboBox cbProgressMethod;
        private System.Windows.Forms.Label lblProgressIndicator;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.CheckBox cbRetryDelete;
        private System.Windows.Forms.CheckBox cbRecordingPreview;
    }
}