using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Crazysoft.OTRRemote
{
    public partial class FrmPreferences : Form
    {
        public FrmPreferences()
        {
            // Set Form's font to Segoe UI if supported
            Graphics.SetVistaProperties(this);

            InitializeComponent();

            this.Text = Lang.OTRRemote.FrmPreferences_Title;

            // Tabs
            tcTabs.TabPages[0].Text = Lang.OTRRemote.FrmPreferences_Tab_Program;
            tcTabs.TabPages[1].Text = Lang.OTRRemote.FrmPreferences_Tab_Network;
            tcTabs.TabPages[2].Text = Lang.OTRRemote.FrmPreferences_Tab_About;

            // Program
            lblProgressIndicator.Text = Lang.OTRRemote.FrmPreferences_lblProgressIndicator;
            cbAutoClose.Text = Lang.OTRRemote.FrmPreferences_cbAutoClose;
            lblSeconds.Text = Lang.OTRRemote.FrmPreferences_lblSeconds;
            btnEditStations.Text = Lang.OTRRemote.FrmPreferences_btnEditStations;
            cbAdjustStartTime.Text = Lang.OTRRemote.FrmPreferences_cbAdjustStartTime;
            cbRecordFollowing.Text = Lang.OTRRemote.FrmPreferences_cbRecordFollowing;
            cbRetryDelete.Text = Lang.OTRRemote.FrmPreferences_cbRetryDelete;
            cbAutoUpdate.Text = Lang.OTRRemote.FrmPreferences_cbAutoUpdate;
            lblLastUpdate.Text = Lang.OTRRemote.FrmPreferences_lblLastUpdate;

            // Network
            cbProxy.Text = Lang.OTRRemote.FrmPreferences_cbProxy;
            rbDefaultProxy.Text = Lang.OTRRemote.FrmPreferences_rbDefaultProxy;
            rbCustomProxy.Text = Lang.OTRRemote.FrmPreferences_rbCustomProxy;
            lblProxyAddress.Text = Lang.OTRRemote.FrmPreferences_lblProxyAddress;
            lblProxyPort.Text = Lang.OTRRemote.FrmPreferences_lblProxyPort;
            cbProxyAuthentication.Text = Lang.OTRRemote.FrmPreferences_cbProxyAuthenticationIE;
            lblProxyUser.Text = Lang.OTRRemote.FrmPreferences_lblProxyUser;
            lblProxyPassword.Text = Lang.OTRRemote.FrmPreferences_lblProxyPassword;

            // About
            lblProductName.Text = Application.CompanyName + " " + Application.ProductName;
            lblVersion.Text = String.Format(Lang.OTRRemote.FrmPreferences_lblVersion, Application.ProductVersion);
            lblCopyright.Text = String.Format(Lang.OTRRemote.FrmPreferences_lblCopyright, "© Crazysoft 2006-2008");
            lblThanks.Text = Lang.OTRRemote.FrmPreferences_lblThanks;

            // Buttons
            llblUpdate.Text = Lang.OTRRemote.FrmPreferences_llblUpdate;
            llblUpdate.LinkArea = new LinkArea(0, llblUpdate.Text.Length);
            btnOK.Text = Lang.OTRRemote.FrmPreferences_btnOK;
            btnCancel.Text = Lang.OTRRemote.FrmPreferences_btnCancel;

            // Progress Methods
            cbProgressMethod.Items.Clear();
            cbProgressMethod.Items.Add(Lang.OTRRemote.FrmPreferences_cbProgressMethod_ShowWindow);
            
            // Because of a bug in Mono, systray icon and hidden mode must not be used
            if (Type.GetType("Mono.Runtime") == null)
            {
                cbProgressMethod.Items.Add(Lang.OTRRemote.FrmPreferences_cbProgressMethod_ShowSystrayIcon);
                cbProgressMethod.Items.Add(Lang.OTRRemote.FrmPreferences_cbProgressMethod_Hide);
            }

            cbProgressMethod.SelectedIndex = 0;
        }

        private void FrmPreferences_Load(object sender, EventArgs e)
        {
            // Only enable automatic updates when running on Windows
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                // Look for installation of Crazysoft AppUpdate
                RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Crazysoft\\AppUpdate");
                if (rk == null || (rk.GetValue("InstallationPath", null) == null))
                {
                    llblUpdate.Enabled = false;
                    cbAutoUpdate.Checked = false;
                    cbAutoUpdate.Enabled = false;
                    lblLastUpdate.Text = String.Format(lblLastUpdate.Text, Lang.OTRRemote.FrmPreferences_lblLastUpdate_Never);
                    lblLastUpdate.Tag = 0;
                    lblLastUpdate.Enabled = false;
                }
            }
            else
            {
                // Hide any Auto-Update controls
                llblUpdate.Visible = false;
                cbAutoUpdate.Checked = false;
                cbAutoUpdate.Visible = false;
                lblLastUpdate.Tag = 0;
                lblLastUpdate.Visible = false;

                // Replace "IE default proxy" with "System default proxy"
                rbDefaultProxy.Text = rbDefaultProxy.Text.Replace("IE", "System");
            }

            if (Program.Settings["Program"].Keys.Count > 0)
            {
                // Program settings
                /**
                 * COMPAT: The SilentMode property was for indicating if the progress window should be shown
                 * This setting was used in program versions < 1.0.8.0
                 */
                if (Program.Settings["Program"].Keys.Contains("SilentMode"))
                {
                    cbProgressMethod.SelectedIndex = Convert.ToBoolean(Program.Settings["Program"]["SilentMode"].Value) ? 2 : 0;
                    Program.Settings["Program"].Keys.Remove("SilentMode");
                }
                else if (!Program.Settings["Program"]["ProgressIndicator"].IsNull)
                {
                    cbProgressMethod.SelectedIndex = Convert.ToInt32(Program.Settings["Program"]["ProgressIndicator"].Value);
                }
                pnlAutoClose.Enabled = cbProgressMethod.SelectedIndex < 2;
                if (!Program.Settings["Program"]["AutoClose"].IsNull)
                {
                    cbAutoClose.Checked = Convert.ToBoolean(Program.Settings["Program"]["AutoClose"].Value);
                }
                if (!Program.Settings["Program"]["AutoCloseTimeout"].IsNull)
                {
                    tbCloseSeconds.Value = Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value);
                }
                if (!Program.Settings["Program"]["AdjustStartTime"].IsNull)
                {
                    cbAdjustStartTime.Checked = Convert.ToBoolean(Program.Settings["Program"]["AdjustStartTime"].Value);
                }
                if (!Program.Settings["Program"]["RecordFollowing"].IsNull)
                {
                    cbRecordFollowing.Checked = Convert.ToBoolean(Program.Settings["Program"]["RecordFollowing"].Value);
                }
                if (!Program.Settings["Program"]["RetryDelete"].IsNull)
                {
                    cbRetryDelete.Checked = Convert.ToBoolean(Program.Settings["Program"]["RetryDelete"].Value);
                }
                if (!Program.Settings["Program"]["EnableAutoUpdate"].IsNull)
                {
                    cbAutoUpdate.Checked = Convert.ToBoolean(Program.Settings["Program"]["EnableAutoUpdate"].Value);
                }
                if (Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    if (!Program.Settings["Program"]["LastAutoUpdate"].IsNull)
                    {
                        DateTime lastUpdate =
                            new DateTime(
                                Convert.ToInt64(Program.Settings["Program"]["LastAutoUpdate"].Value));
                        if (lastUpdate.Ticks == 0)
                        {
                            lblLastUpdate.Text = String.Format(lblLastUpdate.Text, Lang.OTRRemote.FrmPreferences_lblLastUpdate_Never);
                        }
                        else
                        {
                            lblLastUpdate.Text =
                                String.Format(lblLastUpdate.Text, String.Concat(lastUpdate.ToShortDateString(), " ", lastUpdate.ToShortTimeString()));
                        }
                        lblLastUpdate.Tag = lastUpdate.Ticks;
                    }
                    else
                    {
                        lblLastUpdate.Text = String.Format(lblLastUpdate.Text, Lang.OTRRemote.FrmPreferences_lblLastUpdate_Never);
                        lblLastUpdate.Tag = 0;
                    }
                }
            }
            else
            {
                lblLastUpdate.Text = String.Format(lblLastUpdate.Text, Lang.OTRRemote.FrmPreferences_lblLastUpdate_Never);
            }

            if (Program.Settings["Network"].Keys.Count > 0)
            {
                // Network settings
                if (!Program.Settings["Network"]["ProxyType"].IsNull)
                {
                    if (Convert.ToInt32(Program.Settings["Network"]["ProxyType"].Value) > 0)
                    {
                        cbProxy.Checked = true;

                        if (Convert.ToInt32(Program.Settings["Network"]["ProxyType"].Value) > 1)
                        {
                            rbCustomProxy.Checked = true;
                        }
                        else
                        {
                            rbDefaultProxy.Checked = true;
                        }

                        if (!Program.Settings["Network"]["ProxyAuthRequired"].IsNull)
                        {
                            cbProxyAuthentication.Checked = Convert.ToBoolean(Program.Settings["Network"]["ProxyAuthRequired"].Value);
                        }
                    }
                    else
                    {
                        cbProxy.Checked = false;
                    }
                }
                if (!Program.Settings["Network"]["ProxyAddress"].IsNull)
                {
                    tbProxyAddress.Text = Program.Settings["Network"]["ProxyAddress"].Value.ToString();
                }
                if (!Program.Settings["Network"]["ProxyPort"].IsNull)
                {
                    tbProxyPort.Text = Program.Settings["Network"]["ProxyPort"].Value.ToString();
                }

                if (!Program.Settings["Network"]["ProxyUser"].IsNull)
                {
                    tbProxyUser.Text = Program.Settings["Network"]["ProxyUser"].Value.ToString();
                }
                if (!Program.Settings["Network"]["ProxyPassword"].IsNull && !String.IsNullOrEmpty(Program.Settings["Network"]["ProxyPassword"].Value.ToString()))
                {
                    tbProxyPassword.Text = Encryption.Encryption.Decrypt(Program.Settings["Network"]["ProxyPassword"].Value.ToString(), tbProxyUser.Text);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbProxy.Checked && rbCustomProxy.Checked)
            {
                if (String.IsNullOrEmpty(tbProxyAddress.Text))
                {
                    MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_ProxyAddress_Text, Lang.OTRRemote.FrmPreferences_Error_ProxyAddress_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbProxyAddress.Focus();
                    return;
                }
                else if (String.IsNullOrEmpty(tbProxyPort.Text))
                {
                    MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_ProxyPort_Text, Lang.OTRRemote.FrmPreferences_Error_ProxyPort_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbProxyPort.Focus();
                    return;
                }
                else
                {
                    UInt16 result;
                    if (!UInt16.TryParse(tbProxyPort.Text, out result))
                    {
                        MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_PortWrong_Text, Lang.OTRRemote.FrmPreferences_Error_PortWrong_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbProxyPort.Focus();
                        return;
                    }
                }

                if (cbProxyAuthentication.Checked && String.IsNullOrEmpty(tbProxyUser.Text))
                {
                    MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_ProxyUser_Text, Lang.OTRRemote.FrmPreferences_Error_ProxyUser_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbProxyUser.Focus();
                    return;
                }
            }

            // Program settings
            Program.Settings.Sections.Add("Program");
            Program.Settings["Program"].Keys.Add("ProgressIndicator", cbProgressMethod.SelectedIndex);
            Program.Settings["Program"].Keys.Add("AutoClose", cbAutoClose.Checked);
            Program.Settings["Program"].Keys.Add("AutoCloseTimeout", Convert.ToInt32(tbCloseSeconds.Value));
            Program.Settings["Program"].Keys.Add("AdjustStartTime", cbAdjustStartTime.Checked);
            Program.Settings["Program"].Keys.Add("RecordFollowing", cbRecordFollowing.Checked);
            Program.Settings["Program"].Keys.Add("RetryDelete", cbRetryDelete.Checked);
            Program.Settings["Program"].Keys.Add("EnableAutoUpdate", cbAutoUpdate.Checked);
            Program.Settings["Program"].Keys.Add("LastAutoUpdate", lblLastUpdate.Tag);

            // Network settings
            Program.Settings.Sections.Add("Network");
            if (cbProxy.Checked)
            {
                if (rbDefaultProxy.Checked)
                {
                    Program.Settings["Network"].Keys.Add("ProxyType", 1);
                }
                else
                {
                    Program.Settings["Network"].Keys.Add("ProxyType", 2);
                }
            }
            else
            {
                Program.Settings["Network"].Keys.Add("ProxyType", 0);
            }

            Program.Settings["Network"].Keys.Add("ProxyAddress", tbProxyAddress.Text);
            Program.Settings["Network"].Keys.Add("ProxyPort", Convert.ToInt32(tbProxyPort.Text));
            Program.Settings["Network"].Keys.Add("ProxyAuthRequired", cbProxyAuthentication.Checked);
            Program.Settings["Network"].Keys.Add("ProxyUser", tbProxyUser.Text);

            if (!String.IsNullOrEmpty(tbProxyPassword.Text))
            {
                Program.Settings["Network"].Keys.Add("ProxyPassword", Encryption.Encryption.Encrypt(tbProxyPassword.Text, tbProxyUser.Text));
            }
            else
            {
                Program.Settings["Network"].Keys.Add("ProxyPassword", String.Empty);
            }

            Program.Settings.Save();

            this.Close();
        }

        private void rbCustomProxy_CheckedChanged(object sender, EventArgs e)
        {
            pnlProxyData.Enabled = rbCustomProxy.Checked;

            if (rbCustomProxy.Checked)
            {
                cbProxyAuthentication.Text = Lang.OTRRemote.FrmPreferences_cbProxyAuthentication;
            }
            else
            {
                cbProxyAuthentication.Text = Lang.OTRRemote.FrmPreferences_cbProxyAuthenticationIE;
            }
        }

        private void cbProxyAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            pnlProxyAuthentication.Enabled = cbProxyAuthentication.Checked;
        }

        private void cbProxy_CheckedChanged(object sender, EventArgs e)
        {
            pnlProxyOptions.Enabled = cbProxy.Checked;
        }


        private void cbProgressMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAutoClose.Enabled = cbProgressMethod.SelectedIndex < 2;
        }

        private void llblUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Crazysoft\\AppUpdate");

            if (rk == null || (rk != null && rk.GetValue("InstallationPath", null) == null))
            {
                MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_AppUpdate_Text, Lang.OTRRemote.FrmPreferences_Error_AppUpdate_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(Lang.OTRRemote.FrmPreferences_Msg_AppUpdate_Text, Lang.OTRRemote.FrmPreferences_Msg_AppUpdate_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ProcessStartInfo updateApp = new ProcessStartInfo(System.IO.Path.Combine(rk.GetValue("InstallationPath").ToString(), "AppUpdate.exe"));
                    updateApp.Arguments = "Crazysoft.OTR_Remote";
                    updateApp.ErrorDialog = true;
                    updateApp.WorkingDirectory = rk.GetValue("InstallationPath").ToString();
                    Process.Start(updateApp);
                    Application.Exit();
                }
                catch (Exception excp)
                {
                    MessageBox.Show(String.Format(Lang.OTRRemote.FrmPreferences_Error_Update_Text, excp.Message), Lang.OTRRemote.FrmPreferences_Error_Update_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void llblHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ProcessStartInfo homepage = new ProcessStartInfo("http://www.crazysoft.net.ms/");
                homepage.ErrorDialog = true;
                Process.Start(homepage);
            }
            catch (Exception)
            {
            }
        }

        private void btnEditStations_Click(object sender, EventArgs e)
        {
            FrmStations FrmStations = new FrmStations();
            FrmStations.ShowDialog(this);
        }
    }
}