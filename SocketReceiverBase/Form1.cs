using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        TcpSocketServer tcpSrv;
        TcpSocketClient tcpClt;

        string thisExeDirPath;

        string RunMode = "Free";//Free,Lock
        string StatusMode = "OK";//OK,NG,Error
        public string LockSignalSourceName = "";

        public DateTime LockReleaseTime;
        public DateTime LockTime;

        DateTime LastCheckTime;

        public bool Locked { get { return (DateTime.Now - LockReleaseTime).TotalSeconds < 0; } }

        public bool Error
        {
            get
            {
                return button_StatusChange.BackColor == Color.Red;
            }
            set
            {
                if (value)
                {
                    button_StatusChange.BackColor = Color.Red;
                    StatusMode = "NG";
                }
                else
                {
                    button_StatusChange.BackColor = Color.YellowGreen;
                    StatusMode = "OK";
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
            timer_UpdateQueue.Start();
            timer_LabelUpdate.Start();
            Task<bool> srvTask = tcpSrv.StartListening(PortNumberSrv, "UTF8");
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
            Error = !Error;
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
    }
}
