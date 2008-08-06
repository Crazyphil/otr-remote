using System;
using System.Collections.Generic;
using System.Text;

namespace Crazysoft.OTRRemote
{
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
}
