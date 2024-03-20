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
            this.timer_LabelUpdate = new System.Windows.Forms.Timer(this.components);
            this.timer_UpdateQueue = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_clientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_IntervalOK = new System.Windows.Forms.TextBox();
            this.textBox_MessageOK = new System.Windows.Forms.TextBox();
            this.textBox_MessageServerAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.textBox_MessageServerPort = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button_Restrart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Size = new System.Drawing.Size(657, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
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
            this.textBox_IntervalOK.Location = new System.Drawing.Point(615, 179);
            this.textBox_IntervalOK.Name = "textBox_IntervalOK";
            this.textBox_IntervalOK.Size = new System.Drawing.Size(47, 19);
            this.textBox_IntervalOK.TabIndex = 0;
            this.textBox_IntervalOK.Text = "5000";
            // 
            // textBox_MessageOK
            // 
            this.textBox_MessageOK.Location = new System.Drawing.Point(18, 157);
            this.textBox_MessageOK.Name = "textBox_MessageOK";
            this.textBox_MessageOK.Size = new System.Drawing.Size(645, 19);
            this.textBox_MessageOK.TabIndex = 0;
            this.textBox_MessageOK.Text = "OK message";
            // 
            // textBox_MessageServerAddress
            // 
            this.textBox_MessageServerAddress.Location = new System.Drawing.Point(148, 105);
            this.textBox_MessageServerAddress.Name = "textBox_MessageServerAddress";
            this.textBox_MessageServerAddress.Size = new System.Drawing.Size(223, 19);
            this.textBox_MessageServerAddress.TabIndex = 1;
            this.textBox_MessageServerAddress.Text = "localhost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "MessageServerAddress";
            // 
            // textBox_IntervalNG
            // 
            this.textBox_IntervalNG.Location = new System.Drawing.Point(616, 260);
            this.textBox_IntervalNG.Name = "textBox_IntervalNG";
            this.textBox_IntervalNG.Size = new System.Drawing.Size(47, 19);
            this.textBox_IntervalNG.TabIndex = 0;
            this.textBox_IntervalNG.Text = "5000";
            // 
            // textBox_MessageNG
            // 
            this.textBox_MessageNG.Location = new System.Drawing.Point(19, 238);
            this.textBox_MessageNG.Name = "textBox_MessageNG";
            this.textBox_MessageNG.Size = new System.Drawing.Size(645, 19);
            this.textBox_MessageNG.TabIndex = 0;
            this.textBox_MessageNG.Text = "NG message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Message";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Message";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(551, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "Interval";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(552, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Interval";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "status";
            // 
            // textBox_StatusOK
            // 
            this.textBox_StatusOK.Location = new System.Drawing.Point(60, 179);
            this.textBox_StatusOK.Name = "textBox_StatusOK";
            this.textBox_StatusOK.Size = new System.Drawing.Size(61, 19);
            this.textBox_StatusOK.TabIndex = 0;
            this.textBox_StatusOK.Text = "Notice";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "status";
            // 
            // textBox_StatusNG
            // 
            this.textBox_StatusNG.Location = new System.Drawing.Point(61, 260);
            this.textBox_StatusNG.Name = "textBox_StatusNG";
            this.textBox_StatusNG.Size = new System.Drawing.Size(61, 19);
            this.textBox_StatusNG.TabIndex = 0;
            this.textBox_StatusNG.Text = "Warning";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "parameter";
            // 
            // textBox_ParameterOK
            // 
            this.textBox_ParameterOK.Location = new System.Drawing.Point(189, 178);
            this.textBox_ParameterOK.Name = "textBox_ParameterOK";
            this.textBox_ParameterOK.Size = new System.Drawing.Size(198, 19);
            this.textBox_ParameterOK.TabIndex = 0;
            this.textBox_ParameterOK.Text = "voice=female&notify=6&led=00100";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(127, 264);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "parameter";
            // 
            // textBox_ParameterNG
            // 
            this.textBox_ParameterNG.Location = new System.Drawing.Point(189, 260);
            this.textBox_ParameterNG.Name = "textBox_ParameterNG";
            this.textBox_ParameterNG.Size = new System.Drawing.Size(198, 19);
            this.textBox_ParameterNG.TabIndex = 0;
            this.textBox_ParameterNG.Text = "voice=female&notify=6&led=10000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(393, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "checkStyle";
            // 
            // textBox_CheckStyleOK
            // 
            this.textBox_CheckStyleOK.Location = new System.Drawing.Point(460, 178);
            this.textBox_CheckStyleOK.Name = "textBox_CheckStyleOK";
            this.textBox_CheckStyleOK.Size = new System.Drawing.Size(47, 19);
            this.textBox_CheckStyleOK.TabIndex = 0;
            this.textBox_CheckStyleOK.Text = "Once";
            // 
            // textBox_CheckStyleNG
            // 
            this.textBox_CheckStyleNG.Location = new System.Drawing.Point(460, 260);
            this.textBox_CheckStyleNG.Name = "textBox_CheckStyleNG";
            this.textBox_CheckStyleNG.Size = new System.Drawing.Size(47, 19);
            this.textBox_CheckStyleNG.TabIndex = 0;
            this.textBox_CheckStyleNG.Text = "Once";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(393, 264);
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
            // textBox_MessageServerPort
            // 
            this.textBox_MessageServerPort.Location = new System.Drawing.Point(497, 105);
            this.textBox_MessageServerPort.Name = "textBox_MessageServerPort";
            this.textBox_MessageServerPort.Size = new System.Drawing.Size(63, 19);
            this.textBox_MessageServerPort.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(387, 112);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "MessageServerPort";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 291);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox_MessageNG);
            this.Controls.Add(this.textBox_MessageServerPort);
            this.Controls.Add(this.textBox_MessageOK);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_IntervalNG);
            this.Controls.Add(this.textBox_StatusNG);
            this.Controls.Add(this.textBox_ParameterNG);
            this.Controls.Add(this.textBox_ParameterOK);
            this.Controls.Add(this.textBox_CheckStyleNG);
            this.Controls.Add(this.textBox_StatusOK);
            this.Controls.Add(this.textBox_CheckStyleOK);
            this.Controls.Add(this.textBox_IntervalOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_MessageServerAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SocketReceiverBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox_IntervalOK;
        private System.Windows.Forms.TextBox textBox_MessageOK;
        private System.Windows.Forms.TextBox textBox_MessageServerAddress;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.TextBox textBox_MessageServerPort;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button_Restrart;
    }
}

