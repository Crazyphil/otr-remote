using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public sealed class PluginInterop
    {
        private PluginInterop() { }

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
            sw.WriteLine(String.Format("{0}\t{1}\t{2}", DateTime.Now.ToLongTimeString(), function, text));
            sw.Close();
        }

        public static void WriteDebugLog(string function, string text)
        {
            WriteDebugLog(function, text, false);
        }
    }
}
