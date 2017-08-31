using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace KrakenDataMiner
{
    public class LastTradeNumber
    {
        public string GetLastTradeNumber(string dirPath)
        {
            var dir = new DirectoryInfo(dirPath);
            var lastFile = dir.GetFiles().OrderByDescending(x => x.LastWriteTime).First();
            var json = File.ReadAllText(lastFile.FullName);

            var trades = JsonConvert.DeserializeObject<ListEthEurTrades>(json);

            var latestTrdNumber = trades.EthEurTradesList.Max(x=>x.Result.Last);
            return latestTrdNumber;
        }
    }
}
