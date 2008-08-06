using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTR_Remote
{
    public partial class FrmLogin : Form
    {
        public string username = "";
        public string password = "";
        public int timezone = 0;

        public FrmLogin(string username, string password, int timezone)
        {
            this.username = username;
            this.password = password;
            this.timezone = timezone;

            InitializeComponent();

            this.Text = Lang.OTRRemote.FrmLogin_Title;
            lblHeader.Text = Lang.OTRRemote.FrmLogin_lblHeader;

            foreach (Control ctl in this.Controls)
            {
                ctl.Text = Lang.OTRRemote.ResourceManager.GetString("FrmLogin_" + ctl.Name);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            tbUsername.Text = username;
            tbPassword.Text = password;
            tbTimezone.Value = timezone;

            this.Top = Screen.GetWorkingArea(this).Bottom - this.Height;
            this.Left = Screen.GetWorkingArea(this).Right - this.Width;
        }

        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == "")
            {
                MessageBox.Show(Lang.OTRRemote.FrmLogin_Error_Username_Text, Lang.OTRRemote.FrmLogin_Error_Username_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tbPassword.Text == "")
            {
                MessageBox.Show(Lang.OTRRemote.FrmLogin_Error_Password_Text, Lang.OTRRemote.FrmLogin_Error_Passwort_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            username = tbUsername.Text;
            password = tbPassword.Text;
            timezone = Convert.ToInt32(tbTimezone.Value);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void FrmLogin_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }
    }
}