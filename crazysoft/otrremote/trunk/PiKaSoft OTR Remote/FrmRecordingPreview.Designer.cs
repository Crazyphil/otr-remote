namespace Crazysoft.OTRRemote
{
    partial class FrmRecordingPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecordingPreview));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblIntroduction = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbStation = new System.Windows.Forms.TextBox();
            this.lblStation = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.tbGenre = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbSeries = new System.Windows.Forms.GroupBox();
            this.lblSeriesTime = new System.Windows.Forms.Label();
            this.lblSeriesDays = new System.Windows.Forms.Label();
            this.nudSeriesDays = new System.Windows.Forms.NumericUpDown();
            this.cbSeriesRule = new System.Windows.Forms.ComboBox();
            this.lblSeriesRecording = new System.Windows.Forms.Label();
            this.lblRecordingCount = new System.Windows.Forms.Label();
            this.lblNumberHelp = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.gbSeries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeriesDays)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(358, 68);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(334, 47);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Recording Preview";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIntroduction
            // 
            this.lblIntroduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntroduction.Location = new System.Drawing.Point(12, 91);
            this.lblIntroduction.Name = "lblIntroduction";
            this.lblIntroduction.Size = new System.Drawing.Size(334, 28);
            this.lblIntroduction.TabIndex = 3;
            this.lblIntroduction.Text = "Add/Remove recording to/from OTR using following data:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 132);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Title:";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(90, 129);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(256, 20);
            this.tbTitle.TabIndex = 5;
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(90, 184);
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(256, 20);
            this.tbStation.TabIndex = 7;
            // 
            // lblStation
            // 
            this.lblStation.AutoSize = true;
            this.lblStation.Location = new System.Drawing.Point(12, 187);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(43, 13);
            this.lblStation.TabIndex = 6;
            this.lblStation.Text = "Station:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(90, 210);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(256, 20);
            this.dtpStartDate.TabIndex = 8;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(12, 216);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 9;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(12, 242);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(58, 13);
            this.lblStartTime.TabIndex = 11;
            this.lblStartTime.Text = "Start Time:";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(90, 236);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(62, 20);
            this.dtpStartTime.TabIndex = 10;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(206, 242);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(55, 13);
            this.lblEndTime.TabIndex = 13;
            this.lblEndTime.Text = "End Time:";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(284, 236);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(62, 20);
            this.dtpEndTime.TabIndex = 12;
            // 
            // tbGenre
            // 
            this.tbGenre.Location = new System.Drawing.Point(90, 262);
            this.tbGenre.Name = "tbGenre";
            this.tbGenre.Size = new System.Drawing.Size(256, 20);
            this.tbGenre.TabIndex = 15;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(12, 265);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(39, 13);
            this.lblGenre.TabIndex = 14;
            this.lblGenre.Text = "Genre:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(190, 394);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(271, 394);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbSeries
            // 
            this.gbSeries.Controls.Add(this.lblSeriesTime);
            this.gbSeries.Controls.Add(this.lblSeriesDays);
            this.gbSeries.Controls.Add(this.nudSeriesDays);
            this.gbSeries.Controls.Add(this.cbSeriesRule);
            this.gbSeries.Controls.Add(this.lblSeriesRecording);
            this.gbSeries.Location = new System.Drawing.Point(12, 298);
            this.gbSeries.Name = "gbSeries";
            this.gbSeries.Size = new System.Drawing.Size(334, 76);
            this.gbSeries.TabIndex = 18;
            this.gbSeries.TabStop = false;
            this.gbSeries.Text = "Series Recording";
            // 
            // lblSeriesTime
            // 
            this.lblSeriesTime.AutoSize = true;
            this.lblSeriesTime.Location = new System.Drawing.Point(6, 48);
            this.lblSeriesTime.Name = "lblSeriesTime";
            this.lblSeriesTime.Size = new System.Drawing.Size(84, 13);
            this.lblSeriesTime.TabIndex = 4;
            this.lblSeriesTime.Text = "to the same time";
            // 
            // lblSeriesDays
            // 
            this.lblSeriesDays.AutoSize = true;
            this.lblSeriesDays.Location = new System.Drawing.Point(238, 22);
            this.lblSeriesDays.Name = "lblSeriesDays";
            this.lblSeriesDays.Size = new System.Drawing.Size(29, 13);
            this.lblSeriesDays.TabIndex = 3;
            this.lblSeriesDays.Text = "days";
            // 
            // nudSeriesDays
            // 
            this.nudSeriesDays.Location = new System.Drawing.Point(187, 19);
            this.nudSeriesDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudSeriesDays.Name = "nudSeriesDays";
            this.nudSeriesDays.Size = new System.Drawing.Size(45, 20);
            this.nudSeriesDays.TabIndex = 2;
            this.nudSeriesDays.ValueChanged += new System.EventHandler(this.cbSeriesRule_SelectedIndexChanged);
            // 
            // cbSeriesRule
            // 
            this.cbSeriesRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSeriesRule.FormattingEnabled = true;
            this.cbSeriesRule.Location = new System.Drawing.Point(96, 45);
            this.cbSeriesRule.Name = "cbSeriesRule";
            this.cbSeriesRule.Size = new System.Drawing.Size(171, 21);
            this.cbSeriesRule.TabIndex = 1;
            this.cbSeriesRule.SelectedIndexChanged += new System.EventHandler(this.cbSeriesRule_SelectedIndexChanged);
            // 
            // lblSeriesRecording
            // 
            this.lblSeriesRecording.AutoSize = true;
            this.lblSeriesRecording.Location = new System.Drawing.Point(6, 22);
            this.lblSeriesRecording.Name = "lblSeriesRecording";
            this.lblSeriesRecording.Size = new System.Drawing.Size(176, 13);
            this.lblSeriesRecording.TabIndex = 0;
            this.lblSeriesRecording.Text = "Remove recordings for the following";
            // 
            // lblRecordingCount
            // 
            this.lblRecordingCount.AutoSize = true;
            this.lblRecordingCount.Location = new System.Drawing.Point(12, 399);
            this.lblRecordingCount.Name = "lblRecordingCount";
            this.lblRecordingCount.Size = new System.Drawing.Size(149, 13);
            this.lblRecordingCount.TabIndex = 19;
            this.lblRecordingCount.Text = "2 recordings will be processed";
            // 
            // lblNumberHelp
            // 
            this.lblNumberHelp.Location = new System.Drawing.Point(87, 152);
            this.lblNumberHelp.Name = "lblNumberHelp";
            this.lblNumberHelp.Size = new System.Drawing.Size(259, 29);
            this.lblNumberHelp.TabIndex = 20;
            this.lblNumberHelp.Text = "Numbers enclosed with { } are incremented with each series recording.";
            // 
            // FrmRecordingPreview
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(358, 431);
            this.Controls.Add(this.lblNumberHelp);
            this.Controls.Add(this.lblRecordingCount);
            this.Controls.Add(this.gbSeries);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbGenre);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.tbStation);
            this.Controls.Add(this.lblStation);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblIntroduction);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmRecordingPreview";
            this.Text = "Crazysoft OTR Remote";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmRecordingPreview_Paint);
            this.pnlHeader.ResumeLayout(false);
            this.gbSeries.ResumeLayout(false);
            this.gbSeries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeriesDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblIntroduction;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbStation;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.TextBox tbGenre;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbSeries;
        private System.Windows.Forms.Label lblSeriesRecording;
        private System.Windows.Forms.ComboBox cbSeriesRule;
        private System.Windows.Forms.NumericUpDown nudSeriesDays;
        private System.Windows.Forms.Label lblSeriesDays;
        private System.Windows.Forms.Label lblSeriesTime;
        private System.Windows.Forms.Label lblRecordingCount;
        private System.Windows.Forms.Label lblNumberHelp;
    }
}