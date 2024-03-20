using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Diagnostics;

using FluentScheduler;
using LiteDB;


namespace SocketSignalServer
{
    public class FluentSchedulerRegistry_FromScheduleLines : Registry
    {
        public List<string> ScheduleList;
        public string DataBaseFilePath;

        private ConnectionString _LiteDBconnectionString;
        private NoticeTransmitter noticeTransmitter;

        List<ClientData> clientList;
        List<FluentSchedulerJob_SchedulerLineRun> jobList;

        private int Name_idx = 0;
        private int Unit_idx = 1;
        private int At_idx = 2;

        public FluentSchedulerRegistry_FromScheduleLines(string DataBaseFilePath, NoticeTransmitter noticeTransmitter, string[] Lines, List<ClientData> clientList)
        {
            _LiteDBconnectionString = new ConnectionString();
            _LiteDBconnectionString.Connection = ConnectionType.Shared;
            _LiteDBconnectionString.Filename = DataBaseFilePath;

            jobList = new List<FluentSchedulerJob_SchedulerLineRun>();

            this.noticeTransmitter = noticeTransmitter;
            this.clientList = clientList;

            ScheduleList = new List<string>();

            foreach (string Line in Lines)
            {
                string[] cols = Line.Split('\t');

                string targetStatusName = cols[Name_idx];
                string IntervalUnitString = cols[Unit_idx];
                string IntervalParam = cols[At_idx];


                if (IntervalUnitString == "EveryDays")
                {
                    try
                    {
                        string[] atinfo = IntervalParam.Split(',');
                        foreach (string t in atinfo)
                        {
                            int[] hm = Array.ConvertAll(t.Split(':'), s => int.Parse(s));
                            int h = hm[0];
                            int m = hm[1];

                            var job = new FluentSchedulerJob_SchedulerLineRun(_LiteDBconnectionString, noticeTransmitter, targetStatusName, new TimeSpan(24, 0, 0), clientList);
                            Schedule(job.Execute()).WithName(targetStatusName).ToRunEvery(1).Days().At(h, m);
                            ScheduleList.Add("EveryDays at " + t);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());

                        ScheduleList.Add("ERROR: " + Line);
                    }
                }
                else if (IntervalUnitString == "EveryHours")
                {
                    try
                    {
                        int[] atinfo = Array.ConvertAll(IntervalParam.Split(','), s => int.Parse(s));
                        foreach (int m in atinfo)
                        {
                            var job = new FluentSchedulerJob_SchedulerLineRun(_LiteDBconnectionString, noticeTransmitter, targetStatusName, new TimeSpan(1, 0, 0), clientList);
                            Schedule(job.Execute()).WithName(targetStatusName).ToRunEvery(1).Hours().At(m);
                            ScheduleList.Add("EveryHours at " + m.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());

                        ScheduleList.Add("ERROR: " + Line);
                    }

                }
                else if (IntervalUnitString == "EverySeconds")
                {
                    try
                    {
                        int[] atinfo = Array.ConvertAll(IntervalParam.Split(','), s => int.Parse(s));
                        foreach (int s in atinfo)
                        {
                            var job = new FluentSchedulerJob_SchedulerLineRun(_LiteDBconnectionString, noticeTransmitter, targetStatusName, new TimeSpan(0, 0, s), clientList);
                            Schedule(job.Execute()).WithName(targetStatusName).ToRunEvery(s).Seconds();
                            ScheduleList.Add("EverySeconds at " + s.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());

                        ScheduleList.Add("ERROR: " + Line);
                    }
                }
            }
        }
    }

    public class FluentSchedulerJob_SchedulerLineRun
    {
        public string targetStatusName;
        public bool CheckNeed;
        public DateTime LastRunTime;

        private ConnectionString _LiteDBconnectionString;
        private NoticeTransmitter noticeTransmitter;
        private List<ClientData> clientList;


        public FluentSchedulerJob_SchedulerLineRun(ConnectionString _LiteDBconnectionString, NoticeTransmitter noticeTransmitter, string targetStatusName, TimeSpan jobInterval, List<ClientData> clientList)
        {
            this._LiteDBconnectionString = _LiteDBconnectionString;
            this.noticeTransmitter = noticeTransmitter;
            this.targetStatusName = targetStatusName;

            this.LastRunTime = DateTime.Now - jobInterval;
            this.clientList = clientList;

        }

        public Action Execute()
        {
            return new Action(delegate ()
            {
                int _retryMax = 10;

                //Retry DataBaseOpen
                for (int retryCount = 0; retryCount < _retryMax; retryCount++)
                {
                    try
                    {
                        using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                        {
                            ILiteCollection<SocketMessage> col = litedb.GetCollection<SocketMessage>("table_Message");

                            foreach (var targetClient in clientList)
                            {

                                //get Latest unchecked message 
                                var latestTargetClientRecord_haveTargetStatusName =

                                    col.Query().Where(x => x.clientName == targetClient.clientName && x.status == targetStatusName && !x.check)
                                               .OrderByDescending(x => x.connectTime)
                                               .FirstOrDefault();

                                if (latestTargetClientRecord_haveTargetStatusName != null)
                                {
                                    noticeTransmitter.AddNotice(targetClient, latestTargetClientRecord_haveTargetStatusName);
                                }


                                //style==Once Message check update
                                var records = col.Query().Where(x => x.clientName == targetClient.clientName && x.status == targetStatusName && !x.check && x.checkStyle=="Once").ToList();

                                foreach (var record in records)
                                {
                                    record.check = true;
                                    col.Update(record.Key(), record);
                                }

                            }

                        }

                        LastRunTime = DateTime.Now;
                        break;

                    }
                    catch (Exception ex)
                    {
                        Debug.Write(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " retry:" + retryCount.ToString());
                        Debug.WriteLine(ex.ToString());
                        Thread.Sleep(100);

                        if (retryCount == _retryMax - 1)
                        {
                            throw;
                        }
                    }
                }

            });

        }

    }

}
