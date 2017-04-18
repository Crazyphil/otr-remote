using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmMain : Form
    {
        internal PluginAPI pluginApi;
        private DialogResult closeReason = DialogResult.None;

        public FrmMain()
        {
            InitializeComponent();

            Program.TranslateControls(this);

            lvSettings.Columns[0].Text = Lang.OTRRemote.FrmMain_lvSettings_Setting;
            lvSettings.Columns[1].Text = Lang.OTRRemote.FrmMain_lvSettings_Value;

            ttTooltip.ToolTipTitle = Lang.OTRRemote.FrmMain_Tooltip_Title;
            ttTooltip.SetToolTip(tbUsername, Lang.OTRRemote.FrmMain_Tooltip_Username);
            ttTooltip.SetToolTip(tbPassword, Lang.OTRRemote.FrmMain_Tooltip_Password);
            ttTooltip.SetToolTip(tbTimezone, Lang.OTRRemote.FrmMain_Tooltip_Timezone);
            ttTooltip.SetToolTip(cbTVGuide, Lang.OTRRemote.FrmMain_Tooltip_TVGuide);
            ttTooltip.SetToolTip(btnPreferences, Lang.OTRRemote.FrmMain_Tooltip_Preferences);
            ttTooltip.SetToolTip(btnHelp, Lang.OTRRemote.FrmMain_Tooltip_Help);
            ttTooltip.SetToolTip(btnOK, Lang.OTRRemote.FrmMain_Tooltip_OK);
            ttTooltip.SetToolTip(btnCancel, Lang.OTRRemote.FrmMain_Tooltip_Cancel);

            // If program is running in portable mode (settings file in app directory) add text to title
            if (System.IO.Path.GetDirectoryName(Program.Settings.SettingsFilePath) == System.IO.Path.GetDirectoryName(Application.ExecutablePath))
            {
                this.Text = String.Concat(this.Text, " Portable");
            }

            pluginApi = new PluginAPI(Program.Settings);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // The VS WinForm Designer doesn't want to set a property automatically, so set it manually
            lvSettings.AllowColumnReorder = false;

            // Search for compatible plugins and include them into the program
            pluginApi.GetPlugins(Application.StartupPath);

            // Enable the plugins, so that they can be added to the list
            foreach (Plugin plugin in pluginApi.Plugins)
            {
#if !DEBUG
                try
                {
#endif
                    PluginInterop.WriteDebugLog("FrmMain_Load()", String.Format("Enabling plugin \"{0}\"", plugin.Instance.Name));
                    if (plugin.Instance.Enable())
                    {
                        cbTVGuide.Items.Add(plugin.Instance.Name);
                    }
#if !DEBUG
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("FrmMain_Load()", String.Format("Enabling the plugin caused error \"{0}\"", excp.Message));
                    pluginApi.ShowPluginError(plugin.PluginPath, excp);
                }
#endif
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
            if (!Program.Settings["Program"]["TVGuideApp"].IsNull)
            {
                if (cbTVGuide.Items.Contains(Program.Settings["Program"]["TVGuideApp"].Value.ToString()))
                {
                    cbTVGuide.SelectedItem = Program.Settings["Program"]["TVGuideApp"].Value.ToString();
                }
            }

            // Load timezone
            if (!Program.Settings["Program"]["Timezone"].IsNull)
            {
                tbTimezone.Value = Convert.ToDecimal(Program.Settings["Program"]["Timezone"].Value);
                
            }
            else
            {
                tbTimezone.Value = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            }

            // Load username
            if (!Program.Settings["Program"]["Username"].IsNull)
            {
                tbUsername.Text = Program.Settings["Program"]["Username"].Value.ToString();
            }

            // Decrypt and load password
            if (!Program.Settings["Program"]["Password"].IsNull)
            {
                tbPassword.Text = Encryption.Encryption.Decrypt(Program.Settings["Program"]["Password"].Value.ToString(), tbUsername.Text);
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
            foreach (Plugin plugin in pluginApi.Plugins)
            {
                try
                {
                    if (plugin.Instance.Name == cbTVGuide.SelectedItem.ToString())
                    {
                        PluginSettingsCollection settings = plugin.Instance.GetSettings();
                        for (int i = 0; i < settings.Count; i++)
                        {
                            PluginSetting setting = settings[i];
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
                    pluginApi.ShowPluginError(plugin.PluginPath, excp);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.closeReason = DialogResult.Cancel;
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

        private void lvSettings_SubItemEndEditing(object sender, Crazysoft.OTRRemote.ListViewEx.SubItemEndEditingEventArgs e)
        {
            // TODO: Add code to invoke the event handler for setting changes in plugins
            // TODO: Add the event prototype to the interface of the plugin
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            // Validate user inputs
            if (String.IsNullOrEmpty(tbUsername.Text))
            {
                if (MessageBox.Show(Lang.OTRRemote.FrmMain_Error_Username_Text, Lang.OTRRemote.FrmMain_Error_Username_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    tbUsername.Focus();
                    return;
                }
                else
                {
                    tbUsername.Text = String.Empty;
                    tbPassword.Text = String.Empty;
                }
            }
            else if (String.IsNullOrEmpty(tbPassword.Text))
            {
                if (MessageBox.Show(Lang.OTRRemote.FrmMain_Error_Password_Text, Lang.OTRRemote.FrmMain_Error_Password_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    tbPassword.Focus();
                    return;
                }
                else
                {
                    tbUsername.Text = String.Empty;
                    tbPassword.Text = String.Empty;
                }
            }

            // Save user settings
            Program.Settings.Sections.Add("Program");
            if (!String.IsNullOrEmpty(tbUsername.Text))
            {
                Program.Settings["Program"].Keys.Add("Username", tbUsername.Text);
                Program.Settings["Program"].Keys.Add("Password", Encryption.Encryption.Encrypt(tbPassword.Text, tbUsername.Text));
            }

            Program.Settings["Program"].Keys.Add("Timezone", Convert.ToInt32(tbTimezone.Value));
            Program.Settings["Program"].Keys.Add("TVGuideApp", cbTVGuide.SelectedItem.ToString());
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
            foreach (Plugin plugin in pluginApi.Plugins)
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
                            if (returnVar.GetType() == typeof(string))
                            {
                                MessageBox.Show(String.Format(Lang.OTRRemote.FrmMain_Error_Plugin_Text, returnVar.ToString()), Lang.OTRRemote.FrmMain_Error_Plugin_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
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
                    pluginApi.ShowPluginError(plugin.PluginPath, excp);
                }
#endif
            }

            this.closeReason = DialogResult.OK;
            this.Close();
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            FrmPreferences FrmPreferences = new FrmPreferences();
            FrmPreferences.ShowDialog(this);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unload all plugins and save their settings
            pluginApi.UnloadPlugins(this.closeReason == DialogResult.OK);

            Program.StartAppUpdate();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            // Show offline help if installed or not using Mono
            if (Type.GetType("Mono.Runtime") == null && System.IO.File.Exists(Lang.OTRRemote.FrmMain_HelpFilename))
            {
                Help.ShowHelp(this, Lang.OTRRemote.FrmMain_HelpFilename);
            }
            else
            {
                string lang = "en";
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "de") {
                    lang = "de";
                }
                System.Diagnostics.Process.Start(String.Concat("http://www.crazysoft-software.tk/help/otrremote/", lang, "/"));
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            this.Invalidate(pnlHeader.DisplayRectangle);
        }

        private void lvSettings_Resize(object sender, EventArgs e)
        {
            // Set the size of the property columns half of the control's width minus scrollbars
            lvSettings.Columns[0].Width = Convert.ToInt32(Math.Floor(lvSettings.Width / 2f)) - 11;
            lvSettings.Columns[1].Width = lvSettings.Columns[0].Width;
        }
    }
}