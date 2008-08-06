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

namespace Crazysoft.OTR_Remote
{
    public partial class FrmProgress : Form
    {
        Uri _requestUri;
        string[] args;
        int countdown = -1;

        public FrmProgress(string[] cmdArgs, Uri requestUri)
        {
            _requestUri = requestUri;
            args = cmdArgs;

            InitializeComponent();

            lblHeader.Text = Lang.OTRRemote.FrmProgress_lblHeader;

            foreach (Control ctl in this.Controls)
            {
                ctl.Text = Lang.OTRRemote.ResourceManager.GetString("FrmProgress_" + ctl.Name);
            }
        }

        private void FrmProgress_Load(object sender, EventArgs e)
        {
            if (Program.Settings.Sections["Program"].Keys["SilentMode"] != null)
            {
                this.Visible = !Convert.ToBoolean(Program.Settings.Sections["Program"].Keys["SilentMode"].Value);
            }

            StartRecordThread(this.Visible);
        }

        private void FrmProgress_Paint(object sender, PaintEventArgs e)
        {
            Graphics.DrawGraphicalHeader(e, this, pnlHeader);
        }

        private void timTimer_Tick(object sender, EventArgs e)
        {
            if (countdown == 0)
            {
                this.Close();
            }

            btnClose.Text = String.Format(Lang.OTRRemote.FrmProgress_btnClose_Countdown, countdown);
            countdown--;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bwWorker.IsBusy)
            {
                bwWorker.CancelAsync();
                bwWorker.ReportProgress(3, Lang.OTRRemote.FrmProgress_Status_Aborting);
                e.Cancel = true;
            }

            Application.Exit();
        }

        private void StartRecordThread(bool showForm)
        {
            ArrayList data = new ArrayList();
            data.Add(_requestUri);
            data.Add(args);

            if (showForm)
            {
                this.Show();
                this.Focus();
            }

            bwWorker.RunWorkerAsync(data);
        }

        private void bwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Initialize the variables provided with the start of the thread
            ArrayList vars = (ArrayList)e.Argument;
            string[] args = (string[])vars[1];
            e.Result = false;

