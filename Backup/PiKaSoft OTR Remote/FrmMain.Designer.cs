namespace Crazysoft.OTR_Remote
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblIntroduction = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblTimezone = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.gbUserdata = new System.Windows.Forms.GroupBox();
            this.gbRecording = new System.Windows.Forms.GroupBox();
            this.lblTVGuide = new System.Windows.Forms.Label();
            this.cbTVGuide = new System.Windows.Forms.ComboBox();
            this.tbTimezone = new System.Windows.Forms.NumericUpDown();
            this.ttTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnPreferences = new System.Windows.Forms.Button();
            this.gbTVGuide = new System.Windows.Forms.GroupBox();
            this.lvSettings = new Crazysoft.OTR_Remote.ListViewEx.ListViewEx();
            this.lvSettingsSetting = new System.Windows.Forms.ColumnHeader();
            this.lvSettingsValue = new System.Windows.Forms.ColumnHeader();
            this.pnlHeader.SuspendLayout();
            this.gbUserdata.SuspendLayout();
            this.gbRecording.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTimezone)).BeginInit();
            this.gbTVGuide.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(514, 68);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(490, 47);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Control Panel";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIntroduction
            // 
            this.lblIntroduction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIntroduction.BackColor = System.Drawing.Color.Transparent;
            this.lblIntroduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntroduction.Location = new System.Drawing.Point(14, 90);
            this.lblIntroduction.Name = "lblIntroduction";
            this.lblIntroduction.Size = new System.Drawing.Size(490, 40);
            this.lblIntroduction.TabIndex = 1;
            this.lblIntroduction.Text = "Welcome to OTR Remote! Please configure the basic data for running the software.\r" +
                "\nNote: Adding and removing of TV shows is done via the commandline interface.";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 22);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "&Username:";
            // 
            // lblTimezone
            // 
            this.lblTimezone.AutoSize = true;
            this.lblTimezone.Location = new System.Drawing.Point(6, 21);
            this.lblTimezone.Name = "lblTimezone";
            this.lblTimezone.Size = new System.Drawing.Size(56, 13);
            this.lblTimezone.TabIndex = 0;
            this.lblTimezone.Text = "&Timezone:";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(279, 22);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "&Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(70, 19);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(100, 20);
            this.tbUsername.TabIndex = 0;
            this.ttTooltip.SetToolTip(this.tbUsername, "This is your OTR username or e-mail address.");
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(341, 19);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 1;
            this.ttTooltip.SetToolTip(this.tbPassword, "This is your OTR password.");
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // gbUserdata
            // 
            this.gbUserdata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserdata.BackColor = System.Drawing.Color.Transparent;
            this.gbUserdata.Controls.Add(this.tbPassword);
            this.gbUserdata.Controls.Add(this.lblPassword);
            this.gbUserdata.Controls.Add(this.lblUsername);
            this.gbUserdata.Controls.Add(this.tbUsername);
            this.gbUserdata.Location = new System.Drawing.Point(12, 138);
            this.gbUserdata.Name = "gbUserdata";
            this.gbUserdata.Size = new System.Drawing.Size(490, 50);
            this.gbUserdata.TabIndex = 0;
            this.gbUserdata.TabStop = false;
            this.gbUserdata.Text = "Your user data at OnlineTVRecorder.com";
            // 
            // gbRecording
            // 
            this.gbRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecording.BackColor = System.Drawing.Color.Transparent;
            this.gbRecording.Controls.Add(this.lblTVGuide);
            this.gbRecording.Controls.Add(this.cbTVGuide);
            this.gbRecording.Controls.Add(this.tbTimezone);
            this.gbRecording.Controls.Add(this.lblTimezone);
            this.gbRecording.Location = new System.Drawing.Point(12, 194);
            this.gbRecording.Name = "gbRecording";
            this.gbRecording.Size = new System.Drawing.Size(490, 48);
            this.gbRecording.TabIndex = 1;
            this.gbRecording.TabStop = false;
            this.gbRecording.Text = "Recording Settings";
            // 
            // lblTVGuide
            // 
            this.lblTVGuide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTVGuide.AutoSize = true;
            this.lblTVGuide.Location = new System.Drawing.Point(248, 22);
            this.lblTVGuide.Name = "lblTVGuide";
            this.lblTVGuide.Size = new System.Drawing.Size(87, 13);
            this.lblTVGuide.TabIndex = 1;
            this.lblTVGuide.Text = "TV &Guide Plugin:";
            this.lblTVGuide.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbTVGuide
            // 
            this.cbTVGuide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTVGuide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTVGuide.FormattingEnabled = true;
            this.cbTVGuide.Location = new System.Drawing.Point(341, 19);
            this.cbTVGuide.Name = "cbTVGuide";
            this.cbTVGuide.Size = new System.Drawing.Size(100, 21);
            this.cbTVGuide.Sorted = true;
            this.cbTVGuide.TabIndex = 1;
            this.ttTooltip.SetToolTip(this.cbTVGuide, resources.GetString("cbTVGuide.ToolTip"));
            this.cbTVGuide.SelectedIndexChanged += new System.EventHandler(this.cbTVGuide_SelectedIndexChanged);
            // 
            // tbTimezone
            // 
            this.tbTimezone.Location = new System.Drawing.Point(70, 19);
            this.tbTimezone.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.tbTimezone.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.tbTimezone.Name = "tbTimezone";
            this.tbTimezone.Size = new System.Drawing.Size(40, 20);
            this.tbTimezone.TabIndex = 0;
            this.ttTooltip.SetToolTip(this.tbTimezone, "Defines the global timezone you are located in.\nThis setting helps OTR to indicat" +
                    "e the correct recording times.");
            // 
            // ttTooltip
            // 
            this.ttTooltip.AutoPopDelay = 15000;
            this.ttTooltip.InitialDelay = 500;
            this.ttTooltip.IsBalloon = true;
            this.ttTooltip.ReshowDelay = 100;
            this.ttTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttTooltip.ToolTipTitle = "Context Sensitive Help";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(427, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.ttTooltip.SetToolTip(this.btnCancel, "Quits the application without saving any changes.");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(346, 386);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.ttTooltip.SetToolTip(this.btnOK, "Saves the changes you made and quits the application.");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(265, 386);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "&Help";
            this.ttTooltip.SetToolTip(this.btnHelp, "Opens OTR Remote\'s help files.");
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnPreferences
            // 
            this.btnPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreferences.Location = new System.Drawing.Point(12, 386);
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.Size = new System.Drawing.Size(75, 23);
            this.btnPreferences.TabIndex = 5;
            this.btnPreferences.Text = "P&references";
            this.ttTooltip.SetToolTip(this.btnPreferences, "Customize the behavior of OTR Remote,\r\nedit network and proxy settings,\r\nlook for" +
                    " new program versions and\r\nshow informations about this application.");
            this.btnPreferences.UseVisualStyleBackColor = true;
            this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
            // 
            // gbTVGuide
            // 
            this.gbTVGuide.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTVGuide.BackColor = System.Drawing.Color.Transparent;
            this.gbTVGuide.Controls.Add(this.lvSettings);
            this.gbTVGuide.Location = new System.Drawing.Point(12, 248);
            this.gbTVGuide.Name = "gbTVGuide";
            this.gbTVGuide.Size = new System.Drawing.Size(490, 119);
            this.gbTVGuide.TabIndex = 2;
            this.gbTVGuide.TabStop = false;
            this.gbTVGuide.Text = "Plugin Settings";
            // 
            // lvSettings
            // 
            this.lvSettings.AllowColumnReorder = true;
            this.lvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvSettingsSetting,
            this.lvSettingsValue});
            this.lvSettings.DoubleClickActivation = false;
            this.lvSettings.FullRowSelect = true;
            this.lvSettings.GridLines = true;
            this.lvSettings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSettings.Location = new System.Drawing.Point(6, 19);
            this.lvSettings.Name = "lvSettings";
            this.lvSettings.Size = new System.Drawing.Size(478, 94);
            this.lvSettings.TabIndex = 0;
            this.lvSettings.UseCompatibleStateImageBehavior = false;
            this.lvSettings.View = System.Windows.Forms.View.Details;
            this.lvSettings.SubItemClicked += new Crazysoft.OTR_Remote.ListViewEx.SubItemEventHandler(this.lvSettings_SubItemClicked);
            this.lvSettings.SubItemEndEditing += new Crazysoft.OTR_Remote.ListViewEx.SubItemEndEditingEventHandler(this.lvSettings_SubItemEndEditing);
            // 
            // lvSettingsSetting
            // 
            this.lvSettingsSetting.Text = "Setting";
            this.lvSettingsSetting.Width = 228;
            // 
            // lvSettingsValue
            // 
            this.lvSettingsValue.Text = "Value";
            this.lvSettingsValue.Width = 229;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 421);
            this.Controls.Add(this.btnPreferences);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbTVGuide);
            this.Controls.Add(this.gbRecording);
            this.Controls.Add(this.gbUserdata);
            this.Controls.Add(this.lblIntroduction);
            this.Controls.Add(this.pnlHeader);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(530, 433);
            this.Name = "FrmMain";
            this.Text = "Crazysoft OTR Remote";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMain_Paint);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.pnlHeader.ResumeLayout(false);
            this.gbUserdata.ResumeLayout(false);
            this.gbUserdata.PerformLayout();
            this.gbRecording.ResumeLayout(false);
            this.gbRecording.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTimezone)).EndInit();
            this.gbTVGuide.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblIntroduction;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblTimezone;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.GroupBox gbUserdata;
        private System.Windows.Forms.GroupBox gbRecording;
        private System.Windows.Forms.NumericUpDown tbTimezone;
        private System.Windows.Forms.ToolTip ttTooltip;
        private System.Windows.Forms.Label lblTVGuide;
        private System.Windows.Forms.ComboBox cbTVGuide;
        private System.Windows.Forms.GroupBox gbTVGuide;
        private Crazysoft.OTR_Remote.ListViewEx.ListViewEx lvSettings;
        private System.Windows.Forms.ColumnHeader lvSettingsSetting;
        private System.Windows.Forms.ColumnHeader lvSettingsValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnPreferences;
    }
}