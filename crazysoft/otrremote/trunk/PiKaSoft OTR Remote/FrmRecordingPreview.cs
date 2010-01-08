using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmRecordingPreview : Form
    {
        public RecordingInfo[] RecordingInfo { get; private set; }

        public FrmRecordingPreview(RecordingInfo recInfo)
        {
            this.RecordingInfo = new RecordingInfo[] { recInfo };

            InitializeComponent();
            Program.TranslateControls(this);

            string mode = recInfo.RemoveMode ? Lang.OTRRemote.FrmRecordingPreview_RemoveText : Lang.OTRRemote.FrmRecordingPreview_AddText;
            string toFrom = recInfo.RemoveMode ? Lang.OTRRemote.FrmRecordingPreview_FromText : Lang.OTRRemote.FrmRecordingPreview_ToText;
            lblIntroduction.Text = String.Format(lblIntroduction.Text, mode, toFrom);
            lblSeriesRecording.Text = String.Format(lblSeriesRecording.Text, mode);

            tbTitle.Text = recInfo.Title;
            tbStation.Text = recInfo.Station;
            dtpStartDate.Value = recInfo.StartDate;
            dtpStartTime.Value = recInfo.StartTime;
            dtpEndTime.Value = recInfo.EndTime;
            tbGenre.Text = recInfo.Genre;

            cbSeriesRule.Items.Clear();
            cbSeriesRule.Items.Add(Lang.OTRRemote.FrmRecordingPreview_cbSeriesRule_Time);
            cbSeriesRule.Items.Add(String.Format(Lang.OTRRemote.FrmRecordingPreview_cbSeriesRule_TimeAndWeekDay, dtpStartDate.Value.ToString("dddd")));
            cbSeriesRule.Items.Add(Lang.OTRRemote.FrmRecordingPreview_cbSeriesRule_TimeAndWorkDay);
            cbSeriesRule.Items.Add(Lang.OTRRemote.FrmRecordingPreview_cbSeriesRule_TimeAndWeekend);
            cbSeriesRule.SelectedIndex = 0;
        }

        private void FrmRecordingPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbSeriesRule.Items.Count > 2)
            {
                cbSeriesRule.Items[1] = String.Format(Lang.OTRRemote.FrmRecordingPreview_cbSeriesRule_TimeAndWeekDay, dtpStartDate.Value.ToString("dddd"));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RecordingInfo recInfo = this.RecordingInfo[0];
            recInfo.Title = CountInTitle(tbTitle.Text, 0, 0);
            recInfo.Station = tbStation.Text;
            recInfo.StartDate = dtpStartDate.Value;
            recInfo.StartTime = dtpStartTime.Value;
            recInfo.EndTime = dtpEndTime.Value;
            recInfo.Genre = tbGenre.Text;

            // Add RecordingInfos for the chosen series rule
            int recCount = 1;
            RecordingInfo[] recInfos = new RecordingInfo[] { recInfo };
            if (nudSeriesDays.Value > 0)
            {
                for (int i = 1; i <= nudSeriesDays.Value; i++)
                {
                    DateTime curDate = dtpStartDate.Value.AddDays(i);
                    bool addDay = IsDateInRule(curDate);

                    if (addDay)
                    {
                        RecordingInfo ri = new RecordingInfo(recInfo);
                        ri.Title = CountInTitle(tbTitle.Text, 0, recCount);
                        ri.Station = tbStation.Text;
                        ri.StartDate = curDate;
                        ri.StartTime = dtpStartTime.Value;
                        ri.EndTime = dtpEndTime.Value;
                        ri.Genre = tbGenre.Text;
                        recCount++;

                        RecordingInfo[] tmp = new RecordingInfo[recCount];
                        Array.Copy(recInfos, tmp, recInfos.Length);
                        recInfos = tmp;
                        recInfos[recCount - 1] = ri;
                    }
                }
            }

            this.RecordingInfo = recInfos;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void cbSeriesRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dateCount = 1;
            for (int i = 1; i <= nudSeriesDays.Value; i++)
            {
                DateTime curDate = dtpStartDate.Value.AddDays(i);
                bool addDay = IsDateInRule(curDate);

                if (addDay)
                {
                    dateCount++;
                }
            }

            if (dateCount == 1)
            {
                lblRecordingCount.Text = Lang.OTRRemote.FrmRecordingPreview_lblRecordingCount;
            }
            else
            {
                lblRecordingCount.Text = String.Format(Lang.OTRRemote.FrmRecordingPreview_lblRecordingCount_Multiple, dateCount);
            }
        }

        private string CountInTitle(string title, int startPos, int numToAdd)
        {
            int first = title.IndexOf('{', startPos);
            int last = title.IndexOf('}', startPos);
            if (first >= 0 && last > first)
            {
                try
                {
                    int length = last - 1 - first;
                    int num = Int32.Parse(title.Substring(first + 1, length));
                    num += numToAdd;
                    title = title.Remove(first, last - first + 1);
                    title = title.Insert(first, num.ToString(String.Concat("D", length)));
                }
                catch (FormatException) { }

                if (last < title.Length)
                {
                    title = CountInTitle(title, last, numToAdd);
                }
            }

            return title;
        }

        private bool IsDateInRule(DateTime date)
        {
            bool addDay = false;
            switch (cbSeriesRule.SelectedIndex)
            {
                case 0: // Everyday
                    addDay = true;
                    break;
                case 1: // specific weekday
                    if (date.DayOfWeek == dtpStartDate.Value.DayOfWeek)
                    {
                        addDay = true;
                    }
                    break;
                case 2: // on workdays

                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        addDay = true;
                    }
                    break;
                case 3: // on weekends
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        addDay = true;
                    }
                    break;
            }

            return addDay;
        }
    }
}
