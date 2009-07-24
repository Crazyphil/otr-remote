using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Crazysoft.Components;

[assembly: CLSCompliant(true)]
namespace Crazysoft.OTRRemote
{
    static class Program
    {
        private static AppSettings _Settings;
        public static AppSettings Settings
        {
            get { return _Settings; }
            set { _Settings = value; }
        }
	
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // COMPAT: Removed with new AppSettings v2.0
            /*_Settings = new AppSettings(String.Format("{1}{0}{2}{0}{3}{0}{4}", System.IO.Path.DirectorySeparatorChar,
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Crazysoft", "OTR Remote",
                                "settings.xml"));*/
            //_Settings.Load();
            _Settings = new AppSettings(true);

#if !DEBUG
            try
            {
#endif
                if (args.Length == 0)
                {
                    Application.Run(new FrmMain());
                    return 0;
                }
                else
                {
                    if ((Array.IndexOf(args, "-a") == -1 && Array.IndexOf(args, "-r") == -1) || Array.IndexOf(args, "-h") != -1)
                    {
                        Application.Run(new FrmHelp());
                        return 0;
                    }
                    else
                    {
                        bool addShow = false;

                        // Struct for keeping information
                        RecI.RecordingInfo recInfo = new RecI.RecordingInfo();

                        // Variable for error messages
                        string message = String.Empty;

                        // Parse Add/Remove argument
                        if (Array.IndexOf(args, "-a") != -1)
                        {
                            addShow = true;
                        }
                        else
                        {
                            addShow = false;
                        }

                        // Parse Station argument
                        int stationPos = Array.FindIndex(args, FindStationArg);
                        if (stationPos != -1)
                        {
                            if (args[stationPos].Length > 3)
                            {
                                // Replace EPG station names with OTR's
                                Stations stations = new Stations();
                                stations.Load();
                                recInfo.Station = args[stationPos].Substring(3).Replace("\"", String.Empty);
                                Station replacement = stations.Find(recInfo.Station);
                                if (replacement != null)
                                {
                                    recInfo.Station = replacement.Name;
                                }
                            }
                        }

                        // Parse StartDate argument
                        int sdPos = Array.FindIndex(args, FindStartDateArg);
                        if (sdPos != -1)
                        {
                            if (args[sdPos].Length > 4)
                            {
                                string[] date = args[sdPos].Substring(4).Split('-');
                                if (date.Length == 3)
                                {
                                    try
                                    {
                                        recInfo.StartDate = DateTime.Parse(String.Format("{0:D2}-{1:D2}-{2:D2}", Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2])));
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }

                        // Parse StartTime argument
                        int stPos = Array.FindIndex(args, FindStartTimeArg);
                        if (stPos != -1)
                        {
                            if (args[stPos].Length > 4)
                            {
                                string[] time = args[stPos].Substring(4).Split(':');
                                if (time.Length == 2)
                                {
                                    try
                                    {
                                        int hour = Convert.ToInt32(time[0]);
                                        int minute = Convert.ToInt32(time[1]);
                                        int endtimepos = Array.FindIndex(args, FindEndTimeArg);

                                        // Declare the given start and end date and time as DateTime
                                        // and convert it to UTC (MEZ - 1)
                                        DateTime argStartTime =
                                            DateTime.Parse(String.Concat(args[sdPos].Substring(args[sdPos].IndexOf('=') + 1), " ",
                                                           args[stPos].Substring(args[stPos].IndexOf('=') + 1)));
                                        if (IsEuropeanDaylightSavingTime(argStartTime))
                                        {
                                            argStartTime = argStartTime.AddHours(-2);
                                        }
                                        else
                                        {
                                            argStartTime = argStartTime.AddHours(-1);
                                        }
                                        DateTime argEndTime;
                                        if (Convert.ToInt32(args[endtimepos].Substring(args[endtimepos].IndexOf('=') + 1).Split(':')[0]) < argStartTime.Hour)
                                        {
                                            argEndTime =
                                                DateTime.Parse(String.Concat(argStartTime.AddDays(1).ToShortDateString(), " ",
                                                               args[endtimepos].Substring(
                                                                   args[endtimepos].IndexOf('=') + 1)));
                                        }
                                        else
                                        {
                                            argEndTime =
                                                DateTime.Parse(String.Concat(args[sdPos].Substring(args[sdPos].IndexOf('=') + 1), " ",
                                                               args[endtimepos].Substring(
                                                                   args[endtimepos].IndexOf('=') + 1)));
                                        }
                                        if (IsEuropeanDaylightSavingTime(argEndTime))
                                        {
                                            argEndTime = argEndTime.AddHours(-2);
                                        }
                                        else
                                        {
                                            argEndTime = argEndTime.AddHours(-1);
                                        }

                                        // Declare local date and time as DateTime
                                        // and convert it to UTC
                                        DateTime localTime = DateTime.Now;
                                        int winterTime =
                                            TimeZone.CurrentTimeZone.GetUtcOffset(
                                                TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Today.Year).End.
                                                    AddDays(1)).Hours;
                                        if (localTime.IsDaylightSavingTime())
                                        {
                                            int summerTime =
                                                TimeZone.CurrentTimeZone.GetUtcOffset(
                                                    TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Today.Year).Start.
                                                        AddDays(1)).Hours;
                                            localTime = localTime.AddHours(winterTime - summerTime);
                                        }
                                        localTime = localTime.AddHours(winterTime * -1);

                                        if (argStartTime.Year == localTime.Year && argStartTime.Month == localTime.Month && argStartTime.Day == localTime.Day)
                                        {
                                            // Check, if show has already ended...
                                            if ((localTime.Day == argEndTime.Day && (localTime.Hour < argEndTime.Hour || (localTime.Hour == argEndTime.Hour && localTime.Minute < argEndTime.Minute))) || localTime.Day < argEndTime.Day)
                                            {
                                                // ... and adjust the start time, if the user wants us to
                                                if (addShow && !Settings["Program"]["AdjustStartTime"].IsNull && Convert.ToBoolean(Settings["Program"]["AdjustStartTime"].Value))
                                                {
                                                    if (argStartTime.Hour < localTime.Hour)
                                                    {
                                                        if (localTime.Minute < 59)
                                                        {
                                                            if (IsEuropeanDaylightSavingTime(localTime))
                                                            {
                                                                hour = localTime.Hour + 2;
                                                            }
                                                            else
                                                            {
                                                                hour = localTime.Hour + 1;
                                                            }
                                                            minute = localTime.Minute + 1;
                                                        }
                                                        else
                                                        {
                                                            if (IsEuropeanDaylightSavingTime(localTime))
                                                            {
                                                                hour = localTime.Hour + 3;
                                                            }
                                                            else
                                                            {
                                                                hour = localTime.Hour + 2;
                                                            }
                                                            minute = 0;
                                                        }
                                                    }
                                                    else if (argStartTime.Hour == localTime.Hour &&
                                                             argStartTime.Minute <= localTime.Minute)
                                                    {
                                                        if (localTime.Minute < 59)
                                                        {
                                                            minute = localTime.Minute + 1;
                                                        }
                                                        else
                                                        {
                                                            if (IsEuropeanDaylightSavingTime(localTime))
                                                            {
                                                                hour = localTime.Hour + 3;
                                                            }
                                                            else
                                                            {
                                                                hour = localTime.Hour + 2;
                                                            }
                                                            minute = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        recInfo.StartTime = DateTime.Parse(String.Format("{0:D2}:{1:D2}", hour, minute));
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }

                        // Parse EndTime argument
                        int etPos = Array.FindIndex(args, FindEndTimeArg);
                        if (etPos != -1)
                        {
                            if (args[etPos].Length > 4)
                            {
                                string[] time = args[etPos].Substring(4).Split(':');
                                if (time.Length == 2)
                                {
                                    try
                                    {
                                        int hour = Convert.ToInt32(time[0]);
                                        int minute = Convert.ToInt32(time[1]);

                                        // Add 5 minutes to end time, if next show should also be recorded
                                        if (addShow && !Settings["Program"]["RecordFollowing"].IsNull && Convert.ToBoolean(Settings["Program"]["RecordFollowing"].Value))
                                        {
                                            if (minute < 55)
                                            {
                                                minute += 5;
                                            }
                                            else
                                            {
                                                if (hour < 23)
                                                {
                                                    hour++;
                                                }
                                                else
                                                {
                                                    hour = 0;
                                                }
                                                minute = (minute - 60) + 5;
                                            }
                                        }

                                        recInfo.EndTime = DateTime.Parse(String.Format("{0:D2}:{1:D2}", hour, minute));
                                    }
                                    catch
                                    {
                                    }

                                    // To avoid recording all shows in 24 hours, add 1 minute to endtime, if it is unknown
                                    // (endtime = starttime, as given by EPG program)
                                    if (recInfo.EndTime.TimeOfDay == recInfo.StartTime.TimeOfDay) {
                                        recInfo.EndTime = recInfo.EndTime.AddMinutes(1);
                                    }
                                }
                            }
                        }

                        // Optional arguments
                        // Parse Title argument
                        int titlePos = Array.FindIndex(args, FindTitleArg);
                        if (titlePos != -1)
                        {
                            if (args[titlePos].Length > 3)
                            {
                                recInfo.Title = args[titlePos].Substring(3);
                            }
                        }

                        // Parse Genre argument
                        int genrePos = Array.FindIndex(args, FindGenreArg);
                        if (genrePos != -1)
                        {
                            if (args[genrePos].Length > 3)
                            {
                                recInfo.Genre = args[genrePos].Substring(3);
                            }
                        }

                        // Parse User argument
                        int userPos = Array.FindIndex(args, FindUserArg);
                        if (userPos != -1)
                        {
                            if (args[userPos].Length > 3)
                            {
                                recInfo.User = args[userPos].Substring(3);
                            }
                        }
                        else if (!Settings["Program"]["Username"].IsNull && !String.IsNullOrEmpty(Settings["Program"]["Username"].Value.ToString()))
                        {
                            recInfo.User = Settings["Program"]["Username"].Value.ToString();
                        }

                        // Parse Password argument
                        int passPos = Array.FindIndex(args, FindPasswordArg);
                        if (passPos != -1)
                        {
                            if (args[passPos].Length > 3)
                            {
                                recInfo.Password = args[passPos].Substring(3);
                            }
                        }
                        else if (!Settings["Program"]["Password"].IsNull && !String.IsNullOrEmpty(Settings["Program"]["Password"].Value.ToString()))
                        {
                            recInfo.Password = Encryption.Encryption.Decrypt(Settings["Program"]["Password"].Value.ToString(), Settings["Program"]["Username"].Value.ToString());
                        }

                        // Parse Timezone argument
                        int tzPos = Array.FindIndex(args, FindTimezoneArg);
                        if (tzPos != -1)
                        {
                            if (args[tzPos].Length > 4)
                            {
                                try
                                {
                                    int tz = Convert.ToInt32(args[tzPos].Substring(4));

                                    recInfo.Timezone = tz;
                                }
                                catch
                                {
                                    recInfo.Timezone = GetTimezone();
                                }
                            }
                            else
                            {
                                recInfo.Timezone = GetTimezone();
                            }
                        }
                        else
                        {
                            recInfo.Timezone = GetTimezone();
                        }

                        // Find unset variables (= unspecified args) and throw an error
                        if (Array.IndexOf(args, "-a") == -1 && Array.IndexOf(args, "-r") == -1)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_Action, "\n");
                        }
                        if (String.IsNullOrEmpty(recInfo.Station))
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_Station, "\n");
                        }
                        if (recInfo.StartDate == null)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_StartDate, "\n");
                        }
                        if (recInfo.StartTime == null)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_StartTime, "\n");
                        }
                        if (recInfo.EndTime == null)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_EndTime, "\n");
                        }

