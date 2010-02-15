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
            // Change culture to English for help screenshots
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
            //Lang.OTRRemote.Culture = System.Globalization.CultureInfo.InvariantCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _Settings = new AppSettings(true, true);

#if !DEBUG
            try
            {
#endif
                // Avoid a Linux commandline bug, where space always begins a new arg, even if under quotes
                args = MergeLinuxCmdArgs(args);

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
                        RecordingInfo[] recInfo = new RecordingInfo[1];

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

                        recInfo[0].RemoveMode = !addShow;

                        #region Required arguments
                        #region Parse Station argument
                        int stationPos = Array.FindIndex(args, FindStationArg);
                        if (stationPos != -1)
                        {
                            if (args[stationPos].Length > 3)
                            {
                                // Replace EPG station names with OTR's
                                Stations stations = new Stations();
                                stations.Load();
                                recInfo[0].Station = args[stationPos].Substring(3).Replace("\"", String.Empty);
                                Station replacement = stations.Find(recInfo[0].Station);
                                if (replacement != null)
                                {
                                    recInfo[0].Station = replacement.Name;
                                }
                            }
                        }
                        #endregion

                        #region Parse StartDate argument
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
                                        recInfo[0].StartDate = DateTime.Parse(String.Format("{0:D2}-{1:D2}-{2:D2}", Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2])));
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Parse StartTime argument
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

                                        recInfo[0].StartTime = DateTime.Parse(String.Format("{0:D2}:{1:D2}", hour, minute));
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Parse EndTime argument
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

                                        recInfo[0].EndTime = DateTime.Parse(String.Format("{0:D2}:{1:D2}", hour, minute));
                                    }
                                    catch
                                    {
                                    }

                                    // To avoid recording all shows in 24 hours, add 2 minutes to endtime, if it is unknown
                                    // (endtime = starttime or endtime = starttime - 1, as given by EPG program)
                                    if (recInfo[0].EndTime.TimeOfDay == recInfo[0].StartTime.TimeOfDay || recInfo[0].EndTime.TimeOfDay == recInfo[0].StartTime.AddMinutes(-1).TimeOfDay)
                                    {
                                        recInfo[0].EndTime = recInfo[0].EndTime.AddMinutes(2);
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Optional arguments
                        #region Parse Title argument
                        int titlePos = Array.FindIndex(args, FindTitleArg);
                        if (titlePos != -1)
                        {
                            if (args[titlePos].Length > 3)
                            {
                                recInfo[0].Title = args[titlePos].Substring(3);
                            }
                        }
                        #endregion

                        #region Parse Genre argument
                        int genrePos = Array.FindIndex(args, FindGenreArg);
                        if (genrePos != -1)
                        {
                            if (args[genrePos].Length > 3)
                            {
                                recInfo[0].Genre = args[genrePos].Substring(3);
                            }
                        }
                        #endregion

                        #region Parse User argument
                        int userPos = Array.FindIndex(args, FindUserArg);
                        if (userPos != -1)
                        {
                            if (args[userPos].Length > 3)
                            {
                                recInfo[0].User = args[userPos].Substring(3);
                            }
                        }
                        else if (!Settings["Program"]["Username"].IsNull && !String.IsNullOrEmpty(Settings["Program"]["Username"].Value.ToString()))
                        {
                            recInfo[0].User = Settings["Program"]["Username"].Value.ToString();
                        }
                        #endregion

                        #region Parse Password argument
                        int passPos = Array.FindIndex(args, FindPasswordArg);
                        if (passPos != -1)
                        {
                            if (args[passPos].Length > 3)
                            {
                                recInfo[0].Password = args[passPos].Substring(3);
                            }
                        }
                        else if (!Settings["Program"]["Password"].IsNull && !String.IsNullOrEmpty(Settings["Program"]["Password"].Value.ToString()))
                        {
                            recInfo[0].Password = Encryption.Encryption.Decrypt(Settings["Program"]["Password"].Value.ToString(), Settings["Program"]["Username"].Value.ToString());
                        }
                        #endregion

                        #region Parse Timezone argument
                        int tzPos = Array.FindIndex(args, FindTimezoneArg);
                        if (tzPos != -1)
                        {
                            if (args[tzPos].Length > 4)
                            {
                                try
                                {
                                    int tz = Convert.ToInt32(args[tzPos].Substring(4));

                                    recInfo[0].Timezone = tz;
                                }
                                catch
                                {
                                    recInfo[0].Timezone = GetTimezone();
                                }
                            }
                            else
                            {
                                recInfo[0].Timezone = GetTimezone();
                            }
                        }
                        else
                        {
                            recInfo[0].Timezone = GetTimezone();
                        }
                        #endregion
                        #endregion

                        // Find unset variables (= unspecified args) and throw an error
                        if (Array.IndexOf(args, "-a") == -1 && Array.IndexOf(args, "-r") == -1)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_Action, "\n");
                        }
                        if (String.IsNullOrEmpty(recInfo[0].Station))
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_Station, "\n");
                        }
                        if (recInfo[0].StartDate == null)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_StartDate, "\n");
                        }
                        if (recInfo[0].StartTime == null)
                        {
                            message = String.Concat(message, "\u2022 ", Lang.OTRRemote.CmdLine_Error_StartTime, "\n");
                        }
                        if (recInfo[0].EndTime == null)
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

                        // Indicate, if the Recording Preview dialog should be shown according to the preferences
                        bool recordingPreview = Settings["Program"]["RecordingPreview"].IsNull || (!Settings["Program"]["RecordingPreview"].IsNull && Convert.ToBoolean(Settings["Program"]["RecordingPreview"].Value));

                        // Initialize request URL and object
                        // Warn the user if he tries to remove a job
                        recInfo[0].RequestString = "http://www.onlinetvrecorder.com/?aktion=";
                        if (addShow)
                        {
                            recInfo[0].RequestString = String.Concat(recInfo[0].RequestString, "createJob");
                        }
                        else
                        {
                            // Ask the user if he knows what he does, before we really delete the recording
                            if (recordingPreview || MessageBox.Show(String.Format(Lang.OTRRemote.CmdLine_DeleteMsg_Text, String.IsNullOrEmpty(recInfo[0].Title) ? Lang.OTRRemote.CmdLine_DeleteMsg_ThisShow : String.Concat("\"", recInfo[0].Title, "\""), recInfo[0].StartDate.ToShortDateString(), recInfo[0].StartTime.ToShortTimeString()), String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.CmdLine_DeleteMsg_Title), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                recInfo[0].RequestString = String.Concat(recInfo[0].RequestString, "deleteJob");
                            }
                            else
                            {
                                return 1;
                            }
                        }

                        // If username and/or password are not set, call the login form
                        if (String.IsNullOrEmpty(recInfo[0].User) || String.IsNullOrEmpty(recInfo[0].Password))
                        {
                            FrmLogin FrmLogin = new FrmLogin(recInfo[0].User, recInfo[0].Password, recInfo[0].Timezone);
                            Application.Run(FrmLogin);

                            if (FrmLogin.DialogResult == DialogResult.Cancel)
                            {
                                return 1;
                            }
                            else
                            {
                                recInfo[0].User = FrmLogin.Username;
                                recInfo[0].Password = FrmLogin.Password;
                                recInfo[0].Timezone = FrmLogin.Timezone;
                            }
                        }

                        // Show recording preview dialog with series programming
                        if (recordingPreview)
                        {
                            FrmRecordingPreview FrmRecordingPreview = new FrmRecordingPreview(recInfo[0]);
                            Application.Run(FrmRecordingPreview);

                            if (FrmRecordingPreview.DialogResult == DialogResult.Cancel)
                            {
                                return 1;
                            }
                            else
                            {
                                recInfo = FrmRecordingPreview.RecordingInfo;
                            }
                        }

                        // Do further processing and recording for each recording info
                        int i = 0;
                        foreach (RecordingInfo ri in recInfo)
                        {
                            // Finish request string
                            recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, "&tvStation=", ri.Station);
                            recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, String.Format("&startDate_year={0:D2}&startDate_month={1:D2}&startDate_day={2:D2}", ri.StartDate.Year, ri.StartDate.Month, ri.StartDate.Day));
                            recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, String.Format("&startTime_hour={0:D2}&startTime_minute={1:D2}", ri.StartTime.Hour, ri.StartTime.Minute));
                            recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, String.Format("&endTime_hour={0:D2}&endTime_minute={1:D2}", ri.EndTime.Hour, ri.EndTime.Minute));

                            // Check for illegal characters in every recording info
                            string urlTitle = ri.Title;
                            if (!String.IsNullOrEmpty(ri.Title))
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

                                recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, "&titleName=", urlTitle.Replace("\"", ""));
                            }
                            if (!String.IsNullOrEmpty(ri.Genre))
                            {
                                recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, "&genre=", ri.Genre.Replace("\"", ""));
                            }

                            recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, "&timezone=", ri.Timezone);

                            // With adding following parameter, OTR returns the result as "TVM2OTR:OK" or "TVM2OTR:ERROR",
                            // except when not logged in (wrong username/password), where a complete webpage is returned
                            recInfo[i].RequestString = String.Concat(recInfo[i].RequestString, "&tvm2otr=true");

                            i++;
                        }

