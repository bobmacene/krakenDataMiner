using Newtonsoft.Json;
using Shared;
using Shared.Models;
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

            if(dir.GetFiles().Any(x=>x.Extension == ".json"))
            {
                lastFile = dir.GetFiles().OrderBy(x => x.LastWriteTime).Last();

                trades = lastFile.Exists
                    ? JsonConvert.DeserializeObject<List<Ohlc>>(
                        File.ReadAllText(lastFile.FullName))
                    : null;
            }

            return trades == null || trades.Count() == 0 ? 0 : trades.Last().UnixTime;
        }
    }
}
