using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KrakenDataMiner;

namespace Shared
{
    class ProcessTradeData
    {
        public void RunApiCallWriteTradeData(SharedData shared)
        {
            var lastTrd = new LastTradeNumber();

            var _since = lastTrd.GetLastTradeNumber(
                ConfigurationManager.AppSettings["PathEthEur"]) ?? "1504169508918596501";

            var _newUrl = string.Empty;
            var _url = _newUrl == string.Empty ?
                Path.Combine(ConfigurationManager.AppSettings["UrlEthEur"], _since) :
                _newUrl;

            var fileName = $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenTrades.json";
            shared.Log.AddLogEvent("Trades filename: ", fileName);

            var savePath = Path.Combine(ConfigurationManager.AppSettings["PathEthEur"], fileName);
            shared.Log.AddLogEvent("TradeFile Path:", savePath);

            shared.Log.AddLogEvent("Last Trade Number: ", $"{_since}\n");
            shared.Log.AddLogEvent("Api Call Path:", _url);

            Action timerAction = () => GetApiTradeWriteToFile(
                shared, out _since, savePath, _url, out _newUrl);

            var count = 0;

            while (!shared.StopApp)
            {
                timerAction.Invoke();
                Task.Delay(5000).Wait();
                shared.Log.AddLogEvent($"Run {++count} Finished\n\n");
                shared.Log.PersistLog();
                shared.Log.Log = string.Empty;
            }
        }

        public void GetApiTradeWriteToFile(
            SharedData shared, out string since, string savePath,  string url, out string newUrl)
        {
            var latestTrades = GetNewTrades(shared, url);

            var currentTrades = GetCurrentTrades(shared, savePath);

            if (currentTrades == null)
            {
                currentTrades = new ListEthEurTrades();
                currentTrades.EthEurTradesList = new List<EthEurTrades>();
                currentTrades.EthEurTradesList.Add(latestTrades);

                shared.Data.WriteTrades(currentTrades, savePath);
                shared.Log.AddLogEvent("Trades Saved To: ", savePath);
            }
            else
            {
                currentTrades.EthEurTradesList.Add(latestTrades);

                shared.Data.OverWriteExistingTrades(currentTrades, savePath);
                shared.Log.AddLogEvent("Trades Saved To: ", savePath);
            }
            since = latestTrades.Result.Last;
            shared.Log.AddLogEvent($"Last Trade Number: ", latestTrades.Result.Last);

            shared.Log.AddLogEvent("Number of new trds added: ", 
                latestTrades.Result.XETHZEUR.Count.ToString());       

            newUrl = Path.Combine(ConfigurationManager.AppSettings["UrlEthEur"], since);
            shared.Log.AddLogEvent($"NewUrl: ", newUrl);

            AddOldestAndMostRecentTradesToLog(shared, latestTrades);
        }

        private static ListEthEurTrades GetCurrentTrades(SharedData shared, string path)
        {
            var fileInfo = new FileInfo(path);
            if(fileInfo.Exists)
            {
                var json = File.ReadAllText(path);
                return shared.Data.Deserialise<ListEthEurTrades>(json);
            }
            else
            {
                return null;
            }
        }

        private static EthEurTrades GetNewTrades(SharedData shared, string url)
        {
            var json = shared.Call.CallApi(url);
            return shared.Data.Deserialise<EthEurTrades>(json);
        }

        private static void AddOldestAndMostRecentTradesToLog(SharedData shared, EthEurTrades latestTrades)
        {
            shared.Log.AddLogEvent($"First/Older Trade:");
            var first = string.Empty;
            foreach (var v in latestTrades.Result.XETHZEUR.First())
            {
                first += v.ToString() + "; ";
            }
            first += "\n";
            shared.Log.AddLogEvent(first.ToString());

            shared.Log.AddLogEvent($"Last/Recent Trade:");
            var last = string.Empty;
            foreach (var v in latestTrades.Result.XETHZEUR.Last())
            {
                last += v.ToString() + "; ";
            }
            last += "\n";
            shared.Log.AddLogEvent(last.ToString());
        }
    }
}

