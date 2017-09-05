using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KrakenDataMiner;
using Shared.Models;

namespace Shared
{
    class ProcessTradeData
    {
        public void RunApiCallWriteTradeData(SharedData shared)
        {
            //var lastTrd = new LastTradeNumber();

            //var _since = lastTrd.GetLastTradeNumber(shared.PathUrl.Addresses["PathEthEur"])
            //    ?? "1504169508918596501";

            var _since = "1504169508918596501";
            var path = shared.PathUrl.Addresses["PathEthEurOhlc"];
            var _newUrl = string.Empty;

            var _url = _newUrl == string.Empty ?
                Path.Combine(shared.PathUrl.Addresses["UrlEtcEurOhlc"], _since) : _newUrl;

            var fileName = $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenOHLC.csv";
            shared.Log.AddLogEvent("Trades filename: ", fileName);

            var savePath = Path.Combine(path, fileName);
            shared.Log.AddLogEvent("TradeFile Path:", savePath);

            shared.Log.AddLogEvent("Last Trade Number: ", $"{_since}\n");
            shared.Log.AddLogEvent("Api Call Path:", _url);

            Action timerAction = () => GetApiTradeWriteToFile(
                shared, out _since, savePath, _url, out _newUrl);

            var count = 0;

            while (!shared.StopApp)
            {
                timerAction.Invoke();
                Task.Delay(60000).Wait();
                shared.Log.AddLogEvent($"Run {++count} Finished\n\n");
                shared.Log.PersistLog();
                shared.Log.Log = string.Empty;
            }
        }

        public void GetApiTradeWriteToFile(
            SharedData shared, out string since, string savePath, string url, out string newUrl)
        {
            var latestTrades = GetNewTrades(shared, url);

            var currentTrades = GetCurrentTrades(shared, savePath);

            if (currentTrades == null)
            {
                currentTrades = new List<Ohlc>();
                currentTrades.AddRange(latestTrades);

                shared.Data.WriteTrades(currentTrades, savePath);
                shared.Log.AddLogEvent("Trades Saved To: ", savePath);
            }
            else
            {
                currentTrades.AddRange(latestTrades);

                shared.Data.OverWriteExistingTrades(currentTrades, savePath);
                shared.Log.AddLogEvent("Trades Saved To: ", savePath);
            }

            since = latestTrades[0].Last.ToString();
            shared.Log.AddLogEvent($"Last Trade Number: ", since);

            shared.Log.AddLogEvent("Number of new trds added: ",
                latestTrades.Count.ToString());

            newUrl = Path.Combine(shared.PathUrl.Addresses["UrlEtcEurOhlc"], since);
            shared.Log.AddLogEvent($"NewUrl: ", newUrl);

            shared.Log.AddLogEvent($"First/Older Trade:", latestTrades.First().ToString());
            shared.Log.AddLogEvent($"Last/Recent Trade:", latestTrades.Last().ToString());
        }

        private static List<Ohlc> GetCurrentTrades(SharedData shared, string path)
        {
            return new FileInfo(path).Exists ?
                shared.Data.Deserialise<List<Ohlc>>(File.ReadAllText(path)) :
                null;

            #region altCode
            //var fileInfo = new FileInfo(path);

            //if (fileInfo.Exists)
            //{
            //    var json = File.ReadAllText(path);
            //    return shared.Data.Deserialise<ListEthEurTrades>(json);
            //}
            //else
            //{
            //    return null;
            //} 
            #endregion
        }

        private static List<Ohlc> GetNewTrades(SharedData shared, string url)
        {
            var json = shared.Call.CallApi(url);
            var newTrds = shared.Data.Deserialise<EthEurOhlc>(json);

            var ohlcs = new List<Ohlc>();
            var last = newTrds.Result.Last;

            foreach (var ohlc in newTrds.Result.XETHZEUR)
            {
                ohlcs.Add(new Ohlc(ohlc, last));
            }

            return ohlcs;
        }

    }
}

