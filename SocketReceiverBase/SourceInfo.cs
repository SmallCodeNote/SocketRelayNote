using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using tcpClient;

namespace SourceInfoUserControl
{
    public partial class SourceInfo : UserControl
    {
        //===================
        // Constructor
        //===================
        public SourceInfo(int Index, string SourceName, string SaveDirPath, string ModelPath, string LastCheckTimeString = "")
        {
            InitializeComponent();
            tcpClt = new TcpSocketClient();
            SourceInfoUpdate(Index, SourceName, SaveDirPath, ModelPath);
            if (LastCheckTimeString == "") { LastCheckTime = DateTime.Now.AddHours(-1); }
            else if (DateTime.TryParse(LastCheckTimeString, out DateTime dateTime)) { LastCheckTime = dateTime; }

        }

        public SourceInfo(int Index, string Line = "\t\t\t")
        {
            InitializeComponent();
            tcpClt = new TcpSocketClient();

            string[] cols = Line.Split('\t');
            string SourceName = cols.Length > 0 ? cols[0] : "";
            string SaveDirPath = cols.Length > 1 ? cols[1] : "";
            string ModelPath = cols.Length > 2 ? cols[2] : "";
            string LastCheckTimeString = cols.Length > 3 ? cols[3] : "";

            SourceInfoUpdate(Index, SourceName, SaveDirPath, ModelPath, LastCheckTimeString);

            if(LastCheckTimeString =="") LastCheckTime = DateTime.Now.AddHours(-1);
        }

        //===================
        // Member variable
        //===================
        TcpSocketClient tcpClt;
        public int TimeOutCount = 0;
        public int Index;

        public Action<int> DeleteThis;
        public Action LoadThis;

        private DateTime _LastCheckTime;
        public DateTime LastCheckTime
        {
            get { return _LastCheckTime; }
            set { _LastCheckTime = value; textBox_LastCheckTime.Text = value.ToString("yyyy/MM/dd HH:mm:ss"); }
        }

        public string SaveDirPath
        {
            get { return textBox_SaveDirPath.Text; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => SaveDirPath = value));
                }
                else
                {
                    textBox_SaveDirPath.Text = value;
                }
            }
        }

        public string ModelPath
        {
            get
            {
                return textBox_ModelPath.Text;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => ModelPath = value));
                }
                else
                {
                    textBox_ModelPath.Text = value;
                }
            }
        }

        public string SourceName
        {
            get { return textBox_SourceName.Text; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => SourceName = value));
                }
                else
                {
                    groupBox_SourceInfo.Text = value;
                    textBox_SourceName.Text = value;
                }
            }
        }

        public string LatestAnswer
        {
            get { return label_LatestAnswer.Text; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => { LatestAnswer = value; }));
                }
                else
                {
                    label_LatestAnswer.Text = value;
                    label_LatestAnswerTime.Text = DateTime.Now.ToString("MM/dd HH:mm:ss");

                    if (value == "") { button_Lamp.BackColor = Color.Red; label_LatestAnswerTime.Text += " (TimeOut x" + TimeOutCount.ToString() + ")"; } else if (value != "Connecting...") { button_Lamp.BackColor = Color.YellowGreen; }
                }
            }
        }

        public bool Alive { get { return button_Lamp.BackColor != Color.Red; } }

        //===================
        // Member function
        //===================

        public void SourceInfoUpdate(int Index, string SourceName, string SaveDirPath, string ModelPath, string LastCheckTimeString = "")
        {
            this.Height = 80;

            this.SourceName = SourceName;
            this.SaveDirPath = SaveDirPath;
            this.ModelPath = ModelPath;
            this.Index = Index;

            if (DateTime.TryParse(LastCheckTimeString, out DateTime datetime)) { this.LastCheckTime = datetime; }

        }

        public override string ToString()
        {
            return SourceName + "\t" + SaveDirPath + "\t" + ModelPath.ToString() + "\t" + LastCheckTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

        //===================
        // Event
        //===================
        private void SourceInfo_Load(object sender, EventArgs e)
        {
            this.groupBox_SourceInfo.Height = 78;
            this.panel_Frame.Height = 55;
            this.panel.Top = 0;
        }

        private void button_Shift_Click(object sender, EventArgs e)
        {
            if (button_Shift.Text == ">")
            {
                this.panel.Top = -55;
                button_Shift.Text = "<";
            }
            else
            {
                this.panel.Top = 0;
                button_Shift.Text = ">";
            }
        }

        private void button_DeleteThis_Click(object sender, EventArgs e)
        {
            DeleteThis(Index);
        }

        private void textBox_SaveDirPath_TextChanged(object sender, EventArgs e)
        {
            if (LoadThis != null) LoadThis();
        }

        private void textBox_ModelPath_TextChanged(object sender, EventArgs e)
        {
            if (LoadThis != null) LoadThis();
        }

        private void textBox_SourceName_TextChanged(object sender, EventArgs e)
        {
            if (LoadThis != null) LoadThis();
            groupBox_SourceInfo.Text = textBox_SourceName.Text;
        }

        private void textBox_LastCheckTime_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(textBox_LastCheckTime.Text, out DateTime dateTime))
            {
                _LastCheckTime = dateTime;
            }
        }
    }
}