                        if (!String.IsNullOrEmpty(message))
                        {
                            if (MessageBox.Show(String.Format(Lang.OTRRemote.CmdLine_ErrorMsg_Text, message), String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.CmdLine_ErrorMsg_Title), MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                Application.Run(new FrmHelp());
                            }

                            return 1;
                        }

                        // Initialize request URL and object
                        // Warn the user, if he tries to remove a job
                        string requeststr = "http://www.onlinetvrecorder.com/?aktion=";
                        if (addShow)
                        {
                            requeststr = String.Concat(requeststr, "createJob");
                        }
                        else
                        {
                            // Ask the user if he knows what he does, before we really delete the recording
                            if (MessageBox.Show(String.Format(Lang.OTRRemote.CmdLine_DeleteMsg_Text, String.IsNullOrEmpty(recInfo.Title) ? Lang.OTRRemote.CmdLine_DeleteMsg_ThisShow : recInfo.Title, recInfo.StartDate.ToShortDateString(), recInfo.StartTime.ToShortTimeString()), String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.CmdLine_DeleteMsg_Title), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                requeststr = String.Concat(requeststr, "deleteJob");
                            }
                            else
                            {
                                return 1;
                            }
                        }

                        // If username and/or password are not set, call the login form
                        if (String.IsNullOrEmpty(recInfo.User) || String.IsNullOrEmpty(recInfo.Password))
                        {
                            FrmLogin FrmLogin = new FrmLogin(recInfo.User, recInfo.Password, recInfo.Timezone);
                            Application.Run(FrmLogin);

                            if (FrmLogin.DialogResult == DialogResult.Cancel)
                            {
                                return 1;
                            }
                            else
                            {
                                recInfo.User = FrmLogin.Username;
                                recInfo.Password = FrmLogin.Password;
                                recInfo.Timezone = FrmLogin.Timezone;
                            }
                        }

