using System;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmStations : Form
    {
        public FrmStations()
        {
            // Set Form's font to Segoe UI if supported
            Graphics.SetVistaProperties(this);

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
            ctl.Text = Lang.OTRRemote.ResourceManager.GetString(String.Concat("FrmStations_", ctl.Name));
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
                if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "stations.xml")))
                {
                    System.IO.FileStream writer = System.IO.File.OpenWrite(System.IO.Path.Combine(Application.StartupPath, "stations.xml"));
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
            if (!Program.Settings["Stations"]["StationsCaseSensitive"].IsNull)
            {
                cbCaseSensitive.Checked = Convert.ToBoolean(Program.Settings["Stations"]["StationsCaseSensitive"].Value);
            }

            Stations stations = new Stations();
            stations.Load();

            foreach (Station station in stations)
            {
                lbOTRStation.Items.Add(station);
            }
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

                tbEPGStation.Text = String.Empty;

                tbOTRStation.Text = station.Name;
                btnOTRAdd.Text = Lang.OTRRemote.FrmStations_btnOTRChange;
                tbOTRStation.Focus();
            }
            else
            {
                tbOTRStation.Text = String.Empty;
                tbEPGStation.Text = String.Empty;
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
                tbEPGStation.Text = String.Empty;
            }
        }

        private void tbOTRStation_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbOTRStation.Text))
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
            if (String.IsNullOrEmpty(tbEPGStation.Text))
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
                tbOTRStation.Text = String.Empty;
                tbEPGStation.Focus();
            }
            else
            {
                Station station = (Station)lbOTRStation.SelectedItem;
                station.Name = tbOTRStation.Text;
                lbOTRStation.Items.Remove(lbOTRStation.SelectedItem);
                lbOTRStation.SelectedIndex = lbOTRStation.Items.Add(station);
                tbOTRStation.Text = String.Empty;
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
                tbEPGStation.Text = String.Empty;
                tbEPGStation.Focus();
            }
            else
            {
                string replacement = tbEPGStation.Text;
                Station station = (Station)lbOTRStation.SelectedItem;
                station.Replacements.Remove(lbEPGStation.SelectedItem.ToString());
                station.Replacements.Add(replacement);
                lbEPGStation.Items.Remove(lbEPGStation.SelectedItem);
                tbEPGStation.Text = String.Empty;
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
                MessageBox.Show(Lang.OTRRemote.FrmStations_SaveMsg_Text, Lang.OTRRemote.FrmStations_SaveMsg_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save settings
            Program.Settings.Sections.Add("Stations");
            Program.Settings["Stations"].Keys.Add("StationsCaseSensitive", cbCaseSensitive.Checked);
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
                tbEPGStation.Text = String.Empty;
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
                tbEPGStation.Text = String.Empty;
                lbEPGStation.SelectedIndex = -1;
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                btnOTRAdd_Click(sender, e);
            }
        }
    }
}