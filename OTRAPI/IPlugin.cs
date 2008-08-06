using System;
using System.Collections.Generic;
using System.Text;

namespace Crazysoft.OTRRemote
{
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
}
