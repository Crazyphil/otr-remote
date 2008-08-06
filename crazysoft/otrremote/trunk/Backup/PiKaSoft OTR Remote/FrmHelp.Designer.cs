namespace Crazysoft.OTR_Remote
{
    partial class FrmHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelp));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblUsageHeader = new System.Windows.Forms.Label();
            this.lblUsageText = new System.Windows.Forms.Label();
            this.lblCmdArgsHeader = new System.Windows.Forms.Label();
            this.lblCmdArgsText = new System.Windows.Forms.Label();
            this.lblOptionalArgsText = new System.Windows.Forms.Label();
            this.lblOptionalArgsHeader = new System.Windows.Forms.Label();
            this.lblNotesText = new System.Windows.Forms.Label();
            this.lblNotesHeader = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(631, 68);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(600, 47);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Recording Help";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsageHeader
            // 
            this.lblUsageHeader.AutoSize = true;
            this.lblUsageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsageHeader.Location = new System.Drawing.Point(12, 90);
            this.lblUsageHeader.Name = "lblUsageHeader";
            this.lblUsageHeader.Size = new System.Drawing.Size(47, 13);
            this.lblUsageHeader.TabIndex = 2;
            this.lblUsageHeader.Text = "Usage:";
            // 
            // lblUsageText
            // 
            this.lblUsageText.Location = new System.Drawing.Point(12, 112);
            this.lblUsageText.Name = "lblUsageText";
            this.lblUsageText.Size = new System.Drawing.Size(607, 24);
            this.lblUsageText.TabIndex = 3;
            this.lblUsageText.Text = "OTRRemote.exe -a|-r -s=\"Station\" -sd=yyyy-mm-dd -st=hh:mm -et=hh:mm [-t=\"Title\"] " +
                "[-u=\"User\"] [-p=\"Password\"] [-tz=Timezone]";
            // 
            // lblCmdArgsHeader
            // 
            this.lblCmdArgsHeader.AutoSize = true;
            this.lblCmdArgsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCmdArgsHeader.Location = new System.Drawing.Point(14, 145);
            this.lblCmdArgsHeader.Name = "lblCmdArgsHeader";
            this.lblCmdArgsHeader.Size = new System.Drawing.Size(148, 13);
            this.lblCmdArgsHeader.TabIndex = 4;
            this.lblCmdArgsHeader.Text = "Commandline Arguments:";
            // 
            // lblCmdArgsText
            // 
            this.lblCmdArgsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCmdArgsText.AutoSize = true;
            this.lblCmdArgsText.Location = new System.Drawing.Point(12, 168);
            this.lblCmdArgsText.Name = "lblCmdArgsText";
            this.lblCmdArgsText.Size = new System.Drawing.Size(251, 91);
            this.lblCmdArgsText.TabIndex = 5;
            this.lblCmdArgsText.Text = resources.GetString("lblCmdArgsText.Text");
            // 
            // lblOptionalArgsText
            // 
            this.lblOptionalArgsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOptionalArgsText.AutoSize = true;
            this.lblOptionalArgsText.Location = new System.Drawing.Point(12, 303);
            this.lblOptionalArgsText.Name = "lblOptionalArgsText";
            this.lblOptionalArgsText.Size = new System.Drawing.Size(334, 65);
            this.lblOptionalArgsText.TabIndex = 7;
            this.lblOptionalArgsText.Text = "-t Title of the TV show to record.\r\n-g Genre of the TV show.\r\n-u Your OTR usernam" +
                "e or e-mail address.\r\n-p Your OTR password.\r\n-tz Timezone: The difference to GMT" +
                " in hours. (e.g. Middle Europe: 1)";
            // 
            // lblOptionalArgsHeader
            // 
            this.lblOptionalArgsHeader.AutoSize = true;
            this.lblOptionalArgsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptionalArgsHeader.Location = new System.Drawing.Point(12, 281);
            this.lblOptionalArgsHeader.Name = "lblOptionalArgsHeader";
            this.lblOptionalArgsHeader.Size = new System.Drawing.Size(121, 13);
            this.lblOptionalArgsHeader.TabIndex = 6;
            this.lblOptionalArgsHeader.Text = "Optional Arguments:";
            // 
            // lblNotesText
            // 
            this.lblNotesText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotesText.AutoSize = true;
            this.lblNotesText.Location = new System.Drawing.Point(14, 414);
            this.lblNotesText.Name = "lblNotesText";
            this.lblNotesText.Size = new System.Drawing.Size(474, 26);
            this.lblNotesText.TabIndex = 9;
            this.lblNotesText.Text = "If an argument is shown within quotation marks, you have to write them too!\nUsern" +
                "ame, password and timezone don\'t have to be set if you configured them in the Co" +
                "ntrol Panel.";
            // 
            // lblNotesHeader
            // 
            this.lblNotesHeader.AutoSize = true;
            this.lblNotesHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotesHeader.Location = new System.Drawing.Point(14, 392);
            this.lblNotesHeader.Name = "lblNotesHeader";
            this.lblNotesHeader.Size = new System.Drawing.Size(44, 13);
            this.lblNotesHeader.TabIndex = 8;
            this.lblNotesHeader.Text = "Notes:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(271, 452);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmHelp
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(631, 487);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNotesText);
            this.Controls.Add(this.lblNotesHeader);
            this.Controls.Add(this.lblOptionalArgsText);
            this.Controls.Add(this.lblOptionalArgsHeader);
            this.Controls.Add(this.lblCmdArgsText);
            this.Controls.Add(this.lblCmdArgsHeader);
            this.Controls.Add(this.lblUsageText);
            this.Controls.Add(this.lblUsageHeader);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crazysoft OTR Remote";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmHelp_Paint);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblUsageHeader;
        private System.Windows.Forms.Label lblUsageText;
        private System.Windows.Forms.Label lblCmdArgsHeader;
        private System.Windows.Forms.Label lblCmdArgsText;
        private System.Windows.Forms.Label lblOptionalArgsText;
        private System.Windows.Forms.Label lblOptionalArgsHeader;
        private System.Windows.Forms.Label lblNotesText;
        private System.Windows.Forms.Label lblNotesHeader;
        private System.Windows.Forms.Button btnClose;
    }
}