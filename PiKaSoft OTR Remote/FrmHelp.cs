using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmHelp : Form
    {
        public FrmHelp()
        {
            InitializeComponent();

            foreach (Control ctl in this.Controls)
            {
                TranslateControl(ctl);
            }
        }

        private void FrmHelp_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TranslateControl(Control ctl)
        {
            ctl.Text = Lang.OTRRemote.ResourceManager.GetString(String.Concat("FrmHelp_", ctl.Name));
            foreach (Control subCtl in ctl.Controls)
            {
                TranslateControl(subCtl);
            }
        }
    }
}