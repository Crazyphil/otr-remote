/*
 * Crazysoft OTR Remote Sample Plugin
 * Copyright (C) Crazysoft 2006
 * 
 * This sample shows, how to develop a plugin for OTR Remote
 * easily and make correct usage of the Plugin Interface.
 * 
 * To be able to use the inteface, please add a Reference
 * to OTRAPI.dll in your project settings.
 * 
 * If you have developed a useful plugin and want it to
 * be listed on our homepage, please contact us on our
 * homepage http://www.crazysoft.net.ms/.
 * We are pleased to see some interesting work from you.
 */

using System;

// The following namespaces are not necessary to
// include. They are used for this specific
// sample plugin to work.
using System.Collections.Generic;
using System.IO;
using System.Text;

// This namespace is important if you don't
// want to always write e.g. Crazysoft.OTR_Remote.PluginSettingsCollection
using Crazysoft.OTR_Remote;

// Your plugin *MUST* inherit from the IPlugin interface
// to be able to communicate with OTR Remote.
// If you don't inherit from it, it will not be included
// in the plugin list in the Control Panel.
public class Sample : IPlugin
{
    // This variable holds all plugin's settings.
    // It will be initialized in the Initialize() method.
    private PluginSettingsCollection settings;

    // This variable holds the plugin's name.
    // You could also directly use the Name property.
    private string _Name = "Sample Plugin";
    public string Name
    {
        get { return _Name; }
    }

    // This variable indicates if the plugin is available.
    // It is initialized in the Enable() method.
    private bool _Enabled;
    public bool Enabled
    {
        get { return _Enabled; }
    }

    public void Initialize()
    {
        // Look for the user's My Documents folder
        string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Initialize the settings variable for adding the plugin's settings
        settings = new PluginSettingsCollection();

        // Add the path to the collection and give it a descriptive name
        settings.Add("Destination Folder", myDocuments, Crazysoft.OTR_Remote.ValueType.String);

        // Add the output filename to the collection
        settings.Add("Destination Filename", "OTRRemote.bat", Crazysoft.OTR_Remote.ValueType.String);
    }

    public bool Enable()
    {
        try
        {
            // Check, if the file can be written at the destination path
            // We get the full path by adding folder and filename
            FileInfo fi = new FileInfo(settings["Destination Folder"].Value + "\\" + settings["Destination Filename"].Value);
            FileStream fs = fi.OpenWrite();
            fs.Close();
            fi.Delete();

            _Enabled = true;
        }
        catch
        {
            _Enabled = false;
        }

        return _Enabled;
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
        // Check, if the destination folder is set
        if (settings["Destination Folder"].Value == "")
        {
            return "Please enter the folder you want to save the script in.";
        }

        // Check, if the destination folder exists
        DirectoryInfo di = new DirectoryInfo(settings["Destination Folder"].Value);
        if (di.Exists)
        {
            // Check, if the destination filename is set
            if (settings["Destination Filename"].Value == "")
            {
                return "Please enter the name of the script.";
            }

            // Instantiate a StreamWriter to write the script file
            StreamWriter sw = new StreamWriter(di.FullName + "\\" + settings["Destination Filename"].Value);

            // Get the path, where OTR Remote is in
            string installPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf('\\'));

            // Add a (nonsense) line to the file, which starts OTR Remote
            // This won't work, because the date is a fixed date in the past
            sw.WriteLine("\"" + installPath + "\\OTRRemote.exe" + "\" -a -s=\"CNN\" -sd=2006-10-29 -st=20:00 -et=20:05 -t=\"News\"");
            sw.Close();
        }
        else
        {
            return "The destination folder \"" + di.FullName + "\" was not found. Please check the \"Destination Folder\" setting.";
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