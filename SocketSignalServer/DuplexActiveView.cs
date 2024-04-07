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

namespace SocketSignalServer
{
    public partial class DuplexActiveView : UserControl
    {
        TcpSocketClient tcpClient;

        public DuplexActiveView(int Index, string Address, int Port)
        {
            InitializeComponent();
            this.Address = Address;
            this.Port = Port;
            this.Index = Index;

            Alive = true;

            tcpClient = new TcpSocketClient();
        }

        public DuplexActiveView(int Index, string Line = "\t")
        {
            InitializeComponent();

            string[] Cols = Line.Split('\t');

            if (Cols.Length > 0) { this.Address = Cols[0]; }
            if (Cols.Length > 1) { int b = -1; int.TryParse(Cols[1], out b); this.Port = b; }

            this.Index = Index;

            Alive = true;

            tcpClient = new TcpSocketClient();
        }

        public override string ToString()
        {
            return Address + "\t" + Port.ToString();
        }

        public string Address { get { return textBox_Address.Text; } set { textBox_Address.Text = value; } }
        public int Port { get { int b = -1; if (!int.TryParse(textBox_Port.Text, out b)) { b = -1; } return b; } set { textBox_Port.Text = value.ToString(); } }
        public bool Alive { get { return button_Status.BackColor != Color.Red; } set { if (value) { button_Status.BackColor = Color.YellowGreen; } else { button_Status.BackColor = Color.Red; } } }
        public int Index;

        public Action<int> DeleteThis;
        private void button_DeleteThis_Click(object sender, EventArgs e)
        {
            DeleteThis(Index);
        }

        public Action LoadThis;

        private void textBox_Address_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadThis();
            }
            catch
            {
            }
        }

        private void textBox_Port_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadThis();
            }
            catch
            {
            }
        }

        private bool _askNow = false;

        public void askAlive()
        {
            if (!_askNow)
            {
                Task.Run(() => _askAlive());
            }
        }

        private async void _askAlive()
        {
            string result = await tcpClient.StartClient(Address, Port, "askAlive", "UTF8");
            if (result == "") { Alive = false; } else { Alive = true; }
            _askNow = false;
        }

    }
}
