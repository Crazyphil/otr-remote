using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;

[assembly: CLSCompliant(true)]
namespace Crazysoft.OTRRemote
{
    public class TVBrowser : IPlugin
    {
        private PluginSettingsCollection settings;
        private Version tvbrowserVersion = new Version(2, 7, 5);

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
            // If TV-Browser path was saved, first look if it is correct and add it, otherwise detect correct path
            if (settings.Contains(Lang.TVBrowser.Plugin_Settings_Path) && IsTvBrowserPath(settings[Lang.TVBrowser.Plugin_Settings_Path].Value)) {
                SetPathSetting(settings[Lang.TVBrowser.Plugin_Settings_Path].Value);
                return true;
            }

            // Only check registry if OTR Remote is running on Windows
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                // Check, if the TV-Browser registry key exists
                // Each TV-Browser version has its own key ("TV-Browser2.5.2", etc.),
                // so we have to go thorugh each entry
                RegistryKey rk = Registry.LocalMachine;
                rk = rk.OpenSubKey("SOFTWARE");
                bool foundTvBrowser = false;

                // On 64-bit systems, the "Wow6432Node" subnode has to be opened to acces registry keys of 32-bit apps
                if (IntPtr.Size * 8 == 64)
                {
                    rk = rk.OpenSubKey("Wow6432Node");
                }

                foreach (string subkey in rk.GetSubKeyNames())
                {
                    if (subkey.StartsWith("TV-Browser", StringComparison.CurrentCultureIgnoreCase) && subkey.Length > 10)
                    {
                        RegistryKey sk = rk.OpenSubKey(subkey);
                        string path = sk.GetValue("Install directory", String.Empty).ToString();
                        sk.Close();

                        if (subkey.Substring(10).Contains("-"))
                        {
                            int index = subkey.Substring(10).IndexOf('-');
                            try
                            {
                                tvbrowserVersion = new Version(subkey.Substring(10, index));
                            }
                            catch (FormatException) { }
                            catch (ArgumentException) { }
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
                            catch (FormatException) { }
                            catch (ArgumentException) { }
                        }
                        PluginInterop.WriteDebugLog("Enable()", String.Concat("Found registry path \"HKEY_LOCAL_MACHINE\\SOFTWARE\\", subkey, "\""));

                        try
                        {
                            FileStream fs = new FileStream(Path.Combine(path, "access.tmp"), FileMode.Create, FileAccess.Write);
                            fs.Close();
                            fs.Dispose();
                            File.Delete(Path.Combine(rk.GetValue("InstallDir").ToString(), "access.tmp"));
                        }
                        catch (UnauthorizedAccessException)
                        {
                            if (tvbrowserVersion.Major < 3)
                            {
                                path = String.Format("{0}{1}TV-Browser{1}{2}", Environment.GetEnvironmentVariable("USERPROFILE"), Path.DirectorySeparatorChar, tvbrowserVersion.ToString());
                            }
                            else
                            {
                                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), tvbrowserVersion.ToString());
                            }
                        }

                        SetPathSetting(path);
                        foundTvBrowser = true;
                    }
                }

                if (!foundTvBrowser)
                {
                    PluginInterop.WriteDebugLog("Enable()", String.Concat("Did not find any registry path starting with \"TV-Browser\" in \"", rk.Name, "\". Switching to Portable Mode."));
                    SetPathSetting(string.Empty);
                }

                rk.Close();
                return true;
            }
            else
            {
                SetPathSetting(string.Empty);
                return true;
            }
        }

        private void SetPathSetting(string defaultPath)
        {
            if (!File.Exists("OTRRemote.jar"))
            {
                PluginInterop.WriteDebugLog("Enable()", "Could not find TV-Browser plugin file \"OTRRemote.jar\", switching to manual mode.");
                tvbrowserVersion = new Version(1, 0);
            }

            // The user has to enter the path manually when running on Linux or TV-Browser is not found in registry (Portable)
            if (!settings.Contains(Lang.TVBrowser.Plugin_Settings_Path))
            {
                settings.Add(Lang.TVBrowser.Plugin_Settings_Path, defaultPath, ValueType.String);
            }
            else
            {
                settings[Lang.TVBrowser.Plugin_Settings_Path].Value = defaultPath;
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
                if (!IsTvBrowserPath(settings[Lang.TVBrowser.Plugin_Settings_Path].Value))
                {
                    return Lang.TVBrowser.Plugin_Error_Invalid_Path;
                }

                try
                {
                    File.Copy("OTRRemote.jar", Path.Combine(Path.Combine(settings[Lang.TVBrowser.Plugin_Settings_Path].Value, "plugins"), "OTRRemote.jar"), true);
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

        private bool IsTvBrowserPath(string path)
        {
            // Check, if the entered TV-Browser installation path exists
            return Directory.Exists(Path.Combine(path, "plugins"));
        }
    }
}
