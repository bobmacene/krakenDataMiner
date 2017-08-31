using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakenMinerDataComparison
{
    class TradesFrmData
    {
        public IEnumerable<Trade> GetTrades(IEnumerable<string[]> trdData, string lastTrdID)
        {
            var trades = new List<Trade>();

            foreach (var trd in trdData)
            {
                trades.Add(new Trade
                {
                    Pair = "ZETHXEUR",
                    Price = Convert.ToDecimal(trd[0]),
                    Size = Convert.ToDecimal(trd[1]),
                    TradeTime = Convert.ToDouble(trd[2]),
                    Direction = trd[3] == "b" ? "BUY" : "SELL",
                    Type = trd[4] == "l" ? "LIMIT" : "MARKET",
                    LastTradeId = lastTrdID,
                    Miscellaneous = trd[5]
                });
            }
            return trades;
        }
    }
}
