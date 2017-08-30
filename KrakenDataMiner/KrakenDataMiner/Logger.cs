using System;
using System.Configuration;
using System.IO;

namespace KrakenDataMiner
{
    class Logger
    {
        private string Log;

        public Logger()
        {
            Log = DateTime.Now + ":    KRAKEN.DATA.MINER STARTED.\n\n";
        }

        public void AddServerTimeToLog(ApiCall api)
        {
            long timeTaken;
            var serverTime = api.CallApi(ConfigurationManager.AppSettings["ServerTimeUrl"], out timeTaken);
            var logLine = $"SERVERTIME: {serverTime}\n{DateTime.Now}:    APICALL TIME TAKEN: ms{timeTaken}";
            Log += $"\n{DateTime.Now}:    {logLine}\n";
        }

        public void AddLogEvent(string label)
        {
            Log += $"{DateTime.Now}:    {label}";
        }

        public void AddLogEvent(string label, string dataToLog)
        {
            Log += $"{DateTime.Now}:    {label} {dataToLog}\n";
        }

        public void PersistLog()
        {
            var path = BuildWritePath();   
            File.WriteAllText(path, Log);
        }

        public void PersistLog(LogAction close)
        {
            Log = $"{Log}\n{DateTime.Now}:  APP CLOSED";
            File.AppendAllText(Log, BuildWritePath());
        }

        private static string BuildWritePath()
        {
            return Path.Combine(
                ConfigurationManager.AppSettings["LogFilePath"],
                $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenDataMiner.txt");
        }
    }

    public enum LogAction { Close }
}

