using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Crazysoft.AppSettings;

namespace Crazysoft.OTR_Remote
{
    static class Program
    {
        private static AppSettings.AppSettings _Settings;
        public static AppSettings.AppSettings Settings
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

            _Settings =
                new AppSettings.AppSettings(
                    System.IO.Path.Combine(
                        System.IO.Path.Combine(
                            System.IO.Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Crazysoft"),
                            "OTR Remote"), "settings.xml"));
            _Settings.Load();

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
                        // Define variables for arguments
                        bool addShow = false;
                        string station = "";
                        string startDate = "";
                        string startTime = "";
                        string endTime = "";

                        // Optional arguments
                        string title = "";
                        string genre = "";
                        string user = "";
                        string password = "";
                        int timezone = 0;

                        // Variable for error messages
                        string message = "";

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
                                station = args[stationPos].Substring(3).Replace("\"", "");
                                Station replacement = stations.Find(station);
                                if (replacement != null)
                                {
                                    station = replacement.Name;
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
                                        int year = Convert.ToInt32(date[0]);
                                        int month = Convert.ToInt32(date[1]);
                                        int day = Convert.ToInt32(date[2]);

                                        startDate = "&startDate_year=" + year + "&startDate_month=" + month + "&startDate_day=" + day;
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
                                            DateTime.Parse(args[sdPos].Substring(args[sdPos].IndexOf('=') + 1) + " " +
                                                           args[stPos].Substring(args[stPos].IndexOf('=') + 1));
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
                                                DateTime.Parse(argStartTime.AddDays(1).ToShortDateString() + " " +
                                                               args[endtimepos].Substring(
                                                                   args[endtimepos].IndexOf('=') + 1));
                                        }
                                        else
                                        {
                                            argEndTime =
                                                DateTime.Parse(args[sdPos].Substring(args[sdPos].IndexOf('=') + 1) + " " +
                                                               args[endtimepos].Substring(
                                                                   args[endtimepos].IndexOf('=') + 1));
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
                                                if (addShow && Settings.Sections["Program"].Keys["AdjustStartTime"] != null && Convert.ToBoolean(Settings.Sections["Program"].Keys["AdjustStartTime"].Value))
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

                                        startTime = "&startTime_hour=" + hour + "&startTime_minute=" + minute;
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
                                        if (addShow && Settings.Sections["Program"].Keys["RecordFollowing"] != null && Convert.ToBoolean(Settings.Sections["Program"].Keys["RecordFollowing"].Value))
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

                                        endTime = "&endTime_hour=" + hour + "&endTime_minute=" + minute;
                                    }
                                    catch
                                    {
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
                                title = args[titlePos].Substring(3);
                            }
                        }

                        // Parse Genre argument
                        int genrePos = Array.FindIndex(args, FindGenreArg);
                        if (genrePos != -1)
                        {
                            if (args[genrePos].Length > 3)
                            {
                                genre = args[genrePos].Substring(3);
                            }
                        }

                        // Parse User argument
                        int userPos = Array.FindIndex(args, FindUserArg);
                        if (userPos != -1)
                        {
                            if (args[userPos].Length > 3)
                            {
                                user = args[userPos].Substring(3);
                            }
                        }
                        else if (Settings.Sections["Program"].Keys["Username"] != null && Settings.Sections["Program"].Keys["Username"].Value.ToString() != "")
                        {
                            user = Settings.Sections["Program"].Keys["Username"].Value.ToString();
                        }