                        requeststr = String.Concat(requeststr, "&tvStation=", recInfo.Station);
                        requeststr = String.Concat(requeststr, String.Format("&startDate_year={0:D2}&startDate_month={1:D2}&startDate_day={2:D2}", recInfo.StartDate.Year, recInfo.StartDate.Month, recInfo.StartDate.Day));
                        requeststr = String.Concat(requeststr, String.Format("&startTime_hour={0:D2}&startTime_minute={1:D2}", recInfo.StartTime.Hour, recInfo.StartTime.Minute));
                        requeststr = String.Concat(requeststr, String.Format("&endTime_hour={0:D2}&endTime_minute={1:D2}", recInfo.EndTime.Hour, recInfo.EndTime.Minute));

                        //requeststr = String.Concat(requeststr, "&email=", recInfo.User, "&pass=", recInfo.Password);

                        string urlTitle = recInfo.Title;
                        if (!String.IsNullOrEmpty(recInfo.Title))
                        {
                            // Convert special characters to readable chars
                            urlTitle = urlTitle.Replace("ä", "ae");
                            urlTitle = urlTitle.Replace("ö", "oe");
                            urlTitle = urlTitle.Replace("ü", "ue");
                            urlTitle = urlTitle.Replace("ß", "ss");
                            urlTitle = urlTitle.Replace("&", "und");

                            if (!addShow)
                            {
                                // Also convert URL-necessary chars
                                urlTitle = urlTitle.Replace(" ", "+");
                                urlTitle = urlTitle.Replace("-", "+");
                                Regex regex = new Regex("[^0-9A-Za-z+.]");
                                urlTitle = regex.Replace(urlTitle, "");
                                urlTitle = urlTitle.Replace("+++", "+");
                                urlTitle = urlTitle.Replace("++", "+");
                            }

                            requeststr = String.Concat(requeststr, "&titleName=", urlTitle.Replace("\"", ""));
                        }
                        if (!String.IsNullOrEmpty(recInfo.Genre))
                        {
                            requeststr = String.Concat(requeststr, "&genre=", recInfo.Genre.Replace("\"", ""));
                        }
                        
