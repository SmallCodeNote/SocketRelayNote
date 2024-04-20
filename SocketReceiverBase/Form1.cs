using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WinFormStringCnvClass;

using tcpServer;
using tcpClient;
using ServerInfoUserControl;
using SourceInfoUserControl;
using PathSearchClass;

namespace SocketReceiverBase
{
    public partial class Form1 : Form
    {
        //===================
        // Constructor
        //===================
        public Form1()
        {
            InitializeComponent();
            thisExeDirPath = Path.GetDirectoryName(Application.ExecutablePath);

            tcpSrv_LockFunctionListener = new TcpSocketServer();
            tcpClt_JudgmentReporter = new TcpSocketClient();

            LockReleaseTime = DateTime.Now;
        }

        //===================
        // Member variable
        //===================
        string thisExeDirPath;

        TcpSocketServer tcpSrv_LockFunctionListener;
        TcpSocketClient tcpClt_JudgmentReporter;

        /// <summary>
        /// Free,Lock
        /// </summary>
        string LockFunctionStatus = "Free";

        public string LockSignalSourceName = "";
        public DateTime LockReleaseTime;
        public DateTime LockTime;
        public DateTime LastCheckTime;
        public bool Locked { get { return DateTime.Now < LockReleaseTime; } }

        public int PortNumber_LockFunctionSrv
        {
            get
            {
                int b = -1;
                if (!int.TryParse(textBox_PortNumber.Text, out b)) { b = -1; }
                return b;
            }
            set
            {
                textBox_PortNumber.Text = value.ToString();
            }
        }

        /// <summary>
        /// OK,NG,Error
        /// </summary>
        public string JudgmentResultString = "OK";

        /// <summary>
        /// true : OK , false : NG
        /// </summary>
        public bool JudgmentFlag
        {
            get
            {
                return button_JudgmentResult.BackColor == Color.YellowGreen;
            }
            set
            {
                if (value)
                {
                    button_JudgmentResult.BackColor = Color.YellowGreen;
                    JudgmentResultString = "OK";
                }
                else
                {
                    button_JudgmentResult.BackColor = Color.Red;
                    JudgmentResultString = "NG";
                }

                tcpSrv_LockFunctionListener.ResponceMessage = LockFunctionStatus + "\t" + JudgmentResultString;
            }
        }

        public string ClientName
        {
            get { return textBox_clientName.Text; }
            set { textBox_clientName.Text = value; }
        }

        public string MessageOK
        {
            get { return textBox_MessageOK.Text; }
            set { textBox_MessageOK.Text = value; }
        }

        public string MessageNG
        {
            get { return textBox_MessageNG.Text; }
            set { textBox_MessageNG.Text = value; }
        }
        public string StatusNG
        {
            get { return textBox_StatusNG.Text; }
            set { textBox_StatusNG.Text = value; }
        }
        public string ParameterNG
        {
            get { return textBox_ParameterNG.Text; }
            set { textBox_ParameterNG.Text = value; }
        }
        public string CheckStyleNG
        {
            get { return textBox_CheckStyleNG.Text; }
            set { textBox_CheckStyleNG.Text = value; }
        }
        public string StatusOK
        {
            get { return textBox_StatusOK.Text; }
            set { textBox_StatusOK.Text = value; }
        }
        public string ParameterOK
        {
            get { return textBox_ParameterOK.Text; }
            set { textBox_ParameterOK.Text = value; }
        }
        public string CheckStyleOK
        {
            get { return textBox_CheckStyleOK.Text; }
            set { textBox_CheckStyleOK.Text = value; }
        }

        public int IntervalOK
        {
            get { return int.Parse(textBox_IntervalOK.Text); }
            set { textBox_IntervalOK.Text = value.ToString(); }
        }

        public int IntervalNG
        {
            get { return int.Parse(textBox_IntervalNG.Text); }
            set { textBox_IntervalNG.Text = value.ToString(); }
        }

