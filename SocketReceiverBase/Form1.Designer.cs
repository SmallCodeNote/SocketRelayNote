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
            this.SuspendLayout();
            // 
            // button_StatusChange
            // 
            this.button_StatusChange.Location = new System.Drawing.Point(12, 8);
            this.button_StatusChange.Name = "button_StatusChange";
            this.button_StatusChange.Size = new System.Drawing.Size(41, 38);
            this.button_StatusChange.TabIndex = 0;
            this.button_StatusChange.UseVisualStyleBackColor = true;
            this.button_StatusChange.EnabledChanged += new System.EventHandler(this.button_StatusChange_EnabledChanged);
            this.button_StatusChange.Click += new System.EventHandler(this.button_StatusChange_Click);
            // 
            // textBox_PortNumber
            // 
            this.textBox_PortNumber.Location = new System.Drawing.Point(72, 27);
            this.textBox_PortNumber.Name = "textBox_PortNumber";
            this.textBox_PortNumber.Size = new System.Drawing.Size(63, 19);
            this.textBox_PortNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "PortNumber";
            // 
            // label_LockedFrom
            // 
            this.label_LockedFrom.AutoSize = true;
            this.label_LockedFrom.Location = new System.Drawing.Point(172, 9);
            this.label_LockedFrom.Name = "label_LockedFrom";
            this.label_LockedFrom.Size = new System.Drawing.Size(11, 12);
            this.label_LockedFrom.TabIndex = 3;
            this.label_LockedFrom.Text = "-";
            // 
            // label_LockedTime
            // 
            this.label_LockedTime.AutoSize = true;
            this.label_LockedTime.Location = new System.Drawing.Point(172, 21);
            this.label_LockedTime.Name = "label_LockedTime";
            this.label_LockedTime.Size = new System.Drawing.Size(11, 12);
            this.label_LockedTime.TabIndex = 3;
            this.label_LockedTime.Text = "-";
            // 
            // label_ResetTime
            // 
            this.label_ResetTime.AutoSize = true;
            this.label_ResetTime.Location = new System.Drawing.Point(172, 34);
            this.label_ResetTime.Name = "label_ResetTime";
            this.label_ResetTime.Size = new System.Drawing.Size(11, 12);
            this.label_ResetTime.TabIndex = 3;
            this.label_ResetTime.Text = "-";
            // 
            // label_RemainingTime
            // 
            this.label_RemainingTime.AutoSize = true;
            this.label_RemainingTime.Location = new System.Drawing.Point(172, 46);
            this.label_RemainingTime.Name = "label_RemainingTime";
            this.label_RemainingTime.Size = new System.Drawing.Size(11, 12);
            this.label_RemainingTime.TabIndex = 3;
            this.label_RemainingTime.Text = "-";
            // 
            // button_Lock
            // 
            this.button_Lock.Location = new System.Drawing.Point(361, 9);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 64);
            this.Controls.Add(this.button_Lock);
            this.Controls.Add(this.label_RemainingTime);
            this.Controls.Add(this.label_ResetTime);
            this.Controls.Add(this.label_LockedTime);
            this.Controls.Add(this.label_LockedFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_PortNumber);
            this.Controls.Add(this.button_StatusChange);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SocketReceiverBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

