using Microsoft.Win32;
using System;
using System.IO;

[assembly: CLSCompliant(true)]
namespace Crazysoft.OTRRemote
{
    public class TVgenial : IPlugin
    {
        private PluginSettingsCollection _settings;

        private Version appVer;

        private string _Name = "TVgenial";
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
            // This plugin is only compatible with Windows
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                RegistryKey rk = Registry.CurrentUser;
                rk = rk.OpenSubKey("Software\\ARAKON-Systems\\TVgenial");
                if (rk == null)
                {
                    PluginInterop.WriteDebugLog("Initialize()",
                                                "Registry path \"HKEY_CURRENT_USER\\Software\\ARAKON-Systems\\TVgenial\" not found");
                    return;
                }
                _settings = new PluginSettingsCollection();

                appVer = new Version(rk.GetValue("CurrentVersion", "3.40").ToString());

                // Application directory
                try
                {
                    // A user found a bug which seems to come from an unset InstallDir in a cracked installation
                    if (rk.GetValue("InstallDir") == null || !Directory.Exists(rk.GetValue("InstallDir").ToString()))
                    {
                        PluginInterop.WriteDebugLog("Initialize()",
                                                    "Registry value \"InstallDir\" not found or wrong path. Corrupt or cracked installer?");
                        return;
                    }

                    FileStream fs =
                        new FileStream(Path.Combine(rk.GetValue("InstallDir").ToString(), "access.tmp"), FileMode.Create,
                                       FileAccess.Write);
                    fs.Close();
                    fs.Dispose();
                    File.Delete(Path.Combine(rk.GetValue("InstallDir").ToString(), "access.tmp"));

                    if (appVer.Major == 4)
                    {
                        _settings.Add(Lang.TVgenial.Plugin_Settings_Path,
                                     String.Concat(rk.GetValue("InstallDir").ToString(), "\\Interfaces\\OTR"),
                                     ValueType.String);
                    }
                    else
                    {
                        _settings.Add(Lang.TVgenial.Plugin_Settings_Path, rk.GetValue("InstallDir").ToString(),
                                     ValueType.String);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TVgenial")))
                    {
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TVgenial"));
                    }
                    PluginInterop.WriteDebugLog("Initialize()", "Write access to TVgenial install directory denied, writing to LocalApplicationData");
                    _settings.Add(Lang.TVgenial.Plugin_Settings_Path, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TVgenial"), ValueType.String);
                }

                // Recording Script name
                _settings.Add(Lang.TVgenial.Plugin_Settings_Name, "RecorderScript.OTR.txt", ValueType.String);

                // Enable Deletion?
                _settings.Add(Lang.TVgenial.Plugin_Settings_Delete, "Yes", ValueType.Boolean);

                // Automatically set OTR Remote as default recording interface?
                _settings.Add(Lang.TVgenial.Plugin_Settings_Default, "Yes", ValueType.Boolean);

                rk.Close();
            }
        }

        public bool Enable()
        {
            // The plugin can only be enabled on Windows
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                try
                {
                    // Look for TVgenial registry key
                    RegistryKey rk = Registry.CurrentUser;
                    rk = rk.OpenSubKey("Software\\ARAKON-Systems\\TVgenial");
                    if (rk != null)
                    {
                        if (rk.GetValue("InstallDir") == null || !Directory.Exists(rk.GetValue("InstallDir").ToString()))
                        {
                            PluginInterop.ShowMessageBox(Lang.TVgenial.Plugin_Error_NoInstallDir, "TVgenial Plugin",
                                                         PluginInterop.MessageBoxIcon.Error);
                            _Enabled = false;
                        }
                        else
                        {
                            _Enabled = true;
                        }
                    }
                    else
                    {
                        _Enabled = false;
                    }
                }
                catch (Exception e)
                {
                    PluginInterop.WriteDebugLog("Enable()", "Error while trying to enable the plugin: " + e.Message);
                    _Enabled = false;
                }

                return _Enabled;
            }
            
            // Write the Linux unavailability to the log and return false
            PluginInterop.WriteDebugLog("Enable()", "This plugin is not available on Linux.");
            return false;
        }

        public PluginSettingsCollection GetSettings()
        {
            return _settings;
        }

        public void SetSettings(PluginSettingsCollection settings)
        {
            this._settings = settings;
        }

