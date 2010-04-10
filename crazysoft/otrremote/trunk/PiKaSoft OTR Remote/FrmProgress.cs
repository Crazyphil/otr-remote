using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Crazysoft.Encryption;

namespace Crazysoft.OTRRemote
{
    public partial class FrmProgress : Form
    {
        // Best practice recommends defining a private object to lock on - used for Windows 7 Progress Bar
        private static Object _syncLock = new Object();

        // Variables needed for processing the recording
        Uri _requestUri;
        RecordingInfo[] _recInfo;

        // Countdown variable for timed closing of form
        int _countdown = -1;

        // Variable for holding the deletion retry time
        DateTime _retryTime;

        // Variables for current recording and total recordings if multiple recordings exist
        int _currentRecording;
        
        // Variables for indication the form's display status
        public enum FormDisplayMode { ShowWindow, ShowSystray, Hide }
        FormDisplayMode _displayMode;
        public FormDisplayMode DisplayMode
        {
            get
            {
                return _displayMode;
            }
        }

        // Variables to hold OTR's authentication cookies
        CookieContainer _cookies = new CookieContainer();

        // enum to indicate worker result
        enum WorkerResult { Success, Deferred, Cancelled, Error }

        bool _justLoaded;

        // Background Worker
        private System.ComponentModel.BackgroundWorker bwWorker;

        // Struct for worker params
        struct BackgroundWorkerParams
        {
            public string RequestString;
            public RecordingInfo RecordingInfo;
        }

        struct WebpageErrors
        {
            public string DeleteNotRecorded_de;
            public string DeleteNotRecorded_en;
            public string AddTooLate_de;
            public string AddTooLate_en;
            public string AddWrongLogin_de;
            public string AddWrongLogin_en;
            public string AddSuccess_de;
            public string AddSuccess_en;
        }

        // An instance of the Windows 7 Taskbar ProgressBar class
        Windows7.ProgressBar taskbarProgressBar;

        // Indicates, if the BackgroundWorker is currently busy
        public bool IsWorking { get; private set; }

        // Indicates, if StartRecordingThread() is called manually or via the Form_Load event
        public bool ManualCall { get; set; }

        public FrmProgress(RecordingInfo[] recInfo)
        {
            _requestUri = new Uri(Uri.EscapeUriString(recInfo[0].RequestString));
            _recInfo = recInfo;
            _currentRecording = 0;

            // Write form's display mode to variable
            // First, look for legacy setting (OTR Remote < 2.0)
            if (Program.Settings["Program"].Keys.Contains("SilentMode"))
            {
                _displayMode = Convert.ToBoolean(Program.Settings["Program"]["SilentMode"].Value) ? FormDisplayMode.Hide : FormDisplayMode.ShowWindow;
            }
            else if (!Program.Settings["Program"]["ProgressIndicator"].IsNull)
            { // Set display mode according to setting
                _displayMode = (FormDisplayMode)Convert.ToInt32(Program.Settings["Program"]["ProgressIndicator"].Value);
            }
            else
            { // Default is to show the window
                _displayMode = FormDisplayMode.ShowWindow;
            }

            // If form's display mod is Hide, initialize minimal form
            if (_displayMode == FormDisplayMode.Hide)
            {
                InitializeHiddenComponent();
            }
            else
            {
                InitializeComponent();
                if (_displayMode == FormDisplayMode.ShowSystray)
                {
                    this.Opacity = 0;
                }
            }

            // Only translate controls if the form is not hidden
            if (_displayMode != FormDisplayMode.Hide)
            {
                lblHeader.Text = Lang.OTRRemote.FrmProgress_lblHeader;
                Program.TranslateControls(this);
            }

            this.ManualCall = true;
        }

        private void FrmProgress_Load(object sender, EventArgs e)
        {
            // Set form and systray icon visibility according to form's display status
            if (!IsWindowsSeven())
            {
                niSystray.Visible = _displayMode == FormDisplayMode.ShowSystray;
            }

            this.ManualCall = false;
            StartRecordThread(_displayMode);
        }

        private void FrmProgress_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void niSystray_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }

