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

            Program.TranslateControls(this);
        }

        private void FrmHelp_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}