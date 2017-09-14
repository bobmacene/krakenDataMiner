﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Shared.Models;
using KrakenDataMiner;

namespace Shared
{
    public enum CurrencyPair { EthEur, BtcEur, LtcEur }

    internal class ProcessTradeData
    {
        public void CallApi(SharedData shared, CurrencyPair pair)
        {
            var lastTrd = new LastTradeNumber();
            var _newUrl = string.Empty;

            var _jsonPath = pair == CurrencyPair.EthEur ? shared.EthOhlcPathJson :
                pair == CurrencyPair.BtcEur ? shared.BtcOhlcPathJson :
                shared.LtcOhlcPathJson;
            shared.Log.AddLogEvent("JsonTradeFile Path:", _jsonPath);

            var _csvPath = pair == CurrencyPair.EthEur ? shared.EthOhlcPathCsv:
               pair == CurrencyPair.BtcEur ? shared.BtcOhlcPathCsv :
               shared.LtcOhlcPathCsv;
            shared.Log.AddLogEvent("CsvTradeFile Path:", _csvPath);

            Func<string> newUrlAction = () =>  
            pair == CurrencyPair.EthEur ? shared.EthOhlcUrl :
            pair == CurrencyPair.BtcEur ? shared.BtcOhlcUrl : 
            shared.LtcOhlcUrl;

            var _url = _newUrl == string.Empty ? newUrlAction.Invoke() : _newUrl;
            shared.Log.AddLogEvent("Api Call Path:", _url);

            var _dirPath = Path.GetDirectoryName(_jsonPath);

            long _since = lastTrd.GetLastTradeNumber(_dirPath);

            if (_since == 0) _since = 1502194310;                       //"1504169508918596501";
            shared.Log.AddLogEvent("Last Trade Number: ", $"{_since}\n");
            
            GetTrades(shared, pair, _url, _jsonPath, _csvPath, out _since, out _newUrl);

            if (pair == CurrencyPair.BtcEur) shared.BtcOhlcUrl = _newUrl;
            if (pair == CurrencyPair.EthEur) shared.EthOhlcUrl = _newUrl;
            if (pair == CurrencyPair.LtcEur) shared.LtcOhlcUrl = _newUrl;

            shared.Log.AddLogEvent($"Run {++shared.Count} Finished\n\n");
            shared.Log.PersistLog();
            shared.Log.Log = string.Empty;
        }

        public void GetTrades(SharedData shared, CurrencyPair pair, string url, 
            string jsonPath, string csvPath, out long since, out string newUrl)
        {
            var latestTrades = GetNewTrades(shared, url, pair);

            var currentTrades = GetCurrentTrades(shared, jsonPath);

            if (currentTrades == null)
            {
                File.AppendAllLines(csvPath, latestTrades.ConvertAll(x => x.ToString()));
                shared.Data.WriteTrades(latestTrades, jsonPath);
                shared.Log.AddLogEvent("Trades Saved To: ", jsonPath);
            }
            else
            {
                var lastTrdTime = currentTrades.Last().UnixTime;

                var newTrds = latestTrades
                    .Where(x => x.UnixTime > lastTrdTime).OrderBy(x=>x.UnixTime);

                if (newTrds.Any())
                {
                    currentTrades.AddRange(newTrds);
                    shared.Data.OverWriteExistingTrades(currentTrades, jsonPath);
                    File.AppendAllLines(csvPath, newTrds.ToList().ConvertAll(x => x.ToString()));
                    shared.Log.AddLogEvent("Trades Saved To: ", jsonPath);
                }
            }

            since = latestTrades.Last().Last;
            shared.Log.AddLogEvent($"Last Trade Number: ", since.ToString());

            shared.Log.AddLogEvent("Number of new trds added: ",
                latestTrades.Count.ToString());

            newUrl = Path.Combine(url, since.ToString());
            shared.Log.AddLogEvent($"NewUrl: ", newUrl);

            shared.Log.AddLogEvent($"First/Older Trade:", latestTrades.First().ToString());
            shared.Log.AddLogEvent($"Last/Recent Trade:", latestTrades.Last().ToString());
        }

        private static List<Ohlc> GetCurrentTrades(SharedData shared, string path)
        {
            return new FileInfo(path).Exists ?
                shared.Data.Deserialise<List<Ohlc>>(File.ReadAllText(path)) :
                null;
        }

        private static List<Ohlc> GetNewTrades(SharedData shared, string url, CurrencyPair pair)
        {
            var json = shared.Call.CallApi(url);
            return ProcessJsonModel(shared, json, pair);
        }

        private static List<Ohlc> ProcessJsonModel(SharedData shared, string json, CurrencyPair pair)
        {
            var ohlcs = new List<Ohlc>();
            string last;

            switch(pair)
            { 
                case CurrencyPair.EthEur:
                var newEthEurTrds = shared.Data.Deserialise<EthEurOhlc>(json);
                last = newEthEurTrds.Result.Last;

                foreach (var ohlc in newEthEurTrds.Result.XETHZEUR)
                {
                    ohlcs.Add(new Ohlc(ohlc, last));
                }
                    return ohlcs;

                case CurrencyPair.BtcEur:
                     var newBtcEurTrds = shared.Data.Deserialise<BtcEurOhlc>(json);
                    last = newBtcEurTrds.Result.Last;

                foreach (var ohlc in newBtcEurTrds.Result.XXBTZEUR)
                {
                    ohlcs.Add(new Ohlc(ohlc, last));
                }
                    return ohlcs;

                case CurrencyPair.LtcEur:
                var newLtcEurTrds = shared.Data.Deserialise<LtcEurOhlc>(json);
                last = newLtcEurTrds.Result.Last;

                foreach (var ohlc in newLtcEurTrds.Result.XLTCZEUR)
                {
                    ohlcs.Add(new Ohlc(ohlc, last));
                }
                    return ohlcs;
                default:
                    return null;
            }

        }
    }
}

