namespace Crazysoft.OTR_Remote
{
    partial class FrmPluginError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPluginError));
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbDebugInfos = new System.Windows.Forms.TextBox();
            this.pbError = new System.Windows.Forms.PictureBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.Location = new System.Drawing.Point(66, 12);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(348, 48);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "An error has occured in the plugin {0}. Below is the extended error information, " +
                "which you may want to send to the plugin\'s developer.";
            // 
            // tbDebugInfos
            // 
            this.tbDebugInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDebugInfos.Location = new System.Drawing.Point(12, 66);
            this.tbDebugInfos.Multiline = true;
            this.tbDebugInfos.Name = "tbDebugInfos";
            this.tbDebugInfos.ReadOnly = true;
            this.tbDebugInfos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDebugInfos.Size = new System.Drawing.Size(402, 137);
            this.tbDebugInfos.TabIndex = 3;
            // 
            // pbError
            // 
            this.pbError.Image = ((System.Drawing.Image)(resources.GetObject("pbError.Image")));
            this.pbError.Location = new System.Drawing.Point(12, 12);
            this.pbError.Name = "pbError";
            this.pbError.Size = new System.Drawing.Size(48, 48);
            this.pbError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbError.TabIndex = 2;
            this.pbError.TabStop = false;
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnContinue.Image = ((System.Drawing.Image)(resources.GetObject("btnContinue.Image")));
            this.btnContinue.Location = new System.Drawing.Point(290, 209);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(124, 28);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestart.Image = ((System.Drawing.Image)(resources.GetObject("btnRestart.Image")));
            this.btnRestart.Location = new System.Drawing.Point(160, 209);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(124, 28);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "&Restart program";
            this.btnRestart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(30, 209);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(124, 28);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.Text = "&Exit program";
            this.btnQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // FrmPluginError
            // 
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnContinue;
            this.ClientSize = new System.Drawing.Size(426, 249);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.pbError);
            this.Controls.Add(this.tbDebugInfos);
            this.Controls.Add(this.lblDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPluginError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error in plugin";
            this.Load += new System.EventHandler(this.FrmPluginError_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbDebugInfos;
        private System.Windows.Forms.PictureBox pbError;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnQuit;
    }
}