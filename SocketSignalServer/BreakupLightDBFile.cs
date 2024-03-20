using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentScheduler;

using LiteDB;

namespace SocketSignalServer
{
    public class BreakupLightDBFile
    {
        Action job;

        ConnectionString _LightDB_ConnectionString;

        public string TableName = "table_Message";

        public BreakupLightDBFile(string dbFilename, int backupIntervalMinute)
        {
            JobManager.Initialize();

            job = () =>
            {
                BreakupLightDBFile_byMonthFile(dbFilename, backupIntervalMinute);
            };

            JobManager.AddJob(job, s => s.ToRunEvery(1).Hours().At(0));

            _LightDB_ConnectionString = new ConnectionString();
            _LightDB_ConnectionString.Connection = ConnectionType.Shared;

        }


        public void BreakupLightDBFile_byMonthFile(string dbFilename, int backupIntervalMinute)
        {
            string backupDirTopPath = Path.Combine(Path.GetDirectoryName(dbFilename), "_backup");
            BreakupLightDB_byMonthFile(dbFilename, backupDirTopPath, backupIntervalMinute);

        }


        public void BreakupLightDB_byMonthFile(string dbFilename, string backupDirTopPath, int backupIntervalMinute)
        {
            _LightDB_ConnectionString.Filename = dbFilename;

            TimeSpan timeSpan = new TimeSpan(0, backupIntervalMinute, 0);

            using (LiteDatabase litedb = new LiteDatabase(_LightDB_ConnectionString))
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

                    var backupQueryList = resultQueryList
                        .Where(x => x.connectTime < fileTime1 && x.connectTime >= fileTime0)
                        ;

                    using (LiteDatabase litedbBackup = new LiteDatabase(backupFilePath))
                    {
                        var colbk = litedbBackup.GetCollection<SocketMessage>(TableName);
                        foreach (SocketMessage skm in backupQueryList)
                        {
                            colbk.Insert(skm.Key(), skm);
                            col.Delete(skm.Key());

                        }

                    }

                    fileTime0 = fileTime1;
                    fileTime1 = DateTime.Parse((fileTime1 + fileTimeSpan).ToString("yyyy/MM/01"));

                    if (fileTime1 > maxTime) fileTime1 = maxTime;

                } while (fileTime0 < maxTime);

            }

        }

    }
}