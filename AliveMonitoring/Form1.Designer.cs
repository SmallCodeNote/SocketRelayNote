namespace AliveMonitoring
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_StatusChange = new System.Windows.Forms.Button();
            this.textBox_PortNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_LockedFrom = new System.Windows.Forms.Label();
            this.label_LockedTime = new System.Windows.Forms.Label();
            this.label_ResetTime = new System.Windows.Forms.Label();
            this.label_RemainingTime = new System.Windows.Forms.Label();
            this.button_Lock = new System.Windows.Forms.Button();
            this.timer_LabelUpdate = new System.Windows.Forms.Timer(this.components);
            this.timer_UpdateQueue = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Restrart = new System.Windows.Forms.Button();
            this.textBox_clientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_MessageInterval = new System.Windows.Forms.TextBox();
            this.textBox_MessageOK = new System.Windows.Forms.TextBox();
            this.textBox_MessageNG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl_ServerInfo = new System.Windows.Forms.TabControl();
            this.tabPage_View = new System.Windows.Forms.TabPage();
            this.panel_Frame = new System.Windows.Forms.Panel();
            this.panel_View = new System.Windows.Forms.Panel();
            this.tabPage_List = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ServerList = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Monitor = new System.Windows.Forms.TabPage();
            this.tabPage_Message = new System.Windows.Forms.TabPage();
            this.tabPage_HTTP = new System.Windows.Forms.TabPage();
            this.textBox_TimeoutMessageParameter = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_httpTimeout = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button_AddressListLoad = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridView_AddressList = new System.Windows.Forms.DataGridView();
            this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_AddressName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox_voiceOffSwitch = new System.Windows.Forms.CheckBox();
            this.toolStripDropDownButton_VoiceSwitch = new System.Windows.Forms.ToolStripDropDownButton();
            this.label_SetClipboard = new System.Windows.Forms.Label();
            this.textBox_TimeoutMessage = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button_ResetMessageInterval = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_TimeOutCheckInterval = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl_ServerInfo.SuspendLayout();
            this.tabPage_View.SuspendLayout();
            this.panel_Frame.SuspendLayout();
            this.tabPage_List.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Monitor.SuspendLayout();
            this.tabPage_Message.SuspendLayout();
            this.tabPage_HTTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AddressList)).BeginInit();
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
            // timer_LabelUpdate
            // 
            this.timer_LabelUpdate.Interval = 1000;
            this.timer_LabelUpdate.Tick += new System.EventHandler(this.timer_LabelUpdate_Tick_Tick);
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
            // textBox_MessageInterval
            // 
            this.textBox_MessageInterval.Location = new System.Drawing.Point(562, 215);
            this.textBox_MessageInterval.Name = "textBox_MessageInterval";
            this.textBox_MessageInterval.Size = new System.Drawing.Size(48, 19);
            this.textBox_MessageInterval.TabIndex = 0;
            this.textBox_MessageInterval.Text = "5";
            this.textBox_MessageInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_MessageOK
            // 
            this.textBox_MessageOK.Location = new System.Drawing.Point(11, 42);
            this.textBox_MessageOK.Name = "textBox_MessageOK";
            this.textBox_MessageOK.Size = new System.Drawing.Size(627, 19);
            this.textBox_MessageOK.TabIndex = 0;
            this.textBox_MessageOK.Text = "OK message";
            // 
            // textBox_MessageNG
            // 
            this.textBox_MessageNG.Location = new System.Drawing.Point(12, 123);
            this.textBox_MessageNG.Name = "textBox_MessageNG";
            this.textBox_MessageNG.Size = new System.Drawing.Size(627, 19);
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
            this.label6.Location = new System.Drawing.Point(485, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "Interval(sec.)";
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
            this.textBox_ParameterOK.Text = "voice=female&notify=6&led=00000";
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
            this.toolStripDropDownButton_VoiceSwitch,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(731, 22);
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
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox_MessageNG);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox_MessageOK);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_CheckStyleOK);
            this.groupBox2.Controls.Add(this.textBox_StatusNG);
            this.groupBox2.Controls.Add(this.textBox_StatusOK);
            this.groupBox2.Controls.Add(this.textBox_ParameterNG);
            this.groupBox2.Controls.Add(this.textBox_CheckStyleNG);
            this.groupBox2.Controls.Add(this.textBox_ParameterOK);
            this.groupBox2.Location = new System.Drawing.Point(14, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(655, 194);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(264, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(374, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "request = {ClientName}\\t{Status}\\t{Message}\\t{Parameter}\\t{CheckStyle}";
            // 
            // tabControl_ServerInfo
            // 
            this.tabControl_ServerInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl_ServerInfo.Controls.Add(this.tabPage_View);
            this.tabControl_ServerInfo.Controls.Add(this.tabPage_List);
            this.tabControl_ServerInfo.Location = new System.Drawing.Point(6, 6);
            this.tabControl_ServerInfo.Name = "tabControl_ServerInfo";
            this.tabControl_ServerInfo.SelectedIndex = 0;
            this.tabControl_ServerInfo.ShowToolTips = true;
            this.tabControl_ServerInfo.Size = new System.Drawing.Size(565, 256);
            this.tabControl_ServerInfo.TabIndex = 12;
            this.tabControl_ServerInfo.SelectedIndexChanged += new System.EventHandler(this.tabControl_ServerInfo_SelectedIndexChanged);
            // 
            // tabPage_View
            // 
            this.tabPage_View.Controls.Add(this.panel_Frame);
            this.tabPage_View.Location = new System.Drawing.Point(4, 4);
            this.tabPage_View.Name = "tabPage_View";
            this.tabPage_View.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_View.Size = new System.Drawing.Size(557, 230);
            this.tabPage_View.TabIndex = 0;
            this.tabPage_View.Text = "View";
            this.tabPage_View.UseVisualStyleBackColor = true;
            // 
            // panel_Frame
            // 
            this.panel_Frame.AutoScroll = true;
            this.panel_Frame.Controls.Add(this.panel_View);
            this.panel_Frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Frame.Location = new System.Drawing.Point(3, 3);
            this.panel_Frame.Name = "panel_Frame";
            this.panel_Frame.Size = new System.Drawing.Size(551, 224);
            this.panel_Frame.TabIndex = 0;
            // 
            // panel_View
            // 
            this.panel_View.Location = new System.Drawing.Point(0, 0);
            this.panel_View.Name = "panel_View";
            this.panel_View.Size = new System.Drawing.Size(697, 147);
            this.panel_View.TabIndex = 0;
            // 
            // tabPage_List
            // 
            this.tabPage_List.Controls.Add(this.label3);
            this.tabPage_List.Controls.Add(this.textBox_ServerList);
            this.tabPage_List.Location = new System.Drawing.Point(4, 4);
            this.tabPage_List.Name = "tabPage_List";
            this.tabPage_List.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_List.Size = new System.Drawing.Size(557, 230);
            this.tabPage_List.TabIndex = 1;
            this.tabPage_List.Text = "List";
            this.tabPage_List.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 12);
            this.label3.TabIndex = 13;
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
            this.textBox_ServerList.Size = new System.Drawing.Size(551, 209);
            this.textBox_ServerList.TabIndex = 0;
            this.textBox_ServerList.WordWrap = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.tabPage_Monitor);
            this.tabControl1.Controls.Add(this.tabPage_Message);
            this.tabControl1.Controls.Add(this.tabPage_HTTP);
            this.tabControl1.Location = new System.Drawing.Point(0, 94);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 293);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage_Monitor
            // 
            this.tabPage_Monitor.Controls.Add(this.tabControl_ServerInfo);
            this.tabPage_Monitor.Location = new System.Drawing.Point(4, 4);
            this.tabPage_Monitor.Name = "tabPage_Monitor";
            this.tabPage_Monitor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Monitor.Size = new System.Drawing.Size(690, 285);
            this.tabPage_Monitor.TabIndex = 0;
            this.tabPage_Monitor.Text = "Monitor";
            this.tabPage_Monitor.UseVisualStyleBackColor = true;
            // 
            // tabPage_Message
            // 
            this.tabPage_Message.Controls.Add(this.button_ResetMessageInterval);
            this.tabPage_Message.Controls.Add(this.groupBox2);
            this.tabPage_Message.Controls.Add(this.textBox_TimeOutCheckInterval);
            this.tabPage_Message.Controls.Add(this.label7);
            this.tabPage_Message.Controls.Add(this.textBox_MessageInterval);
            this.tabPage_Message.Controls.Add(this.label6);
            this.tabPage_Message.Location = new System.Drawing.Point(4, 4);
            this.tabPage_Message.Name = "tabPage_Message";
            this.tabPage_Message.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Message.Size = new System.Drawing.Size(690, 285);
            this.tabPage_Message.TabIndex = 1;
            this.tabPage_Message.Text = "Message";
            this.tabPage_Message.UseVisualStyleBackColor = true;
            // 
            // tabPage_HTTP
            // 
            this.tabPage_HTTP.Controls.Add(this.textBox_TimeoutMessage);
            this.tabPage_HTTP.Controls.Add(this.label18);
            this.tabPage_HTTP.Controls.Add(this.checkBox_voiceOffSwitch);
            this.tabPage_HTTP.Controls.Add(this.textBox_TimeoutMessageParameter);
            this.tabPage_HTTP.Controls.Add(this.label_SetClipboard);
            this.tabPage_HTTP.Controls.Add(this.label17);
            this.tabPage_HTTP.Controls.Add(this.textBox_httpTimeout);
            this.tabPage_HTTP.Controls.Add(this.label14);
            this.tabPage_HTTP.Controls.Add(this.button_AddressListLoad);
            this.tabPage_HTTP.Controls.Add(this.label16);
            this.tabPage_HTTP.Controls.Add(this.dataGridView_AddressList);
            this.tabPage_HTTP.Location = new System.Drawing.Point(4, 4);
            this.tabPage_HTTP.Name = "tabPage_HTTP";
            this.tabPage_HTTP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_HTTP.Size = new System.Drawing.Size(690, 285);
            this.tabPage_HTTP.TabIndex = 2;
            this.tabPage_HTTP.Text = "HTTP";
            this.tabPage_HTTP.UseVisualStyleBackColor = true;
            // 
            // textBox_TimeoutMessageParameter
            // 
            this.textBox_TimeoutMessageParameter.Location = new System.Drawing.Point(7, 217);
            this.textBox_TimeoutMessageParameter.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_TimeoutMessageParameter.Name = "textBox_TimeoutMessageParameter";
            this.textBox_TimeoutMessageParameter.Size = new System.Drawing.Size(403, 19);
            this.textBox_TimeoutMessageParameter.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 203);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(143, 12);
            this.label17.TabIndex = 18;
            this.label17.Text = "TimeoutMessageParameter";
            // 
            // textBox_httpTimeout
            // 
            this.textBox_httpTimeout.Location = new System.Drawing.Point(453, 36);
            this.textBox_httpTimeout.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_httpTimeout.Name = "textBox_httpTimeout";
            this.textBox_httpTimeout.Size = new System.Drawing.Size(82, 19);
            this.textBox_httpTimeout.TabIndex = 17;
            this.textBox_httpTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(452, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "http Timeout(sec.)";
            // 
            // button_AddressListLoad
            // 
            this.button_AddressListLoad.Location = new System.Drawing.Point(363, 16);
            this.button_AddressListLoad.Margin = new System.Windows.Forms.Padding(2);
            this.button_AddressListLoad.Name = "button_AddressListLoad";
            this.button_AddressListLoad.Size = new System.Drawing.Size(54, 18);
            this.button_AddressListLoad.TabIndex = 15;
            this.button_AddressListLoad.Text = "Load";
            this.button_AddressListLoad.UseVisualStyleBackColor = true;
            this.button_AddressListLoad.Click += new System.EventHandler(this.button_AddressListLoad_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 21);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 12);
            this.label16.TabIndex = 14;
            this.label16.Text = "AddressList";
            // 
            // dataGridView_AddressList
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_AddressList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_AddressList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AddressList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAddress,
            this.Column_AddressName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_AddressList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_AddressList.Location = new System.Drawing.Point(14, 36);
            this.dataGridView_AddressList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_AddressList.Name = "dataGridView_AddressList";
            this.dataGridView_AddressList.RowTemplate.Height = 24;
            this.dataGridView_AddressList.Size = new System.Drawing.Size(404, 120);
            this.dataGridView_AddressList.TabIndex = 13;
            this.dataGridView_AddressList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_AddressList_CellPainting);
            this.dataGridView_AddressList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_AddressList_CellValueChanged);
            // 
            // ColumnAddress
            // 
            this.ColumnAddress.HeaderText = "Address";
            this.ColumnAddress.Name = "ColumnAddress";
            // 
            // Column_AddressName
            // 
            this.Column_AddressName.HeaderText = "Name";
            this.Column_AddressName.Name = "Column_AddressName";
            // 
            // checkBox_voiceOffSwitch
            // 
            this.checkBox_voiceOffSwitch.AutoSize = true;
            this.checkBox_voiceOffSwitch.Checked = true;
            this.checkBox_voiceOffSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_voiceOffSwitch.Location = new System.Drawing.Point(285, 17);
            this.checkBox_voiceOffSwitch.Name = "checkBox_voiceOffSwitch";
            this.checkBox_voiceOffSwitch.Size = new System.Drawing.Size(73, 16);
            this.checkBox_voiceOffSwitch.TabIndex = 20;
            this.checkBox_voiceOffSwitch.Text = "voiceOFF";
            this.checkBox_voiceOffSwitch.UseVisualStyleBackColor = true;
            this.checkBox_voiceOffSwitch.CheckedChanged += new System.EventHandler(this.checkBox_voiceOffSwitch_CheckedChanged);
            // 
            // toolStripDropDownButton_VoiceSwitch
            // 
            this.toolStripDropDownButton_VoiceSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton_VoiceSwitch.Image = global::AliveMonitoring.Properties.Resources.VoiceON048;
            this.toolStripDropDownButton_VoiceSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_VoiceSwitch.Name = "toolStripDropDownButton_VoiceSwitch";
            this.toolStripDropDownButton_VoiceSwitch.ShowDropDownArrow = false;
            this.toolStripDropDownButton_VoiceSwitch.Size = new System.Drawing.Size(20, 20);
            this.toolStripDropDownButton_VoiceSwitch.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton_VoiceSwitch.Click += new System.EventHandler(this.toolStripDropDownButton_VoiceSwitch_Click);
            // 
            // label_SetClipboard
            // 
            this.label_SetClipboard.AutoSize = true;
            this.label_SetClipboard.Location = new System.Drawing.Point(328, 168);
            this.label_SetClipboard.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SetClipboard.Name = "label_SetClipboard";
            this.label_SetClipboard.Size = new System.Drawing.Size(73, 12);
            this.label_SetClipboard.TabIndex = 18;
            this.label_SetClipboard.Text = "{ServerName}";
            this.label_SetClipboard.Click += new System.EventHandler(this.label_SetClipboard_Click);
            // 
            // textBox_TimeoutMessage
            // 
            this.textBox_TimeoutMessage.Location = new System.Drawing.Point(8, 182);
            this.textBox_TimeoutMessage.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_TimeoutMessage.Name = "textBox_TimeoutMessage";
            this.textBox_TimeoutMessage.Size = new System.Drawing.Size(403, 19);
            this.textBox_TimeoutMessage.TabIndex = 22;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 168);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 12);
            this.label18.TabIndex = 21;
            this.label18.Text = "TimeoutMessage";
            // 
            // button_ResetMessageInterval
            // 
            this.button_ResetMessageInterval.Location = new System.Drawing.Point(620, 215);
            this.button_ResetMessageInterval.Name = "button_ResetMessageInterval";
            this.button_ResetMessageInterval.Size = new System.Drawing.Size(48, 44);
            this.button_ResetMessageInterval.TabIndex = 11;
            this.button_ResetMessageInterval.Text = "Reset";
            this.button_ResetMessageInterval.UseVisualStyleBackColor = true;
            this.button_ResetMessageInterval.Click += new System.EventHandler(this.button_ResetMessageInterval_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(410, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "TimeOutCheckInterval(sec.)";
            // 
            // textBox_TimeOutCheckInterval
            // 
            this.textBox_TimeOutCheckInterval.Location = new System.Drawing.Point(562, 240);
            this.textBox_TimeOutCheckInterval.Name = "textBox_TimeOutCheckInterval";
            this.textBox_TimeOutCheckInterval.Size = new System.Drawing.Size(48, 19);
            this.textBox_TimeOutCheckInterval.TabIndex = 0;
            this.textBox_TimeOutCheckInterval.Text = "30";
            this.textBox_TimeOutCheckInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 441);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AliveMonitoring";
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
            this.panel_Frame.ResumeLayout(false);
            this.tabPage_List.ResumeLayout(false);
            this.tabPage_List.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Monitor.ResumeLayout(false);
            this.tabPage_Message.ResumeLayout(false);
            this.tabPage_Message.PerformLayout();
            this.tabPage_HTTP.ResumeLayout(false);
            this.tabPage_HTTP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AddressList)).EndInit();
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
        private System.Windows.Forms.Timer timer_LabelUpdate;
        private System.Windows.Forms.Timer timer_UpdateQueue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_clientName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_MessageInterval;
        private System.Windows.Forms.TextBox textBox_MessageOK;
        private System.Windows.Forms.TextBox textBox_MessageNG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Panel panel_Frame;
        private System.Windows.Forms.Panel panel_View;
        private System.Windows.Forms.TabPage tabPage_List;
        private System.Windows.Forms.TextBox textBox_ServerList;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Monitor;
        private System.Windows.Forms.TabPage tabPage_Message;
        private System.Windows.Forms.TabPage tabPage_HTTP;
        private System.Windows.Forms.TextBox textBox_TimeoutMessageParameter;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_httpTimeout;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button_AddressListLoad;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dataGridView_AddressList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_AddressName;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_VoiceSwitch;
        private System.Windows.Forms.CheckBox checkBox_voiceOffSwitch;
        private System.Windows.Forms.Label label_SetClipboard;
        private System.Windows.Forms.TextBox textBox_TimeoutMessage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button_ResetMessageInterval;
        private System.Windows.Forms.TextBox textBox_TimeOutCheckInterval;
        private System.Windows.Forms.Label label7;
    }
}

