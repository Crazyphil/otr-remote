using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Resources;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmHowTo : Form
    {
        public FrmHowTo()
        {
            InitializeComponent();
            this.Text = Lang.Clickfinder.FrmHowTo_Title;
            lblDescription.Text = Lang.Clickfinder.FrmHowTo_lblDescription;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}