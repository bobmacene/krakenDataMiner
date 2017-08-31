using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakenMinerDataComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\bob\Documents\KrakenDataMiner\Trades\EthEur\2017.08.31_204332_KrakenTrades.json";
            var json = File.ReadAllText(path);
            var data = new DataAccess();
            var trades = data.Deserialise<ListEthEurTrades>(json);

            var allTrds = trades.EthEurTradesList.SelectMany(x => x.Result.XETHZEUR);

            var allTrdsNoDupes = new HashSet<string[]>(allTrds);

            Console.WriteLine($"AllTrds Count: {allTrds.Count()}\n" +
                $"allTrdsNoDupes Count: {allTrdsNoDupes.Count}\n" +
                $"Dupes#: {allTrds.Count() - allTrdsNoDupes.Count}");

            Console.ReadLine();
        }
    }
}
