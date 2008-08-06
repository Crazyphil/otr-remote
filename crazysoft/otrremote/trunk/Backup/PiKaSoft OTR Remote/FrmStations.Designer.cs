namespace Crazysoft.OTR_Remote
{
    partial class FrmStations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStations));
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblOTRStation = new System.Windows.Forms.Label();
            this.lbOTRStation = new System.Windows.Forms.ListBox();
            this.lbEPGStation = new System.Windows.Forms.ListBox();
            this.lblEPGStation = new System.Windows.Forms.Label();
            this.btnOTRAdd = new System.Windows.Forms.Button();
            this.tbOTRStation = new System.Windows.Forms.TextBox();
            this.btnOTRRemove = new System.Windows.Forms.Button();
            this.btnEPGRemove = new System.Windows.Forms.Button();
            this.tbEPGStation = new System.Windows.Forms.TextBox();
            this.btnEPGAdd = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbAdvanced = new System.Windows.Forms.GroupBox();
            this.cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.gbAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(356, 44);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Every EPG software has its own name for TV stations. To make sure OTR gets the co" +
                "rrect station name, enter the replacements here.";
            // 
            // lblOTRStation
            // 
            this.lblOTRStation.AutoSize = true;
            this.lblOTRStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOTRStation.Location = new System.Drawing.Point(12, 61);
            this.lblOTRStation.Name = "lblOTRStation";
            this.lblOTRStation.Size = new System.Drawing.Size(105, 13);
            this.lblOTRStation.TabIndex = 1;
            this.lblOTRStation.Text = "OTR Station Names:";
            // 
            // lbOTRStation
            // 
            this.lbOTRStation.DisplayMember = "Name";
            this.lbOTRStation.FormattingEnabled = true;
            this.lbOTRStation.Location = new System.Drawing.Point(15, 77);
            this.lbOTRStation.Name = "lbOTRStation";
            this.lbOTRStation.Size = new System.Drawing.Size(157, 160);
            this.lbOTRStation.Sorted = true;
            this.lbOTRStation.TabIndex = 2;
            this.lbOTRStation.SelectedIndexChanged += new System.EventHandler(this.lbOTRStation_SelectedIndexChanged);
            // 
            // lbEPGStation
            // 
            this.lbEPGStation.FormattingEnabled = true;
            this.lbEPGStation.Location = new System.Drawing.Point(207, 77);
            this.lbEPGStation.Name = "lbEPGStation";
            this.lbEPGStation.Size = new System.Drawing.Size(157, 160);
            this.lbEPGStation.Sorted = true;
            this.lbEPGStation.TabIndex = 7;
            this.lbEPGStation.SelectedIndexChanged += new System.EventHandler(this.lbEPGStation_SelectedIndexChanged);
            // 
            // lblEPGStation
            // 
            this.lblEPGStation.AutoSize = true;
            this.lblEPGStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPGStation.Location = new System.Drawing.Point(204, 61);
            this.lblEPGStation.Name = "lblEPGStation";
            this.lblEPGStation.Size = new System.Drawing.Size(139, 13);
            this.lblEPGStation.TabIndex = 6;
            this.lblEPGStation.Text = "EPG Station Replacements:";
            // 
            // btnOTRAdd
            // 
            this.btnOTRAdd.Enabled = false;
            this.btnOTRAdd.Location = new System.Drawing.Point(15, 269);
            this.btnOTRAdd.Name = "btnOTRAdd";
            this.btnOTRAdd.Size = new System.Drawing.Size(76, 23);
            this.btnOTRAdd.TabIndex = 4;
            this.btnOTRAdd.Text = "&Add";
            this.btnOTRAdd.UseVisualStyleBackColor = true;
            this.btnOTRAdd.Click += new System.EventHandler(this.btnOTRAdd_Click);
            // 
            // tbOTRStation
            // 
            this.tbOTRStation.Location = new System.Drawing.Point(15, 243);
            this.tbOTRStation.Name = "tbOTRStation";
            this.tbOTRStation.Size = new System.Drawing.Size(157, 20);
            this.tbOTRStation.TabIndex = 3;
            this.tbOTRStation.TextChanged += new System.EventHandler(this.tbOTRStation_TextChanged);
            this.tbOTRStation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOTRStation_KeyPress);
            // 
            // btnOTRRemove
            // 
            this.btnOTRRemove.Enabled = false;
            this.btnOTRRemove.Location = new System.Drawing.Point(97, 269);
            this.btnOTRRemove.Name = "btnOTRRemove";
            this.btnOTRRemove.Size = new System.Drawing.Size(75, 23);
            this.btnOTRRemove.TabIndex = 5;
            this.btnOTRRemove.Text = "&Remove";
            this.btnOTRRemove.UseVisualStyleBackColor = true;
            this.btnOTRRemove.Click += new System.EventHandler(this.btnOTRRemove_Click);
            // 
            // btnEPGRemove
            // 
            this.btnEPGRemove.Enabled = false;
            this.btnEPGRemove.Location = new System.Drawing.Point(289, 269);
            this.btnEPGRemove.Name = "btnEPGRemove";
            this.btnEPGRemove.Size = new System.Drawing.Size(75, 23);
            this.btnEPGRemove.TabIndex = 10;
            this.btnEPGRemove.Text = "R&emove";
            this.btnEPGRemove.UseVisualStyleBackColor = true;
            this.btnEPGRemove.Click += new System.EventHandler(this.btnEPGRemove_Click);
            // 
            // tbEPGStation
            // 
            this.tbEPGStation.Location = new System.Drawing.Point(207, 243);
            this.tbEPGStation.Name = "tbEPGStation";
            this.tbEPGStation.Size = new System.Drawing.Size(157, 20);
            this.tbEPGStation.TabIndex = 8;
            this.tbEPGStation.TextChanged += new System.EventHandler(this.tbEPGStation_TextChanged);
            this.tbEPGStation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEPGStation_KeyPress);
            // 
            // btnEPGAdd
            // 
            this.btnEPGAdd.Enabled = false;
            this.btnEPGAdd.Location = new System.Drawing.Point(207, 269);
            this.btnEPGAdd.Name = "btnEPGAdd";
            this.btnEPGAdd.Size = new System.Drawing.Size(76, 23);
            this.btnEPGAdd.TabIndex = 9;
            this.btnEPGAdd.Text = "A&dd";
            this.btnEPGAdd.UseVisualStyleBackColor = true;
            this.btnEPGAdd.Click += new System.EventHandler(this.btnEPGAdd_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(113, 367);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(194, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbAdvanced
            // 
            this.gbAdvanced.Controls.Add(this.cbCaseSensitive);
            this.gbAdvanced.Location = new System.Drawing.Point(12, 300);
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.Size = new System.Drawing.Size(356, 61);
            this.gbAdvanced.TabIndex = 11;
            this.gbAdvanced.TabStop = false;
            this.gbAdvanced.Text = "Ad&vanced Settings";
            // 
            // cbCaseSensitive
            // 
            this.cbCaseSensitive.AutoSize = true;
            this.cbCaseSensitive.Checked = true;
            this.cbCaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaseSensitive.Location = new System.Drawing.Point(6, 28);
            this.cbCaseSensitive.Name = "cbCaseSensitive";
            this.cbCaseSensitive.Size = new System.Drawing.Size(223, 17);
            this.cbCaseSensitive.TabIndex = 0;
            this.cbCaseSensitive.Text = "Case-&sensitive station name replacements";
            this.cbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // FrmStations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 403);
            this.Controls.Add(this.gbAdvanced);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnEPGRemove);
            this.Controls.Add(this.tbEPGStation);
            this.Controls.Add(this.btnEPGAdd);
            this.Controls.Add(this.btnOTRRemove);
            this.Controls.Add(this.tbOTRStation);
            this.Controls.Add(this.btnOTRAdd);
            this.Controls.Add(this.lbEPGStation);
            this.Controls.Add(this.lblEPGStation);
            this.Controls.Add(this.lbOTRStation);
            this.Controls.Add(this.lblOTRStation);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStations";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Station Corrections";
            this.Load += new System.EventHandler(this.FrmStations_Load);
            this.gbAdvanced.ResumeLayout(false);
            this.gbAdvanced.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblOTRStation;
        private System.Windows.Forms.ListBox lbOTRStation;
        private System.Windows.Forms.ListBox lbEPGStation;
        private System.Windows.Forms.Label lblEPGStation;
        private System.Windows.Forms.Button btnOTRAdd;
        private System.Windows.Forms.TextBox tbOTRStation;
        private System.Windows.Forms.Button btnOTRRemove;
        private System.Windows.Forms.Button btnEPGRemove;
        private System.Windows.Forms.TextBox tbEPGStation;
        private System.Windows.Forms.Button btnEPGAdd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbAdvanced;
        private System.Windows.Forms.CheckBox cbCaseSensitive;
    }
}