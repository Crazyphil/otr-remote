using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;

namespace Crazysoft.OTR_Remote
{
    public class Clickfinder : IPlugin
    {
        private PluginSettingsCollection settings;

        private string _Name = "TV Movie Clickfinder";
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
            settings = new PluginSettingsCollection();
        }

        public bool Enable()
        {
            // This plugin is only available on Windows, so only enable it there
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                try
                {
                    // Look for Clickfinder registry key
                    RegistryKey rk = Registry.LocalMachine;
                    rk = rk.OpenSubKey("SOFTWARE\\Ewe\\TVGhost\\Gemeinsames");
                    if (rk != null)
                    {
                        if (File.Exists("ClickfinderHelper.exe"))
                        {
                            _Enabled = true;
                        }
                        else
                        {
                            PluginInterop.WriteDebugLog("Enable()",
                                                        "ClickfinderHelper.exe not found in OTR Remote application path");
                            _Enabled = false;
                        }
                        rk.Close();
                    }
                    else
                    {
                        PluginInterop.WriteDebugLog("Enable()",
                                                    "Registry path \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Ewe\\TVGhost\\Gemeinsames\" not found");
                        _Enabled = false;
                    }
                }
                catch (Exception e)
                {
                    PluginInterop.WriteDebugLog("Enable()", "Error while enabling plugin: " + e.Message);
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
#if !DEBUG
            try
            {
#endif
                // Register OTR Remote AddOn
                RegistryKey rk = Registry.LocalMachine;
                rk = rk.OpenSubKey("SOFTWARE\\Ewe\\TVGhost\\TVGhost", true);
                if (rk.GetValue("AddOns") != null)
                {
                    if (!rk.GetValue("AddOns").ToString().Contains("Crazysoft_OTR_Remote"))
                    {
                        rk.SetValue("AddOns", rk.GetValue("AddOns").ToString() + ",Crazysoft_OTR_Remote");
                    }
                }
                else
                {
                    rk.SetValue("AddOns", "Crazysoft_OTR_Remote");
                }

                // Write AddOn subkey
                rk = rk.CreateSubKey("AddOn_Crazysoft_OTR_Remote");
                rk.SetValue("AddOnName", "Crazysoft_OTR_Remote");
                rk.SetValue("EinbindungsModus", 2, RegistryValueKind.DWord);
                rk.SetValue("ExeDateiname",
                            System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0,
                                                                                                 System.Reflection.
                                                                                                     Assembly.
                                                                                                     GetExecutingAssembly
                                                                                                     ().Location.
                                                                                                     LastIndexOf('\\')) +
                            "\\ClickfinderHelper.exe");
                rk.SetValue("KurzBeschreibung", Lang.Clickfinder.Plugin_ShortDescription);
                rk.SetValue("LangBeschreibung", Lang.Clickfinder.Plugin_LongDescription);
                rk.SetValue("ParameterFest", "");
                rk.SetValue("ParameterZusatz", 2, RegistryValueKind.DWord);
                rk.SetValue("SpezialButtonGrafikName", "AddOn");
                rk.SetValue("SpezialButtonToolTiptext", Lang.Clickfinder.Plugin_ShortDescription);
                rk.Close();
#if !DEBUG
            }
            catch (System.Security.SecurityException)
            {
                return Lang.Clickfinder.Plugin_AccessErr;
            }
            catch (Exception excp)
            {
                return String.Format(Lang.Clickfinder.Plugin_Error, excp.Message);
            }
#endif

            FrmHowTo FrmHowTo = new FrmHowTo();
            FrmHowTo.ShowDialog();
            return null;
        }

        public void Dispose()
        {
            settings = null;
            _Enabled = false;
        }
    }
}