using Microsoft.Win32;
using System;
using System.IO;

namespace Crazysoft.OTR_Remote
{
    public class TVgenial : IPlugin
    {
        private PluginSettingsCollection settings;

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
                settings = new PluginSettingsCollection();

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
                        new FileStream(rk.GetValue("InstallDir").ToString() + "\\access.tmp", FileMode.Create,
                                       FileAccess.Write);
                    fs.Close();
                    fs.Dispose();
                    File.Delete(rk.GetValue("InstallDir").ToString() + "\\access.tmp");

                    if (appVer.Major == 4)
                    {
                        settings.Add(Lang.TVgenial.Plugin_Settings_Path,
                                     rk.GetValue("InstallDir").ToString() + "\\Interfaces\\OTR",
                                     ValueType.String);
                    }
                    else
                    {
                        settings.Add(Lang.TVgenial.Plugin_Settings_Path, rk.GetValue("InstallDir").ToString(),
                                     ValueType.String);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                          "\\TVgenial"))
                    {
                        Directory.CreateDirectory(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TVgenial");
                    }
                    PluginInterop.WriteDebugLog("Initialize()",
                                                "Write access to TVgenial install directory denied, writing to LocalApplicationData");
                    settings.Add(Lang.TVgenial.Plugin_Settings_Path,
                                 Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                 "\\TVgenial", ValueType.String);
                }

                // Recording Script name
                settings.Add(Lang.TVgenial.Plugin_Settings_Name, "RecorderScript.OTR.txt",
                             ValueType.String);

                // Enable Deletion?
                settings.Add(Lang.TVgenial.Plugin_Settings_Delete, "No", ValueType.Boolean);

                // Automatically set OTR Remote as default recording interface?
                settings.Add(Lang.TVgenial.Plugin_Settings_Default, "Yes", ValueType.Boolean);

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
            return settings;
        }

        public void SetSettings(PluginSettingsCollection settings)
        {
            this.settings = settings;
        }

        public object SaveRecordingScript()
        {
            if (settings[Lang.TVgenial.Plugin_Settings_Path].Value == "")
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
                    FileStream fs = new FileStream(rk.GetValue("InstallDir").ToString() + "\\access.tmp", FileMode.Create, FileAccess.Write);
                    fs.Close();
                    fs.Dispose();
                    File.Delete(rk.GetValue("InstallDir").ToString() + "\\access.tmp");
                }
                catch (UnauthorizedAccessException)
                {
                    return Lang.TVgenial.Plugin_Error_AccessDenied_Interface;
                }

                Directory.CreateDirectory(installDir + "\\Interfaces\\OTR");

                // Write logo to OTR interface directory
                Lang.TVgenial.Logo.Save(installDir + "\\Interfaces\\OTR\\Logo.png");

                // Write recorder.ini
                // This file contains common information about the interface
                StreamWriter recorder = new StreamWriter(installDir + "\\Interfaces\\OTR\\recorder.ini");
                recorder.WriteLine("RecorderType = rct_Script");
                recorder.WriteLine("Name         = OnlineTvRecorder");
                recorder.WriteLine("Version      = 1.0.2.2");
                recorder.WriteLine("Description  = Free online TV recording");
                recorder.WriteLine("VendorURL    = www.crazysoft.net.ms");
                recorder.WriteLine("debuginfo    = 0");
                recorder.Close();
            }
            rk.Close();

            DirectoryInfo di = new DirectoryInfo(settings[Lang.TVgenial.Plugin_Settings_Path].Value);
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

            if (settings[Lang.TVgenial.Plugin_Settings_Name].Value == "")
            {
                return Lang.TVgenial.Plugin_Error_Name;
            }

            if (appVer.Major == 4)
            {
                // Write setup.ini
                // This file contains information about the RecorderScript file
                StreamWriter setup = new StreamWriter(installDir + "\\Interfaces\\OTR\\setup.ini");
                setup.WriteLine("[setup]");
                setup.WriteLine("Scriptfile = " + settings[Lang.TVgenial.Plugin_Settings_Path].Value + "\\" + settings[Lang.TVgenial.Plugin_Settings_Name].Value);
                setup.Close();

                // Change recorders.ini
                // This file contains information about the existing interfaces and which should be used
                StreamReader sr = new StreamReader(installDir + "\\recorders.ini");
                string file = "";
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
                        if (settings[Lang.TVgenial.Plugin_Settings_Default].Value == "Yes")
                        {
                            if (line.Trim().EndsWith("2"))
                            {
                                file += "active=1\r\n";
                                line = sr.ReadLine();
                                continue;
                            }
                        }
                        file += line + "\r\n";
                    }
                    else if (!isOTRSection && line.Trim().StartsWith("ID"))
                    {
                        int id = Convert.ToInt32(line.Trim().Split('=')[1]);
                        if (id > maxID)
                        {
                            maxID = id;
                        }
                        file += line + "\r\n";
                    }
                    else if (!isOTRSection)
                    {
                        file += line + "\r\n";
                    }

                    line = sr.ReadLine();
                }
                sr.Close();

                // Add OTR section to file
                file += "[OTR]\r\n";
                if (settings[Lang.TVgenial.Plugin_Settings_Default].Value == "Yes")
                {
                    file += "active=2\r\n";
                }
                else
                {
                    file += "active=0\r\n";
                }
                file += "ID=" + (maxID + 1) + "\r\n";
                file += "selectable=1\r\n";
                file += "sync_to_other=0\r\n";

                // Write new file
                StreamWriter recorders = new StreamWriter(installDir + "\\recorders.ini");
                recorders.Write(file);
                recorders.Close();

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VirtualStore\\Program Files\\TVgenial\\recorders.ini"))
                {
                    File.Copy(installDir + "\\recorders.ini",
                              Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                              "\\VirtualStore\\Program Files\\TVgenial\\recorders.ini", true);
                }
            }
            else
            {
                // Set Recording Interface as default
                if (settings[Lang.TVgenial.Plugin_Settings_Default].Value == "Yes")
                {
                    rk = Registry.CurrentUser;
                    rk = rk.OpenSubKey("Software\\ARAKON-Systems\\TVgenial", true);
                    rk.SetValue("RecorderScriptFile", di.FullName + "\\" + settings[Lang.TVgenial.Plugin_Settings_Name].Value, RegistryValueKind.String);
                    rk.SetValue("RecorderSetupFileName", System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf('\\')) + "\\OTRRemote.exe");
                    rk.SetValue("RecorderSupportChange", 0, RegistryValueKind.DWord);
                    if (settings[Lang.TVgenial.Plugin_Settings_Delete].Value == "Yes")
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

            StreamWriter sw = new StreamWriter(di.FullName + "\\" + settings[Lang.TVgenial.Plugin_Settings_Name].Value);

            // Add Job-Addition-Line
            sw.WriteLine("DoRecordWaitFor \"" + System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf('\\')) + "\\OTRRemote.exe" + "\" '-a -s=''\"'s'\"'' -sd='yyyy-mm-dd' -st='hh:nn' -et='rr:ff' -t=''\"'#'\"''");
            // Add Job-Deletion-Line
            if (settings[Lang.TVgenial.Plugin_Settings_Delete].Value == "Yes")
            {
                sw.WriteLine("DoDeleteWaitFor \"" + System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf('\\')) + "\\OTRRemote.exe" + "\" '-r -s=''\"'s'\"'' -sd='yyyy-mm-dd' -st='hh:nn' -et='rr:ff' -t=''\"'#'\"''");
            }
            sw.Close();

            return null;
        }

        public void Dispose()
        {
            settings = null;
            _Enabled = false;
        }
    }
}