                        requeststr = String.Concat(requeststr, "&timezone=", recInfo.Timezone);

                        // With adding following parameter, OTR returns the result as "TVM2OTR:OK" or "TVM2OTR:ERROR",
                        // except when not logged in (wrong username/password), where a complete webpage is returned
                        requeststr = String.Concat(requeststr, "&tvm2otr=true");
#if DEBUG
                        System.Threading.Thread.Sleep(7000);
#endif
                        Uri requestUri = new Uri(Uri.EscapeUriString(requeststr));
                        FrmProgress FrmProgress = new FrmProgress(requestUri, recInfo);
                        Application.Run(FrmProgress);

                        if (FrmProgress.DialogResult == DialogResult.OK)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
#if !DEBUG
            }
            catch (Exception uncaughtExcp)
            {
                if (!Application.MessageLoop)
                {
                    Application.Run(new CrashHandler.FrmCrash(Application.ProductName, Application.ProductVersion, uncaughtExcp));
                }
                else
                {
                    Form.ActiveForm.ShowDialog(new CrashHandler.FrmCrash(Application.ProductName, Application.ProductVersion, uncaughtExcp));
                }

                return 1;
            }
#endif
        }

        private static bool FindStationArg(string element)
        {
            if (element.StartsWith("-s="))
            {
                return true;
            }
            return false;
        }

        private static bool FindStartDateArg(string element)
        {
            if (element.StartsWith("-sd="))
            {
                return true;
            }
            return false;
        }

        private static bool FindStartTimeArg(string element)
        {
            if (element.StartsWith("-st="))
            {
                return true;
            }
            return false;
        }

        private static bool FindEndTimeArg(string element)
        {
            if (element.StartsWith("-et="))
            {
                return true;
            }
            return false;
        }

        private static bool FindTitleArg(string element)
        {
            if (element.StartsWith("-t="))
            {
                return true;
            }
            return false;
        }

        private static bool FindGenreArg(string element)
        {
            if (element.StartsWith("-g="))
            {
                return true;
            }
            return false;
        }

        private static bool FindUserArg(string element)
        {
            if (element.StartsWith("-u="))
            {
                return true;
            }
            return false;
        }

        private static bool FindPasswordArg(string element)
        {
            if (element.StartsWith("-p="))
            {
                return true;
            }
            return false;
        }

        private static bool FindTimezoneArg(string element)
        {
            if (element.StartsWith("-tz="))
            {
                return true;
            }
            return false;
        }

        private static int GetTimezone()
        {
            if (Settings["Program"]["Timezone"].IsNull)
            {
                return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            }
            else
            {
                return Convert.ToInt32(Settings["Program"]["Timezone"].Value);
            }
        }

        private static bool IsEuropeanDaylightSavingTime(DateTime time)
        {
            if (time.Month > 3 && time.Month < 10)
            {
                return true;
            }
            else
            {
                if (time.Month == 3)
                {
                    int lastSunday = 0;
                    DateTime findSunday = new DateTime(time.Year, time.Month, 1);
                    for (int i = 2; i <= 31; i++)
                    {
                        if (findSunday.DayOfWeek == DayOfWeek.Sunday)
                        {
                            lastSunday = findSunday.Day;
                        }
                        findSunday = findSunday.AddDays(1);
                    }
                    if (time.Day == lastSunday)
                    {
                        if (time.Hour > 2)
                        {
                            return true;
                        }
                    }
                    else if (time.Day > lastSunday)
                    {
                        return true;
                    }
                }
                else if (time.Month == 10)
                {
                    int lastSunday = 0;
                    DateTime findSunday = new DateTime(time.Year, time.Month, 1);
                    for (int i = 2; i <= 31; i++)
                    {
                        if (findSunday.DayOfWeek == DayOfWeek.Sunday)
                        {
                            lastSunday = findSunday.Day;
                        }
                        findSunday = findSunday.AddDays(1);
                    }
                    if (time.Day == lastSunday)
                    {
                        if (time.Hour < 3)
                        {
                            return true;
                        }
                    }
                    else if (time.Day < lastSunday)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public static class RecI
    {
        public struct RecordingInfo
        {
            public string Station;
            public DateTime StartDate;
            public DateTime StartTime;
            public DateTime EndTime;

            // Optional arguments
            public string Title;
            public string Genre;
            public string User;
            public string Password;
            public int Timezone;
        }
    }
}