        //===================
        // Member function
        //===================
        public void LockClient(double Minutes)
        {
            LockTime = DateTime.Now;
            LockReleaseTime = DateTime.Now.AddMinutes(Minutes);
        }

        private void LockStatusUpdate()
        {
            if (this.InvokeRequired) { this.Invoke((Action)(() => LockStatusUpdate())); }
            else
            {
                if (DateTime.Now > LockReleaseTime)
                {
                    label_LockedFrom.Text = "- Release -";
                    label_LockedTime.Text = "- Release -";
                    label_ResetTime.Text = "- Release -";
                    button_Lock.Text = "";
                    button_Lock.BackColor = Color.YellowGreen;
                }
                else
                {
                    label_LockedFrom.Text = LockSignalSourceName;
                    label_LockedTime.Text = LockTime.ToString("yyyy/MM/dd HH:mm:ss");
                    label_ResetTime.Text = LockReleaseTime.ToString("yyyy/MM/dd HH:mm:ss");
                    label_RemainingTime.Text = getElapsedTimeString(LockReleaseTime - DateTime.Now);

                    button_Lock.Text = "";
                    button_Lock.BackColor = Color.Gray;
                }
            }
        }

        public string getElapsedTimeString(TimeSpan elapsedTime)
        {
            if (elapsedTime.TotalDays >= 365) { return (elapsedTime.TotalDays / 365.2425).ToString("0") + " year"; }
            if (elapsedTime.TotalDays >= 30) { return (elapsedTime.TotalDays / 30.436875).ToString("0") + " month"; }
            if (elapsedTime.TotalDays >= 7) { return (elapsedTime.TotalDays / 7).ToString("0") + " week"; }
            if (elapsedTime.TotalDays >= 1) { return (elapsedTime.TotalDays / 7).ToString("0") + " day"; }
            if (elapsedTime.TotalHours >= 1) { return (elapsedTime.TotalHours).ToString("0") + " hour"; }
            if (elapsedTime.TotalMinutes >= 1) { return (elapsedTime.TotalMinutes).ToString("0") + " minute"; }
            if (elapsedTime.TotalSeconds >= 1) { return (elapsedTime.TotalSeconds).ToString("0") + " sec."; }
            return "now";
        }

