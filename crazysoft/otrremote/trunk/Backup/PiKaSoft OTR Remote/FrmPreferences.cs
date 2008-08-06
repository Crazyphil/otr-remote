using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Crazysoft.OTR_Remote
{
    public partial class FrmPreferences : Form
    {
        public FrmPreferences()
        {
            InitializeComponent();

            this.Text = Lang.OTRRemote.FrmPreferences_Title;

            // Tabs
            tcTabs.TabPages[0].Text = Lang.OTRRemote.FrmPreferences_Tab_Program;
            tcTabs.TabPages[1].Text = Lang.OTRRemote.FrmPreferences_Tab_Network;
            tcTabs.TabPages[2].Text = Lang.OTRRemote.FrmPreferences_Tab_About;

            // Program
            cbSilent.Text = Lang.OTRRemote.FrmPreferences_cbSilent;
            cbAutoClose.Text = Lang.OTRRemote.FrmPreferences_cbAutoClose;
            btnEditStations.Text = Lang.OTRRemote.FrmPreferences_btnEditStations;
            cbAdjustStartTime.Text = Lang.OTRRemote.FrmPreferences_cbAdjustStartTime;
            cbRecordFollowing.Text = Lang.OTRRemote.FrmPreferences_cbRecordFollowing;
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
            lblCopyright.Text = String.Format(Lang.OTRRemote.FrmPreferences_lblCopyright, "© Crazysoft 2006-2007");
            lblThanks.Text = Lang.OTRRemote.FrmPreferences_lblThanks;

            // Buttons
            llblUpdate.Text = Lang.OTRRemote.FrmPreferences_llblUpdate;
            llblUpdate.LinkArea = new LinkArea(0, llblUpdate.Text.Length);
            btnOK.Text = Lang.OTRRemote.FrmPreferences_btnOK;
            btnCancel.Text = Lang.OTRRemote.FrmPreferences_btnCancel;
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
                    lblLastUpdate.Text = String.Format(lblLastUpdate.Text, "-");
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

            if (Program.Settings.Sections["Program"].Keys.Count > 0)
            {
                // Program settings
                if (Program.Settings.Sections["Program"].Keys.Contains("SilentMode"))
                {
                    cbSilent.Checked = !Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["SilentMode"].Value);
                }
                pnlAutoClose.Enabled = cbSilent.Checked;
                if (Program.Settings.Sections["Program"].Keys.Contains("AutoClose"))
                {
                    cbAutoClose.Checked = Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["AutoClose"].Value);
                }
                if (Program.Settings.Sections["Program"].Keys.Contains("AutoCloseTimeout"))
                {
                    tbCloseSeconds.Value = Convert.ToInt32(Program.Settings.Sections["Program"].Keys["AutoCloseTimeout"].Value);
                }
                if (Program.Settings.Sections["Program"].Keys.Contains("AdjustStartTime"))
                {
                    cbAdjustStartTime.Checked = Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["AdjustStartTime"].Value);
                }
                if (Program.Settings.Sections["Program"].Keys.Contains("RecordFollowing"))
                {
                    cbRecordFollowing.Checked = Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["RecordFollowing"].Value);
                }
                if (Program.Settings.Sections["Program"].Keys.Contains("EnableAutoUpdate"))
                {
                    cbAutoUpdate.Checked = Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["EnableAutoUpdate"].Value);
                }
                if (Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    if (Program.Settings.Sections["Program"].Keys.Contains("LastAutoUpdate"))
                    {
                        DateTime lastUpdate =
                            new DateTime(
                                Convert.ToInt64(Program.Settings.Sections["Program"].Keys["LastAutoUpdate"].Value));
                        if (lastUpdate.Ticks == 0)
                        {
                            lblLastUpdate.Text = String.Format(lblLastUpdate.Text, "-");
                        }
                        else
                        {
                            lblLastUpdate.Text =
                                String.Format(lblLastUpdate.Text,
                                              lastUpdate.ToShortDateString() + " " + lastUpdate.ToShortTimeString());
                        }
                        lblLastUpdate.Tag = lastUpdate.Ticks;
                    }
                    else
                    {
                        lblLastUpdate.Text = String.Format(lblLastUpdate.Text, "-");
                        lblLastUpdate.Tag = 0;
                    }
                }
            }

            if (Program.Settings.Sections["Network"].Keys.Count > 0)
            {
                // Network settings
                if (Program.Settings.Sections["Network"].Keys.Contains("ProxyType"))
                {
                    if (Convert.ToInt32(Program.Settings.Sections["Network"].Keys["ProxyType"].Value) > 0)
                    {
                        cbProxy.Checked = true;

                        if (Convert.ToInt32(Program.Settings.Sections["Network"].Keys["ProxyType"].Value) > 1)
                        {
                            rbCustomProxy.Checked = true;
                        }
                        else
                        {
                            rbDefaultProxy.Checked = true;
                        }

                        if (Program.Settings.Sections["Network"].Keys.Contains("ProxyAuthRequired"))
                        {
                            cbProxyAuthentication.Checked = Convert.ToBoolean(Program.Settings.Sections["Network"].Keys["ProxyAuthRequired"].Value);
                        }
                    }
                    else
                    {
                        cbProxy.Checked = false;
                    }
                }
            }

            if (Program.Settings.Sections["Network"].Keys.Contains("ProxyAddress"))
            {
                tbProxyAddress.Text = Program.Settings.Sections["Network"].Keys["ProxyAddress"].Value.ToString();
            }
            if (Program.Settings.Sections["Network"].Keys.Contains("ProxyPort"))
            {
                tbProxyPort.Text = Program.Settings.Sections["Network"].Keys["ProxyPort"].Value.ToString();
            }

            if (Program.Settings.Sections["Network"].Keys.Contains("ProxyUser"))
            {
                tbProxyUser.Text = Program.Settings.Sections["Network"].Keys["ProxyUser"].Value.ToString();
            }
            if (Program.Settings.Sections["Network"].Keys.Contains("ProxyPassword") && Program.Settings.Sections["Network"].Keys["ProxyPassword"].Value.ToString() != "")
            {
                tbProxyPassword.Text = Encryption.Encryption.Decrypt(Program.Settings.Sections["Network"].Keys["ProxyPassword"].Value.ToString(), tbProxyUser.Text);
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
                if (tbProxyAddress.Text == "")
                {
                    MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_ProxyAddress_Text, Lang.OTRRemote.FrmPreferences_Error_ProxyAddress_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbProxyAddress.Focus();
                    return;
                }
                else if (tbProxyPort.Text == "")
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

                if (cbProxyAuthentication.Checked && tbProxyUser.Text == "")
                {
                    MessageBox.Show(Lang.OTRRemote.FrmPreferences_Error_ProxyUser_Text, Lang.OTRRemote.FrmPreferences_Error_ProxyUser_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbProxyUser.Focus();
                    return;
                }
            }

            // Program settings
            if (!Program.Settings.Sections.Contains("Program"))
            {
                Program.Settings.Sections.Add("Program");
            }
            Program.Settings.Sections["Program"].Keys.Add("SilentMode", !cbSilent.Checked);
            Program.Settings.Sections["Program"].Keys.Add("AutoClose", cbAutoClose.Checked);
            Program.Settings.Sections["Program"].Keys.Add("AutoCloseTimeout", Convert.ToInt32(tbCloseSeconds.Value));
            Program.Settings.Sections["Program"].Keys.Add("AdjustStartTime", cbAdjustStartTime.Checked);
            Program.Settings.Sections["Program"].Keys.Add("RecordFollowing", cbRecordFollowing.Checked);
            Program.Settings.Sections["Program"].Keys.Add("EnableAutoUpdate", cbAutoUpdate.Checked);
            Program.Settings.Sections["Program"].Keys.Add("LastAutoUpdate", lblLastUpdate.Tag);

            // Network settings
            if (!Program.Settings.Sections.Contains("Network"))
            {
                Program.Settings.Sections.Add("Network");
            }
            if (cbProxy.Checked)
            {
                if (rbDefaultProxy.Checked)
                {
                    Program.Settings.Sections["Network"].Keys.Add("ProxyType", 1);
                }
                else
                {
                    Program.Settings.Sections["Network"].Keys.Add("ProxyType", 2);
                }
            }
            else
            {
                Program.Settings.Sections["Network"].Keys.Add("ProxyType", 0);
            }

            Program.Settings.Sections["Network"].Keys.Add("ProxyAddress", tbProxyAddress.Text);
            Program.Settings.Sections["Network"].Keys.Add("ProxyPort", Convert.ToInt32(tbProxyPort.Text));
            Program.Settings.Sections["Network"].Keys.Add("ProxyAuthRequired", cbProxyAuthentication.Checked);
            Program.Settings.Sections["Network"].Keys.Add("ProxyUser", tbProxyUser.Text);

            if (tbProxyPassword.Text != "")
            {
                Program.Settings.Sections["Network"].Keys.Add("ProxyPassword", Encryption.Encryption.Encrypt(tbProxyPassword.Text, tbProxyUser.Text));
            }
            else
            {
                Program.Settings.Sections["Network"].Keys.Add("ProxyPassword", "");
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

        private void cbSilent_CheckedChanged(object sender, EventArgs e)
        {
            pnlAutoClose.Enabled = cbSilent.Checked;
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
                    ProcessStartInfo updateApp = new ProcessStartInfo(rk.GetValue("InstallationPath").ToString() + "\\AppUpdate.exe");
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
            catch
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