        public object SaveRecordingScript()
        {
            if (String.IsNullOrEmpty(_settings[Lang.TVgenial.Plugin_Settings_Path].Value))
            {
                return Lang.TVgenial.Plugin_Error_Path;
            }

            RegistryKey rk = Registry.CurrentUser;
            rk = rk.OpenSubKey("Software\\ARAKON-Systems\\TVgenial");
            string installDir = rk.GetValue("InstallDir").ToString();

            // Actions for TVgenial 4.0
            if (appVer.Major == 4)
            {
                try
                {
                    FileStream fs = new FileStream(Path.Combine(installDir, "access.tmp"), FileMode.Create, FileAccess.Write);
                    fs.Close();
                    fs.Dispose();
                    File.Delete(Path.Combine(installDir, "access.tmp"));
                }
                catch (UnauthorizedAccessException)
                {
                    return Lang.TVgenial.Plugin_Error_AccessDenied_Interface;
                }

                Directory.CreateDirectory(String.Concat(installDir, "\\Interfaces\\OTR"));

                // Write logo to OTR interface directory
                Lang.TVgenial.Logo.Save(String.Concat(installDir, "\\Interfaces\\OTR\\Logo.png"));

                // Write recorder.ini
                // This file contains common information about the interface
                StreamWriter recorder = new StreamWriter(String.Concat(installDir, "\\Interfaces\\OTR\\recorder.ini"));
                recorder.WriteLine("RecorderType = rct_Script");
                recorder.WriteLine("Name         = OnlineTvRecorder");
                recorder.WriteLine("Version      = 1.0.2.4");
                recorder.WriteLine("Description  = Free online TV recording");
                recorder.WriteLine("VendorURL    = www.crazysoft.net.ms");
                recorder.WriteLine("debuginfo    = false");
                recorder.Close();
            }
            rk.Close();

            DirectoryInfo di = new DirectoryInfo(_settings[Lang.TVgenial.Plugin_Settings_Path].Value);
            if (!di.Exists)
            {
                try
                {
                    Directory.CreateDirectory(di.FullName);
                }
                catch (UnauthorizedAccessException)
                {
                    return Lang.TVgenial.Plugin_Error_AccessDenied_Script;
                }
            }

            if (String.IsNullOrEmpty(_settings[Lang.TVgenial.Plugin_Settings_Name].Value))
            {
                return Lang.TVgenial.Plugin_Error_Name;
            }

