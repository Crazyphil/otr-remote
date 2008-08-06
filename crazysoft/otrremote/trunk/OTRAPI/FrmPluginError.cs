using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmPluginError : Form
    {
        Exception excp = new Exception();
        string plugin = "";

        public FrmPluginError(string pluginFile, Exception exception)
        {
            InitializeComponent();

            excp = exception;
            plugin = pluginFile;
        }

        private void FrmPluginError_Load(object sender, EventArgs e)
        {
            lblDescription.Text = String.Format(lblDescription.Text, plugin);
            tbDebugInfos.Text = "Exception: " + excp.Message + "\r\n\r\nSource: " + excp.Source + "\r\n\r\nStack Trace:\r\n" + excp.StackTrace;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}