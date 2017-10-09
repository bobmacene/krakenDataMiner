﻿using System;
using System.Configuration;
using System.IO;

namespace Shared
{
    public class Logger
    {
        public string Log;
        public string LogPath;
        public string Filename;

        public Logger()
        {
            Log = DateTime.Now + ":    KRAKEN.DATA.MINER STARTED.\n\n";
            Filename = $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenDataMiner.txt";
            LogPath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], Filename);
        }

        public void AddServerTimeToLog(ApiCall api, out string serverTime)
        {
            long timeTaken;
            serverTime = api.CallApi(ConfigurationManager.AppSettings["ServerTimeUrl"], out timeTaken);
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
            File.AppendAllText(LogPath, Log);
        }
    }

}

