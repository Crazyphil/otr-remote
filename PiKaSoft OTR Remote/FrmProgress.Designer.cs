namespace Crazysoft.OTRRemote
{
    partial class FrmProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProgress));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.timTimer = new System.Windows.Forms.Timer(this.components);
            this.timRetry = new System.Windows.Forms.Timer(this.components);
            this.bwWorker = new System.ComponentModel.BackgroundWorker();
            this.niSystray = new System.Windows.Forms.NotifyIcon(this.components);
            this.timRetryTime = new System.Windows.Forms.Timer(this.components);
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
            this.pnlHeader.Size = new System.Drawing.Size(292, 68);
            this.pnlHeader.TabIndex = 3;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(268, 47);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Action Progress";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(12, 98);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(268, 26);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Please wait, while OTR Remote modifies the recording.";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 149);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 165);
            this.pbProgress.Maximum = 3;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(268, 23);
            this.pbProgress.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(102, 194);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timTimer
            // 
            this.timTimer.Interval = 1000;
            this.timTimer.Tick += new System.EventHandler(this.timRetry_Tick);
            // 
            // timRetry
            // 
            this.timRetry.Interval = 600000;
            this.timRetry.Tick += new System.EventHandler(this.timRetry_Tick);
            // 
            // bwWorker
            // 
            this.bwWorker.WorkerReportsProgress = true;
            this.bwWorker.WorkerSupportsCancellation = true;
            this.bwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwWorker_DoWork);
            this.bwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwWorker_RunWorkerCompleted);
            this.bwWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwWorker_ProgressChanged);
            // 
            // niSystray
            // 
            this.niSystray.Icon = ((System.Drawing.Icon)(resources.GetObject("niSystray.Icon")));
            this.niSystray.Text = "Crazysoft OTR Remote";
            this.niSystray.DoubleClick += new System.EventHandler(this.niSystray_Click);
            // 
            // timRetryTime
            // 
            this.timRetryTime.Interval = 1000;
            this.timRetryTime.Tick += new System.EventHandler(this.timRetryTime_Tick);
            // 
            // FrmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 229);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crazysoft OTR Remote";
            this.Load += new System.EventHandler(this.FrmProgress_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmProgress_Paint);
            this.VisibleChanged += new System.EventHandler(this.FrmProgress_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgress_FormClosing);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timTimer;
        private System.Windows.Forms.Timer timRetry;
        private System.Windows.Forms.Timer timRetryTime;
        private System.Windows.Forms.NotifyIcon niSystray;

        // Minimal form initializer with only Timer, BackgroundWorker and NotifyIcon for Hidden mode
        private void InitializeHiddenComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProgress));
            this.timTimer = new System.Windows.Forms.Timer(this.components);
            this.timRetry = new System.Windows.Forms.Timer(this.components);
            this.bwWorker = new System.ComponentModel.BackgroundWorker();
            this.niSystray = new System.Windows.Forms.NotifyIcon(this.components);
            this.timRetryTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timTimer
            // 
            this.timTimer.Interval = 1000;
            this.timTimer.Tick += new System.EventHandler(this.timTimer_Tick);
            // 
            // timRetry
            // 
            this.timRetry.Interval = 600000;
            this.timTimer.Tick += new System.EventHandler(this.timRetry_Tick);
            // 
            // bwWorker
            // 
            this.bwWorker.WorkerReportsProgress = true;
            this.bwWorker.WorkerSupportsCancellation = true;
            this.bwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwWorker_DoWork);
            this.bwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwWorker_RunWorkerCompleted);
            this.bwWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwWorker_ProgressChanged);
            // 
            // niSystray
            // 
            this.niSystray.Icon = ((System.Drawing.Icon)(resources.GetObject("niSystray.Icon")));
            this.niSystray.Text = "Crazysoft OTR Remote";
            // 
            // timRetryTime
            // 
            this.timRetryTime.Interval = 1000;
            this.timRetryTime.Tick += new System.EventHandler(this.timRetryTime_Tick);
            // 
            // FrmProgress
            // 
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProgress";
            this.Text = "Crazysoft OTR Remote";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmProgress_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgress_FormClosing);
            this.VisibleChanged += new System.EventHandler(FrmProgress_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.ComponentModel.BackgroundWorker bwWorker;
    }
}