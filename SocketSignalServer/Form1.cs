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
using tcpServer;


namespace SocketSignalServer
{
    public partial class Form1 : Form
    {
        //===================
        // Constructor
        //===================
        public Form1()
        {
            InitializeComponent();
            JobManager.Initialize();

            tcpSrv = new TcpSocketServer();

            noticeTransmitter = new NoticeTransmitter(checkBox_voiceOffSwitch.Checked);
            noticeTransmitter.StartNoticeCheck();

            thisExeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            LastCheckTime = DateTime.Now;
            portNumber = -1;

            _LiteDBconnectionString = new ConnectionString();
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            clientList = new List<ClientData>();

            random = new Random();

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
        int portNumber;
        List<ClientData> clientList;

        NoticeTransmitter noticeTransmitter;
        AddressBook addressBook;

        DateTime LastCheckTime;

        ConnectionString _LiteDBconnectionString;

        /// <summary>
        /// database open retry period in second.
        /// </summary>
        public double retryPeriod { get { return (_retryCountMax * _retryWait) / 1000.0; } set { _retryCountMax = (int)((value * 1000.0) / _retryWait); } }
        private int _retryCountMax = 60;

        /// <summary>
        /// retry wait in millisecond.
        /// </summary>
        public int retryWait { get { return _retryWait; } set { double retryPeriodBuff = retryPeriod; _retryWait = value; retryPeriod = retryPeriodBuff; } }
        private int _retryWait = 500;

        private Random random;


        //===================
        // Member function
        //===================
        private void DebugOutDirPathReset(string targetDir)
        {
            if (targetDir == "{ExecutablePath}") { targetDir = Path.GetDirectoryName(Application.ExecutablePath); }
            if (Directory.Exists(targetDir))
            {
                string outFilename = Path.Combine(targetDir, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd_HHmm").Substring(0, 12) + "0.txt");
                if (!Directory.Exists(Path.GetDirectoryName(outFilename))) { Directory.CreateDirectory(Path.GetDirectoryName(outFilename)); };

                DefaultTraceListener dtl = (DefaultTraceListener)Debug.Listeners["Default"];
                dtl.LogFileName = outFilename;
            }
            else if (textBox_DebugOutDirPath.Text.Length > 1)
            {
                MessageBox.Show("DebugOutDirPath Not Found.\r\n[" + textBox_DebugOutDirPath.Text + "]", "Directory Not Found.", MessageBoxButtons.OK);
            }
        }

        private void DebugOutFilenameReset(string targetDir)
        {
            if (targetDir == "{ExecutablePath}") { targetDir = Path.GetDirectoryName(Application.ExecutablePath); }
            if (Directory.Exists(targetDir))
            {
                string outFilename = Path.Combine(targetDir, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd_HHmm").Substring(0, 12) + "0.txt");
                if (!Directory.Exists(Path.GetDirectoryName(outFilename))) { Directory.CreateDirectory(Path.GetDirectoryName(outFilename)); };

                DefaultTraceListener dtl = (DefaultTraceListener)Debug.Listeners["Default"];
                if (dtl.LogFileName != outFilename) { dtl.LogFileName = outFilename; };
            }
        }

        private void clearControlCollection(System.Windows.Forms.Control.ControlCollection cc)
        {
            for (int i = 0; i < cc.Count; i++) { cc[i].Dispose(); }
            cc.Clear();
        }

        private void updatePanel_StatusList(SocketMessage socketMessage)
        {
            foreach (MessageItemView messageItemView in panel_StatusList.Controls)
            {
                if (messageItemView.clientName == socketMessage.clientName)
                {
                    messageItemView.socketMessage = socketMessage;
                }
            }
        }

        private void DeleteDuplexSystemView(int targetIndex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => DeleteDuplexSystemView(targetIndex)));
            }
            else
            {
                if (targetIndex < panel_DuplexSystemView.Controls.Count)
                {
                    panel_DuplexSystemView.Controls.RemoveAt(targetIndex);
                    UpdateDuplexSystemText();
                    UpdateLayoutDuplexSystemView();
                }
            }
        }
        private void RefreshDuplexSystemView()
        {

            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => RefreshDuplexSystemView()));
            }
            else
            {
                panel_DuplexSystemView.Controls.Clear();
                panel_DuplexSystemView.Height = 0;

                string[] Lines = textBox_DuplexActiveList.Text.Replace("\r\n", "\n").Trim('\n').Split('\n');
                int PositionTop = 0;
                int ctrlIndex = 0;


                foreach (var Line in Lines)
                {
                    if (Line == "") continue;
                    var ctrl = new DuplexActiveView(ctrlIndex, Line);
                    ctrl.DeleteThis = (Action<int>)((int x) => DeleteDuplexSystemView(x));
                    ctrl.LoadThis = (Action)(() => EnableLoadDuplexSystemView());
                    ctrl.Top = PositionTop;
                    PositionTop += ctrl.Height;
                    panel_DuplexSystemView.Controls.Add(ctrl);

                    ctrlIndex++;
                }
                panel_DuplexSystemView.Height = PositionTop;
            }
        }

        private void UpdateLayoutDuplexSystemView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => UpdateLayoutDuplexSystemView()));
            }
            else
            {
                panel_DuplexSystemView.Height = 0;
                int PositionTop = 0;
                int ctrlIndex = 0;
                foreach (DuplexActiveView ctrl in panel_DuplexSystemView.Controls)
                {
                    ctrl.Top = PositionTop;
                    ctrl.Index = ctrlIndex;
                    PositionTop += ctrl.Height;

                    ctrlIndex++;
                }
                panel_DuplexSystemView.Height = PositionTop;
            }
        }

        private void UpdateDuplexSystemText()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => UpdateDuplexSystemText()));
            }
            else
            {

                List<string> Lines = new List<string>();

                foreach (DuplexActiveView View in panel_DuplexSystemView.Controls)
                {
                    Lines.Add(View.ToString());

                }
                textBox_DuplexActiveList.Text = string.Join("\r\n", Lines.ToArray());
            }
        }

        private void updateStatusTab()
        {
            if (!File.Exists(textBox_DataBaseFilePath.Text)) return;
            _LiteDBconnectionString.Filename = textBox_DataBaseFilePath.Text;
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
            {
                try
                {
                    List<SocketMessage> colQuery;
                    List<SocketMessage> dataset;

                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        Debug.WriteLine("OpenLiteDB\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: " + retryCount.ToString());

                        var col = litedb.GetCollection<SocketMessage>("table_Message");
                        colQuery = col.Query().ToList();
                    }

                    dataset = colQuery.OrderByDescending(x => x.connectTime).ToList();

                    foreach (MessageItemView messageItemView in panel_StatusList.Controls)
                    {
                        string clientName = messageItemView.groupBox_ClientName.Text;
                        var query = dataset.Where(x => x.clientName == clientName).ToList();
                        if (query.Count() > 0)
                        {
                            SocketMessage socketMessage = query.First();
                            messageItemView.socketMessage = socketMessage;
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount == _retryCountMax - 1)
                    {
                        Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: reachMAX " + retryCount.ToString());
                        Debug.WriteLine(ex.ToString());
                        break;
                    }
                    Thread.Sleep((int)(_retryWait * (random.NextDouble() + 0.5)));
                }
            }
        }

        private void ClientListInitialize()
        {
            clientList.Clear();
            button_AddressListLoad_Click(null, null);

            for (int i = 0; i < dataGridView_ClientList.RowCount - 1; i++)
            {
                try
                {
                    var cells = dataGridView_ClientList.Rows[i].Cells;

                    string clientName = cells[0].Value.ToString();
                    string addressKeys = cells[1].Value.ToString();
                    string timeoutCheck = cells[2].Value.ToString();
                    string timeoutLength = cells[3].Value.ToString();
                    string timeoutMessage = cells[4].Value.ToString();

                    string code = clientName + "\t" + timeoutCheck + "\t" + timeoutLength + "\t" + timeoutMessage;

                    ClientData cd = new ClientData(code, addressBook.getAddress(addressKeys));

                    if (cd.clientName != "") clientList.Add(cd);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
                }
            }
            ButtonEnable(button_ClientListLoad, false);
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

        private void SchedulerInitialize()
        {
            JobManager.StopAndBlock();
            JobManager.RemoveAllJobs();

            List<string> Lines = new List<string>();
            for (int i = 0; i < dataGridView_SchedulerList.RowCount - 1; i++)
            {
                var cells = dataGridView_SchedulerList.Rows[i].Cells;
                string code = cells[0].Value.ToString();
                code += cells.Count > 1 && cells[1].Value != null ? "\t" + cells[1].Value.ToString() : "\t";
                code += cells.Count > 2 && cells[2].Value != null ? "\t" + cells[2].Value.ToString() : "\t";
                code += cells.Count > 3 && cells[3].Value != null ? "\t" + cells[3].Value.ToString() : "\t";

                if (code != "") Lines.Add(code);
            }
            var job = new FluentSchedulerRegistry_FromScheduleLines(textBox_DataBaseFilePath.Text, noticeTransmitter, Lines.ToArray(), clientList);
            JobManager.Initialize(job);
            ButtonEnable(button_SchedulerList, false);
        }

        private void addNewDataFromServerQueueToDataBase()
        {
            List<string> queueList = new List<string>();
            string receivedSocketMessage = "";

            //============
            // ReadQueue and Entry dataBase file
            //============
            while (tcpSrv.ReceivedSocketQueue.TryDequeue(out receivedSocketMessage))
            {
                queueList.Add(receivedSocketMessage);
                string[] cols = receivedSocketMessage.Split('\t');
                string dbFilename = textBox_DataBaseFilePath.Text;

                //if (cols.Length >= 4 && File.Exists(dbFilename))
                if (cols.Length >= 4)
                {
                    DateTime connectTime;
                    if (DateTime.TryParse(cols[0], out connectTime)) { connectTime = DateTime.Now; } else { continue; }

                    string clientName = cols[1];
                    string status = cols[2];
                    string message = cols[3];
                    string parameter = cols.Length > 4 ? cols[4] : "";
                    string checkStyle = cols.Length > 5 ? cols[5] : "Once";

                    SocketMessage socketMessage = new SocketMessage(connectTime, clientName, status, message, parameter, checkStyle);
                    updatePanel_StatusList(socketMessage);

                    string key = socketMessage.Key();
                    _LiteDBconnectionString.Filename = dbFilename;
                    _LiteDBconnectionString.Connection = ConnectionType.Shared;

                    for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
                    {
                        try
                        {
                            using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                            {
                                Debug.WriteLine("OpenLiteDB\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: " + retryCount.ToString());


                                ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");
                                if (col.FindById(key) == null)
                                {
                                    col.Insert(key, socketMessage);
                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (retryCount == _retryCountMax - 1)
                            {
                                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: reachMAX " + retryCount.ToString());
                                Debug.WriteLine(ex.ToString());
                                break;
                            }
                            Thread.Sleep((int)(_retryWait * (random.NextDouble() + 0.5)));
                        }
                    }
                }
            }
            textBox_queueList.Text += "\t" + string.Join("\r\n\t", queueList.ToArray());
        }

        private void checkNoticeDataFromDataBase_and_AddNotice()
        {
            string dbFilename = textBox_DataBaseFilePath.Text;
            if (!File.Exists(dbFilename)) return;
            _LiteDBconnectionString.Filename = dbFilename;
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
            {
                try
                {
                    SocketMessage[] dataset;

                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        Debug.WriteLine("OpenLiteDB\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: " + retryCount.ToString());

                        ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");
                        dataset = col.Query()
                                .Where(x => !x.check)
                                .OrderByDescending(x => x.connectTime).ToArray();
                    }

                    foreach (var target in clientList)
                    {
                        var latestNoticeRecord = dataset.Where(x => x.clientName == target.clientName).FirstOrDefault();

                        if (latestNoticeRecord != null && latestNoticeRecord.connectTime > latestNoticeRecord.connectTime)
                        {
                            noticeTransmitter.AddNotice(target, latestNoticeRecord);
                        }
                    }

                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount == _retryCountMax - 1)
                    {
                        Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: reachMAX " + retryCount.ToString());
                        Debug.WriteLine(ex.ToString());
                        break;
                    }
                    Thread.Sleep((int)(_retryWait * (random.NextDouble() + 0.5)));
                }
            }
        }

        private void checkTimeoutFromDataBase_and_AddNotice()
        {
            string dbFilename = textBox_DataBaseFilePath.Text;
            if (!File.Exists(dbFilename)) return;

            _LiteDBconnectionString.Filename = dbFilename;
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
            {
                try
                {
                    SocketMessage[] dataset0;
                    SocketMessage[] dataset1;

                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        Debug.WriteLine("OpenLiteDB\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: " + retryCount.ToString());

                        var col = litedb.GetCollection<SocketMessage>("table_Message");
                        dataset0 = col.Query()
                                .Where(x => x.status != "Timeout")
                                .OrderByDescending(x => x.connectTime).ToArray();
                        dataset1 = col.Query()
                            .Where(x => x.status == "Timeout" && !x.check)
                            .OrderByDescending(x => x.connectTime).ToArray();
                    }

                    foreach (var clientTarget in clientList)
                    {
                        //MessageRecord from Client
                        var latestRecord = dataset0.Where(x => x.clientName == clientTarget.clientName).FirstOrDefault();

                        //MessageRecord Timeout
                        var listedTimeoutMessage = dataset1.Where(x => x.clientName == clientTarget.clientName).OrderByDescending(x => x.connectTime).ToList();
                        if (clientTarget.lastAccessTime == null) { clientTarget.lastAccessTime = DateTime.Now; };//First Time

                        //Acccess Time Update
                        if (latestRecord != null && clientTarget.lastAccessTime < latestRecord.connectTime)
                        {
                            clientTarget.lastAccessTime = latestRecord.connectTime;
                        };

                        bool flag1 = (DateTime.Now - clientTarget.lastAccessTime).TotalSeconds > clientTarget.timeoutLength;
                        bool flag2 = listedTimeoutMessage.Count == 0;

                        if (clientTarget.timeoutCheck && flag1 && flag2)
                        {
                            SocketMessage timeoutMessage = new SocketMessage(clientTarget.lastAccessTime, clientTarget.clientName, "Timeout", clientTarget.timeoutMessage, "", "Once");
                            timeoutMessage.parameter = textBox_TimeoutMessageParameter.Text;
                            noticeTransmitter.AddNotice(clientTarget, timeoutMessage);
                            clientTarget.lastTimeoutDetectedTime = DateTime.Now;
                        }
                    }

                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount == _retryCountMax - 1)
                    {
                        Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: reachMAX " + retryCount.ToString());
                        Debug.WriteLine(ex.ToString());
                        break;
                    }
                    Thread.Sleep((int)(_retryWait * (random.NextDouble() + 0.5)));
                }
            }
        }

        //===================
        // Event
        //===================
        private void Form1_Load(object sender, EventArgs e)
        {
            string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");

            if (File.Exists(paramFilename))
            {
                WinFormStringCnv.setControlFromString(this, File.ReadAllText(paramFilename));
            }

            DebugOutDirPathReset(textBox_DebugOutDirPath.Text);
            checkBox_voiceOffSwitch_CheckedChanged();

            ClientListInitialize();
            AddressListInitialize();
            SchedulerInitialize();
            RefreshDuplexSystemView();

            timer_DebugFilepathUpdate.Start();
            timer_DuplexActiveListUpdate.Start();

            if (panel_DuplexSystemView.Controls.Count > 0)
            {
                toolStripDropDownButton_Class.Image = Properties.Resources.Standby048;
            }
            else
            {
                toolStripDropDownButton_Class.Image = Properties.Resources.Active048;
            }


            int.TryParse(textBox_httpTimeout.Text, out noticeTransmitter.httpTimeout);

            if (checkBox_serverAutoStart.Checked)
            {
                try
                {
                    portNumber = int.Parse(textBox_portNumber.Text);
                    button_Start.Text = "ServerStop";
                    tcpSrv.StartListening(portNumber, "UTF8");

                    if (int.TryParse(textBox_TimeoutCheckInterval.Text, out int TimeoutCheckInterval))
                    {
                        timer_checkTimeout.Interval = TimeoutCheckInterval * 1000;
                    }
                    timer_checkTimeout.Start();

                    timer_CheckQueue.Start();
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string FormContents = WinFormStringCnv.ToString(this);
            string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
            File.WriteAllText(paramFilename, FormContents);
        }

        private void button_Start_Click(object sender = null, EventArgs e = null)
        {
            if (button_Start.Text == "ServerStart" && int.TryParse(textBox_portNumber.Text, out portNumber))
            {
                button_Start.Text = "ServerStop";
                timer_CheckQueue.Start();
                timer_checkTimeout.Start();
                tcpSrv.StartListening(portNumber, "UTF8");

            }
            else if (!int.TryParse(textBox_portNumber.Text, out portNumber))
            {
                textBox_portNumber.Select();
            }
            else
            {
                tcpSrv.StopListening();
                button_Start.Text = "ServerStart";
            }
        }


        private void timer_CheckQueue_Tick(object sender, EventArgs e)
        {
            timer_CheckQueue.Stop();

            textBox_queueList.Text = DateTime.Now.ToString("HH:mm:ss") + "\t" + tcpSrv.ReceivedSocketQueue.Count.ToString() + "\r\n";

            if ((tcpSrv.LastReceiveTime - LastCheckTime).TotalSeconds > 0 && tcpSrv.ReceivedSocketQueue.Count > 0)
            {
                addNewDataFromServerQueueToDataBase();
                checkNoticeDataFromDataBase_and_AddNotice();

                LastCheckTime = DateTime.Now;
            }
            else
            {
                if (tabControl_Top.SelectedTab == tabPage_Status)
                {
                    updateStatusTab();
                }
            }

            //checkTimeoutFromDataBase_and_AddNotice();

            if (button_Start.Text != "ServerStart")
            {
                toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                if (tcpSrv.ListeningRun)
                {
                    toolStripStatusLabel1.Text += " / TCP Listening Run";
                }
                else
                {
                    toolStripStatusLabel1.Text += " / TCP Listening Stop";
                }
                timer_CheckQueue.Start();
            }
            else
            {
                toolStripStatusLabel1.Text = "Stop TCP Listener.";
            }
        }

        private void button_getDataBaseFilePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "LiteDB File|*.db";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            textBox_DataBaseFilePath.Text = sfd.FileName;

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
                    MessageItemView messageItemView = new MessageItemView();
                    panel_StatusList.Controls.Add(messageItemView);
                    panel_StatusList.Controls[i].Top = TopBuff;
                    ((MessageItemView)panel_StatusList.Controls[i]).clientName = dataGridView_ClientList.Rows[i].Cells[0].Value.ToString();

                    TopBuff += panel_StatusList.Controls[i].Height;
                }
                updateStatusTab();
            }
        }

        private void tabPage_Log_Enter(object sender, EventArgs e)
        {
            string dbFilename = textBox_DataBaseFilePath.Text;
            if (!File.Exists(dbFilename)) return;
            _LiteDBconnectionString.Filename = dbFilename;
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
            {
                try
                {
                    List<SocketMessage> query;
                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        Debug.WriteLine("OpenLiteDB\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: " + retryCount.ToString());

                        var col = litedb.GetCollection<SocketMessage>("table_Message");
                        query = col.Query().OrderBy(x => x.connectTime, 0).ToList().Take(50).ToList();
                    }

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

                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount == _retryCountMax - 1)
                    {
                        Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry: reachMAX " + retryCount.ToString());
                        Debug.WriteLine(ex.ToString());
                        break;
                    }
                    Thread.Sleep((int)(_retryWait * (random.NextDouble() + 0.5)));
                }
            }
        }

        private void button_LogReload_Click(object sender, EventArgs e)
        {
            tabPage_Log_Enter(null, null);
        }


        private void button_ClientListLoad_Click(object sender, EventArgs e)
        {
            ClientListInitialize();
            if (int.TryParse(textBox_TimeoutCheckInterval.Text, out int TimeoutCheckInterval))
            {
                timer_checkTimeout.Interval = TimeoutCheckInterval * 1000;
            }
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
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            try
            {
                using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                {
                    Debug.WriteLine("OpenLiteDB\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name);

                    for (DateTime connectTime = DateTime.Parse("2020/01/01"); connectTime < DateTime.Parse("2024/01/31"); connectTime += TP)
                    {
                        SocketMessage socketMessage = new SocketMessage(connectTime, "Test", "Test", "Test", "parameterTest");
                        ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");
                        col.Insert(socketMessage.Key(), socketMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
            }
        }

        private void button_BreakupDatabasefile_Click(object sender, EventArgs e)
        {
            string dbFilename = textBox_DataBaseFilePath.Text;
            if (!File.Exists(dbFilename)) return;

            BreakupLightDBFile job = new BreakupLightDBFile(dbFilename, int.Parse(textBox_PostTime.Text));
            job.BreakupLightDBFile_byMonthFile(dbFilename, int.Parse(textBox_PostTime.Text));
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

        private void textBox_portNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int.TryParse(textBox_portNumber.Text, out portNumber);
            }
        }

        private void checkBox_voiceOffSwitch_CheckedChanged(object sender = null, EventArgs e = null)
        {
            noticeTransmitter.voiceOFF = checkBox_voiceOffSwitch.Checked;

            if (checkBox_voiceOffSwitch.Checked)
            {
                toolStripDropDownButton_VoiceSwitch.Image = icon_voiceOFF;
            }
            else
            {
                toolStripDropDownButton_VoiceSwitch.Image = icon_voiceON;
            }
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

        private void label_IntervalSelector_Click(object sender, EventArgs e)
        {
            string clip = Clipboard.GetText();

            if (clip == "EveryDays") { Clipboard.SetText("EveryHours"); }
            else if (clip == "EveryHours") { Clipboard.SetText("EverySeconds"); }
            else { Clipboard.SetText("EveryDays"); }

            label_IntervalSelectorNow.Text = Clipboard.GetText();

        }

        private void button_DebugOutDirPathReset_Click(object sender, EventArgs e)
        {
            DebugOutDirPathReset(textBox_DebugOutDirPath.Text);
        }

        private void label_DebugOutDirPath_Click(object sender, EventArgs e)
        {
            textBox_DebugOutDirPath.Text = "{ExecutablePath}";
        }

        private void timer_DebugFilepathUpdate_Tick(object sender, EventArgs e)
        {
            DebugOutFilenameReset(textBox_DebugOutDirPath.Text);
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

        private void timer_checkTimeout_Tick(object sender, EventArgs e)
        {
            timer_checkTimeout.Stop();

            checkTimeoutFromDataBase_and_AddNotice();

            if (button_Start.Text != "ServerStart")
            {
                timer_checkTimeout.Start();
            }

        }

        private void textBox_TimeoutCheckInterval_TextChanged(object sender, EventArgs e)
        {
            button_ClientListLoad.Enabled = true;
            button_ClientListLoad.BackColor = Color.GreenYellow;
        }

        private void button_AddDuplexActiveList_Click(object sender, EventArgs e)
        {
            int ctrlIndex = panel_DuplexSystemView.Controls.Count;
            DuplexActiveView ctrl = new DuplexActiveView(ctrlIndex);
            ctrl.DeleteThis = (Action<int>)((int x) => DeleteDuplexSystemView(ctrlIndex));
            ctrl.LoadThis = (Action)(() => EnableLoadDuplexSystemView());
            ctrl.Top = panel_DuplexSystemView.Height;

            panel_DuplexSystemView.Controls.Add(ctrl);
            panel_DuplexSystemView.Height += ctrl.Height;
            UpdateDuplexSystemText();

        }

        private void tabControl_DuplexSystemView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_DuplexSystemView.SelectedTab == tabPage_DuplexSystemText)
            {
                UpdateDuplexSystemText();
            }
            else if (tabControl_DuplexSystemView.SelectedTab == tabPage_DuplexSystemView)
            {

            }
        }

        private void EnableLoadDuplexSystemView()
        {
            ButtonEnable(button_LoadDuplexSystemView, true);
        }

        private void button_LoadDuplexSystemView_Click(object sender, EventArgs e)
        {
            UpdateDuplexSystemText();
            if (int.TryParse(textBox_DuplexActiveListCheckInterval.Text, out int b))
            {
                timer_DuplexActiveListUpdate.Interval = b * 1000;
            }
            ButtonEnable(button_LoadDuplexSystemView, false);
        }

        private void timer_DuplexActiveListUpdate_Tick(object sender, EventArgs e)
        {
            List<Task> taskList = new List<Task>();

            foreach (DuplexActiveView ctrl in panel_DuplexSystemView.Controls)
            {
                ctrl.askAlive();
            }

            bool activeAlive = false;

            foreach (DuplexActiveView ctrl in panel_DuplexSystemView.Controls)
            {
                activeAlive = activeAlive || ctrl.Alive;
            }

            noticeTransmitter.isActive = !activeAlive;

            if (activeAlive)
            {
                toolStripStatusLabel2.Text = "StandbyMode(ActiveAlive)";
                toolStripDropDownButton_Class.Image = Properties.Resources.Standby048;
            }
            else if(!activeAlive && panel_DuplexSystemView.Controls.Count>0)
            {
                toolStripStatusLabel2.Text = "ActiveMode(ActiveDown)";
                toolStripDropDownButton_Class.Image = Properties.Resources.Active048;
            }
            else
            {
                toolStripStatusLabel2.Text = "ActiveMode";
                toolStripDropDownButton_Class.Image = Properties.Resources.Active048;
            }
        }

        private void textBox_DuplexActiveListCheckInterval_TextChanged(object sender, EventArgs e)
        {
            ButtonEnable(button_LoadDuplexSystemView, true);
        }
    }
}
