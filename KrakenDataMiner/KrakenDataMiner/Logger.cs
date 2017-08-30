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
            Log = DateTime.Now + ":    KRAKEN.DATA.MINER STARTED.\n";
        }

        public void AddServerTimeToLog(ApiCall api)
        {
            long timeTaken;
            var serverTime = api.CallApi(ConfigurationManager.AppSettings["ServerTimeUrl"], out timeTaken);
            AddLogEvent("ApiCall executed");
            Log = $"{Log}\nSERVERTIME:\t\t{serverTime}\nAPICALL TIME TAKEN: ms{timeTaken}";
        }

        public void AddLogEvent(string label)
        {
            Log += $"\n{DateTime.Now}:    {label}";
        }

        public void AddLogEvent(string label, string dataToLog)
        {
            Log += $"\n{DateTime.Now}:    {label}\n{dataToLog}";
        }

        public void PersistLog()
        {
            File.AppendAllText(Log, ConfigurationManager.AppSettings["LogFilePath"]);
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
                $"{DateTime.UtcNow.ToString("yyyy.MM.dd_HH:mm:ss")}_KrakenLog.txt");
        }
    }

    public enum LogAction { Close }
}

