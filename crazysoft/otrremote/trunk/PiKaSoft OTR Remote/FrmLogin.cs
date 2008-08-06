using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmLogin : Form
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Timezone { get; set; }

        public FrmLogin(string username, string password, int timezone)
        {
            this.Username = username;
            this.Password = password;
            this.Timezone = timezone;

            InitializeComponent();

            this.Text = Lang.OTRRemote.FrmLogin_Title;
            lblHeader.Text = Lang.OTRRemote.FrmLogin_lblHeader;

            foreach (Control ctl in this.Controls)
            {
                ctl.Text = Lang.OTRRemote.ResourceManager.GetString(String.Concat("FrmLogin_", ctl.Name));
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            tbUsername.Text = Username;
            tbPassword.Text = Password;
            tbTimezone.Value = Timezone;

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
            if (String.IsNullOrEmpty(tbUsername.Text))
            {
                MessageBox.Show(Lang.OTRRemote.FrmLogin_Error_Username_Text, Lang.OTRRemote.FrmLogin_Error_Username_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show(Lang.OTRRemote.FrmLogin_Error_Password_Text, Lang.OTRRemote.FrmLogin_Error_Passwort_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Username = tbUsername.Text;
            this.Password = tbPassword.Text;
            this.Timezone = Convert.ToInt32(tbTimezone.Value);

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