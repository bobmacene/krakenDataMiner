using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KrakenMinerDataComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\bob\Documents\KrakenDataMiner\Trades\EthEur\2017.08.31_204332_KrakenTrades.json";
            var json = File.ReadAllText(path);
            var data = new DataAccess();
            var tradeArrays = data.Deserialise<ListEthEurTrades>(json);

            var allTrds = new List<Trade>();
            var trdsFrmData = new TradesFrmData();

            foreach (var trdSet in tradeArrays.EthEurTradesList)
            {
                var lastTrdId = trdSet.Result.Last;

                var trds = trdsFrmData.GetTrades(trdSet.Result.XETHZEUR, lastTrdId);

                allTrds.AddRange(trds);
            }

            var dupeTrds = new List<Trade>();
            var removedTrds = new List<Trade>(allTrds);

            foreach(var trd in allTrds)
            {
                removedTrds.Remove(trd);

                if (removedTrds.Contains(trd)) dupeTrds.Add(trd);
            }

            Console.WriteLine($"dupeTrds Count: {dupeTrds.Count()}");
            
            Console.ReadLine();
        }
    }
}


/*   NO DUPES WERE FOUND
 *   
  var allTrds = tradeArrays.EthEurTradesList.SelectMany(x => x.Result.XETHZEUR);

            var allTrdsNoDupes = new HashSet<string[]>(allTrds);  //no dupes found

            Console.WriteLine($"AllTrds Count: {allTrds.Count()}\n" +
                $"allTrdsNoDupes Count: {allTrdsNoDupes.Count}\n" +
                $"Dupes#: {allTrds.Count() - allTrdsNoDupes.Count}");
     */
