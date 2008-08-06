using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTR_Remote
{
    public class PluginAPI
    {
        public static ArrayList GetPlugins(string path)
        {
            PluginInterop.WriteDebugLog("GetPlugins()", "Initializing OTRAPI Version 1.0.1.0", true);
            PluginInterop.WriteDebugLog("GetPlugins()", "Loading plugin files from " + path);

            ArrayList plugins = new ArrayList();
            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                Plugin plugin = new Plugin();
#if !DEBUG
                try
                {
#endif
                    PluginInterop.WriteDebugLog("GetPlugins()", "Loading assembly file " + file);
                    plugin = AddPlugin(file);
#if !DEBUG
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("GetPlugins()", "Activator failed with error \"" + excp.Message + "\"");
                    ShowPluginError(plugin.PluginPath, excp);
                }
#endif

                if (plugin != null)
                {
                    PluginInterop.WriteDebugLog("GetPlugins()", "Plugin successfully initialized");
                    plugins.Add(plugin);
                }
            }
            return plugins;
        }

        private static Plugin AddPlugin(string filename)
        {
            Assembly pluginAssembly = Assembly.LoadFrom(filename);

            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic)
                {
                    if (!pluginType.IsAbstract)
                    {
                        Type typeInterface = pluginType.GetInterface("Crazysoft.OTR_Remote.IPlugin", true);

                        if (typeInterface != null)
                        {
                            Plugin plugin = new Plugin();
                            plugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

#if !DEBUG
                            try
                            {
#endif
                                // Initialize the plugin for further use
                                plugin.Instance.Initialize();
#if !DEBUG
                            }
                            catch (Exception excp)
                            {
                                PluginInterop.WriteDebugLog("AddPlugin()", "Initialization failed with error \"" + excp.Message + "\"");
                                ShowPluginError(plugin.PluginPath, excp);
                            }
#endif
                            return plugin;
                        }
                    }
                }
            }

            PluginInterop.WriteDebugLog("AddPlugin()", "Assembly is no valid OTR Remote plugin file");
            return null;
        }

        public static void ShowPluginError(string pluginPath, Exception e)
        {
            string pluginName = e.Source;
            if (pluginPath != null)
            {
                FileInfo fi = new FileInfo(pluginPath);
                pluginName = fi.Name;
            }

            FrmPluginError FrmPluginError = new FrmPluginError(pluginName, e);
            FrmPluginError.ShowDialog();
        }
    }

    public interface IPlugin
    {
        string Name { get; }
        bool Enabled { get; }

        void Initialize();
        bool Enable();
        PluginSettingsCollection GetSettings();
        void SetSettings(PluginSettingsCollection settings);
        object SaveRecordingScript();
        void Dispose();
    }

    public class Plugin
    {
        private IPlugin _Instance;

        public IPlugin Instance
        {
            get { return _Instance; }
            set { _Instance = value; }
        }

        private string _PluginPath;

        public string PluginPath
        {
            get { return _PluginPath; }
            set { _PluginPath = value; }
        }
    }

    public class PluginInterop
    {
        public enum MessageBoxIcon { Information, Warning, Error }

        public static void ShowMessageBox(string text, string caption, MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
                case MessageBoxIcon.Warning:
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                case MessageBoxIcon.Error:
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
            }
        }

        internal static void WriteDebugLog(string function, string text, bool createNewFile)
        {
            StreamWriter sw =
                new StreamWriter(Path.Combine(Path.GetTempPath(), "OTR_Plugins.log"), !createNewFile);
            sw.WriteLine(DateTime.Now.ToLongTimeString() + "\t" + function + "\t" + text);
            sw.Close();
        }

        public static void WriteDebugLog(string function, string text)
        {
            WriteDebugLog(function, text, false);
        }
    }
}
