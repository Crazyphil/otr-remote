using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PluginClickfinderHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exit program if no arguments are given
            if (args.Length == 0)
            {
                return;
            }

            string sender = "";
            string beginn = "";
            int dauer = 0;
            string sendung = "";
            string genre = "";

            int checksum = 0;
            // Extract variable informations
            foreach (string arg in args)
            {
                if (arg.Contains("="))
                {
                    switch (arg.Split('=')[0])
                    {
                        case "Sender":
                            sender = arg.Split('=')[1];
                            checksum += 1;
                            break;
                        case "Beginn":
                            beginn = arg.Split('=')[1];
                            checksum += 2;
                            break;
                        case "Dauer":
                            dauer = Convert.ToInt32(arg.Split('=')[1]);
                            checksum += 3;
                            break;
                        case "Sendung":
                            sendung = arg.Split('=')[1];
                            checksum += 4;
                            break;
                        case "Genre":
                            genre = arg.Split('=')[1];
                            checksum += 5;
                            break;
                    }
                }
            }

            // If not all variables were filled, exit program
            if (checksum != 15)
            {
                return;
            }

            // Convert beginn variable to OTR Remote usable data
            DateTime start = new DateTime(Convert.ToInt32(beginn.Substring(0, 4)), Convert.ToInt32(beginn.Substring(4, 2)), Convert.ToInt32(beginn.Substring(6, 2)), Convert.ToInt32(beginn.Substring(8, 2)), Convert.ToInt32(beginn.Substring(10, 2)), 0);
            DateTime end = start.AddMinutes(dauer);

            // Variables for OTR Remote
            string startdate = start.ToString("yyyy-MM-dd");
            string starttime = start.ToString("HH:mm");
            string endtime = end.ToString("HH:mm");

            try
            {
                ProcessStartInfo otrremote = new ProcessStartInfo("OTRRemote.exe");
                otrremote.Arguments = "-a -s=\"" + sender + "\" -sd=" + startdate + " -st=" + starttime + " -et=" + endtime + " -t=\"" + sendung + "\" -g=\"" + genre + "\"";
                otrremote.ErrorDialog = true;
                otrremote.WorkingDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf('\\'));
                Process.Start(otrremote);
            }
            catch (Exception excp)
            {
                Console.WriteLine("Crazysoft OTR Remote could not be started: " + excp.Message);
            }
        }
    }
}
