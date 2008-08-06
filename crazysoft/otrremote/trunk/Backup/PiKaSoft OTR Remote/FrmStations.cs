using System;
using System.Windows.Forms;

namespace Crazysoft.OTR_Remote
{
    public partial class FrmStations : Form
    {
        public FrmStations()
        {
            InitializeComponent();

            // Translate form
            this.Text = Lang.OTRRemote.FrmStations_Title;
            foreach (Control ctl in this.Controls)
            {
                TranslateControl(ctl);
            }
        }

        private void TranslateControl(Control ctl)
        {
            ctl.Text = Lang.OTRRemote.ResourceManager.GetString("FrmStations_" + ctl.Name);
            foreach (Control subCtl in ctl.Controls)
            {
                TranslateControl(subCtl);
            }
        }

        private void FrmStations_Load(object sender, EventArgs e)
        {
            // Check, if we have write access to the file
            try
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\stations.xml"))
                {
                    System.IO.FileStream writer = System.IO.File.OpenWrite(Application.StartupPath + "\\stations.xml");
                    writer.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    Lang.OTRRemote.FrmStations_AccessMsg_Text,
                    Lang.OTRRemote.FrmStations_AccessMsg_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = false;
            }

            // Load settings
            if (Program.Settings.Sections["Stations"].Keys["StationsCaseSensitive"] != null)
            {
                cbCaseSensitive.Checked = Convert.ToBoolean(Program.Settings.Sections["Stations"].Keys["StationsCaseSensitive"].Value);
            }

            Stations stations = new Stations();
            stations.Load();

            foreach (Station station in stations)
            {
                lbOTRStation.Items.Add(station);
            }

            /*if (lbOTRStation.Items.Count > 0)
            {
                lbOTRStation.SelectedIndex = 0;
            }*/
        }

        private void lbOTRStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbEPGStation.Items.Clear();

            if (lbOTRStation.SelectedIndex > -1)
            {
                Station station = (Station)lbOTRStation.SelectedItem;
                foreach (string replacement in station.Replacements)
                {
                    lbEPGStation.Items.Add(replacement);
                }

                /*if (lbEPGStation.Items.Count > 0)
                {
                    lbEPGStation.SelectedIndex = 0;
                }
                else
                {*/
                    tbEPGStation.Text = "";
                //}

                tbOTRStation.Text = station.Name;
                btnOTRAdd.Text = Lang.OTRRemote.FrmStations_btnOTRChange;
                tbOTRStation.Focus();
            }
            else
            {
                tbOTRStation.Text = "";
                tbEPGStation.Text = "";
            }
        }

        private void lbEPGStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbEPGStation.SelectedIndex > -1)
            {
                tbEPGStation.Text = lbEPGStation.SelectedItem.ToString();
                btnEPGAdd.Text = Lang.OTRRemote.FrmStations_btnEPGChange;
                tbEPGStation.Focus();
            }
            else
            {
                tbEPGStation.Text = "";
            }
        }

        private void tbOTRStation_TextChanged(object sender, EventArgs e)
        {
            if (tbOTRStation.Text == "")
            {
                btnOTRAdd.Text = Lang.OTRRemote.FrmStations_btnOTRAdd;
                btnOTRAdd.Enabled = false;
            }
            else
            {
                btnOTRAdd.Enabled = true;
                btnOTRRemove.Enabled = true;
            }
        }

        private void tbEPGStation_TextChanged(object sender, EventArgs e)
        {
            if (tbEPGStation.Text == "")
            {
                btnEPGAdd.Text = Lang.OTRRemote.FrmStations_btnEPGAdd;
                btnEPGAdd.Enabled = false;
            }
            else
            {
                btnEPGAdd.Enabled = true;
                btnEPGRemove.Enabled = true;
            }
        }

        private void btnOTRAdd_Click(object sender, EventArgs e)
        {
            if (btnOTRAdd.Text == Lang.OTRRemote.FrmStations_btnOTRAdd)
            {
                Station station = new Station(tbOTRStation.Text);
                lbOTRStation.SelectedIndex = lbOTRStation.Items.Add(station);
                tbOTRStation.Text = "";
                tbEPGStation.Focus();
            }
            else
            {
                Station station = (Station)lbOTRStation.SelectedItem;
                station.Name = tbOTRStation.Text;
                lbOTRStation.Items.Remove(lbOTRStation.SelectedItem);
                lbOTRStation.SelectedIndex = lbOTRStation.Items.Add(station);
                tbOTRStation.Text = "";
                tbEPGStation.Focus();
            }
        }

        private void btnOTRRemove_Click(object sender, EventArgs e)
        {
            int itemIndex = lbOTRStation.SelectedIndex;
            if (itemIndex > -1)
            {
                if (itemIndex == lbOTRStation.Items.Count - 1)
                {
                    itemIndex--;
                }
                lbOTRStation.Items.Remove(lbOTRStation.SelectedItem);
                lbOTRStation.SelectedIndex = itemIndex;
            }
        }

        private void btnEPGAdd_Click(object sender, EventArgs e)
        {
            if (btnEPGAdd.Text == Lang.OTRRemote.FrmStations_btnEPGAdd)
            {
                Station station = (Station)lbOTRStation.SelectedItem;
                station.Replacements.Add(tbEPGStation.Text);
                lbEPGStation.Items.Add(this.tbEPGStation.Text);
                tbEPGStation.Text = "";
                tbEPGStation.Focus();
            }
            else
            {
                string replacement = tbEPGStation.Text;
                Station station = (Station)lbOTRStation.SelectedItem;
                station.Replacements.Remove(lbEPGStation.SelectedItem.ToString());
                station.Replacements.Add(replacement);
                lbEPGStation.Items.Remove(lbEPGStation.SelectedItem);
                tbEPGStation.Text = "";
                tbEPGStation.Focus();
            }
        }

        private void btnEPGRemove_Click(object sender, EventArgs e)
        {
            int itemIndex = lbEPGStation.SelectedIndex;
            if (itemIndex > -1)
            {
                if (itemIndex == lbEPGStation.Items.Count - 1)
                {
                    itemIndex--;
                }
                lbEPGStation.Items.Remove(lbEPGStation.SelectedItem);
                lbEPGStation.SelectedIndex = itemIndex;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Stations stations = new Stations();
            foreach (Station station in lbOTRStation.Items)
            {
                stations.Add(station);
            }

            if (!stations.Save())
            {
                MessageBox.Show(
                    Lang.OTRRemote.FrmStations_SaveMsg_Text,
                    Lang.OTRRemote.FrmStations_SaveMsg_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save settings
            if (!Program.Settings.Sections.Contains("Stations"))
            {
                Program.Settings.Sections.Add("Stations");
                Program.Settings.Sections["Stations"].Keys.Add("StationsCaseSensitive", cbCaseSensitive.Checked);
            }
            else
            {
                Program.Settings.Sections["Stations"].Keys["StationsCaseSensitive"].Value = cbCaseSensitive.Checked;
            }
            Program.Settings.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbEPGStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                tbEPGStation.Text = "";
                lbEPGStation.SelectedIndex = -1;
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                btnEPGAdd_Click(sender, e);
            }
        }

        private void tbOTRStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                tbEPGStation.Text = "";
                lbEPGStation.SelectedIndex = -1;
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                btnOTRAdd_Click(sender, e);
            }
        }
    }
}