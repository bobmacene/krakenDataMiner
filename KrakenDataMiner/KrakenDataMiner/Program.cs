﻿using System;
using Shared;
using System.Threading.Tasks;

namespace KrakenDataMiner
{
    public class Program
    {
        static void Main(string[] args)
        { 
            var tradeData = new ProcessTradeData();

            var _stop = false;                                  //close app
            var shared = new SharedData();

            try
            {
                shared.Log.AddServerTimeToLog(shared.Call);

                var exitAsyncTask = Task.Run(() => 
                {
                    shared.Exit.ExitAppProcess(shared, out _stop);

                    if (_stop)
                    {
                        shared.Log.AddLogEvent("\t\t\tAPP MANUALLY EXITED\n\n");
                        shared.Log.AddServerTimeToLog(shared.Call);
                        shared.Log.AddLogEvent("FINISHED");
                        shared.Log.PersistLog();
                        Environment.Exit(-1);
                    }
                });
                shared.Log.AddLogEvent("Exit Task Started\n\n");

                var processTask = new Task(() => 
                {
                    tradeData.RunApiCallWriteTradeData(shared);
                });
                shared.Log.AddLogEvent("Process Task Started\n\n");
                processTask.RunSynchronously();
            }
            catch (Exception ex)
            {
                shared.Log.AddLogEvent(ex.ToString());
                shared.Log.PersistLog();
            }
        }

    }
}