            // Get the directory where OTR Remote resides in for later use
            string otrRemoteDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (appVer.Major == 4)
            {
                // Write setup.ini
                // This file contains information about the RecorderScript file
                string pluginDir = String.Concat(installDir, "\\Interfaces\\OTR");
                Directory.CreateDirectory(pluginDir);

                StreamWriter setup = new StreamWriter(String.Concat(pluginDir, "\\setup.ini"));
                setup.WriteLine("[setup]");
                setup.WriteLine(String.Concat("Scriptfile = ", Path.Combine(_settings[Lang.TVgenial.Plugin_Settings_Path].Value, _settings[Lang.TVgenial.Plugin_Settings_Name].Value)));
                setup.Close();

                // Change recorders.ini
                // This file contains information about the existing interfaces and which should be used
                // Starting from Version 4.05, this file is saved in the user's AppData directory
                // So we have to change the installDir, if this is the case
                if (!File.Exists(Path.Combine(installDir, "recorders.ini")))
                {
                    // Use AppData directory
                    installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVgenial");

                    // If file doesn't exist there, create an empty one
                    if (!File.Exists(Path.Combine(installDir, "recorders.ini")))
                    {
                        StreamWriter tmpsw = File.CreateText(Path.Combine(installDir, "recorders.ini"));
                        tmpsw.Close();
                    }
                }

                StreamReader sr = new StreamReader(Path.Combine(installDir, "recorders.ini"));
                string file = String.Empty;
                string line = sr.ReadLine();
                int maxID = 99;
                bool isOTRSection = false;
                while (line != null)
                {
                    if (isOTRSection && line.Trim().StartsWith("["))
                    {
                        isOTRSection = false;
                    }
                    if (line.Trim().StartsWith("[OTR]"))
                    {
                        isOTRSection = true;
                    }

                    if (!isOTRSection && line.Trim().StartsWith("active"))
                    {
                        if (_settings[Lang.TVgenial.Plugin_Settings_Default].Value == "Yes")
                        {
                            if (line.Trim().EndsWith("2"))
                            {
                                file = String.Concat(file, "active=1\r\n");
                                line = sr.ReadLine();
                                continue;
                            }
                        }
                        file = String.Concat(file, line, "\r\n");
                    }
                    else if (!isOTRSection && line.Trim().StartsWith("ID"))
                    {
                        int id = Convert.ToInt32(line.Trim().Split('=')[1]);
                        if (id > maxID)
                        {
                            maxID = id;
                        }
                        file = String.Concat(file, line, "\r\n");
                    }
                    else if (!isOTRSection)
                    {
                        file = String.Concat(file, line, "\r\n");
                    }

                    line = sr.ReadLine();
                }
                sr.Close();

                // Add OTR section to file
                file = String.Concat(file, "[OTR]\r\n");
                if (_settings[Lang.TVgenial.Plugin_Settings_Default].Value == "Yes")
                {
                    file = String.Concat(file, "active=2\r\n");
                }
                else
                {
                    file = String.Concat(file, "active=0\r\n");
                }
                file = String.Concat(file, "ID=", (maxID + 1), "\r\n");
                file = String.Concat(file, "selectable=1\r\n");
                file = String.Concat(file, "sync_to_other=0\r\n");

                // Write new file
                StreamWriter recorders = new StreamWriter(Path.Combine(installDir, "recorders.ini"));
                recorders.Write(file);
                recorders.Close();

                // If TVgenial < 4.05 is run on Vista, VirtualStore is used because of missing admin rights
                // For TVgenial to be able to use the INI file, we (although one shouldn't play with VirtualStore) copy the file
                if (File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\VirtualStore\\Program Files\\TVgenial\\recorders.ini")))
                {
                    File.Copy(Path.Combine(installDir, "recorders.ini"), String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\VirtualStore\\Program Files\\TVgenial\\recorders.ini"), true);
                }
            }
            else
            {
                // Set Recording Interface as default
                if (_settings[Lang.TVgenial.Plugin_Settings_Default].Value == "Yes")
                {
                    rk = Registry.CurrentUser;
                    rk = rk.OpenSubKey("Software\\ARAKON-Systems\\TVgenial", true);
                    rk.SetValue(String.Concat("RecorderScriptFile", Path.Combine(di.FullName, _settings[Lang.TVgenial.Plugin_Settings_Name].Value)), RegistryValueKind.String);
                    rk.SetValue("RecorderSetupFileName", Path.Combine(otrRemoteDir, "OTRRemote.exe"));
                    rk.SetValue("RecorderSupportChange", 0, RegistryValueKind.DWord);
                    if (_settings[Lang.TVgenial.Plugin_Settings_Delete].Value == "Yes")
                    {
                        rk.SetValue("RecorderSupportDelete", 1, RegistryValueKind.DWord);
                    }
                    else
                    {
                        rk.SetValue("RecorderSupportDelete", 0, RegistryValueKind.DWord);
                    }
                    rk.SetValue("RecorderType", 1, RegistryValueKind.DWord);
                    rk.Close();
                }
            }

            StreamWriter sw = new StreamWriter(Path.Combine(di.FullName, _settings[Lang.TVgenial.Plugin_Settings_Name].Value));

            // Add Job-Addition-Line
            sw.WriteLine(String.Concat("DoRecordWaitFor \"", Path.Combine(otrRemoteDir, "OTRRemote.exe"), "\" '-a -s=''\"'s'\"'' -sd='yyyy-mm-dd' -st='hh:nn' -et='rr:ff' -t=''\"'#'\"''"));
            // Add Job-Deletion-Line
            if (_settings[Lang.TVgenial.Plugin_Settings_Delete].Value == "Yes")
            {
                sw.WriteLine(String.Concat("DoDeleteWaitFor \"", Path.Combine(otrRemoteDir, "OTRRemote.exe"), "\" '-r -s=''\"'s'\"'' -sd='yyyy-mm-dd' -st='hh:nn' -et='rr:ff' -t=''\"'#'\"''"));
            }
            sw.Close();

            return null;
        }

        public void Dispose()
        {
            _settings = null;
            _Enabled = false;
        }
    }
}