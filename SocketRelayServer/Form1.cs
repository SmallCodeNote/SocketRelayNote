using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

using FluentScheduler;

using LiteDB;

using WinFormStringCnvClass;


namespace tcpServer
{
    public partial class Form1 : Form
    {
        TcpSocketServer tcp;
        NoticeTransmitter noticeTransmitter;
        List<ClientData> clientList;
        AddressBook addressBook;

        DateTime LastCheckTime;
        string thisExeDirPath;
        string datebasePath;
        int portNumber;

        ConnectionString _LiteDBconnectionString;
        static int _dbOpenRetryCountMax = 10;



        public Form1()
        {
            InitializeComponent();
            JobManager.Initialize();

            tcp = new TcpSocketServer();


            noticeTransmitter = new NoticeTransmitter(checkBox_debugModeSwitch.Checked);
            noticeTransmitter.StartNoticeCheck();

            thisExeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            datebasePath = Path.Combine(thisExeDirPath, "lite.db");
            LastCheckTime = DateTime.Now;
            portNumber = -1;

            //this.panel_StatusListFrame.MouseWheel += new MouseEventHandler(this.panel_StatusListFrame_MouseWheel);
            //this.tabPage_Status.MouseWheel += new MouseEventHandler(this.panel_StatusListFrame_MouseWheel);

            _LiteDBconnectionString = new ConnectionString();
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            clientList = new List<ClientData>();

        }