                this.Opacity = 1;
                this.ShowInTaskbar = true;
                this.Show();
            }
        }

        private void timTimer_Tick(object sender, EventArgs e)
        {
            if (_countdown == 0)
            {
                this.Close();
            }

            btnClose.Text = String.Format(Lang.OTRRemote.FrmProgress_btnClose_Countdown, _countdown);
            _countdown--;
        }

        private void timRetryTime_Tick(object sender, EventArgs e)
        {
            TimeSpan timeToRetry = _retryTime.Subtract(DateTime.Now);
            string timeStr = String.Format("{0:d2}:{1:d2}", timeToRetry.Minutes, timeToRetry.Seconds);

            lblStatus.Text = String.Format(Lang.OTRRemote.FrmProgress_Status_DeletionDeferred, timeStr);
            niSystray.Text = String.Format(Lang.OTRRemote.FrmProgress_Systray_Deletion_Timer, timeStr);
        }
        
        private void timRetry_Tick(object sender, EventArgs e)
        {
            timRetry.Enabled = false;
            timRetryTime.Enabled = false;
            niSystray.Text = "OTR Remote";

            // Restart Recording Thread after 10 minutes, if deletion failed
            StartRecordThread(_displayMode);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Pseudo-Bugfix for the form closing under Windows XP in Systray mode without any reason
            if (e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
                return;
            }

            if (bwWorker.IsBusy)
            {
                bwWorker.CancelAsync();
                bwWorker.ReportProgress(3, Lang.OTRRemote.FrmProgress_Status_Aborting);
                e.Cancel = true;
            }
            else
            {
                if (_displayMode != FormDisplayMode.Hide)
                {
                    Program.StartAppUpdate();
                }
            }
        }

        public delegate void StartRecordThreadDelegate(FormDisplayMode displayMode);
        public void StartRecordThread(FormDisplayMode displayMode)
        {
            this.IsWorking = true;

            // Create new Background Worker for each recording to avoid conflicts with current status
            InitializeBackgroundWorker();
            BackgroundWorkerParams data = new BackgroundWorkerParams();
            data.RequestString = _requestUri.ToString();
            data.RecordingInfo = _recInfo[_currentRecording];

            switch (displayMode)
            {
                case FormDisplayMode.ShowWindow:
                    this.Show();
                    this.Focus();
                    this.ShowInTaskbar = true;
                    break;
                case FormDisplayMode.ShowSystray:
                    this.Hide();
                    this.SendToBack();
                    this.WindowState = FormWindowState.Minimized;
                    if (!IsWindowsSeven())
                    {
                        this.ShowInTaskbar = false;
                    }
                    else
                    {
                        this.ShowInTaskbar = true;
                    }
                    _justLoaded = true;
                    break;
                case FormDisplayMode.Hide:
                    this.Hide();
                    this.SendToBack();
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    break;
            }

            bwWorker.RunWorkerAsync(data);
        }

        #region Background Worker
        private void InitializeBackgroundWorker()
        {
            bwWorker = new System.ComponentModel.BackgroundWorker();
            bwWorker.WorkerReportsProgress = true;
            bwWorker.WorkerSupportsCancellation = true;
            bwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(bwWorker_DoWork);
            bwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bwWorker_RunWorkerCompleted);
            bwWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bwWorker_ProgressChanged);
        }

        private void bwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Initialize the variables provided with the start of the thread
            BackgroundWorkerParams data = (BackgroundWorkerParams)e.Argument;
            e.Result = WorkerResult.Error;
            Exception execException = null;

            try
            {
                // Authenticate with a POST request
                HttpWebRequest request = CreateNewRequest(data.RequestString, true);

                if (bwWorker.CancellationPending)
                {
                    e.Result = WorkerResult.Cancelled;
                    e.Cancel = true;
                    return;
                }

                string epgid = String.Empty;
                // If we should delete the recording, look for the EPG ID
                if (data.RequestString.Contains("?aktion=deleteJob"))
                {
                    epgid = FindEpgId(data.RecordingInfo);
                    if (String.IsNullOrEmpty(epgid))
                    {
                        e.Cancel = true;
                        return;
                    }
                    request =
                        CreateNewRequest(String.Concat("http://www.onlinetvrecorder.com/index.php?aktion=deleteJob&quickexit=true&epgid=",
                                         Convert.ToBase64String(Encoding.Default.GetBytes(epgid))), true);
                }

                if (bwWorker.CancellationPending)
                {
                    e.Result = WorkerResult.Cancelled;
                    e.Cancel = true;
                    return;
                }

                bwWorker.ReportProgress(2, Lang.OTRRemote.FrmProgress_Status_Sending);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Uri responseUrl = response.ResponseUri;
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, System.Text.Encoding.UTF8);
                string webpage = sr.ReadToEnd();
                sr.Close();
                response.Close();

                // Cache object for German and English error messages
                WebpageErrors errMsgs = new WebpageErrors();

                // First, cache German messages
                Lang.OTRRemote.Culture = System.Globalization.CultureInfo.GetCultureInfo("de");
                errMsgs.DeleteNotRecorded_de = Lang.OTRRemote.FrmProgress_Result_NotRecorded;
                errMsgs.AddTooLate_de = Lang.OTRRemote.FrmProgress_Result_TooLate;
                errMsgs.AddWrongLogin_de = Lang.OTRRemote.FrmProgress_Result_WrongLogin;
                errMsgs.AddSuccess_de = Lang.OTRRemote.FrmProgress_Result_Success;

                // Then, cache English messages
                Lang.OTRRemote.Culture = System.Globalization.CultureInfo.GetCultureInfo("en");
                errMsgs.DeleteNotRecorded_en = Lang.OTRRemote.FrmProgress_Result_NotRecorded;
                errMsgs.AddTooLate_en = Lang.OTRRemote.FrmProgress_Result_TooLate;
                errMsgs.AddWrongLogin_en = Lang.OTRRemote.FrmProgress_Result_WrongLogin;
                errMsgs.AddSuccess_en = Lang.OTRRemote.FrmProgress_Result_Success;

                // Set the language back to the UI language
                Lang.OTRRemote.Culture = System.Globalization.CultureInfo.CurrentUICulture;

                // Choose the error message set, depending on if we want to record or delete
                if (data.RequestString.Contains("?aktion=deleteJob"))
                { // Check for deletion errors
                    // Variable for holding the user's answer when asked if he wants to retry deleting
                    DialogResult retryAnswer;

                    if (webpage.Contains(errMsgs.DeleteNotRecorded_de) || webpage.Contains(errMsgs.DeleteNotRecorded_en))
                    { // Check if the user tries to delete the recording too early
                        // Ask the user to automatically retry deletion
                        if (this.ManualCall)
                        {
                            // When calling the recording procedure manually (= no form really exists), Deletion Retry is forbidden
                            retryAnswer = DialogResult.No;
                        }
                        else if (!Program.Settings["Program"]["RetryDelete"].IsNull && Convert.ToBoolean(Program.Settings["Program"]["RetryDelete"].Value))
                        {
                            retryAnswer = DialogResult.Yes;
                        }
                        else
                        {
                            retryAnswer = MessageBox.Show(Lang.OTRRemote.FrmProgress_ErrorMsg_NotRecorded, Lang.OTRRemote.FrmProgress_ErrorMsg_DeleteError_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }

                        // If the user wants to retry automatically ...
                        if (retryAnswer == DialogResult.Yes)
                        {
                            // ... return and try again later
                            e.Result = WorkerResult.Deferred;
                            return;
                        }
                    }
                    else
                    { // Check if any other error occured
                        if (webpage.Contains("No valid URL") || webpage.Contains("<font color='red'>"))
                        {
                            MessageBox.Show(Lang.OTRRemote.FrmProgress_ErrorMsg_DeleteError_Text,
                                String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_DeleteError_Title),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Deletion was successful
                            e.Result = WorkerResult.Success;
                        }
                    }
                }
                else
                { // Check for recording errors
                    // Check for the various status messages in the returned page
                    if (webpage.Contains(errMsgs.AddTooLate_de) || webpage.Contains(errMsgs.AddTooLate_en))
                    {
                        UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                        MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_Date,
                                        String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_Title),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (webpage.Contains(errMsgs.AddWrongLogin_de) || webpage.Contains(errMsgs.AddWrongLogin_en))
                    {
                        UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                        MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_WrongLogin,
                                        String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_Title),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (webpage.Contains(errMsgs.AddSuccess_de) || webpage.Contains(errMsgs.AddSuccess_en))
                    {
                        e.Result = WorkerResult.Success;
                    }
                    else if (responseUrl.AbsolutePath == Lang.OTRRemote.FrmProgress_Result_MaintenanceFileName)
                    {
                        UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                        MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_Maintenance,
                                        String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_Title),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Unknown error - Write debug log
                        string debugfile = Path.Combine(Path.GetTempPath(), "OTR_Debug.html");
                        StreamWriter sw = new StreamWriter(debugfile);
                        sw.WriteLine(String.Concat("<p><tt><b>Crazysoft OTR Remote run on ", DateTime.Now.ToString("U"), "</b><br />"));
                        sw.WriteLine(String.Concat("<br />\n<b>Called URL:</b> ", data.RequestString, "<br />"));
                        sw.WriteLine("<b>Received Server Answer:</b></tt></p>");
                        sw.Write(webpage);
                        sw.Close();

                        UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                        MessageBox.Show(String.Format(Lang.OTRRemote.FrmProgress_Error_ParseError, debugfile),
                                        String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_Title),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            catch (WebException exc)
            {
                string message = exc.Message;
                if (exc.InnerException != null)
                {
                    message = String.Format("{0} ({1})", message, exc.InnerException.Message);
                }
                UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                MessageBox.Show(String.Format(Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Text, message), Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                execException = exc;
            }
            catch (Exception exc)
            {
                execException = exc;
            }
            finally
            {
                RaiseCompletedEvent(sender, new RunWorkerCompletedEventArgs(e.Result, execException, e.Cancel));
                this.IsWorking = false;
            }
        }

        private void bwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RecordingInfo recI = _recInfo[_currentRecording];

            string statusText = _recInfo.Length > 1 ? String.Format(Lang.OTRRemote.FrmProgress_Status_Multiple, _currentRecording + 1, _recInfo.Length, e.UserState.ToString()) : e.UserState.ToString();
            
            string formatStr = Lang.OTRRemote.FrmProgress_lblCurRec_Full;
            // If no station or title is given, modify format string
            if (String.IsNullOrEmpty(recI.Station) && String.IsNullOrEmpty(recI.Title))
            {
                formatStr = String.Empty;
            }
            else if (String.IsNullOrEmpty(recI.Station))
            {
                formatStr = Lang.OTRRemote.FrmProgress_lblCurRec_NoStation;
            }
            else if (String.IsNullOrEmpty(recI.Title))
            {
                formatStr = Lang.OTRRemote.FrmProgress_lblCurRec_NoTitle;
            }

            string curRecording = String.Format(formatStr, recI.StartDate.ToShortDateString(), recI.StartTime.ToShortTimeString(), recI.Station, recI.Title);

            if (_displayMode != FormDisplayMode.Hide)
            {
                lblStatus.Text = statusText;
                lblCurRec.Text = curRecording;
                
                pbProgress.Value = e.ProgressPercentage;
                UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Normal);
            }

            // Write output to systray if icon is used
            if (_displayMode == FormDisplayMode.ShowSystray && !this.ShowInTaskbar)
            {
                niSystray.ShowBalloonTip(2000, _recInfo[_currentRecording].Title, statusText, ToolTipIcon.Info);
            }
        }

        private void bwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                switch ((WorkerResult)e.Result)
                {
                    case WorkerResult.Deferred:
                        this.Hide();
                        if (!IsWindowsSeven())
                        {
                            niSystray.Visible = true;
                            this.ShowInTaskbar = false;
                            niSystray.ShowBalloonTip(5000, Lang.OTRRemote.FrmProgress_Systray_Deletion_Title, Lang.OTRRemote.FrmProgress_Systray_Deletion_Intro, ToolTipIcon.Info);
                        }

                        _retryTime = DateTime.Now.AddMinutes(10);
                        timRetry.Enabled = true;
                        timRetryTime.Enabled = true;
                        
                        pbProgress.Value = 0;
                        UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Paused);
                        break;
                    case WorkerResult.Success:
                        // If further recordings have to be processed and no error occured, restart with next recording
                        _currentRecording++;
                        if (_currentRecording < _recInfo.Length)
                        {
                            _requestUri = new Uri(Uri.EscapeUriString(_recInfo[_currentRecording].RequestString));
                            this.Invoke(new StartRecordThreadDelegate(StartRecordThread), _displayMode);
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            if (_displayMode != FormDisplayMode.Hide)
                            {
                                lblStatus.Text = Lang.OTRRemote.FrmProgress_Status_Finished;
                                pbProgress.Value = 3;
                                UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Normal);
                            }
                            else
                            {
                                this.Close();
                            }

                            if (_displayMode == FormDisplayMode.ShowSystray)
                            {
                                int closeTimeout = 5000;
                                if (!Program.Settings["Program"]["AutoClose"].IsNull &&
                                    Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value) > 0)
                                {
                                    closeTimeout = Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value) * 1000;
                                }
                                if (!IsWindowsSeven())
                                {
                                    niSystray.ShowBalloonTip(closeTimeout, _recInfo[_currentRecording].Title, Lang.OTRRemote.FrmProgress_Status_Finished, ToolTipIcon.Info);
                                }
                            }
                        }
                        break;
                    case WorkerResult.Error:
                    default:
                        // Wenn ein Fehler nach der ersten Aufnahme aufgetreten ist, muss dem EPG-Programm für eine richtige
                        // Anzeige trotzdem eine Erfolgsmeldung geliefert werden
                        if (_currentRecording > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }

                        if (_displayMode != FormDisplayMode.Hide)
                        {
                            lblStatus.Text = Lang.OTRRemote.FrmProgress_Status_Error;
                            lblStatus.ForeColor = Color.Red;
                            pbProgress.Value = 0;
                            UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.NoProgress);
                        }
                        else
                        {
                            this.Close();
                        }
                        if (_displayMode == FormDisplayMode.ShowSystray)
                        {
                            int closeTimeout = 5000;
                            if (!Program.Settings["Program"]["AutoClose"].IsNull && Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value) > 0)
                            {
                                closeTimeout = Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value) * 1000;
                            }
                            if (!IsWindowsSeven())
                            {
                                niSystray.ShowBalloonTip(closeTimeout, _recInfo[_currentRecording].Title, Lang.OTRRemote.FrmProgress_Status_Error, ToolTipIcon.Error);
                            }
                        }
                        break;
                }

                WorkerResult result = (WorkerResult)e.Result;
                if ((result == WorkerResult.Success && _currentRecording == _recInfo.Length) ||
                    (result != WorkerResult.Deferred && result != WorkerResult.Success))
                {
                    if (!Program.Settings["Program"]["AutoClose"].IsNull)
                    {
                        if (Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value) > 0)
                        {
                            _countdown = Convert.ToInt32(Program.Settings["Program"]["AutoCloseTimeout"].Value) - 1;
                            btnClose.Text = string.Format(Lang.OTRRemote.FrmProgress_btnClose_Countdown, _countdown + 1);
                            timTimer.Enabled = true;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }
        #endregion

        #region CreateRequest methods
        private HttpWebRequest CreateNewRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Read and process proxy settings
            if (!Program.Settings["Network"]["ProxyType"].IsNull && Convert.ToInt32(Program.Settings["Network"]["ProxyType"].Value) == 0)
            {
                request.Proxy = null;
            }
            else if (Program.Settings["Network"]["ProxyType"].IsNull || Convert.ToInt32(Program.Settings["Network"]["ProxyType"].Value) == 1)
            {
                if (Program.Settings.RunningOnUnix())
                {
                    // Replace "IE proxy" with "System proxy"
                    bwWorker.ReportProgress(1, Lang.OTRRemote.FrmProgress_Status_Proxy.Replace("IE", "System"));
                }
                else
                {
                    bwWorker.ReportProgress(1, Lang.OTRRemote.FrmProgress_Status_Proxy);
                }
                request.Proxy = WebRequest.GetSystemWebProxy();
                request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

                if (!Program.Settings["Network"]["ProxyAuthRequired"].IsNull && Convert.ToBoolean(Program.Settings["Network"]["ProxyAuthRequired"].Value))
                {
                    NetworkCredential creds = new NetworkCredential(Program.Settings["Network"]["ProxyUser"].Value.ToString(),
                                                      Encryption.Encryption.Decrypt(Program.Settings["Network"]["ProxyPassword"].Value.ToString(),
                                                          Program.Settings["Network"]["ProxyUser"].Value.ToString()));
                    request.Proxy.Credentials = creds;
                }
            }
            else
            {
                WebProxy proxy = new WebProxy();
                Uri proxyUri =
                    new Uri(String.Concat("http://", Program.Settings["Network"]["ProxyAddress"].Value.ToString(), ":",
                            Program.Settings["Network"]["ProxyPort"].Value.ToString()));
                proxy.Address = proxyUri;

                if (!Program.Settings["Network"]["ProxyAuthRequired"].IsNull && Convert.ToBoolean(Program.Settings["Network"]["ProxyAuthRequired"].Value))
                {
                    NetworkCredential creds = new NetworkCredential(Program.Settings["Network"]["ProxyUser"].Value.ToString(),
                                                      Encryption.Encryption.Decrypt(Program.Settings["Network"]["ProxyPassword"].Value.ToString(),
                                                          Program.Settings["Network"]["ProxyUser"].Value.ToString()));
                    proxy.Credentials = creds;
                }
                else
                {
                    proxy.UseDefaultCredentials = false;
                }

                request.Proxy = proxy;
            }

            // Set User Agent
            request.UserAgent = String.Concat("Crazysoft.OTRRemote/", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            request.Timeout = 30000;
            request.ProtocolVersion = HttpVersion.Version10;

            // Add OTR language cookie
            if (_cookies.Count == 0)
            {
                _cookies.Add(new Cookie("OTRLANG", System.Globalization.CultureInfo.CurrentUICulture.Parent.Name == "de" ? "de" : "en", "/", "www.onlinetvrecorder.com"));
            }

            return request;
        }

        private HttpWebRequest CreateNewRequest(string url, bool needsAuthentication)
        {
            if (needsAuthentication)
            {
                HttpWebRequest request;
                if (_cookies.Count == 0)
                {
                    request = CreateNewRequest("http://www.onlinetvrecorder.com/index.php?go=home");

                    string postString = String.Concat("email=", _recInfo[_currentRecording].User.Replace("@", "%40"), "&pass=", _recInfo[_currentRecording].Password, "&btn_login=Login");

                    // POST user login data
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] postData = encoding.GetBytes(postString);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = postData.Length;
                    request.CookieContainer = _cookies;
                    Stream requestWriter = request.GetRequestStream();
                    requestWriter.Write(postData, 0, postData.Length);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    _cookies.Add(response.Cookies);
                    response.Close();
                }

                request = CreateNewRequest(url);
                request.CookieContainer = _cookies;
                return request;
            }
            else
            {
                return CreateNewRequest(url);
            }
        }
        #endregion

        private string FindEpgId(RecordingInfo recInfo)
        {
            // Get Unix timestamp of date
            TimeSpan ts = new DateTime(recInfo.StartDate.Year, recInfo.StartDate.Month, recInfo.StartDate.Day, 0, 0, 0).ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime();

            // Create search URL string
            string searchUrl = String.Concat("http://epg.onlinetvrecorder.com/broadcasts/search?searchterm=", recInfo.Title, "&beginn=",
                               ts.TotalSeconds + "&sender=" + recInfo.Station.ToLower(),
                               "&typ=&wish=&search=suche&otr_stations=1");
            string postString = String.Concat("user[email]=", recInfo.User.Replace("@", "%40"), "&user[password]=", recInfo.Password, "&commit=einloggen");
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postData = encoding.GetBytes(postString);

            // Search for EPG ID
            try
            {
                // First, log in at the EPG service
                HttpWebRequest request = CreateNewRequest("http://epg.onlinetvrecorder.com/login/login");
                request.CookieContainer = new CookieContainer();
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                Stream requestWriter = request.GetRequestStream();
                requestWriter.Write(postData, 0, postData.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                CookieCollection cookies = response.Cookies;
                StreamReader sr = new StreamReader(response.GetResponseStream());
                string answer = sr.ReadToEnd();
                sr.Close();

                // Check, if the login was successful
                if (answer.Contains("Invalid user/password combination"))
                {
                    MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_WrongLogin,
                                    String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_Title),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return String.Empty;
                }

                request = CreateNewRequest(searchUrl);
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
                bwWorker.ReportProgress(2, Lang.OTRRemote.FrmProgress_Status_SearchingEPG);
                response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream());
                answer = sr.ReadToEnd();
                sr.Close();

                if (answer.Contains("No broadcasts found!"))
                {
                    UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                    MessageBox.Show(Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Text,
                                    String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Title),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return String.Empty;
                }

                int startIndex = answer.IndexOf("<td align=\"center\"><input id=\"recording[") + 40;
                string epgid = String.Empty;
                while (String.IsNullOrEmpty(epgid))
                {
                    if (startIndex > answer.Length)
                    {
                        break;
                    }

                    int endIndex = answer.IndexOf("]\" name=", startIndex);
                    if (endIndex < startIndex)
                    {
                        break;
                    }

                    epgid = answer.Substring(startIndex, endIndex - startIndex);

                    startIndex = answer.IndexOf("<td class=\"search\"><i>", endIndex) + 22;
                    endIndex = answer.IndexOf(":  ", startIndex);
                    string date = answer.Substring(startIndex, endIndex - startIndex);

                    startIndex = endIndex + 3;
                    endIndex = answer.IndexOf("</i>", startIndex);
                    string time = answer.Substring(startIndex, endIndex - startIndex);

                    DateTime correctDate = new DateTime(recInfo.StartDate.Year, recInfo.StartDate.Month, recInfo.StartDate.Day, recInfo.StartTime.Hour, recInfo.StartTime.Minute, 0);

                    if (date != correctDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) || time != correctDate.ToString("HH:mm"))
                    {
                        epgid = String.Empty;
                        startIndex = answer.IndexOf("<td align=\"center\"><input id=\"recording[", endIndex) + 40;
                    }
                }

                if (String.IsNullOrEmpty(epgid))
                {
                    UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                    MessageBox.Show(Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Text,
                                    String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Title),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return epgid;
            }
            catch (WebException excp)
            {
                string message = excp.Message;
                if (excp.InnerException != null)
                {
                    message = String.Concat(message, " (", excp.InnerException.Message, ")");
                }
                UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState.Error);
                MessageBox.Show(String.Format(Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Text, message),
                                String.Concat("Crazysoft OTR Remote: ", Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Title),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }
        }

        void FrmProgress_VisibleChanged(object sender, System.EventArgs e)
        {
            // This method handles the different display styles when minimizing/showing the form
            if (_displayMode == FormDisplayMode.ShowSystray)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    if (!IsWindowsSeven())
                    {
                        this.ShowInTaskbar = false;
                    }
                }
                else if (_justLoaded)
                {
                    this.WindowState = FormWindowState.Minimized;
                    _justLoaded = false;
                }
            }
            else if (_displayMode == FormDisplayMode.Hide)
            {
                if (this.Visible)
                {
                    this.Hide();
                }
            }
            
        }

        private void UpdateTaskbarProgressBar(Windows7.TaskbarButtonProgressState state)
        {
            // This method mirrors the state of the in-form progress bar on Windows 7
            if (IsWindowsSeven())
            {
                if (taskbarProgressBar == null)
                {
                    lock (_syncLock)
                    {
                        taskbarProgressBar = new Windows7.ProgressBar();
                    }
                }

                taskbarProgressBar.State = state;
                // Treat single recordings different than multi-recordings in terms of progress indication
                if (_recInfo.Length > 1)
                {
                    taskbarProgressBar.MaxValue = _recInfo.Length;
                    if (_currentRecording + 1 > _recInfo.Length)
                    {
                        taskbarProgressBar.CurrentValue = taskbarProgressBar.MaxValue;
                    }
                    else
                    {
                        taskbarProgressBar.CurrentValue = _currentRecording + 1;
                    }
                }
                else
                {
                    taskbarProgressBar.MaxValue = pbProgress.Maximum;
                    if (state != Windows7.TaskbarButtonProgressState.NoProgress && pbProgress.Value == 0)
                    {
                        taskbarProgressBar.CurrentValue = taskbarProgressBar.MaxValue;
                    }
                    else
                    {
                        taskbarProgressBar.CurrentValue = pbProgress.Value;
                    }
                }
            }
        }

        private bool IsWindowsSeven()
        {
            OperatingSystem os = System.Environment.OSVersion;
            if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 6 && os.Version.Minor >= 1 || os.Version.Major > 6)))
            {
                return true;
            }
            return false;
        }

        private void RaiseCompletedEvent(object sender, RunWorkerCompletedEventArgs args)
        {
            if (this.ManualCall)
            {
                // When calling the recording procedure manually (= no form really exists), call the RunWorkerCompleted event manually
                bwWorker_RunWorkerCompleted(sender, args);
            }
        }
    }
}