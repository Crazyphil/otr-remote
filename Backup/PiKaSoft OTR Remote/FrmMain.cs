using Microsoft.Win32;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Crazysoft.OTR_Remote
{
    public partial class FrmMain : Form
    {
        ArrayList pluginCollection = new ArrayList();

        public FrmMain()
        {
            InitializeComponent();

            lblHeader.Text = Lang.OTRRemote.FrmMain_lblHeader;
            lblIntroduction.Text = Lang.OTRRemote.FrmMain_lblIntroduction;

            gbUserdata.Text = Lang.OTRRemote.FrmMain_gbUserdata;
            lblUsername.Text = Lang.OTRRemote.FrmMain_lblUsername;
            lblPassword.Text = Lang.OTRRemote.FrmMain_lblPassword;

            gbRecording.Text = Lang.OTRRemote.FrmMain_gbRecording;
            lblTimezone.Text = Lang.OTRRemote.FrmMain_lblTimezone;
            lblTVGuide.Text = Lang.OTRRemote.FrmMain_lblTVGuide;

            gbTVGuide.Text = Lang.OTRRemote.FrmMain_gbTVGuide;
            lvSettings.Columns[0].Text = Lang.OTRRemote.FrmMain_lvSettings_Setting;
            lvSettings.Columns[1].Text = Lang.OTRRemote.FrmMain_lvSettings_Value;

            btnPreferences.Text = Lang.OTRRemote.FrmMain_btnPreferences;
            btnHelp.Text = Lang.OTRRemote.FrmMain_btnHelp;
            btnOK.Text = Lang.OTRRemote.FrmMain_btnOK;
            btnCancel.Text = Lang.OTRRemote.FrmMain_btnCancel;

            ttTooltip.ToolTipTitle = Lang.OTRRemote.FrmMain_Tooltip_Title;
            ttTooltip.SetToolTip(tbUsername, Lang.OTRRemote.FrmMain_Tooltip_Username);
            ttTooltip.SetToolTip(tbPassword, Lang.OTRRemote.FrmMain_Tooltip_Password);
            ttTooltip.SetToolTip(tbTimezone, Lang.OTRRemote.FrmMain_Tooltip_Timezone);
            ttTooltip.SetToolTip(cbTVGuide, Lang.OTRRemote.FrmMain_Tooltip_TVGuide);
            ttTooltip.SetToolTip(btnPreferences, Lang.OTRRemote.FrmMain_Tooltip_Preferences);
            ttTooltip.SetToolTip(btnHelp, Lang.OTRRemote.FrmMain_Tooltip_Help);
            ttTooltip.SetToolTip(btnOK, Lang.OTRRemote.FrmMain_Tooltip_OK);
            ttTooltip.SetToolTip(btnCancel, Lang.OTRRemote.FrmMain_Tooltip_Cancel);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // The VS WinForm Designer doesn't want to set a property automatically, so set it manually
            lvSettings.AllowColumnReorder = false;

            // Search for compatible plugins and include them into the program
            pluginCollection = PluginAPI.GetPlugins(Application.StartupPath);

            // Enable the plugins, so that they can be added to the list
            foreach (Plugin plugin in pluginCollection)
            {
                try
                {
                    PluginInterop.WriteDebugLog("FrmMain_Load()", "Enabling plugin \"" + plugin.Instance.Name + "\"");
                    if (plugin.Instance.Enable())
                    {
                        cbTVGuide.Items.Add(plugin.Instance.Name);
                    }
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("FrmMain_Load()", "Enabling the plgin caused error \"" + excp.Message + "\"");
                    PluginAPI.ShowPluginError(plugin.PluginPath, excp);
                }
            }

            // Finally, look if any plugins could be enabled
            if (cbTVGuide.Items.Count == 0)
            {
                gbTVGuide.Enabled = false;
                cbTVGuide.Enabled = false;
                cbTVGuide.Items.Add(Lang.OTRRemote.FrmMain_Error_NoPlugin);
            }

            // Select first list item
            cbTVGuide.SelectedIndex = 0;

            // Load settings from setting file
            // Load TV Guide App
            if (Program.Settings.Sections["Program"].Keys["TVGuideApp"] != null)
            {
                if (cbTVGuide.Items.Contains(Program.Settings.Sections["Program"].Keys["TVGuideApp"].Value.ToString()))
                {
                    cbTVGuide.SelectedItem = Program.Settings.Sections["Program"].Keys["TVGuideApp"].Value.ToString();
                }
            }

            // Load timezone
            if (Program.Settings.Sections["Program"].Keys["Timezone"] == null)
            {
                tbTimezone.Value = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            }
            else
            {
                tbTimezone.Value = Convert.ToDecimal(Program.Settings.Sections["Program"].Keys["Timezone"].Value);
            }

            // Load username
            if (Program.Settings.Sections["Program"].Keys["Username"] != null)
            {
                tbUsername.Text = Program.Settings.Sections["Program"].Keys["Username"].Value.ToString();
            }

            // Decrypt and load password
            if (Program.Settings.Sections["Program"].Keys["Password"] != null)
            {
                tbPassword.Text = Encryption.Encryption.Decrypt(Program.Settings.Sections["Program"].Keys["Password"].Value.ToString(), tbUsername.Text);
            }
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void cbTVGuide_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Fill property bag with settings of selected plugin
            lvSettings.Items.Clear();
            foreach (Plugin plugin in pluginCollection)
            {
                try
                {
                    if (plugin.Instance.Name == cbTVGuide.SelectedItem.ToString())
                    {
                        for (int i = 0; i < plugin.Instance.GetSettings().Count; i++)
                        {
                            PluginSetting setting = plugin.Instance.GetSettings()[i];
                            ListViewItem item = new ListViewItem();
                            item.Text = setting.Name;

                            // Convert English boolean values to localized ones
                            if (setting.ValueType == ValueType.Boolean)
                            {
                                if (setting.Value.ToLower() == "yes")
                                {
                                    item.SubItems.Add(Lang.OTRRemote.FrmMain_Value_BoolYes);
                                }
                                else
                                {
                                    item.SubItems.Add(Lang.OTRRemote.FrmMain_Value_BoolNo);
                                }
                            }
                            else
                            {
                                item.SubItems.Add(setting.Value);
                            }
                            
                            item.Tag = setting.ValueType;
                            lvSettings.Items.Add(item);
                        }
                    }
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("cbTVGuide_SelectedIndexChanged()",
                                                "Failed to load plugin's properties: " + excp.Message);
                    PluginAPI.ShowPluginError(plugin.PluginPath, excp);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lvSettings_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
        {
            if (e.SubItem != 0)
            {
                switch ((ValueType)e.Item.Tag)
                {
                    case ValueType.String:
                        TextBox tb = new TextBox();
                        this.Controls.Add(tb);
                        lvSettings.StartEditing(tb, e.Item, e.SubItem);
                        break;
                    case ValueType.Boolean:
                        ComboBox cb = new ComboBox();
                        this.Controls.Add(cb);
                        cb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cb.Items.Add(Lang.OTRRemote.FrmMain_Value_BoolYes);
                        cb.Items.Add(Lang.OTRRemote.FrmMain_Value_BoolNo);
                        cb.Tag = "ComboBox";
                        lvSettings.StartEditing(cb, e.Item, e.SubItem);
                        break;
                    case ValueType.Integer:
                        NumericUpDown ud = new NumericUpDown();
                        this.Controls.Add(ud);
                        lvSettings.StartEditing(ud, e.Item, e.SubItem);
                        break;
                }
            }
        }

        private void lvSettings_SubItemEndEditing(object sender, Crazysoft.OTR_Remote.ListViewEx.SubItemEndEditingEventArgs e)
        {
            // TODO: Add code to invoke the event handler for setting changes in plugins
            // TODO: Add the event prototype to the interface of the plugin
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validate user inputs
            if (tbUsername.Text == "")
            {
                if (MessageBox.Show(Lang.OTRRemote.FrmMain_Error_Username_Text, Lang.OTRRemote.FrmMain_Error_Username_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    tbUsername.Focus();
                    return;
                }
                else
                {
                    tbUsername.Text = "";
                    tbPassword.Text = "";
                }
            }
            else if (tbPassword.Text == "")
            {
                if (MessageBox.Show(Lang.OTRRemote.FrmMain_Error_Password_Text, Lang.OTRRemote.FrmMain_Error_Password_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    tbPassword.Focus();
                    return;
                }
                else
                {
                    tbUsername.Text = "";
                    tbPassword.Text = "";
                }
            }

            // Save user settings
            if (tbUsername.Text != "")
            {
                if (Program.Settings.Sections["Program"].Keys["Username"] != null)
                {
                    Program.Settings.Sections["Program"].Keys["Username"].Value = tbUsername.Text;
                }
                else
                {
                    if (!Program.Settings.Sections.Contains("Program"))
                    {
                        Program.Settings.Sections.Add("Program");
                    }
                    Program.Settings.Sections["Program"].Keys.Add("Username", tbUsername.Text);
                }
                if (Program.Settings.Sections["Program"].Keys["Password"] != null)
                {
                    Program.Settings.Sections["Program"].Keys["Password"].Value =
                        Encryption.Encryption.Encrypt(tbPassword.Text, tbUsername.Text);
                }
                else
                {
                    Program.Settings.Sections["Program"].Keys.Add("Password",
                                                                  Encryption.Encryption.Encrypt(tbPassword.Text,
                                                                                                tbUsername.Text));
                }
            }

            if (Program.Settings.Sections["Program"].Keys["Timezone"] != null)
            {
                Program.Settings.Sections["Program"].Keys["Timezone"].Value = Convert.ToInt32(tbTimezone.Value);
            }
            else
            {
                Program.Settings.Sections["Program"].Keys.Add("Timezone", Convert.ToInt32(tbTimezone.Value));
                
            }
            if (Program.Settings.Sections["Program"].Keys["TVGuideApp"] != null)
            {
                Program.Settings.Sections["Program"].Keys["TVGuideApp"].Value = cbTVGuide.SelectedItem.ToString();
            }
            else
            {
                Program.Settings.Sections["Program"].Keys.Add("TVGuideApp", cbTVGuide.SelectedItem.ToString());
            }
            Program.Settings.Save();

            PluginSettingsCollection settings = new PluginSettingsCollection();
            foreach (ListViewItem item in lvSettings.Items)
            {
                if ((ValueType)item.Tag == ValueType.Boolean)
                {
                    if (item.SubItems[1].Text == Lang.OTRRemote.FrmMain_Value_BoolYes)
                    {
                        settings.Add(item.Text, "Yes", (ValueType)item.Tag);
                    }
                    else
                    {
                        settings.Add(item.Text, "No", (ValueType)item.Tag);
                    }
                }
                else
                {
                    settings.Add(item.Text, item.SubItems[1].Text, (ValueType)item.Tag);
                }
            }

            // Save the plugin's settings and execute the script action
            foreach (Plugin plugin in pluginCollection)
            {
#if !DEBUG
                try
                {
#endif
                    if (plugin.Instance.Name == cbTVGuide.SelectedItem.ToString())
                    {
                        plugin.Instance.SetSettings(settings);
                        object returnVar = plugin.Instance.SaveRecordingScript();
                        if (returnVar != null)
                        {
                            if (returnVar == typeof(string))
                            {
                                MessageBox.Show(String.Format(Lang.OTRRemote.FrmMain_Error_Plugin_Text, returnVar.ToString()), Lang.OTRRemote.FrmMain_Error_Plugin_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show(String.Format(Lang.OTRRemote.FrmMain_Error_PluginResult_Text, returnVar.ToString()), Lang.OTRRemote.FrmMain_Error_PluginResult_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
#if !DEBUG
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("btnOK_Click()", "Failed to set plugin's properties: " + excp.Message);
                    PluginAPI.ShowPluginError(plugin.PluginPath, excp);
                }
#endif
                }

            Application.Exit();
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            FrmPreferences FrmPreferences = new FrmPreferences();
            FrmPreferences.ShowDialog(this);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Plugin plugin in pluginCollection)
            {
                try
                {
                    PluginInterop.WriteDebugLog("FrmMain_FormClosing()", "Unloading plugin \"" + plugin.Instance.Name + "\"");
                    plugin.Instance.Dispose();
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("btnOK_Click()", "Failed to unload plugin: " + excp.Message);
                    PluginAPI.ShowPluginError(plugin.PluginPath, excp);
                }
            }

            // Only enable auto-update on Windows
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                // Call AppUpdate if user enabled auto update and last update is at least 7 days old
                if (!Program.Settings.Sections["Program"].Keys.Contains("EnableAutoUpdate") ||
                    Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["EnableAutoUpdate"].Value))
                {
                    DateTime lastUpdate = new DateTime(0);
                    if (Program.Settings.Sections["Program"].Keys.Contains("LastAutoUpdate"))
                    {
                        lastUpdate =
                            new DateTime(
                                Convert.ToInt64(Program.Settings.Sections["Program"].Keys["LastAutoUpdate"].Value));
                    }

                    if (lastUpdate <= DateTime.Today.AddDays(-7))
                    {
                        // Check if AppUpdate is installed and start update
                        RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Crazysoft\\AppUpdate");

                        if (rk != null && rk.GetValue("InstallationPath", null) != null)
                        {
                            try
                            {
                                System.Diagnostics.ProcessStartInfo updateApp =
                                    new System.Diagnostics.ProcessStartInfo(rk.GetValue("InstallationPath").ToString() +
                                                                            "\\AppUpdate.exe");
                                updateApp.Arguments = "Crazysoft.OTR_Remote";
                                updateApp.ErrorDialog = false;
                                updateApp.WorkingDirectory = rk.GetValue("InstallationPath").ToString();
                                System.Diagnostics.Process.Start(updateApp);

                                Program.Settings.Sections["Program"].Keys.Add("LastAutoUpdate", DateTime.Now.Ticks);
                                Program.Settings.Save();
                                Application.Exit();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Lang.OTRRemote.FrmMain_HelpFilename);
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            this.Invalidate(pnlHeader.DisplayRectangle);
        }
    }
}