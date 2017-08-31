
using System;

namespace Shared.Models
{
    public class Trade
    {
        public string Pair { get; set; }
        public decimal Price { get; set; }
        public decimal Size { get; set; }
        public string Direction { get; set; }
        public string Type { get; set; }
        public double TradeTime { get; set; }
        public string Miscellaneous { get; set; }
        public string LastTradeId { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ Convert.ToInt32(Direction);
                result = (result * 397) ^ Convert.ToInt32(Price);
                result = (result * 397) ^ Convert.ToInt32(Size);
                result = (result * 397) ^ Convert.ToInt32(TradeTime);
                return result;
            }
        }
    }
}
