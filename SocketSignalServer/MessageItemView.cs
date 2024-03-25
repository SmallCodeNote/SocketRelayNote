using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

using LiteDB;

namespace SocketSignalServer
{
    public partial class MessageItemView : UserControl
    {
        public SocketMessage _message;
        
        private ConnectionString _LiteDBconnectionString;

        NoticeTransmitter noticeTransmitter;
        public List<NoticeMessage> _noticeList_CheckChange;
        

        public MessageItemView(NoticeTransmitter noticeTransmitter, ConnectionString LiteDBconnectionString)
        {
            InitializeComponent();

            this.label_Status.Text = "";
            this.label_LastConnectTime.Text = "";
            this.label_ElapsedTime.Text = "";
            this.label_LastMessage.Text = "";

            this.noticeTransmitter = noticeTransmitter;

            this._LiteDBconnectionString = LiteDBconnectionString;
            this._LiteDBconnectionString.Connection = ConnectionType.Shared;

        }

        public void setItems(SocketMessage message)
        {
            _message = message;
            
            
            this.groupBox_ClientName.Text = message.clientName;
            this.label_Status.Text = message.status.ToString();
            this.label_LastConnectTime.Text = message.connectTime.ToString("yyyy/MM/dd HH:mm:ss");
            this.label_ElapsedTime.Text = getElapsedTimeString(DateTime.Now - message.connectTime);
            this.label_LastMessage.Text = message.message;
            this.checkBox_check.Checked = message.check;
            

        }

        public string getElapsedTimeString(TimeSpan elapsedTime)
        {
            if (elapsedTime.TotalDays >= 365) { return (elapsedTime.TotalDays / 365.2425).ToString("0") + " year"; }
            if (elapsedTime.TotalDays >= 30) { return (elapsedTime.TotalDays / 30.436875).ToString("0") + " month"; }
            if (elapsedTime.TotalDays >= 7) { return (elapsedTime.TotalDays / 7).ToString("0") + " week"; }
            if (elapsedTime.TotalDays >= 1) { return (elapsedTime.TotalDays / 7).ToString("0") + " day"; }
            if (elapsedTime.TotalHours >= 1) { return (elapsedTime.TotalHours).ToString("0") + " hour"; }
            if (elapsedTime.TotalMinutes >= 1) { return (elapsedTime.TotalMinutes).ToString("0") + " minute"; }
            return "now";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _message.check = checkBox_check.Checked;

            try
            {
                using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                {
                    var col = litedb.GetCollection<SocketMessage>("table_Message");

                    try
                    {
                        var record = col.FindOne(x => x.connectTime == this._message.connectTime && x.clientName == this._message.clientName && x.status == this._message.status);
                        string key = this._message.clientName + "_" + this._message.connectTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
                        record.check = checkBox_check.Checked;
                        col.Update(key, record);

                        foreach (var q in _noticeList_CheckChange)
                        {
                            noticeTransmitter.NoticeQueue.Enqueue(q);
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());
            }
        }

        private void button_AllCheck_Click(object sender, EventArgs e)
        {
            int _retryCountMax = 10;

            for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
            {
                try
                {
                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");
                        try
                        {
                            var records = col.Query()
                                .Where(x => x.clientName == this._message.clientName && x.check == false)
                                .ToList();

                            foreach (var record in records)
                            {
                                record.check = true;
                                col.Update(record.Key(), record);
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());
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
                    Thread.Sleep(100);

                }
            }


        }
    }
}
