using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentScheduler;
using LiteDB;

namespace SocketSignalServer
{
    public class BreakupLightDBFile
    {
        Action job;
        ConnectionString _LiteDBconnectionString;
        public string TableName = "table_Message";

        /// <summary>
        /// database open retry period in second.
        /// </summary>
        public double retryPeriod { get { return (_retryCountMax * _retryWait) / 1000.0; } set { _retryCountMax = (int)((value * 1000.0) / _retryWait); } }
        private int _retryCountMax = 60;

        /// <summary>
        /// retry wait in millisecond.
        /// </summary>
        public int retryWait { get { return _retryWait; } set { double retryPeriodBuff = retryPeriod; _retryWait = value; retryPeriod = retryPeriodBuff; } }
        private int _retryWait = 500;

        private Random random;

        public BreakupLightDBFile(string dbFilename, int backupIntervalMinute)
        {
            _LiteDBconnectionString = new ConnectionString();
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            JobManager.Initialize();
            job = () =>
            {
                BreakupLightDBFile_byMonthFile(dbFilename, backupIntervalMinute);
            };
            JobManager.AddJob(job, s => s.WithName("BreakupLightDBFile").ToRunEvery(1).Hours().At(0));
        }

        public void BreakupLightDBFile_byMonthFile(string dbFilename, int backupIntervalMinute)
        {
            string backupDirTopPath = Path.Combine(Path.GetDirectoryName(dbFilename), "_backup");
            BreakupLightDB_byMonthFile(dbFilename, backupDirTopPath, backupIntervalMinute);
        }

        public void BreakupLightDB_byMonthFile(string dbFilename, string backupDirTopPath, int backupIntervalMinute)
        {
            if (!File.Exists(dbFilename)) return;
            _LiteDBconnectionString.Filename = dbFilename;
            _LiteDBconnectionString.Connection = ConnectionType.Shared;

            TimeSpan timeSpan = new TimeSpan(0, backupIntervalMinute, 0);

            for (int retryCount = 0; retryCount < _retryCountMax; retryCount++)
            {
                try
                {
                    using (LiteDatabase litedb = new LiteDatabase(_LiteDBconnectionString))
                    {
                        var col = litedb.GetCollection<SocketMessage>(TableName);

                        var result = col.Query()
                            .Where(x => x.connectTime < (DateTime)(DateTime.Now - timeSpan))
                            .OrderBy(x => x.connectTime)
                            ;

                        List<SocketMessage> resultQueryList = result.ToList();

                        DateTime minTime = result.First().connectTime;
                        DateTime maxTime = result.Offset(result.Count() - 1).First().connectTime;

                        TimeSpan fileTimeSpan = new TimeSpan(31, 0, 0, 0);
                        DateTime fileTime0 = DateTime.Parse(minTime.ToString("yyyy/MM/01"));
                        DateTime fileTime1 = DateTime.Parse((fileTime0 + fileTimeSpan).ToString("yyyy/MM/01"));

                        do
                        {
                            string backupFilePath = Path.Combine(backupDirTopPath, fileTime0.ToString("yyyy"), fileTime0.ToString("yyyyMM")) + ".db";
                            string backupFileDir = Path.GetDirectoryName(backupFilePath);

                            if (!Directory.Exists(backupFileDir)) Directory.CreateDirectory(backupFileDir);
                            var backupQueryList = resultQueryList.Where(x => x.connectTime < fileTime1 && x.connectTime >= fileTime0);
                            try
                            {
                                using (LiteDatabase litedbBackup = new LiteDatabase(backupFilePath))
                                {
                                    var colbk = litedbBackup.GetCollection<SocketMessage>(TableName);
                                    foreach (SocketMessage skm in backupQueryList)
                                    {
                                        colbk.Insert(skm.Key(), skm);
                                        col.Delete(skm.Key());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + ex.ToString());
                            }

                            fileTime0 = fileTime1;
                            fileTime1 = DateTime.Parse((fileTime1 + fileTimeSpan).ToString("yyyy/MM/01"));

                            if (fileTime1 > maxTime) fileTime1 = maxTime;

                        } while (fileTime0 < maxTime);

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
                    Thread.Sleep((int)(_retryWait * (random.NextDouble() + 0.5)));
                }
            }
        }
    }
}