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

            tcpSrv = new TcpSocketServer();
            tcpClt = new TcpSocketClient();
            LockReleaseTime = DateTime.Now;
        }

        //===================
        // Member variable
        //===================
        string thisExeDirPath;

        TcpSocketServer tcpSrv;
        TcpSocketClient tcpClt;

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

        public bool Locked { get { return (DateTime.Now - LockReleaseTime).TotalSeconds < 0; } }

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

        public string MessageSeverAddress
        {
            get { return textBox_MessageServerAddress.Text; }
            set { textBox_MessageServerAddress.Text = value; }
        }

        public int MessageServerPort
        {
            get { int b = -1; if (!int.TryParse(textBox_MessageServerPort.Text, out b)) b = -1; return b; }
            set { textBox_MessageServerPort.Text = value.ToString(); }
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
        public void LockClient(int Minutes)
        {
            LockTime = DateTime.Now;
            LockReleaseTime = DateTime.Now + new TimeSpan(0, Minutes, 0);
        }

        private void LabelUpdate()
        {
            if ((DateTime.Now - LockReleaseTime).TotalSeconds > 0)
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


            tcpClt = new TcpSocketClient();

            timer_UpdateQueue.Start();
            timer_LabelUpdate.Start();
            timer_SendMessage.Start();
            tcpSrv.StartListening(PortNumberSrv, "UTF8");

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

            if (OK)
            {
                OK = true;

                string request =  ClientName
                    + "\t" + StatusOK
                    + "\t" + MessageOK
                    + "\t" + ParameterOK
                    + "\t" + CheckStyleOK;

                int messageServerPort = MessageServerPort;

                if (messageServerPort >= 1024)
                {
                    Task task = tcpClt.StartClient(MessageSeverAddress, messageServerPort, request);
                    
                }

            }
            else
            {
                OK = false;

                string request =  ClientName
                    + "\t" + StatusNG
                    + "\t" + MessageNG
                    + "\t" + ParameterNG
                    + "\t" + CheckStyleNG;

                int messageServerPort = MessageServerPort;

                if (messageServerPort >= 1024)
                {
                    Task task = tcpClt.StartClient(MessageSeverAddress, messageServerPort, request);
                }
            }

            timer_SendMessage.Start();
        }

        private void button_Restrart_Click(object sender, EventArgs e)
        {
            tcpSrv.StopListening();

            tcpSrv.StartListening(PortNumberSrv, "UTF8");
        }
    }
}
