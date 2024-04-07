using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Net.Http;
using System.Collections.Concurrent;
using System.Diagnostics;

using System.Xml;

namespace SocketSignalServer
{
    public class NoticeTransmitter
    {
        //===================
        // Constructor
        //===================
        public NoticeTransmitter(bool voiceOFF = true,bool isActive = false)
        {
            NoticeQueue = new ConcurrentQueue<NoticeMessage>();
            NoticeRunningDictionary = new ConcurrentDictionary<string, NoticeMessageHandling>();

            this.voiceOFF = voiceOFF;
            this.isActive = isActive;
        }

        //===================
        // Member variable
        //===================
        /// <summary>
        /// Received Notice Queue.
        /// </summary>
        public ConcurrentQueue<NoticeMessage> NoticeQueue;

        /// <summary>
        /// Running Notice.
        /// </summary>
        public ConcurrentDictionary<string, NoticeMessageHandling> NoticeRunningDictionary;

        private bool _noticeCheckContinueFlag = false;
        public int _threadSleepLength = 100;

        /// <summary>
        /// timeout length (sec.)
        /// </summary>
        public int httpTimeout = 3;

        public bool voiceOFF = true;

        /// <summary>
        /// duplex system class  true:Active / false:Standby
        /// </summary>
        public bool isActive = false;

        //===================
        // Member function
        //===================
        public bool AddNotice(NoticeMessage notice)
        {
            if (!NoticeRunningDictionary.ContainsKey(notice.Key))
            {
                NoticeQueue.Enqueue(notice);
                return true;
            };

            return false;
        }

        public bool AddNotice(ClientData targetClient, SocketMessage socketMessage)
        {
            bool result = true;

            foreach (var address in targetClient.addressList)
            {
                NoticeMessage item = new NoticeMessage(address.address, socketMessage);

                result = result && AddNotice(item);
            }

            return result;
        }

        public void StartNoticeCheck()
        {
            if (!_noticeCheckContinueFlag)
            {
                _noticeCheckContinueFlag = true;
                Task.Run(() => NoticeCheck());
            }

        }

        public void StopNoticeCheck()
        {
            _noticeCheckContinueFlag = false;
            return;
        }

        public void NoticeCheck()
        {
            while (_noticeCheckContinueFlag)
            {
                try
                {
                    if (NoticeRunningDictionary.Count > 0)
                    {
                        foreach (var item in NoticeRunningDictionary)
                        {
                            if (item.Value.FinishNotice)
                            {
                                NoticeMessageHandling h;
                                if (NoticeRunningDictionary.TryRemove(item.Key, out h)) { h.Dispose(); };
                            }
                        }
                    }

                    if (NoticeQueue.Count > 0)
                    {
                        NoticeMessage b;
                        if (NoticeQueue.TryDequeue(out b))
                        {
                            NoticeMessageHandling handling = new NoticeMessageHandling(b, httpTimeout, voiceOFF,isActive);

                            if (NoticeRunningDictionary.TryAdd(b.Key, handling))
                            {
                                handling.StartNotice();
                            }
                        }
                    }

                    Thread.Sleep(_threadSleepLength);

                }
                catch (Exception ex)
                {
                    Debug.Write(DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " ");
                    Debug.WriteLine(ex.ToString());
                }
            }

        }

    }

    public class NoticeMessageHandling : IDisposable
    {
        //===================
        // Constructor
        //===================
        public NoticeMessageHandling(NoticeMessage notice, int timeout = 3, bool voiceOFF = true, bool isActive = false)
        {
            this.notice = notice;
            httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, timeout);

            this.voiceOFF = voiceOFF;
            this.isActive = isActive;
        }

        //===================
        // Member variable
        //===================
        private HttpClient httpClient;
        public NoticeMessage notice;

        public bool FinishNotice = false;
        public int WaitNoticeFinish_Timeout = 30;

        public int _threadSleepLength = 100;
        public bool voiceOFF = true;