#if DEBUG
                        System.Threading.Thread.Sleep(7000);
#endif
                        FrmProgress FrmProgress = new FrmProgress(recInfo);

                        // Check if the form should be loaded or if the BackgroundWorker should be started directly
                        switch (FrmProgress.DisplayMode)
                        {
                            case FrmProgress.FormDisplayMode.ShowWindow:
                                Application.Run(FrmProgress);
                                break;
                            case FrmProgress.FormDisplayMode.ShowSystray:
                                FrmProgress.ShowDialog();
                                break;
                            case FrmProgress.FormDisplayMode.Hide:
                                FrmProgress.StartRecordThread(FrmProgress.DisplayMode);
                                while (FrmProgress.IsWorking)
                                {
                                    System.Threading.Thread.Sleep(100);
                                }
                                break;
                        }

                        int programResult = 1;
                        if (FrmProgress.DialogResult == DialogResult.OK)
                        {
                            programResult = 0;
                        }

                        return programResult;
                    }
                }
#if !DEBUG
            }
            catch (Exception uncaughtExcp)
            {
                if (Application.OpenForms.Count == 0)
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

        private static string[] MergeLinuxCmdArgs(string[] args)
        {
            if (Settings.RunningOnUnix())
            {
                System.Collections.Generic.List<string> arglist = new System.Collections.Generic.List<string>();
                bool unclosedString = false;
                foreach (string arg in args)
                {
                    if ((!String.IsNullOrEmpty(arg) && arg[0].CompareTo('-') != 0) || unclosedString)
                    {
                        if (arglist.Count > 0)
                        {
                            arglist[arglist.Count - 1] = String.Concat(arglist[arglist.Count - 1], " ", arg);
                        }
                    }
                    else
                    {
                        arglist.Add(arg);

                        // If an uneven number of quotes is in the arg, force start or end of merging
                        if ((arg.Length - arg.Replace("\"", "").Length) % 2 != 0)
                        {
                            unclosedString = !unclosedString;
                        }
                    }
                }

                return arglist.ToArray();
            }
            else
            {
                return args;
            }
        }

        public static void TranslateControls(Form frm)
        {
            foreach (Control ctl in frm.Controls)
            {
                TranslateControl(frm, ctl);
            }
        }

        private static void TranslateControl(Form frm, Control ctl)
        {
            string text = Lang.OTRRemote.ResourceManager.GetString(String.Concat(frm.Name, "_", ctl.Name));
            if (text != null)
            {
                ctl.Text = text;
            }

            foreach (Control subCtl in ctl.Controls)
            {
                TranslateControl(frm, subCtl);
            }
        }

        public static void StartAppUpdate()
        {
            // Only enable auto-update on Windows
            if (!Program.Settings.RunningOnUnix())
            {
                // Call AppUpdate if user enabled auto update and last update is at least 7 days old
                if (Settings["Program"]["EnableAutoUpdate"].IsNull || Convert.ToBoolean(Settings["Program"]["EnableAutoUpdate"].Value))
                {
                    DateTime lastUpdate = new DateTime(0);
                    if (!Settings["Program"]["LastAutoUpdate"].IsNull)
                    {
                        lastUpdate = new DateTime(Convert.ToInt64(Settings["Program"]["LastAutoUpdate"].Value));
                    }

                    if (lastUpdate <= DateTime.Today.AddDays(-7))
                    {
                        // Check if AppUpdate is installed and start update
                        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Crazysoft\\AppUpdate");

                        if (rk != null && rk.GetValue("InstallationPath", null) != null)
                        {
                            try
                            {
                                System.Diagnostics.ProcessStartInfo updateApp =
                                    new System.Diagnostics.ProcessStartInfo(System.IO.Path.Combine(rk.GetValue("InstallationPath").ToString(), "AppUpdate.exe"));
                                updateApp.Arguments = "Crazysoft.OTR_Remote";
                                updateApp.ErrorDialog = false;
                                updateApp.WorkingDirectory = rk.GetValue("InstallationPath").ToString();
                                System.Diagnostics.Process.Start(updateApp);

                                Settings.Sections.Add("Program");
                                Settings["Program"].Keys.Add("LastAutoUpdate", DateTime.Now.Ticks);
                                Settings.Save();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
        }
    }

    public struct RecordingInfo
    {
        public bool RemoveMode;
        public string Station;
        public DateTime StartDate;
        public DateTime StartTime;
        public DateTime EndTime;
        public string RequestString;

        // Optional arguments
        public string Title;
        public string Genre;
        public string User;
        public string Password;
        public int Timezone;

        public RecordingInfo(RecordingInfo ri)
        {
            this.RemoveMode = ri.RemoveMode;
            this.Station = ri.Station;
            this.StartDate = ri.StartDate;
            this.StartTime = ri.StartTime;
            this.EndTime = ri.EndTime;
            this.RequestString = ri.RequestString;

            this.Title = ri.Title;
            this.Genre = ri.Genre;
            this.User = ri.User;
            this.Password = ri.Password;
            this.Timezone = ri.Timezone;
        }
    }
}