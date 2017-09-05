using System;
using System.Collections.Generic;
using Shared;
using Shared.Models;
using System.IO;
using Newtonsoft.Json;

namespace KarakenDataMinerOHLC
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR";
            var api = new ApiCall();
            var json = api.CallApi(url);

            var data = new DataAccess();

            var trdData = data.Deserialise<EthEurTrades>(json);

            var ohlcStrings = new List<OhlcString>();

            foreach (var arr in trdData.Result.XETHZEUR)
            {
                ohlcStrings.Add(new OhlcString
                {
                    Time = arr[0],
                    Open = arr[1],
                    High = arr[2],
                    Low = arr[3],
                    Close = arr[4],
                    Vwap = arr[5],
                    Volume = arr[6],
                    Count = arr[7]
                });
            }

            var ohlcData = new List<Ohlc>();

            foreach (var ohlcString in ohlcStrings)
            {
                ohlcData.Add(new Ohlc(ohlcString, trdData.Result.Last));
            }

            var pathUrl = new PathsUrls();

            var filename = $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenOhlcData.json";
            var path = Path.Combine(pathUrl.Addresses["PathEthEurOhlc"], filename);



            data.Write(ohlcData, path);
            var ohlcs = data.Read<List<Ohlc>>(path);

           
            var path1 = @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur\2017.09.05_132156_KrakenOhlcData.json";
            //var ohlcs = data.Read<OhlcList>(path1);

            //var strArr = new List<string>();

            //foreach (var o in ohlcData)
            //{
            //    strArr.Add(o.ToString());
            //}

            //File.WriteAllLines(path, strArr);
        }

    }
}