        private void ButtonEnable(Button button, bool enable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => ButtonEnable(button, enable)));
            }
            else
            {
                if (enable)
                {
                    button.Enabled = true;
                    button.BackColor = Color.GreenYellow;
                }
                else
                {
                    button.Enabled = false;
                    button.BackColor = Color.Transparent;
                }
            }
        }

        //ServerList Set================================

        private void DeleteServerListView(int targetIndex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => DeleteServerListView(targetIndex)));
            }
            else
            {
                if (targetIndex < panel_ServerListView.Controls.Count)
                {
                    panel_ServerListView.Controls.RemoveAt(targetIndex);
                    textBox_ServerList.Text = ServerInfoViewToString();
                    UpdateLayoutServerListView();
                }
            }
        }

        private void EnableLoadServerListView()
        {
            ButtonEnable(button_LoadServerListView, true);
        }

        private void UpdateLayoutServerListView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => UpdateLayoutServerListView()));
            }
            else
            {
                panel_ServerListView.Height = 0;
                int ctrlIndex = 0;

                foreach (ServerInfo ctrl in panel_ServerListView.Controls)
                {
                    ctrl.Top = panel_ServerListView.Height;
                    ctrl.Index = ctrlIndex;
                    ctrlIndex++;

                    panel_ServerListView.Height += ctrl.Height;
                }
            }
        }

        private void UpdateServerListView()
        {
            panel_ServerListView.Controls.Clear();
            panel_ServerListView.Height = 0;
            int ctrlIndex = 0;

            string[] Lines = textBox_ServerList.Text.Replace("\r\n", "\n").Trim('\n').Split('\n');
            foreach (var Line in Lines)
            {
                if (Line == "" || Line[0] == '#' || Line.Split('\t').Length < 3) continue;
                ServerInfo serverInfo = new ServerInfo(ctrlIndex, Line); ctrlIndex++;
                AddServerInfoToPanel(panel_ServerListView, serverInfo);
            }

            if (panel_ServerListView.Controls.Count == 0)
            {
                ServerInfo serverInfo = new ServerInfo(ctrlIndex, "", "", -1); ctrlIndex++;
                AddServerInfoToPanel(panel_ServerListView, serverInfo);
            }
        }

        private void AddServerInfoToPanel(Panel panel, ServerInfo serverInfo)
        {
            serverInfo.Top = panel.Height;
            serverInfo.DeleteThis = (Action<int>)((int x) => DeleteServerListView(x));
            serverInfo.LoadThis = (Action)(() => EnableLoadServerListView());

            panel.Height += serverInfo.Height;
            panel.Controls.Add(serverInfo);
        }

        private string ServerInfoViewToString()
        {
            List<string> Lines = new List<string>();
            foreach (var ctrl in panel_ServerListView.Controls)
            {
                if (ctrl is ServerInfo)
                {
                    Lines.Add(((ServerInfo)ctrl).ToString());
                }
            }
            return string.Join("\r\n", Lines.ToArray());
        }

        private void SendMessageForServers(string request)
        {
            foreach (var ctrl in panel_ServerListView.Controls)
            {
                if (ctrl is ServerInfo)
                {
                    ((ServerInfo)ctrl).SendMessage(request);
                }
            }
        }

        //SourceList Set================================

        private void DeleteSourceListView(int targetIndex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => DeleteSourceListView(targetIndex)));
            }
            else
            {
                if (targetIndex < panel_SourceListView.Controls.Count)
                {
                    panel_SourceListView.Controls.RemoveAt(targetIndex);
                    textBox_SourceList.Text = SourceInfoViewToString();
                    UpdateLayoutSourceListView();
                }
            }
        }
        
        private void EnableLoadSourceListView()
        {
            ButtonEnable(button_LoadSourceListView, true);
        }

        private void UpdateLayoutSourceListView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => UpdateLayoutSourceListView()));
            }
            else
            {
                panel_SourceListView.Height = 0;

                int ctrlIndex = 0;
                foreach (SourceInfo ctrl in panel_SourceListView.Controls)
                {
                    ctrl.Top = panel_SourceListView.Height;
                    ctrl.Index = ctrlIndex;
                    panel_SourceListView.Height += ctrl.Height;

                    ctrlIndex++;
                }
            }
        }

        private void UpdateSourceListView()
        {
            string[] Lines = textBox_SourceList.Text.Replace("\r\n", "\n").Trim('\n').Split('\n');
            panel_SourceListView.Controls.Clear();
            panel_SourceListView.Height = 0;

            int ctrlIndex = 0;
            foreach (var Line in Lines)
            {
                if (Line == "" || Line[0] == '#' || Line.Split('\t').Length < 3) continue;
                SourceInfo sourceInfo = new SourceInfo(ctrlIndex, Line); ctrlIndex++;
                AddSourceInfoToPanel(panel_SourceListView, sourceInfo);
            }

            if (panel_SourceListView.Controls.Count == 0)
            {
                SourceInfo sourceInfo = new SourceInfo(ctrlIndex, "", "", ""); ctrlIndex++;
                AddSourceInfoToPanel(panel_SourceListView, sourceInfo);
            }
        }

        private void AddSourceInfoToPanel(Panel panel, SourceInfo ctrl)
        {
            ctrl.Top = panel.Height;
            ctrl.DeleteThis = (Action<int>)((int x) => DeleteSourceListView(x));
            ctrl.LoadThis = (Action)(() => EnableLoadSourceListView());

            panel.Height += ctrl.Height;
            panel.Controls.Add(ctrl);
        }

        private string SourceInfoViewToString()
        {
            List<string> Lines = new List<string>();
            foreach (var ctrl in panel_SourceListView.Controls)
            {
                if (ctrl is SourceInfo)
                {
                    Lines.Add(((SourceInfo)ctrl).ToString());
                }
            }
            return string.Join("\r\n", Lines.ToArray());
        }

        public void sendMessageOK()
        {
            string request =
                ClientName + "\t" +
                StatusOK + "\t" +
                MessageOK + "\t" +
                ParameterOK + "\t" +
                CheckStyleOK;

            SendMessageForServers(request);
        }

        public void sendMessageNG()
        {
            string request = "";

            if (Locked)
            {
                request =
                    ClientName + "\t" +
                    StatusOK + "\t" +
                    MessageNG + "\t" +
                    ParameterNG + "\t" +
                    CheckStyleNG;
            }
            else
            {
                request =
                    ClientName + "\t" +
                    StatusNG + "\t" +
                    MessageNG + "\t" +
                    ParameterNG + "\t" +
                    CheckStyleNG;
            }
            SendMessageForServers(request);
        }

        //===================
        // Event
        //===================
        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TEXT|*.txt";
            if (false && ofd.ShowDialog() == DialogResult.OK)
            {
                WinFormStringCnv.setControlFromString(this, File.ReadAllText(ofd.FileName));
            }
            else
            {
                string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
                if (File.Exists(paramFilename))
                {
                    WinFormStringCnv.setControlFromString(this, File.ReadAllText(paramFilename));
                }
            }

            panel_ServerListView.Height = 0;
            UpdateServerListView();

            
            UpdateSourceListView();

            timer_LockFunctionListenerQueueUpdate.Start();
            timer_SendMessage.Start();

            tcpSrv_LockFunctionListener.StartListening(PortNumber_LockFunctionSrv, "UTF8");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string FormContents = WinFormStringCnv.ToString(this);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";

            if (false && sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, FormContents);
            }
            else
            {
                string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
                File.WriteAllText(paramFilename, FormContents);
            }
        }

        private void timer_LockFunctionListenerQueueUpdate_Tick(object sender, EventArgs e)
        {
            timer_LockFunctionListenerQueueUpdate.Stop();
            //============
            // New data check from Queue
            //============
            if ((tcpSrv_LockFunctionListener.LastReceiveTime - LastCheckTime).TotalSeconds > 0 && tcpSrv_LockFunctionListener.ReceivedSocketQueue.Count > 0)
            {
                string receivedSocketMessage = "";

                //============
                // ReadQueue 
                //============
                while (tcpSrv_LockFunctionListener.ReceivedSocketQueue.TryDequeue(out receivedSocketMessage))
                {
                    Debug.WriteLine(receivedSocketMessage);
                    if (receivedSocketMessage.IndexOf("Lock") >= 0)
                    {
                        string[] cols = receivedSocketMessage.Split('\t');

                        if (cols.Length > 2 && int.TryParse(cols[2], out int Minutes))
                        {
                            LockTime = DateTime.Now;
                            LockReleaseTime = DateTime.Now + new TimeSpan(0, Minutes, 0);
                        }

                        if (cols.Length > 3 )
                        {
                            LockSignalSourceName = cols[3];
                        }
                    }
                    else if (receivedSocketMessage.IndexOf("Release") >= 0)
                    {
                        LockReleaseTime = DateTime.Now;
                    }
                }
                LastCheckTime = DateTime.Now;
            }

            LockStatusUpdate();
            timer_LockFunctionListenerQueueUpdate.Start();
        }

        int timer_SendMessage_Count = 0;
        bool timer_SendMessage_LastJudgmentFlag = true;
        private void timer_SendMessage_Tick(object sender, EventArgs e)
        {
            timer_SendMessage.Stop();

            if (timer_SendMessage_LastJudgmentFlag != JudgmentFlag && !JudgmentFlag)
            {
                timer_SendMessage_Count = -1;
            };

            if (JudgmentFlag && timer_SendMessage_Count < 0)
            {
                sendMessageOK();
                if (!int.TryParse(textBox_IntervalOK.Text, out timer_SendMessage_Count)) { timer_SendMessage_Count = 300; }
            }
            else if (!JudgmentFlag && timer_SendMessage_Count < 0)
            {
                sendMessageNG();
                if (!int.TryParse(textBox_IntervalNG.Text, out timer_SendMessage_Count)) { timer_SendMessage_Count = 10; }
            }

            timer_SendMessage_Count--;
            timer_SendMessage.Start();
        }

        private void button_JudgmentResult_Click(object sender, EventArgs e)
        {
            JudgmentFlag = !JudgmentFlag;
        }

        private void button_Restrart_Click(object sender, EventArgs e)
        {
            tcpSrv_LockFunctionListener.StopListening();
            tcpSrv_LockFunctionListener.StartListening(PortNumber_LockFunctionSrv, "UTF8");
        }

        private void tabControl_ServerInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_ServerInfo.SelectedTab == tabPage_ServerView)
            {
                UpdateServerListView();
            }
            else if (tabControl_ServerInfo.SelectedTab == tabPage_ServerList)
            {
                textBox_ServerList.Text = ServerInfoViewToString();
            }
        }

        private void button_AddServerList_Click(object sender, EventArgs e)
        {
            int ctrlIndex = panel_ServerListView.Controls.Count;

            ServerInfo ctrl = new ServerInfo(ctrlIndex);
            ctrl.Top = panel_ServerListView.Height;
            ctrl.DeleteThis = (Action<int>)((int x) => DeleteServerListView(x));
            ctrl.LoadThis = (Action)(() => EnableLoadServerListView());

            panel_ServerListView.Controls.Add(ctrl);
            panel_ServerListView.Height += ctrl.Height;
            textBox_ServerList.Text = ServerInfoViewToString();
        }

        private void button_LoadServerListView_Click(object sender, EventArgs e)
        {
            textBox_ServerList.Text = ServerInfoViewToString();
            ButtonEnable(button_LoadServerListView, false);
        }

        private void tabControl_SourceInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_SourceInfo.SelectedTab == tabPage_SourceView)
            {
                UpdateSourceListView();
            }
            else if (tabControl_SourceInfo.SelectedTab == tabPage_SourceList)
            {
                textBox_SourceList.Text = SourceInfoViewToString();
            }
        }

        private void button_AddSourceList_Click(object sender, EventArgs e)
        {
            int ctrlIndex = panel_SourceListView.Controls.Count;

            SourceInfo ctrl = new SourceInfo(ctrlIndex);
            ctrl.Top = panel_SourceListView.Height;
            ctrl.DeleteThis = (Action<int>)((int x) => DeleteSourceListView(x));
            ctrl.LoadThis = (Action)(() => EnableLoadSourceListView());

            panel_SourceListView.Controls.Add(ctrl);
            panel_SourceListView.Height += ctrl.Height;
            textBox_SourceList.Text = SourceInfoViewToString();
        }

        private void button_LoadSourceListView_Click(object sender, EventArgs e)
        {
            textBox_SourceList.Text = SourceInfoViewToString();
            ButtonEnable(button_LoadSourceListView, false);
        }

        private void timer_FileCheck_Tick(object sender, EventArgs e)
        {
            foreach (var ctrl in panel_SourceListView.Controls)
            {
                if (ctrl is SourceInfo)
                {
                    SourceInfo sourceInfo = (SourceInfo)ctrl;
                    string[] FilePaths = PathSearch.NewerFilesFromDateDirectory(sourceInfo.SaveDirPath, sourceInfo.LastCheckTime, searchPattern: "*.csv");

                    if (FilePaths.Length > 0)
                    {
                        Array.Sort(FilePaths);
                        sourceInfo.LastCheckTime = PathSearch.GetCreateTimeFromFilePath(FilePaths[FilePaths.Length - 1]);
                    }

                }
            }
        }
    }
}
