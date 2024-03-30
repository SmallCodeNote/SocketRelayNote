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

namespace SocketReceiverBase
{
    public partial class ServerInfo : UserControl
    {
        //===================
        // Constructor
        //===================
        public ServerInfo(string ServerName, string Address, int Port)
        {
            InitializeComponent();
            tcpClt = new TcpSocketClient();

            ServerInfoUpdate(ServerName, Address, Port);
        }

        public ServerInfo(string Line)
        {
            InitializeComponent();
            tcpClt = new TcpSocketClient();

            string[] cols = Line.Split('\t');

            string ServerName = cols[0];
            string Address = cols[1];
            int Port = int.Parse(cols[2]);

            ServerInfoUpdate(ServerName, Address, Port);
        }

        //===================
        // Member variable
        //===================
        TcpSocketClient tcpClt;

        public string Address
        {
            get { return textBox_Address.Text; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => Address = value));
                }
                else
                {
                    textBox_Address.Text = value;
                }
            }
        }

        public int Port
        {
            get
            {
                int b = -1;
                if (!int.TryParse(textBox_Port.Text, out b)) { b = -1; };
                return b;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => Port = value));
                }
                else
                {
                    textBox_Port.Text = value.ToString();
                }
            }
        }

        public string ServerName
        {
            get { return textBox_ServerName.Text; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => ServerName = value));
                }
                else
                {
                    groupBox_ServerInfo.Text = value;
                    textBox_ServerName.Text = value;
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

                    if (value == "") { button_Lamp.BackColor = Color.Red; label_LatestAnswerTime.Text += " (TimeOut)"; } else { button_Lamp.BackColor = Color.YellowGreen; }
                }
            }
        }

        //===================
        // Member function
        //===================

        public void ServerInfoUpdate(string ServerName, string Address, int Port)
        {
            this.Height = 70;

            this.ServerName = ServerName;
            this.Address = Address;
            this.Port = Port;
        }

        public override string ToString()
        {
            return ServerName + "\t" + Address + "\t" + Port.ToString();
        }

        public async void SendMessage(string request)
        {
            if (Port >= 1024)
            {
                LatestAnswer = await tcpClt.StartClient(Address, Port, request, "UTF8");
            }
        }

        //===================
        // Event
        //===================
        private void ServerInfo_Load(object sender, EventArgs e)
        {
            this.groupBox_ServerInfo.Height = 68;
            this.panel_Frame.Height = 45;
            this.panel.Top = 0;
        }

        private void button_Shift_Click(object sender, EventArgs e)
        {
            if (button_Shift.Text == ">")
            {
                this.panel.Top = -45;
                button_Shift.Text = "<";
            }
            else
            {
                this.panel.Top = 0;
                button_Shift.Text = ">";
            }
        }
    }
}