        //=====================
        #region topForm Event
        //=====================
        async private void Form1_Load(object sender, EventArgs e)
        {


            string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");

            if (File.Exists(paramFilename))
            {
                WinFormStringCnv.setControlFromString(this, File.ReadAllText(paramFilename));
            }

            ClientListInitialize();
            AddressListInitialize();
            SchedulerInitialize();
            timer_updateStatus.Start();

            int.TryParse(textBox_httpTimeout.Text, out noticeTransmitter.httpTimeout);

            if (checkBox_serverAutoStart.Checked)
            {
                try
                {
                    portNumber = int.Parse(textBox_portNumber.Text);

                    button_Start.Text = "Stop";
                    timer_UpdateList.Start();

                    if (!await tcp.StartListening(portNumber, "UTF8"))
                    {
                        toolStripStatusLabel1.Text = "TCP Listening Start Error";
                        button_Start.Text = "Start";
                        return;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
                }
            }
            else
            {
                tabPage_Status.Select();
            }

        }

        async private void button_Start_Click(object sender = null, EventArgs e = null)
        {
            if (button_Start.Text == "Start" && int.TryParse(textBox_portNumber.Text, out portNumber))
            {
                button_Start.Text = "Stop";
                timer_UpdateList.Start();

                if (!await tcp.StartListening(portNumber, "UTF8"))
                {
                    toolStripStatusLabel1.Text = "TCP Listening Start Error";
                    return;
                }

            }
            else if (int.TryParse(textBox_portNumber.Text, out portNumber))
            {
                textBox_portNumber.Select();
            }
            else
            {
                tcp.StopListening();
                button_Start.Text = "Start";

            }

        }

        private void timer_UpdateList_Tick(object sender, EventArgs e)
        {
            timer_UpdateList.Stop();

            textBox_queueList.Text = DateTime.Now.ToString("HH:mm:ss") + "\t" + tcp.ReceivedSocketQueue.Count.ToString() + "\r\n";

            //============
            // New data check from Queue
            //============
            if ((tcp.LastReceiveTime - LastCheckTime).TotalSeconds > 0 && tcp.ReceivedSocketQueue.Count > 0)
            {
                List<string> queueList = new List<string>();

                string receivedSocketMessage = "";

                //============
                // ReadQueue and Entry dataBase file
                //============
                while (tcp.ReceivedSocketQueue.TryDequeue(out receivedSocketMessage))
                {
                    queueList.Add(receivedSocketMessage);
                    string[] cols = receivedSocketMessage.Split('\t');

                    string dbFilename = textBox_DataBaseFilePath.Text;

                    if (cols.Length >= 4 && File.Exists(dbFilename))
                    {
                        DateTime connectTime;
                        if (DateTime.TryParse(cols[0], out connectTime)) { connectTime = DateTime.Now; } else { continue; }

                        string clientName = cols[1];
                        string status = cols[2];
                        string message = cols[3];
                        string parameter = cols.Length > 4 ? cols[4] : "";
                        string checkStyle = cols[5];

                        try
                        {
                            SocketMessage socketMessage = new SocketMessage(connectTime, clientName, status, message, parameter, checkStyle);
                            string key = socketMessage.Key();

                            _LiteDBconnectionString.Filename = dbFilename;

                            using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                            {
                                ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");
                                col.Insert(key, socketMessage);
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
                        }

                    }

                }

                textBox_queueList.Text += "\t" + string.Join("\r\n\t", queueList.ToArray());

                //========================
                // NoticeCheck from dataBase
                //========================
                for (int retryCount = 0; retryCount < _dbOpenRetryCountMax; retryCount++)
                {
                    try
                    {
                        string dbFilename = textBox_DataBaseFilePath.Text;

                        if (!File.Exists(dbFilename)) break;
                        _LiteDBconnectionString.Filename = dbFilename;
                        using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                        {
                            ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");

                            foreach (var target in clientList)
                            {
                                var latestNoticeRecord = col.Query()
                                    .Where(x => !x.check && x.clientName == target.clientName)
                                    .OrderByDescending(x => x.connectTime)
                                    .FirstOrDefault();

                                if (latestNoticeRecord != null && latestNoticeRecord.connectTime > latestNoticeRecord.connectTime)
                                {
                                    noticeTransmitter.AddNotice(target, latestNoticeRecord);
                                }

                            }

                        }

                        break;

                    }
                    catch (Exception ex)
                    {
                        Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry:" + retryCount.ToString());
                        Debug.WriteLine(ex.ToString());
                        Thread.Sleep(100);

                        if (retryCount == _dbOpenRetryCountMax - 1) throw;

                    }
                }

                LastCheckTime = DateTime.Now;

            }

            //========================
            // TimeoutCheck
            //========================
            for (int retryCount = 0; retryCount < _dbOpenRetryCountMax; retryCount++)
            {
                try
                {
                    string dbFilename = textBox_DataBaseFilePath.Text;
                    if (!File.Exists(dbFilename)) break;

                    _LiteDBconnectionString.Filename = dbFilename;
                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");

                        foreach (var target in clientList)
                        {
                            //MessageRecord from Client
                            var latestRecord = col.Query()
                                .Where(x => x.clientName == target.clientName && x.status != "Timeout")
                                .OrderByDescending(x => x.connectTime)
                                .FirstOrDefault();

                            //MessageRecord Timeout
                            var listedTimeoutMessage = col.Query()
                                .Where(x => x.clientName == target.clientName && x.status == "Timeout" && !x.check)
                                .OrderByDescending(x => x.connectTime).ToList();


                            //First Time
                            if (target.lastAccessTime == null)
                            {
                                target.lastAccessTime = DateTime.Now;
                            };

                            //Acccess Time Update
                            if (latestRecord != null && target.lastAccessTime < latestRecord.connectTime)
                            {
                                target.lastAccessTime = latestRecord.connectTime;
                            };


                            bool flag1 = (DateTime.Now - target.lastAccessTime).TotalSeconds > target.timeoutLength;
                            bool flag2 = listedTimeoutMessage.Count == 0;

                            if (target.timeoutCheck && flag1 && flag2)
                            {
                                SocketMessage timeoutMessage = new SocketMessage(target.lastAccessTime, target.clientName, "Timeout", target.timeoutMessage, "", "Ever");
                                noticeTransmitter.AddNotice(target, timeoutMessage);
                                target.lastTimeoutDetectedTime = DateTime.Now;
                            }

                        }

                    }
                    break;
                }
                catch (Exception ex)
                {
                    Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry:" + retryCount.ToString());
                    Debug.WriteLine(ex.ToString());
                    Thread.Sleep(100);

                    if (retryCount == _dbOpenRetryCountMax - 1) throw;

                }
            }


            if (button_Start.Text != "Start")
            {
                toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                timer_UpdateList.Start();
            }
            else
            {
                toolStripStatusLabel1.Text = "Stop TCP Listener.";
            }

        }

        #endregion
        //=====================


        //=====================
        #region tabPage_Setting Event
        //=====================
        private void button_getDataBaseFilePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "LiteDB File|*.db";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            textBox_DataBaseFilePath.Text = sfd.FileName;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string FormContents = WinFormStringCnv.ToString(this);
            string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
            File.WriteAllText(paramFilename, FormContents);
        }


