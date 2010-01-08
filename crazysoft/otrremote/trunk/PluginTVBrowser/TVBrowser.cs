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

                        SetPathSetting(path);

                        foundTvBrowser = true;
                    }
                }

                if (!foundTvBrowser)
                {
                    PluginInterop.WriteDebugLog("Enable()", String.Concat("Did not find any registry path starting with \"TV-Browser\" in \"", rk.Name, "\". Switching to Portable Mode."));
                    tvbrowserVersion = new Version(2, 6, 2);
                    SetPathSetting(string.Empty);
                }

                rk.Close();
                return true;
            }
            else
            {
                tvbrowserVersion = new Version(2, 6, 2);
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

                    string otrpath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "OTRRemote.exe");
                    // If the user has given a relative path to TV-Browser (= portable), also add a relative path to OTR Remote
                    if (!Path.IsPathRooted(settings[Lang.TVBrowser.Plugin_Settings_Path].Value) || settings[Lang.TVBrowser.Plugin_Settings_Path].Value[0] == Path.DirectorySeparatorChar)
                    {
                        otrpath = RelativePathTo(Path.GetFullPath(settings[Lang.TVBrowser.Plugin_Settings_Path].Value), otrpath);
                    }

                    sw.WriteLine(otrpath);
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

        /// <summary>
        /// Creates a relative path from one file
        /// or folder to another.
        /// </summary>
        /// <param name="fromDirectory">
        /// Contains the directory that defines the 
        /// start of the relative path.
        /// </param>
        /// <param name="toPath">
        /// Contains the path that defines the
        /// endpoint of the relative path.
        /// </param>
        /// <returns>
        /// The relative path from the start
        /// directory to the end path.
        /// </returns>
        /// <see cref="http://weblogs.asp.net/pwelter34/archive/2006/02/08/create-a-relative-path-code-snippet.aspx"/>
        /// <exception cref="ArgumentNullException"></exception>
        public static string RelativePathTo(string fromDirectory, string toPath)
        {
            if (fromDirectory == null)
                throw new ArgumentNullException("fromDirectory");

            if (toPath == null)
                throw new ArgumentNullException("toPath");

            bool isRooted = Path.IsPathRooted(fromDirectory) && Path.IsPathRooted(toPath);

            if (isRooted)
            {
                bool isDifferentRoot = string.Compare(Path.GetPathRoot(fromDirectory), Path.GetPathRoot(toPath), true) != 0;

                if (isDifferentRoot)
                    return toPath;
            }

            System.Collections.Specialized.StringCollection relativePath = new System.Collections.Specialized.StringCollection();
            string[] fromDirectories = fromDirectory.Split(Path.DirectorySeparatorChar);

            string[] toDirectories = toPath.Split(Path.DirectorySeparatorChar);

            int length = Math.Min(fromDirectories.Length, toDirectories.Length);

            int lastCommonRoot = -1;

            // find common root
            for (int x = 0; x < length; x++)
            {
                if (string.Compare(fromDirectories[x], toDirectories[x], true) != 0)
                    break;

                lastCommonRoot = x;
            }
            if (lastCommonRoot == -1)
                return toPath;

            // add relative folders in from path
            for (int x = lastCommonRoot + 1; x < fromDirectories.Length; x++)
                if (fromDirectories[x].Length > 0)
                    relativePath.Add("..");

            // add to folders to path
            for (int x = lastCommonRoot + 1; x < toDirectories.Length; x++)
                relativePath.Add(toDirectories[x]);

            // create relative path
            string[] relativeParts = new string[relativePath.Count];
            relativePath.CopyTo(relativeParts, 0);

            string newPath = string.Join(Path.DirectorySeparatorChar.ToString(), relativeParts);

            return newPath;
        }
    }
}
