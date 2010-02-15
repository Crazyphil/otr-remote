using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace Crazysoft.OTRRemote
{
    public class PluginAPI
    {
        public List<Plugin> Plugins { get; private set; }

        private Crazysoft.Components.AppSettings settings;
        private Regex disallowedChars = new Regex(@"\W+");

        public PluginAPI(Crazysoft.Components.AppSettings settings)
        {
            this.settings = settings;
        }

        public List<Plugin> GetPlugins(string path)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            PluginInterop.WriteDebugLog("GetPlugins()", String.Concat("Initializing OTRAPI Version ", version.ToString()), true);
            PluginInterop.WriteDebugLog("GetPlugins()", String.Format("Loading plugin files from {0}", path));

            Plugins = new List<Plugin>();
            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                Plugin plugin = new Plugin();
#if !DEBUG
                try
                {
#endif
                    PluginInterop.WriteDebugLog("GetPlugins()", String.Format("Loading assembly file {0}", file));
                    plugin = AddPlugin(file);

                    if (plugin != null && !settings["Plugins"][disallowedChars.Replace(plugin.Instance.Name, "")].IsNull)
                    {
                        try
                        {
                            plugin.Instance.SetSettings((PluginSettingsCollection)settings["Plugins"][disallowedChars.Replace(plugin.Instance.Name, "")].Value);
                            PluginInterop.WriteDebugLog("GetPlugins()", "Successfully loaded plugin settings.");
                        }
                        catch (Exception excp)
                        {
                            PluginInterop.WriteDebugLog("GetPlugins()", String.Format("Failed to load plugin's settings: {0}", excp.Message));
                            ShowPluginError(plugin.PluginPath, excp);
                        }
                    }
#if !DEBUG
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("GetPlugins()", String.Format("Activator failed with error \"{0}\"", excp.Message));
                    ShowPluginError(plugin.PluginPath, excp);
                }
#endif

                if (plugin != null)
                {
                    PluginInterop.WriteDebugLog("GetPlugins()", "Plugin successfully initialized");
                    Plugins.Add(plugin);
                }
            }
            return Plugins;
        }

        private Plugin AddPlugin(string filename)
        {
            Assembly pluginAssembly = Assembly.LoadFrom(filename);

            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic)
                {
                    if (!pluginType.IsAbstract)
                    {
                        Type typeInterface = pluginType.GetInterface("Crazysoft.OTRRemote.IPlugin", true);

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
                                PluginInterop.WriteDebugLog("AddPlugin()", String.Format("Initialization failed with error \"{0}\"", excp.Message));
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

        public void UnloadPlugins(bool saveSettings)
        {
            foreach (Plugin plugin in Plugins)
            {
                try
                {
                    PluginInterop.WriteDebugLog("UnloadPlugins()", String.Format("Saving settings for plugin \"{0}\"", plugin.Instance.Name));
                    settings.Sections["Plugins"].Keys[disallowedChars.Replace(plugin.Instance.Name, "")].Value = plugin.Instance.GetSettings();
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("UnloadPlugins()", String.Format("Failed to save plugin's settings: {0}", excp.Message));
                    ShowPluginError(plugin.PluginPath, excp);
                }

                try
                {
                    PluginInterop.WriteDebugLog("UnloadPlugins()", String.Format("Unloading plugin \"{0}\"", plugin.Instance.Name));
                    plugin.Instance.Dispose();
                }
                catch (Exception excp)
                {
                    PluginInterop.WriteDebugLog("UnloadPlugins()", String.Format("Failed to unload plugin: {0}", excp.Message));
                    ShowPluginError(plugin.PluginPath, excp);
                }
            }

            if (saveSettings)
            {
                // Legacy Mode *MUST* be disabled to successfully save the settings
                settings.UpdateLegacyData();
                settings.Save();
            }
        }

        public void ShowPluginError(string pluginPath, Exception exception)
        {
            string pluginName = exception.Source;
            if (pluginPath != null)
            {
                FileInfo fi = new FileInfo(pluginPath);
                pluginName = fi.Name;
            }

            FrmPluginError FrmPluginError = new FrmPluginError(pluginName, exception);
            FrmPluginError.ShowDialog();
        }
    }
}