        private void tabPage_Status_Enter(object sender, EventArgs e)
        {

            int ClientCount = dataGridView_ClientList.Rows.Count - 1;
            panel_StatusList.Height = ClientCount * 100;

            if (panel_StatusList.Controls.Count != ClientCount)
            {

                clearControlCollection(panel_StatusList.Controls);

                int TopBuff = 0;
                for (int i = 0; i < ClientCount; i++)
                {
                    panel_StatusList.Controls.Add(new MessageItemView(noticeTransmitter, _LiteDBconnectionString));
                    panel_StatusList.Controls[i].Top = TopBuff;
                    ((MessageItemView)panel_StatusList.Controls[i]).groupBox_ClientName.Text = dataGridView_ClientList.Rows[i].Cells[0].Value.ToString();

                    TopBuff += panel_StatusList.Controls[i].Height;

                }

                updateStatusList();

            }

        }


        #endregion
        //=====================

        private void clearControlCollection(System.Windows.Forms.Control.ControlCollection cc)
        {
            for (int i = 0; i < cc.Count; i++)
            {
                cc[i].Dispose();

            }
            cc.Clear();

        }

        private void tabPage_Log_Enter(object sender, EventArgs e)
        {
            try
            {

                string dbFilename = textBox_DataBaseFilePath.Text;
                if (!File.Exists(dbFilename)) return;

                _LiteDBconnectionString.Filename = dbFilename;

                using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                {
                    var col = litedb.GetCollection<SocketMessage>("table_Message");

                    try
                    {
                        ILiteQueryable<SocketMessage> query = col.Query().OrderBy(x => x.connectTime, 0);
                        if (query.Count() > 0)
                        {
                            List<string> Lines = new List<string>();
                            foreach (SocketMessage socketMessage in query.ToArray())
                            {
                                Lines.Add(socketMessage.ToString());
                            }

                            textBox_Log.Text = String.Join("\r\n", Lines.ToArray());
                        }

                        label_LogUpdateTime.Text = "Log Update ... " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
            }

        }

        private void button_LogReload_Click(object sender, EventArgs e)
        {
            tabPage_Log_Enter(null, null);
        }

        private void DataGridView_CellPainting_AddRowIndex(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void button_ClientListLoad_Click(object sender, EventArgs e)
        {
            ClientListInitialize();


        }

        private void button_AddressListLoad_Click(object sender, EventArgs e)
        {
            AddressListInitialize();
        }

        private void button_SchedulerList_Click(object sender, EventArgs e)
        {
            SchedulerInitialize();
        }


        private void textBox_httpTimeout_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_httpTimeout.Text, out noticeTransmitter.httpTimeout);

        }

        private void button_CreateDammyData_Click(object sender, EventArgs e)
        {
            TimeSpan TP = new TimeSpan(0, 8, 0, 0);

            string dbFilename = textBox_DataBaseFilePath.Text;
            if (!File.Exists(dbFilename)) return;

            _LiteDBconnectionString.Filename = dbFilename;

            using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
            {
                for (DateTime connectTime = DateTime.Parse("2020/01/01"); connectTime < DateTime.Parse("2024/01/31"); connectTime += TP)
                {
                    SocketMessage socketMessage = new SocketMessage(connectTime, "Test", "Test", "Test", "parameterTest");
                    ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");

                    col.Insert(socketMessage.Key(), socketMessage);

                }
            }
        }

        private void button_BreakupDatabasefile_Click(object sender, EventArgs e)
        {
            string dbFilename = textBox_DataBaseFilePath.Text;
            if (!File.Exists(dbFilename)) return;

            BreakupLightDBFile job = new BreakupLightDBFile(dbFilename, int.Parse(textBox_PostTime.Text));
            job.BreakupLightDBFile_byMonthFile(dbFilename, int.Parse(textBox_PostTime.Text));
        }

        private void dataGridView_ClientList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            button_ClientListLoad.Enabled = true;
            button_ClientListLoad.BackColor = Color.GreenYellow;
        }

        private void dataGridView_ClientList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            ButtonEnable(button_ClientListLoad, true);
        }

        private void dataGridView_AddressList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ButtonEnable(button_AddressListLoad, true);
        }

        private void dataGridView_SchedulerList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ButtonEnable(button_SchedulerList, true);
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

        private void textBox_portNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int.TryParse(textBox_portNumber.Text, out portNumber);
            }

        }

        private void timer_updateStatus_Tick(object sender, EventArgs e)
        {
            timer_updateStatus.Stop();
            if (tabControl_Top.SelectedTab == tabPage_Status)
            {
                updateStatusList();

            }
            timer_updateStatus.Start();
        }

        private void checkBox_debugModeSwitch_CheckedChanged(object sender, EventArgs e)
        {
            noticeTransmitter._debug = checkBox_debugModeSwitch.Checked;
        }
    }

}
