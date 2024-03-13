using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace tcpServer
{
    class TcpSocketServer
    {
        public DateTime LastReceiveTime;

        /// <summary>
        /// Received TcpSocket Queue.
        /// </summary>
        public ConcurrentQueue<string> ReceivedSocketQueue;
        public int _ReceivedSocketQueueMaxSize = 1024;

        private bool _ListeningContinueFlag;
        public int _bufferSize = 1024;

        public TcpSocketServer()
        {
            LastReceiveTime = DateTime.Now;
            ReceivedSocketQueue = new ConcurrentQueue<string>();

        }

        public void StopListening()
        {
            _ListeningContinueFlag = false;
            return;
        }

        public async Task<bool> StartListening(int port, string encoding = "ASCII")
        {
            _ListeningContinueFlag = true;
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            var tcpServer = new TcpListener(localEndPoint);

            try
            {
                tcpServer.Start();

                while (_ListeningContinueFlag)
                {
                    using (var tcpClient = await tcpServer.AcceptTcpClientAsync())
                    {
                        var request = await ReceiveAsync(tcpClient, encoding);

                        if (ReceivedSocketQueue.Count >= _ReceivedSocketQueueMaxSize) { string b = ""; ReceivedSocketQueue.TryDequeue(out b); }
                        LastReceiveTime = DateTime.Now;
                        ReceivedSocketQueue.Enqueue(LastReceiveTime.ToString("yyyy/MM/dd HH:mm:ss.fff") + "\t" + request);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());
            }

            return false;
        }

        public async Task<string> ReceiveAsync(TcpClient tcpClient, string encoding = "ASCII")
        {
            byte[] buffer = new byte[_bufferSize];
            string request = "";

            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    if (encoding == "ASCII")
                    {
                        do
                        {
                            int byteSize = await stream.ReadAsync(buffer, 0, buffer.Length);
                            request += Encoding.ASCII.GetString(buffer, 0, byteSize);
                        }
                        while (stream.DataAvailable);

                        //Responce code for client
                        var response = "received : " + request;
                        buffer = Encoding.ASCII.GetBytes(response);
                        await stream.WriteAsync(buffer, 0, buffer.Length);
                        Debug.WriteLine($"Response : {response}");
                    }
                    else //UTF8
                    {
                        do
                        {
                            int byteSize = await stream.ReadAsync(buffer, 0, buffer.Length);
                            request += Encoding.UTF8.GetString(buffer, 0, byteSize);
                        }
                        while (stream.DataAvailable);

                        //Responce code for client
                        var response = "received : " + request;
                        buffer = Encoding.UTF8.GetBytes(response);
                        await stream.WriteAsync(buffer, 0, buffer.Length);
                        Debug.WriteLine($"Response : {response}");

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());
            }

            return request;
        }

    }
}