                        // Parse Password argument
                        int passPos = Array.FindIndex(args, FindPasswordArg);
                        if (passPos != -1)
                        {
                            if (args[passPos].Length > 3)
                            {
                                password = args[passPos].Substring(3);
                            }
                        }
                        else if (Settings.Sections["Program"].Keys["Password"] != null && Settings.Sections["Program"].Keys["Password"].Value.ToString() != "")
                        {
                            password = Encryption.Encryption.Decrypt(Settings.Sections["Program"].Keys["Password"].Value.ToString(), Settings.Sections["Program"].Keys["Username"].Value.ToString());
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

                                    timezone = tz;
                                }
                                catch
                                {
                                    timezone = GetTimezone();
                                }
                            }
                            else
                            {
                                timezone = GetTimezone();
                            }
                        }
                        else
                        {
                            timezone = GetTimezone();
                        }

                        // Find unset variables (= unspecified args) and throw an error
                        if (Array.IndexOf(args, "-a") == -1 && Array.IndexOf(args, "-r") == -1)
                        {
                            message += "\u2022 " + Lang.OTRRemote.CmdLine_Error_Action + "\n";
                        }
                        if (station == "")
                        {
                            message += "\u2022 " + Lang.OTRRemote.CmdLine_Error_Station + "\n";
                        }
                        if (startDate == "")
                        {
                            message += "\u2022 " + Lang.OTRRemote.CmdLine_Error_StartDate + "\n";
                        }
                        if (startTime == "")
                        {
                            message += "\u2022 " + Lang.OTRRemote.CmdLine_Error_StartTime + "\n";
                        }
                        if (endTime == "")
                        {
                            message += "\u2022 " + Lang.OTRRemote.CmdLine_Error_EndTime + "\n";
                        }

                        if (message != "")
                        {
                            if (MessageBox.Show(String.Format(Lang.OTRRemote.CmdLine_ErrorMsg_Text, message), "Crazysoft OTR Remote: " + Lang.OTRRemote.CmdLine_ErrorMsg_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
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
                            requeststr += "createJob";
                        }
                        else
                        {
                            // Get start date and time from commandline args
                            DateTime begin =
                                DateTime.Parse(args[sdPos].Substring(args[sdPos].IndexOf('=') + 1) + " " +
                                               args[stPos].Substring(args[stPos].IndexOf('=') + 1));
                            // Ask the user if he knows what he does, before we really delete the recording
                            if (MessageBox.Show(String.Format(Lang.OTRRemote.CmdLine_DeleteMsg_Text, title, begin.ToShortDateString(), begin.ToShortTimeString()), "Crazysoft OTR Remote: " + Lang.OTRRemote.CmdLine_DeleteMsg_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                requeststr += "deleteJob";
                            }
                            else
                            {
                                return 1;
                            }
                        }

                        // If username and/or password are not set, call the login form
                        if (user == "" || password == "")
                        {
                            FrmLogin FrmLogin = new FrmLogin(user, password, timezone);
                            Application.Run(FrmLogin);

                            if (FrmLogin.DialogResult == DialogResult.Cancel)
                            {
                                return 1;
                            }
                            else
                            {
                                user = FrmLogin.username;
                                password = FrmLogin.password;
                                timezone = FrmLogin.timezone;
                            }
                        }

                        requeststr += "&tvStation=" + station;
                        requeststr += startDate;
                        requeststr += startTime;
                        requeststr += endTime;

                        requeststr += "&email=" + user;
                        requeststr += "&pass=" + password;

                        if (title != "")
                        {
                            // Convert special characters to readable chars
                            title = title.Replace("ä", "ae");
                            title = title.Replace("ö", "oe");
                            title = title.Replace("ü", "ue");
                            title = title.Replace("ß", "ss");
                            title = title.Replace("&", "und");

                            if (!addShow)
                            {
                                // Also convert URL-necessary chars
                                title = title.Replace(" ", "+");
                                title = title.Replace("-", "+");
                                Regex regex = new Regex("[^0-9A-Za-z+.]");
                                title = regex.Replace(title, "");
                                title = title.Replace("+++", "+");
                                title = title.Replace("++", "+");
                            }

                            requeststr += "&titleName=" + title.Replace("\"", "");
                        }
                        if (genre != "")
                        {
                            requeststr += "&genre=" + genre.Replace("\"", "");
                        }
                        
                        requeststr += "&timezone=" + timezone;
                        //System.Threading.Thread.Sleep(7000);
                        Uri requestUri = new Uri(Uri.EscapeUriString(requeststr));
                        FrmProgress FrmProgress = new FrmProgress(args, requestUri);
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
            if (Settings.Sections["Program"].Keys["Timezone"] != null)
            {
                return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            }
            else
            {
                return Convert.ToInt32(Settings.Sections["Program"].Keys["Timezone"].Value);
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
}