        /// <summary>
        /// duplex system class  true:Active / false:Standby
        /// </summary>
        public bool isActive = false;

        public int timeout
        {
            get { return (int)httpClient.Timeout.TotalSeconds; }
            set { httpClient.Timeout = new TimeSpan(0, 0, value); }
        }

        //===================
        // Member function
        //===================
        public Task StartNotice()
        {
            return Task.Run(() =>
            {
                FinishNotice = false;
                
                if (!WaitNoticeFinish())
                {
                    FinishNotice = true;
                    return; //notice continue check error
                }
                
                SendNotice();
                Thread.Sleep(timeout * 1000);  //(timeout[sec.] x 1000) [ms]
                WaitNoticeFinish();
                FinishNotice = true;

            });

        }

        private string SendNotice()
        {
            string speech = (notice.message != null && notice.message.Length > 0) ? "speech=" + notice.message : "";
            string parameter = (notice.parameter != null && notice.parameter.Length > 0) ?  notice.parameter : "";
            string separator = (speech.Length > 0 && parameter.Length > 0) ? "&" : "";

            if (speech.Length == 0 && parameter.Length == 0) return "";
           
            string url = @"http://" + notice.address + @"/api/control?" + parameter + separator + speech;

            Debug.Write(DateTime.Now.ToString("HH:mm:ss") + "\t" + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "\t");
            Debug.WriteLine(url);

            if (voiceOFF || !isActive) { return ""; }

            try
            {
                string pageBody = httpClient.GetStringAsync(url).Result;
                return pageBody;
            }
            catch (Exception ex)
            {
                Debug.Write(DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " ");
                Debug.WriteLine(ex.ToString());
            }

            return "";
        }

        private bool WaitNoticeFinish()
        {
            DateTime startTime = DateTime.Now;

                Debug.WriteLine("WaitStart_Debug " + DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " ");
                Thread.Sleep(10000);
                Debug.WriteLine("WaitEnd_Debug " + DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " ");

            string url = @"http://" + notice.address + @"/api/status?format=xml";
            Debug.WriteLine(url);

            if (voiceOFF || !isActive) { return true; }

            do
            {
                try
                {
                    string pageBody = httpClient.GetStringAsync(url).Result;

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(pageBody);

                    XmlNode soundNode = doc.SelectSingleNode("//sound[@name='SOUND']");
                    string soundValue = soundNode.Attributes["value"].Value;

                    bool waitContinue = soundValue != "0";
                    if (!waitContinue)
                    {
                        Debug.Write("WaitEnd " + DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " ");
                        Debug.WriteLine(url);
                        return true;
                    }

                    Thread.Sleep(_threadSleepLength);

                }
                catch (Exception ex)
                {
                    Debug.Write("WaitEnd_ERROR " + DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "]]");
                    Debug.WriteLine(ex.ToString());
                    return false;
                }

            } while ((DateTime.Now - startTime).TotalSeconds > WaitNoticeFinish_Timeout);

            Debug.WriteLine("WaitEnd_TimeOut " + DateTime.Now.ToString("HH:mm:ss") + " " + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "]]");

            return false;
        }

        public void Dispose()
        {
            httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

    }

    public class NoticeMessage
    {
        public string address;
        public string message;
        public string parameter;
        public DateTime keyTime;

        public string Key
        {
            get { return this.address + "_" + keyTime.ToString("yyyy/MM/dd HH:mm:ss.fff") + "_" + message; }
        }


        //===================
        // Member function
        //===================
        public NoticeMessage(string address, SocketMessage socket)
        {
            this.address = address;
            this.message = socket.message;
            this.parameter = socket.parameter;
            this.keyTime = socket.connectTime;
        }

        public static bool operator ==(NoticeMessage c1, NoticeMessage c2)
        {
            return c1.Key == c2.Key;
        }

        public static bool operator !=(NoticeMessage c1, NoticeMessage c2)
        {
            return c1.Key != c2.Key;

        }

    }

}
