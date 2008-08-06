using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace Crazysoft.OTR_Remote
{
    public class TVBrowser : IPlugin
    {
        private PluginSettingsCollection settings;
        private Version tvbrowserVersion;

        private string _Name = "TV-Browser";
        public string Name
        {
            get { return _Name; }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
        }

        public void Initialize()
        {
            // Initialize an empty settings variable
            settings = new PluginSettingsCollection();
        }

        public bool Enable()
        {
            // Only check registry if OTR Remote is running on Windows
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                // Check, if the TV-Browser registry key exists
                // Each TV-Browser version has its own key ("TV-Browser2.5.2", etc.),
                // so we have to go thorugh each entry
                RegistryKey rk = Registry.LocalMachine;
                rk = rk.OpenSubKey("SOFTWARE");
                bool foundTvBrowser = false;
                foreach (string subkey in rk.GetSubKeyNames())
                {
                    if (subkey.StartsWith("TV-Browser", StringComparison.CurrentCultureIgnoreCase))
                    {
                        RegistryKey sk = rk.OpenSubKey(subkey);
                        if (!settings.Contains(Lang.TVBrowser.Plugin_Settings_Path))
                        {
                            settings.Add(Lang.TVBrowser.Plugin_Settings_Path,
                                         sk.GetValue("Install directory", "").ToString(), ValueType.String);
                        }
                        else
                        {
                            settings[Lang.TVBrowser.Plugin_Settings_Path].Value =
                                sk.GetValue("Install directory", "").ToString();
                        }
                        sk.Close();

                        if (subkey.Substring(10).Contains("-"))
                        {
                            int index = subkey.Substring(10).IndexOf('-');
                            try
                            {
                                tvbrowserVersion = new Version(subkey.Substring(10, index));
                            }
                            catch (FormatException)
                            {
                            }
                        }
                        else
                        {
                            int endindex = 10;
                            int length = subkey.Length - 10;
                            foreach (char c in subkey.Substring(10))
                            {
                                int result;
                                if (c != '.' && !Int32.TryParse(c.ToString(), out result))
                                {
                                    length = endindex - 10;
                                    break;
                                }
                                endindex++;
                            }
                            try
                            {
                                tvbrowserVersion = new Version(subkey.Substring(10, length));
                            }
                            catch (FormatException)
                            {
                            }
                        }
                        PluginInterop.WriteDebugLog("Enable()",
                                                    "Found registry path \"HKEY_LOCAL_MACHINE\\SOFTWARE\\" + subkey +
                                                    "\"");

                        if (!File.Exists("OTRRemote.jar"))
                        {
                            PluginInterop.WriteDebugLog("Enable()",
                                                        "Could not find TV-Browser plugin file \"OTRRemote.jar\", switching to manual mode.");
                            tvbrowserVersion = new Version(1, 0);
                        }

                        foundTvBrowser = true;
                    }
                }

                if (!foundTvBrowser)
                {
                    PluginInterop.WriteDebugLog("Enable()",
                                                "Did not find any registry path starting with \"TV-Browser\" in \"" +
                                                rk.Name + "\"");
                }

                rk.Close();
                return foundTvBrowser;
            }
            else
            {
                tvbrowserVersion = new Version(2, 6, 2);
                if (!File.Exists("OTRRemote.jar"))
                {
                    PluginInterop.WriteDebugLog("Enable()",
                                                "Could not find TV-Browser plugin file \"OTRRemote.jar\", switching to manual mode.");
                    tvbrowserVersion = new Version(1, 0);
                }

                // The user has to enter the path manually when running on Linux
                if (!settings.Contains(Lang.TVBrowser.Plugin_Settings_Path))
                {
                    settings.Add(Lang.TVBrowser.Plugin_Settings_Path, "", ValueType.String);
                }
                else
                {
                    settings[Lang.TVBrowser.Plugin_Settings_Path].Value = "";
                }

                return true;
            }
        }

        public PluginSettingsCollection GetSettings()
        {
            return settings;
        }

        public void SetSettings(PluginSettingsCollection newSettings)
        {
            settings = newSettings;
        }

        public object SaveRecordingScript()
        {
            // Check, if we should install the automatic or the manual plugin
            if (tvbrowserVersion.Major >= 2)
            {
                // Check, if the entered TV-Browser installation path exists
                if (!Directory.Exists(Path.Combine(settings[Lang.TVBrowser.Plugin_Settings_Path].Value, "plugins")))
                {
                    return Lang.TVBrowser.Plugin_Error_Invalid_Path;
                }

                try
                {
                    File.Copy("OTRRemote.jar", Path.Combine(Path.Combine(settings[Lang.TVBrowser.Plugin_Settings_Path].Value, "plugins"), "OTRRemote.jar"), true);
                    StreamWriter sw = new StreamWriter(Path.Combine(Path.Combine(settings[Lang.TVBrowser.Plugin_Settings_Path].Value, "plugins"), "OTRRemote.jar.config"));
                    
                    // When running on the Mono Framework, add "mono" before the filename
                    if (Type.GetType("Mono.Runtime") != null)
                    {
                        sw.Write("mono ");
                    }

                    sw.WriteLine(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "OTRRemote.exe"));
                    sw.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    return Lang.TVBrowser.Plugin_Error_AccessDenied;
                }

            }
            else
            {
                PluginInterop.WriteDebugLog("SaveRecordingScript()",
                                            "You need at least TV-Browser 2.0 to use the automatic plugin installation.");
                // Open the manual installation wizard
                FrmWizard FrmWizard = new FrmWizard();
                FrmWizard.ShowDialog();
            }

            // If everything went fine, return null
            return null;
        }

        public void Dispose()
        {
            // The garbage collector would normally clean up everything itself.
            // But let's be nice developers and do it ourselves.
            settings = null;
            _Enabled = false;
        }
    }
}
