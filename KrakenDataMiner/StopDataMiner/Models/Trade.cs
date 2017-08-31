
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
        public long LastTradeId { get; set; }

    }