            try
            {
                // Authenticate with a POST request
                HttpWebRequest request = CreateNewRequest(vars[0].ToString());

                // Extract user data from query string
                int emailStart = vars[0].ToString().IndexOf("&email=") + 7;
                int emailEnd = vars[0].ToString().IndexOf('&', emailStart);
                string email = vars[0].ToString().Substring(emailStart, emailEnd - emailStart);

                int passStart = vars[0].ToString().IndexOf("&pass=") + 6;
                int passEnd = vars[0].ToString().IndexOf('&', passStart);
                string pass = vars[0].ToString().Substring(passStart, passEnd - passStart);

                string epgid = "";
                // If we should delete the recording, look for the EPG ID
                if (vars[0].ToString().Contains("?aktion=deleteJob"))
                {
                    epgid = FindEpgId(vars[0].ToString());
                    if (epgid == "")
                    {
                        e.Cancel = true;
                        return;
                    }
                    request =
                        CreateNewRequest("http://www.onlinetvrecorder.com/index.php?aktion=deleteJob&epgid=" +
                                         Convert.ToBase64String(Encoding.Default.GetBytes(epgid)));
                }

                string postString = "email=" + email.Replace("@", "%40") + "&pass=" + pass + "&btn_login=Login";

                // POST user login data
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postData = encoding.GetBytes(postString);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                request.CookieContainer = new CookieContainer();
                Stream requestWriter = request.GetRequestStream();
                requestWriter.Write(postData, 0, postData.Length);

                if (bwWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                bwWorker.ReportProgress(2, Lang.OTRRemote.FrmProgress_Status_Sending);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, System.Text.Encoding.UTF8);
                string webpage = sr.ReadToEnd();
                sr.Close();
                response.Close();

                // Choose the error message set, depending on if we want to record or delete
                if (vars[0].ToString().Contains("?aktion=deleteJob"))
                {
                    if (webpage.Contains("No valid URL") || webpage.Contains("<font color='red'>"))
                    {
                        int startIndex = webpage.IndexOf("<font color='red'><b>") + 21;
                        int endIndex = webpage.IndexOf("</b>", startIndex);
                        MessageBox.Show(
                            Lang.OTRRemote.FrmProgress_ErrorMsg_DeleteError_Text,
                            "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_DeleteError_Title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        e.Result = true;
                    }
                }
                else
                {
                    // First, set the status message texts to German
                    Lang.OTRRemote.Culture = System.Globalization.CultureInfo.GetCultureInfo("de");

                    // Check for the various status messages in the returned page
                    if (webpage.Contains(Lang.OTRRemote.FrmProgress_Result_TooLate))
                    {
                        MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_Date,
                                        "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_Title,
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (webpage.Contains(Lang.OTRRemote.FrmProgress_Result_WrongLogin))
                    {
                        MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_WrongLogin,
                                        "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_Title,
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (webpage.Contains(Lang.OTRRemote.FrmProgress_Result_Success))
                    {
                        e.Result = true;
                    }
                    else
                    {
                        // If no German texts are found, try the English ones
                        Lang.OTRRemote.Culture = System.Globalization.CultureInfo.GetCultureInfo("en");

                        if (webpage.Contains(Lang.OTRRemote.FrmProgress_Result_TooLate))
                        {
                            MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_Date,
                                            "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_Title,
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (webpage.Contains(Lang.OTRRemote.FrmProgress_Result_WrongLogin))
                        {
                            MessageBox.Show(Lang.OTRRemote.FrmProgress_Error_WrongLogin,
                                            "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_Title,
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (webpage.Contains(Lang.OTRRemote.FrmProgress_Result_Success))
                        {
                            e.Result = true;
                        }
                        else
                        {
                            // Unknown error
                            // Write debug log
                            string debugfile = Path.Combine(Path.GetTempPath(), "OTR_Debug.html");
                            StreamWriter sw = new StreamWriter(debugfile);
                            sw.WriteLine("<p><tt><b>Crazysoft OTR Remote run on " + DateTime.Now.ToString("U") +
                                         "</b><br />");
                            sw.Write("<b>Commandline Arguments:</b>");
                            foreach (string arg in args)
                            {
                                sw.Write(" " + arg);
                            }
                            sw.WriteLine("<br />\n<b>Called URL:</b> " + vars[0].ToString() + "<br />");
                            sw.WriteLine("<b>Received Server Answer:</b></tt></p>");
                            sw.Write(webpage);
                            sw.Close();

                            MessageBox.Show(String.Format(Lang.OTRRemote.FrmProgress_Error_ParseError, debugfile),
                                            "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_Title,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        }
                    }

                    // Set the language back to the UI language
                    Lang.OTRRemote.Culture = System.Globalization.CultureInfo.CurrentUICulture;
                }
            }
            catch (WebException exc)
            {
                string message = exc.Message;
                if (exc.InnerException != null)
                {
                    message = message + " (" + exc.InnerException.Message + ")";
                }
                MessageBox.Show(String.Format(Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Text, message), Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = e.UserState.ToString();
            pbProgress.Value = e.ProgressPercentage;
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
                if ((bool)e.Result != true || e.Error != null)
                {
                    lblStatus.Text = Lang.OTRRemote.FrmProgress_Status_Error;
                    lblStatus.ForeColor = Color.Red;
                    pbProgress.Value = 0;
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    lblStatus.Text = Lang.OTRRemote.FrmProgress_Status_Finished;
                    pbProgress.Value = 3;
                    this.DialogResult = DialogResult.OK;
                }
                if (Program.Settings.Sections["Program"].Keys["AutoClose"] != null)
                {
                    if (Convert.ToInt32(Program.Settings.Sections["Program"].Keys["AutoCloseTimeout"].Value) > 0)
                    {
                        countdown = Convert.ToInt32(Program.Settings.Sections["Program"].Keys["AutoCloseTimeout"].Value) - 1;
                        btnClose.Text = string.Format(Lang.OTRRemote.FrmProgress_btnClose_Countdown, countdown + 1);
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

        private HttpWebRequest CreateNewRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Read and process proxy settings
            if (Program.Settings.Sections["Network"].Keys["ProxyType"] != null && Convert.ToInt32(Program.Settings.Sections["Network"].Keys["ProxyType"].Value) == 0)
            {
                request.Proxy = null;
            }
            else if (Program.Settings.Sections["Network"].Keys["ProxyType"] == null || Convert.ToInt32(Program.Settings.Sections["Network"].Keys["ProxyType"].Value) == 1)
            {
                bwWorker.ReportProgress(1, Lang.OTRRemote.FrmProgress_Status_Proxy);
                request.Proxy = WebRequest.GetSystemWebProxy();
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                if (Program.Settings.Sections["Network"].Keys["ProxyAuthRequired"] != null && Convert.ToBoolean(Program.Settings.Sections["Network"].Keys["ProxyAuthRequired"].Value))
                {
                    NetworkCredential creds =
                        new NetworkCredential(Program.Settings.Sections["Network"].Keys["ProxyUser"].Value.ToString(),
                                              Encryption.Encryption.Decrypt(
                                                  Program.Settings.Sections["Network"].Keys["ProxyPassword"].Value.
                                                      ToString(),
                                                  Program.Settings.Sections["Network"].Keys["ProxyUser"].Value.ToString()));
                    request.Proxy.Credentials = creds;
                }
            }
            else
            {
                WebProxy proxy = new WebProxy();
                Uri proxyUri =
                    new Uri("http://" + Program.Settings.Sections["Network"].Keys["ProxyAddress"].Value.ToString() + ":" +
                            Program.Settings.Sections["Network"].Keys["ProxyPort"].Value.ToString());
                proxy.Address = proxyUri;

                if (Program.Settings.Sections["Network"].Keys["ProxyAuthRequired"] != null && Convert.ToBoolean(Program.Settings.Sections["Network"].Keys["ProxyAuthRequired"].Value))
                {
                    NetworkCredential creds =
                        new NetworkCredential(Program.Settings.Sections["Network"].Keys["ProxyUser"].Value.ToString(),
                                              Encryption.Encryption.Decrypt(
                                                  Program.Settings.Sections["Network"].Keys["ProxyPassword"].Value.
                                                      ToString(),
                                                  Program.Settings.Sections["Network"].Keys["ProxyUser"].Value.ToString()));
                    proxy.Credentials = creds;
                }
                else
                {
                    proxy.UseDefaultCredentials = false;
                }

                request.Proxy = proxy;
            }

            // Set User Agent
            request.UserAgent = "Crazysoft.OTR_Remote/" +
                                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            request.Timeout = 30000;
            request.ProtocolVersion = HttpVersion.Version10;

            return request;
        }

        private string FindEpgId(string url)
        {
            // Extract needed data from URL
            // Title
            int titleStart = url.IndexOf("&titleName=") + 11;
            int titleEnd = url.IndexOf('&', titleStart);
            string title = url.Substring(titleStart, titleEnd - titleStart);

            // Station
            int stationStart = url.IndexOf("&tvStation=") + 11;
            int stationEnd = url.IndexOf('&', stationStart);
            string station = url.Substring(stationStart, stationEnd - stationStart);

            // Start date parts
            int startDateStart = url.IndexOf("&startDate_year=") + 16;
            int startDateEnd = url.IndexOf('&', startDateStart);
            int year = Convert.ToInt32(url.Substring(startDateStart, startDateEnd - startDateStart));

            startDateStart = url.IndexOf("&startDate_month=") + 17;
            startDateEnd = url.IndexOf('&', startDateStart);
            int month = Convert.ToInt32(url.Substring(startDateStart, startDateEnd - startDateStart));

            startDateStart = url.IndexOf("&startDate_day=") + 15;
            startDateEnd = url.IndexOf('&', startDateStart);
            int day = Convert.ToInt32(url.Substring(startDateStart, startDateEnd - startDateStart));

            // Start time parts
            int startTimeStart = url.IndexOf("&startTime_hour=") + 16;
            int startTimeEnd = url.IndexOf('&', startTimeStart);
            int hour = Convert.ToInt32(url.Substring(startTimeStart, startTimeEnd - startTimeStart));

            startTimeStart = url.IndexOf("&startTime_minute=") + 18;
            startTimeEnd = url.IndexOf('&', startTimeStart);
            int minute = Convert.ToInt32(url.Substring(startTimeStart, startTimeEnd - startTimeStart));

            // Username and password
            int emailStart = url.IndexOf("&email=") + 7;
            int emailEnd = url.IndexOf('&', emailStart);
            string email = url.Substring(emailStart, emailEnd - emailStart);

            int passStart = url.IndexOf("&pass=") + 6;
            int passEnd = url.IndexOf('&', passStart);
            string pass = url.Substring(passStart, passEnd - passStart);

            // Get Unix timestamp of date
            TimeSpan ts = new DateTime(year, month, day, 0, 0, 0).ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0);

            // Create search URL string
            string searchUrl = "http://epg.onlinetvrecorder.com/broadcasts/search?searchterm=" + title + "&beginn=" +
                               ts.TotalSeconds + "&sender=" + station.ToLower() +
                               "&typ=&wish=&search=suche&otr_stations=1";
            string postString = "user[email]=" + email.Replace("@", "%40") + "&user[password]=" + pass + "&commit=Login";
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
                                    "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                request = CreateNewRequest(searchUrl);
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
                bwWorker.ReportProgress(2, Lang.OTRRemote.FrmProgress_Status_SearchingEPG);
                response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream());
                string webpage = sr.ReadToEnd();
                sr.Close();

                if (webpage.Contains("No broadcasts found!"))
                {
                    MessageBox.Show(Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Text,
                                    "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                int startIndex = webpage.IndexOf("<td align=\"center\"><input id=\"recording[") + 40;
                string epgid = "";
                while (epgid == "")
                {
                    if (startIndex > webpage.Length)
                    {
                        break;
                    }

                    int endIndex = webpage.IndexOf("]\" name=", startIndex);
                    if (endIndex < startIndex)
                    {
                        break;
                    }

                    epgid = webpage.Substring(startIndex, endIndex - startIndex);

                    startIndex = webpage.IndexOf("<td class=\"search\"><i>") + 22;
                    endIndex = webpage.IndexOf(":  ", startIndex);
                    string date = webpage.Substring(startIndex, endIndex - startIndex);

                    startIndex = endIndex + 3;
                    endIndex = webpage.IndexOf("</i>", startIndex);
                    string time = webpage.Substring(startIndex, endIndex - startIndex);

                    DateTime correctDate = new DateTime(year, month, day, hour, minute, 0);

                    if (date != correctDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) || time != correctDate.ToString("HH:mm"))
                    {
                        epgid = "";
                        startIndex = endIndex + 4;
                    }
                }

                if (epgid == "")
                {
                    MessageBox.Show(Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Text,
                                    "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_EPGNotFound_Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return epgid;
            }
            catch (WebException excp)
            {
                string message = excp.Message;
                if (excp.InnerException != null)
                {
                    message = message + " (" + excp.InnerException.Message + ")";
                }
                MessageBox.Show(String.Format(Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Text, message),
                                "Crazysoft OTR Remote: " + Lang.OTRRemote.FrmProgress_ErrorMsg_ConnErr_Title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
    }
}