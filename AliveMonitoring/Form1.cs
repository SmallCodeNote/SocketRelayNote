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
using SocketSignalServer;

namespace AliveMonitoring
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

            tcpSrv = new TcpSocketServer();
            tcpClt = new TcpSocketClient();
            LockReleaseTime = DateTime.Now;

            noticeTransmitter = new NoticeTransmitter(true);
            noticeTransmitter.StartNoticeCheck();

            icon_voiceON = Properties.Resources.VoiceON048;
            icon_voiceOFF = Properties.Resources.VoiceOFF048;

        }

        //===================
        // Member variable
        //===================
        string thisExeDirPath;
        Bitmap icon_voiceON;
        Bitmap icon_voiceOFF;

        TcpSocketServer tcpSrv;
        TcpSocketClient tcpClt;

        NoticeTransmitter noticeTransmitter;
        AddressBook addressBook;
        ClientData target;
        int TimeOutThreshold = 5;

        /// <summary>
        /// Free,Lock
        /// </summary>
        string RunMode = "Free";

        /// <summary>
        /// OK,NG,Error
        /// </summary>
        string StatusMode = "OK";

        public string LockSignalSourceName = "";

        public DateTime LockReleaseTime;
        public DateTime LockTime;

        DateTime LastCheckTime;

        public bool Locked { get { return DateTime.Now < LockReleaseTime; } }

        public bool OK
        {

            get
            {
                return button_StatusChange.BackColor == Color.YellowGreen;
            }
            set
            {
                if (value)
                {
                    button_StatusChange.BackColor = Color.YellowGreen;
                    StatusMode = "OK";
                }
                else
                {
                    button_StatusChange.BackColor = Color.Red;
                    StatusMode = "NG";
                }

                tcpSrv.ResponceMessage = RunMode + "\t" + StatusMode;
            }
        }

        public int PortNumberSrv
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

        public int Interval
        {
            get { return int.Parse(textBox_MessageInterval.Text); }
            set { textBox_MessageInterval.Text = value.ToString(); }
        }

        //===================
        // Member function
        //===================
        public void LockClient(int Minutes)
        {
            LockTime = DateTime.Now;
            LockReleaseTime = DateTime.Now + new TimeSpan(0, Minutes, 0);
        }

        private void LabelUpdate()
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

        private void update_tabPage_View()
        {

            string[] Lines = textBox_ServerList.Text.Replace("\r\n", "\n").Trim('\n').Split('\n');

            panel_View.Controls.Clear();

            int TopPosition = 0;
            foreach (var Line in Lines)
            {
                if (Line == "" || Line[0] == '#' || Line.Split('\t').Length < 3) continue;
                ServerInfo serverInfo = new ServerInfo(Line);
                serverInfo.Top = TopPosition;
                TopPosition += serverInfo.Height;
                panel_View.Controls.Add(serverInfo);
            }

            if (panel_View.Controls.Count == 0)
            {
                ServerInfo serverInfo = new ServerInfo("", "", -1);
                serverInfo.Top = TopPosition;
                TopPosition += serverInfo.Height;
                panel_View.Controls.Add(serverInfo);
            }

            panel_View.Height = TopPosition;
        }

        private bool checkServerAliving()
        {
            bool alive = true;
            foreach (var ctrl in panel_View.Controls)
            {
                if (ctrl is ServerInfo)
                {
                    alive = alive && (((ServerInfo)ctrl).Alive);
                }
            }
            return alive;
        }

        private string ServerInfoViewToString()
        {
            List<string> Lines = new List<string>();
            foreach (var ctrl in panel_View.Controls)
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
            int TimeOutCountMax = 0;
            List<string> ServerNames = new List<string>();
            foreach (var ctrl in panel_View.Controls)
            {
                if (ctrl is ServerInfo)
                {
                    ((ServerInfo)ctrl).SendMessage(request);
                    if (TimeOutCountMax < ((ServerInfo)ctrl).TimeOutCount) { TimeOutCountMax = ((ServerInfo)ctrl).TimeOutCount; }
                    if (TimeOutThreshold < ((ServerInfo)ctrl).TimeOutCount) { ServerNames.Add(((ServerInfo)ctrl).ServerName); ((ServerInfo)ctrl).TimeOutCount = 0; }
                }

            }

            if (ServerNames.Count > 0)
            {
                SocketMessage timeoutMessage = new SocketMessage(target.lastAccessTime, target.clientName, "Timeout", target.timeoutMessage.Replace("{ServerName}", string.Join(",", ServerNames.ToArray())), textBox_TimeoutMessageParameter.Text, "Once");
                noticeTransmitter.AddNotice(target, timeoutMessage);
                target.lastTimeoutDetectedTime = DateTime.Now;
            }

        }

        private void AddressListInitialize()
        {
            List<string> addressList = new List<string>();
            for (int i = 0; i < dataGridView_AddressList.RowCount - 1; i++)
            {
                var cells = dataGridView_AddressList.Rows[i].Cells;
                string code = cells[0].Value.ToString();
                code += cells.Count > 1 && cells[1].Value != null ? "\t" + cells[1].Value.ToString() : "\t";

                if (code != "") addressList.Add(code);
            }
            addressBook = new AddressBook(addressList.ToArray());
            ButtonEnable(button_AddressListLoad, false);
        }

        private void ButtonEnable(Button button, bool enable)
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

            update_tabPage_View();

            tcpClt = new TcpSocketClient();

            timer_UpdateQueue.Start();
            timer_LabelUpdate.Start();
            timer_SendMessage.Start();
            tcpSrv.StartListening(PortNumberSrv, "UTF8");

            AddressListInitialize();
            int.TryParse(textBox_httpTimeout.Text, out noticeTransmitter.httpTimeout);

            string clientName = textBox_clientName.Text;
            string addressKeys = "ALL";
            string timeoutCheck = "false";
            string timeoutLength = "0";
            string timeoutMessage = textBox_TimeoutMessage.Text;

            string code = clientName + "\t" + timeoutCheck + "\t" + timeoutLength + "\t" + timeoutMessage;
            target = new ClientData(code, addressBook.getAddress(addressKeys));

            checkBox_voiceOffSwitch_CheckedChanged();
            button_ResetMessageInterval_Click();

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

        private void button_StatusChange_Click(object sender, EventArgs e)
        {
            OK = !OK;
        }

        private void button_StatusChange_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void timer_LabelUpdate_Tick_Tick(object sender, EventArgs e)
        {
            LabelUpdate();
        }

        private void timer_UpdateQueue_Tick(object sender, EventArgs e)
        {
            timer_UpdateQueue.Stop();
            //============
            // New data check from Queue
            //============
            if ((tcpSrv.LastReceiveTime - LastCheckTime).TotalSeconds > 0 && tcpSrv.ReceivedSocketQueue.Count > 0)
            {
                string receivedSocketMessage = "";

                //============
                // ReadQueue 
                //============
                while (tcpSrv.ReceivedSocketQueue.TryDequeue(out receivedSocketMessage))
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
                    }
                    else if (receivedSocketMessage.IndexOf("Release") >= 0)
                    {
                        LockReleaseTime = DateTime.Now;

                    }
                }
                LastCheckTime = DateTime.Now;
            }
            timer_UpdateQueue.Start();
        }

        private void timer_SendMessage_Tick(object sender, EventArgs e)
        {
            timer_SendMessage.Stop();

            OK = checkServerAliving();

            if (OK)
            {
                OK = true;

                string request =
                    ClientName + "\t" +
                    StatusOK + "\t" +
                    MessageOK + "\t" +
                    ParameterOK + "\t" +
                    CheckStyleOK;

                SendMessageForServers(request);
            }
            else
            {
                OK = false;
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



            timer_SendMessage.Start();
        }

        private void button_Restrart_Click(object sender, EventArgs e)
        {
            tcpSrv.StopListening();
            tcpSrv.StartListening(PortNumberSrv, "UTF8");
        }

        private void tabControl_ServerInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_ServerInfo.SelectedTab == tabPage_View)
            {
                update_tabPage_View();
            }
            else if (tabControl_ServerInfo.SelectedTab == tabPage_List)
            {
                textBox_ServerList.Text = ServerInfoViewToString();
            }
        }

        private void button_AddressListLoad_Click(object sender, EventArgs e)
        {
            AddressListInitialize();
        }

        private void dataGridView_AddressList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 && e.RowIndex >= 0 && e.RowIndex < ((DataGridView)sender).RowCount - 1)
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        private void dataGridView_AddressList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ButtonEnable(button_AddressListLoad, true);
        }

        private void toolStripDropDownButton_VoiceSwitch_Click(object sender, EventArgs e)
        {
            if (toolStripDropDownButton_VoiceSwitch.Image == icon_voiceOFF)
            {
                checkBox_voiceOffSwitch.Checked = false;
            }
            else
            {
                checkBox_voiceOffSwitch.Checked = true;
            }
        }

        private void checkBox_voiceOffSwitch_CheckedChanged(object sender = null, EventArgs e = null)
        {
            noticeTransmitter._voiceOFF = checkBox_voiceOffSwitch.Checked;

            if (checkBox_voiceOffSwitch.Checked)
            {
                toolStripDropDownButton_VoiceSwitch.Image = icon_voiceOFF;
            }
            else
            {
                toolStripDropDownButton_VoiceSwitch.Image = icon_voiceON;
            }
        }

        private void label_SetClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(((Label)sender).Text);
        }

        private void button_ResetMessageInterval_Click(object sender=null, EventArgs e = null)
        {
            int b = 5;
            int c = 30;

            if (!int.TryParse(textBox_MessageInterval.Text, out b)) { b = 5; }

            timer_SendMessage.Stop();
            timer_SendMessage.Interval = b * 1000;
            timer_SendMessage.Start();

            if (!int.TryParse(textBox_TimeOutCheckInterval.Text, out c)) { c = 30; }
            TimeOutThreshold = c / b;

            if (TimeOutThreshold < 3) { TimeOutThreshold = 3; textBox_TimeOutCheckInterval.Text = (TimeOutThreshold * b).ToString(); }

        }
    }
}
