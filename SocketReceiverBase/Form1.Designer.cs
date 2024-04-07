namespace SocketReceiverBase
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_StatusChange = new System.Windows.Forms.Button();
            this.textBox_PortNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_LockedFrom = new System.Windows.Forms.Label();
            this.label_LockedTime = new System.Windows.Forms.Label();
            this.label_ResetTime = new System.Windows.Forms.Label();
            this.label_RemainingTime = new System.Windows.Forms.Label();
            this.button_Lock = new System.Windows.Forms.Button();
            this.timer_ServerInfoUpdate = new System.Windows.Forms.Timer(this.components);
            this.timer_UpdateQueue = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Restrart = new System.Windows.Forms.Button();
            this.textBox_clientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_IntervalOK = new System.Windows.Forms.TextBox();
            this.textBox_MessageOK = new System.Windows.Forms.TextBox();
            this.textBox_IntervalNG = new System.Windows.Forms.TextBox();
            this.textBox_MessageNG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_StatusOK = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_StatusNG = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_ParameterOK = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_ParameterNG = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_CheckStyleOK = new System.Windows.Forms.TextBox();
            this.textBox_CheckStyleNG = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.timer_SendMessage = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl_ServerInfo = new System.Windows.Forms.TabControl();
            this.tabPage_View = new System.Windows.Forms.TabPage();
            this.panel_ServerListFrame = new System.Windows.Forms.Panel();
            this.panel_ServerListView = new System.Windows.Forms.Panel();
            this.tabPage_List = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ServerList = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl_Main = new System.Windows.Forms.TabControl();
            this.tabPage_MessageSetting = new System.Windows.Forms.TabPage();
            this.tabPage_ServerSetting = new System.Windows.Forms.TabPage();
            this.button_AddServerList = new System.Windows.Forms.Button();
            this.button_LoadServerListView = new System.Windows.Forms.Button();
            this.textBox_ServerInfoCheckInterval = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl_ServerInfo.SuspendLayout();
            this.tabPage_View.SuspendLayout();
            this.panel_ServerListFrame.SuspendLayout();
            this.tabPage_List.SuspendLayout();
            this.tabControl_Main.SuspendLayout();
            this.tabPage_MessageSetting.SuspendLayout();
            this.tabPage_ServerSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_StatusChange
            // 
            this.button_StatusChange.Location = new System.Drawing.Point(6, 18);
            this.button_StatusChange.Name = "button_StatusChange";
            this.button_StatusChange.Size = new System.Drawing.Size(41, 38);
            this.button_StatusChange.TabIndex = 0;
            this.button_StatusChange.UseVisualStyleBackColor = true;
            this.button_StatusChange.EnabledChanged += new System.EventHandler(this.button_StatusChange_EnabledChanged);
            this.button_StatusChange.Click += new System.EventHandler(this.button_StatusChange_Click);
            // 
            // textBox_PortNumber
            // 
            this.textBox_PortNumber.Location = new System.Drawing.Point(66, 37);
            this.textBox_PortNumber.Name = "textBox_PortNumber";
            this.textBox_PortNumber.Size = new System.Drawing.Size(63, 19);
            this.textBox_PortNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "PortNumber";
            // 
            // label_LockedFrom
            // 
            this.label_LockedFrom.AutoSize = true;
            this.label_LockedFrom.Location = new System.Drawing.Point(166, 19);
            this.label_LockedFrom.Name = "label_LockedFrom";
            this.label_LockedFrom.Size = new System.Drawing.Size(11, 12);
            this.label_LockedFrom.TabIndex = 3;
            this.label_LockedFrom.Text = "-";
            // 
            // label_LockedTime
            // 
            this.label_LockedTime.AutoSize = true;
            this.label_LockedTime.Location = new System.Drawing.Point(166, 31);
            this.label_LockedTime.Name = "label_LockedTime";
            this.label_LockedTime.Size = new System.Drawing.Size(11, 12);
            this.label_LockedTime.TabIndex = 3;
            this.label_LockedTime.Text = "-";
            // 
            // label_ResetTime
            // 
            this.label_ResetTime.AutoSize = true;
            this.label_ResetTime.Location = new System.Drawing.Point(166, 44);
            this.label_ResetTime.Name = "label_ResetTime";
            this.label_ResetTime.Size = new System.Drawing.Size(11, 12);
            this.label_ResetTime.TabIndex = 3;
            this.label_ResetTime.Text = "-";
            // 
            // label_RemainingTime
            // 
            this.label_RemainingTime.AutoSize = true;
            this.label_RemainingTime.Location = new System.Drawing.Point(166, 56);
            this.label_RemainingTime.Name = "label_RemainingTime";
            this.label_RemainingTime.Size = new System.Drawing.Size(11, 12);
            this.label_RemainingTime.TabIndex = 3;
            this.label_RemainingTime.Text = "-";
            // 
            // button_Lock
            // 
            this.button_Lock.Location = new System.Drawing.Point(505, 8);
            this.button_Lock.Name = "button_Lock";
            this.button_Lock.Size = new System.Drawing.Size(26, 23);
            this.button_Lock.TabIndex = 4;
            this.button_Lock.UseVisualStyleBackColor = true;
            // 
            // timer_ServerInfoUpdate
            // 
            this.timer_ServerInfoUpdate.Interval = 1000;
            this.timer_ServerInfoUpdate.Tick += new System.EventHandler(this.timer_LabelUpdate_Tick_Tick);
            // 
            // timer_UpdateQueue
            // 
            this.timer_UpdateQueue.Interval = 1000;
            this.timer_UpdateQueue.Tick += new System.EventHandler(this.timer_UpdateQueue_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Restrart);
            this.groupBox1.Controls.Add(this.button_StatusChange);
            this.groupBox1.Controls.Add(this.button_Lock);
            this.groupBox1.Controls.Add(this.textBox_clientName);
            this.groupBox1.Controls.Add(this.textBox_PortNumber);
            this.groupBox1.Controls.Add(this.label_RemainingTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_ResetTime);
            this.groupBox1.Controls.Add(this.label_LockedFrom);
            this.groupBox1.Controls.Add(this.label_LockedTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(674, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // button_Restrart
            // 
            this.button_Restrart.Location = new System.Drawing.Point(583, 18);
            this.button_Restrart.Name = "button_Restrart";
            this.button_Restrart.Size = new System.Drawing.Size(61, 38);
            this.button_Restrart.TabIndex = 0;
            this.button_Restrart.Text = "Restrart";
            this.button_Restrart.UseVisualStyleBackColor = true;
            this.button_Restrart.Click += new System.EventHandler(this.button_Restrart_Click);
            // 
            // textBox_clientName
            // 
            this.textBox_clientName.Location = new System.Drawing.Point(383, 44);
            this.textBox_clientName.Name = "textBox_clientName";
            this.textBox_clientName.Size = new System.Drawing.Size(148, 19);
            this.textBox_clientName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "clientName";
            // 
            // textBox_IntervalOK
            // 
            this.textBox_IntervalOK.Location = new System.Drawing.Point(608, 64);
            this.textBox_IntervalOK.Name = "textBox_IntervalOK";
            this.textBox_IntervalOK.Size = new System.Drawing.Size(47, 19);
            this.textBox_IntervalOK.TabIndex = 0;
            this.textBox_IntervalOK.Text = "5000";
            // 
            // textBox_MessageOK
            // 
            this.textBox_MessageOK.Location = new System.Drawing.Point(11, 42);
            this.textBox_MessageOK.Name = "textBox_MessageOK";
            this.textBox_MessageOK.Size = new System.Drawing.Size(645, 19);
            this.textBox_MessageOK.TabIndex = 0;
            this.textBox_MessageOK.Text = "OK message";
            // 
            // textBox_IntervalNG
            // 
            this.textBox_IntervalNG.Location = new System.Drawing.Point(609, 145);
            this.textBox_IntervalNG.Name = "textBox_IntervalNG";
            this.textBox_IntervalNG.Size = new System.Drawing.Size(47, 19);
            this.textBox_IntervalNG.TabIndex = 0;
            this.textBox_IntervalNG.Text = "5000";
            // 
            // textBox_MessageNG
            // 
            this.textBox_MessageNG.Location = new System.Drawing.Point(12, 123);
            this.textBox_MessageNG.Name = "textBox_MessageNG";
            this.textBox_MessageNG.Size = new System.Drawing.Size(645, 19);
            this.textBox_MessageNG.TabIndex = 0;
            this.textBox_MessageNG.Text = "NG message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Message";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Message";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "Interval";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(545, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Interval";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "status";
            // 
            // textBox_StatusOK
            // 
            this.textBox_StatusOK.Location = new System.Drawing.Point(53, 64);
            this.textBox_StatusOK.Name = "textBox_StatusOK";
            this.textBox_StatusOK.Size = new System.Drawing.Size(61, 19);
            this.textBox_StatusOK.TabIndex = 0;
            this.textBox_StatusOK.Text = "Notice";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "status";
            // 
            // textBox_StatusNG
            // 
            this.textBox_StatusNG.Location = new System.Drawing.Point(54, 145);
            this.textBox_StatusNG.Name = "textBox_StatusNG";
            this.textBox_StatusNG.Size = new System.Drawing.Size(61, 19);
            this.textBox_StatusNG.TabIndex = 0;
            this.textBox_StatusNG.Text = "Warning";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(120, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "parameter";
            // 
            // textBox_ParameterOK
            // 
            this.textBox_ParameterOK.Location = new System.Drawing.Point(182, 63);
            this.textBox_ParameterOK.Name = "textBox_ParameterOK";
            this.textBox_ParameterOK.Size = new System.Drawing.Size(198, 19);
            this.textBox_ParameterOK.TabIndex = 0;
            this.textBox_ParameterOK.Text = "voice=female&notify=6&led=00100";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(120, 149);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "parameter";
            // 
            // textBox_ParameterNG
            // 
            this.textBox_ParameterNG.Location = new System.Drawing.Point(182, 145);
            this.textBox_ParameterNG.Name = "textBox_ParameterNG";
            this.textBox_ParameterNG.Size = new System.Drawing.Size(198, 19);
            this.textBox_ParameterNG.TabIndex = 0;
            this.textBox_ParameterNG.Text = "voice=female&notify=6&led=10000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(386, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "checkStyle";
            // 
            // textBox_CheckStyleOK
            // 
            this.textBox_CheckStyleOK.Location = new System.Drawing.Point(453, 63);
            this.textBox_CheckStyleOK.Name = "textBox_CheckStyleOK";
            this.textBox_CheckStyleOK.Size = new System.Drawing.Size(47, 19);
            this.textBox_CheckStyleOK.TabIndex = 0;
            this.textBox_CheckStyleOK.Text = "Once";
            // 
            // textBox_CheckStyleNG
            // 
            this.textBox_CheckStyleNG.Location = new System.Drawing.Point(453, 145);
            this.textBox_CheckStyleNG.Name = "textBox_CheckStyleNG";
            this.textBox_CheckStyleNG.Size = new System.Drawing.Size(47, 19);
            this.textBox_CheckStyleNG.TabIndex = 0;
            this.textBox_CheckStyleNG.Text = "Once";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(386, 149);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "checkStyle";
            // 
            // timer_SendMessage
            // 
            this.timer_SendMessage.Interval = 5000;
            this.timer_SendMessage.Tick += new System.EventHandler(this.timer_SendMessage_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(788, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBox_MessageNG);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox_MessageOK);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_IntervalOK);
            this.groupBox2.Controls.Add(this.textBox_IntervalNG);
            this.groupBox2.Controls.Add(this.textBox_CheckStyleOK);
            this.groupBox2.Controls.Add(this.textBox_StatusNG);
            this.groupBox2.Controls.Add(this.textBox_StatusOK);
            this.groupBox2.Controls.Add(this.textBox_ParameterNG);
            this.groupBox2.Controls.Add(this.textBox_CheckStyleNG);
            this.groupBox2.Controls.Add(this.textBox_ParameterOK);
            this.groupBox2.Location = new System.Drawing.Point(8, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(674, 194);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // tabControl_ServerInfo
            // 
            this.tabControl_ServerInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl_ServerInfo.Controls.Add(this.tabPage_View);
            this.tabControl_ServerInfo.Controls.Add(this.tabPage_List);
            this.tabControl_ServerInfo.Location = new System.Drawing.Point(6, 27);
            this.tabControl_ServerInfo.Name = "tabControl_ServerInfo";
            this.tabControl_ServerInfo.SelectedIndex = 0;
            this.tabControl_ServerInfo.ShowToolTips = true;
            this.tabControl_ServerInfo.Size = new System.Drawing.Size(565, 342);
            this.tabControl_ServerInfo.TabIndex = 12;
            this.tabControl_ServerInfo.SelectedIndexChanged += new System.EventHandler(this.tabControl_ServerInfo_SelectedIndexChanged);
            // 
            // tabPage_View
            // 
            this.tabPage_View.Controls.Add(this.panel_ServerListFrame);
            this.tabPage_View.Location = new System.Drawing.Point(4, 4);
            this.tabPage_View.Name = "tabPage_View";
            this.tabPage_View.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_View.Size = new System.Drawing.Size(557, 316);
            this.tabPage_View.TabIndex = 0;
            this.tabPage_View.Text = "View";
            this.tabPage_View.UseVisualStyleBackColor = true;
            // 
            // panel_ServerListFrame
            // 
            this.panel_ServerListFrame.AutoScroll = true;
            this.panel_ServerListFrame.Controls.Add(this.panel_ServerListView);
            this.panel_ServerListFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ServerListFrame.Location = new System.Drawing.Point(3, 3);
            this.panel_ServerListFrame.Name = "panel_ServerListFrame";
            this.panel_ServerListFrame.Size = new System.Drawing.Size(551, 310);
            this.panel_ServerListFrame.TabIndex = 0;
            // 
            // panel_ServerListView
            // 
            this.panel_ServerListView.Location = new System.Drawing.Point(0, 0);
            this.panel_ServerListView.Name = "panel_ServerListView";
            this.panel_ServerListView.Size = new System.Drawing.Size(548, 147);
            this.panel_ServerListView.TabIndex = 0;
            // 
            // tabPage_List
            // 
            this.tabPage_List.Controls.Add(this.label3);
            this.tabPage_List.Controls.Add(this.textBox_ServerList);
            this.tabPage_List.Location = new System.Drawing.Point(4, 4);
            this.tabPage_List.Name = "tabPage_List";
            this.tabPage_List.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_List.Size = new System.Drawing.Size(557, 316);
            this.tabPage_List.TabIndex = 1;
            this.tabPage_List.Text = "List";
            this.tabPage_List.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "{ServerName}\\t{Address}\\t{Port}";
            // 
            // textBox_ServerList
            // 
            this.textBox_ServerList.AcceptsReturn = true;
            this.textBox_ServerList.AcceptsTab = true;
            this.textBox_ServerList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox_ServerList.Location = new System.Drawing.Point(3, 18);
            this.textBox_ServerList.Multiline = true;
            this.textBox_ServerList.Name = "textBox_ServerList";
            this.textBox_ServerList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ServerList.Size = new System.Drawing.Size(551, 295);
            this.textBox_ServerList.TabIndex = 0;
            this.textBox_ServerList.WordWrap = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 218);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(374, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "request = {ClientName}\\t{Status}\\t{Message}\\t{Parameter}\\t{CheckStyle}";
            // 
            // tabControl_Main
            // 
            this.tabControl_Main.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl_Main.Controls.Add(this.tabPage_ServerSetting);
            this.tabControl_Main.Controls.Add(this.tabPage_MessageSetting);
            this.tabControl_Main.Location = new System.Drawing.Point(12, 94);
            this.tabControl_Main.Multiline = true;
            this.tabControl_Main.Name = "tabControl_Main";
            this.tabControl_Main.SelectedIndex = 0;
            this.tabControl_Main.Size = new System.Drawing.Size(728, 380);
            this.tabControl_Main.TabIndex = 14;
            // 
            // tabPage_MessageSetting
            // 
            this.tabPage_MessageSetting.Controls.Add(this.groupBox2);
            this.tabPage_MessageSetting.Controls.Add(this.label15);
            this.tabPage_MessageSetting.Location = new System.Drawing.Point(4, 4);
            this.tabPage_MessageSetting.Name = "tabPage_MessageSetting";
            this.tabPage_MessageSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_MessageSetting.Size = new System.Drawing.Size(702, 372);
            this.tabPage_MessageSetting.TabIndex = 0;
            this.tabPage_MessageSetting.Text = "Message";
            this.tabPage_MessageSetting.UseVisualStyleBackColor = true;
            // 
            // tabPage_ServerSetting
            // 
            this.tabPage_ServerSetting.Controls.Add(this.textBox_ServerInfoCheckInterval);
            this.tabPage_ServerSetting.Controls.Add(this.label14);
            this.tabPage_ServerSetting.Controls.Add(this.button_AddServerList);
            this.tabPage_ServerSetting.Controls.Add(this.button_LoadServerListView);
            this.tabPage_ServerSetting.Controls.Add(this.tabControl_ServerInfo);
            this.tabPage_ServerSetting.Location = new System.Drawing.Point(4, 4);
            this.tabPage_ServerSetting.Name = "tabPage_ServerSetting";
            this.tabPage_ServerSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ServerSetting.Size = new System.Drawing.Size(702, 372);
            this.tabPage_ServerSetting.TabIndex = 1;
            this.tabPage_ServerSetting.Text = "Server";
            this.tabPage_ServerSetting.UseVisualStyleBackColor = true;
            // 
            // button_AddServerList
            // 
            this.button_AddServerList.Location = new System.Drawing.Point(511, 5);
            this.button_AddServerList.Name = "button_AddServerList";
            this.button_AddServerList.Size = new System.Drawing.Size(20, 20);
            this.button_AddServerList.TabIndex = 15;
            this.button_AddServerList.Text = "+";
            this.button_AddServerList.UseVisualStyleBackColor = true;
            this.button_AddServerList.Click += new System.EventHandler(this.button_AddServerList_Click);
            // 
            // button_LoadServerListView
            // 
            this.button_LoadServerListView.Location = new System.Drawing.Point(529, 5);
            this.button_LoadServerListView.Margin = new System.Windows.Forms.Padding(2);
            this.button_LoadServerListView.Name = "button_LoadServerListView";
            this.button_LoadServerListView.Size = new System.Drawing.Size(42, 19);
            this.button_LoadServerListView.TabIndex = 14;
            this.button_LoadServerListView.Text = "Load";
            this.button_LoadServerListView.UseVisualStyleBackColor = true;
            this.button_LoadServerListView.Click += new System.EventHandler(this.button_LoadServerListView_Click);
            // 
            // textBox_ServerInfoCheckInterval
            // 
            this.textBox_ServerInfoCheckInterval.Location = new System.Drawing.Point(468, 5);
            this.textBox_ServerInfoCheckInterval.Name = "textBox_ServerInfoCheckInterval";
            this.textBox_ServerInfoCheckInterval.Size = new System.Drawing.Size(35, 19);
            this.textBox_ServerInfoCheckInterval.TabIndex = 17;
            this.textBox_ServerInfoCheckInterval.Text = "10";
            this.textBox_ServerInfoCheckInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(367, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "checkInterval(sec.)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 517);
            this.Controls.Add(this.tabControl_Main);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SocketReceiverBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl_ServerInfo.ResumeLayout(false);
            this.tabPage_View.ResumeLayout(false);
            this.panel_ServerListFrame.ResumeLayout(false);
            this.tabPage_List.ResumeLayout(false);
            this.tabPage_List.PerformLayout();
            this.tabControl_Main.ResumeLayout(false);
            this.tabPage_MessageSetting.ResumeLayout(false);
            this.tabPage_MessageSetting.PerformLayout();
            this.tabPage_ServerSetting.ResumeLayout(false);
            this.tabPage_ServerSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_StatusChange;
        private System.Windows.Forms.TextBox textBox_PortNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_LockedFrom;
        private System.Windows.Forms.Label label_LockedTime;
        private System.Windows.Forms.Label label_ResetTime;
        private System.Windows.Forms.Label label_RemainingTime;
        private System.Windows.Forms.Button button_Lock;
        private System.Windows.Forms.Timer timer_ServerInfoUpdate;
        private System.Windows.Forms.Timer timer_UpdateQueue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_clientName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_IntervalOK;
        private System.Windows.Forms.TextBox textBox_MessageOK;
        private System.Windows.Forms.TextBox textBox_IntervalNG;
        private System.Windows.Forms.TextBox textBox_MessageNG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_StatusOK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_StatusNG;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_ParameterOK;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_ParameterNG;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_CheckStyleOK;
        private System.Windows.Forms.TextBox textBox_CheckStyleNG;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer timer_SendMessage;
        private System.Windows.Forms.Button button_Restrart;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl_ServerInfo;
        private System.Windows.Forms.TabPage tabPage_View;
        private System.Windows.Forms.Panel panel_ServerListFrame;
        private System.Windows.Forms.Panel panel_ServerListView;
        private System.Windows.Forms.TabPage tabPage_List;
        private System.Windows.Forms.TextBox textBox_ServerList;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl_Main;
        private System.Windows.Forms.TabPage tabPage_MessageSetting;
        private System.Windows.Forms.TabPage tabPage_ServerSetting;
        private System.Windows.Forms.Button button_AddServerList;
        private System.Windows.Forms.Button button_LoadServerListView;
        private System.Windows.Forms.TextBox textBox_ServerInfoCheckInterval;
        private System.Windows.Forms.Label label14;
    }
}

