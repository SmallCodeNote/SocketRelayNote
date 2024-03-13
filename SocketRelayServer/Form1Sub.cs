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
        private void updateStatusList()
        {
            _LiteDBconnectionString.Filename = textBox_DataBaseFilePath.Text;

            if (!File.Exists(_LiteDBconnectionString.Filename)) return;
            using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
            {
                var col = litedb.GetCollection<SocketMessage>("table_Message");

                foreach (MessageItemView messageItemView in panel_StatusList.Controls)
                {
                    try
                    {
                        string clientName = messageItemView.groupBox_ClientName.Text;
                        ILiteQueryable<SocketMessage> query = col.Query().Where(x => x.clientName == clientName).OrderBy(x => x.connectTime, 0);

                        if (query.Count() > 0)
                        {
                            SocketMessage socketMessage = query.First();
                            messageItemView.setItems(socketMessage);
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " EX:" + ex.ToString());
                    }

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

    }
}
