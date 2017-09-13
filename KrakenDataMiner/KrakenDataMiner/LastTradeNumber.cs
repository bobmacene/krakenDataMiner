using Newtonsoft.Json;
using Shared;
using Shared.Models;
using Shared.PathsUrls;
using Shared.PathUrl;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KrakenDataMiner
{
    public class LastTradeNumber
    {
        public long GetLastTradeNumber(string dirPath)
        {
            var dir = new DirectoryInfo(dirPath);

            FileInfo lastFile = null;
            List<Ohlc> trades = null;

            if(dir.GetFiles().Any())
            {
                lastFile = dir.GetFiles()
                .Where(x => x.Extension.EndsWith(".json"))
                .OrderBy(x => x.LastWriteTime).Last();

                trades = JsonConvert.DeserializeObject<List<Ohlc>>(
                    File.ReadAllText(lastFile.FullName));
            }

            return trades == null || trades.Count() == 0 ? 0 : trades.Last().UnixTime;
        }
    }
}
