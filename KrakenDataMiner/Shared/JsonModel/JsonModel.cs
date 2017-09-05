using Shared.Models;
using System.Collections.Generic;

namespace Shared
{
    enum Pair { XETHXXBT, XETHZEUR }

    public class JsonModel
    {
        public string[] Error { get; set; }
        public Result Result { get; set; }
    }

    public class Result
    {
        public dynamic Dynamic { get; set; }
        public string Last { get; set; }
    }

    public class ListEthEurTrades
    {
        public List<EthEurTrades> EthEurTradesList { get; set; }
    }


    public class EthEurTrades
    {
        public string[] Error { get; set; }
        public EthEur Result { get; set; }
    }

    public class EthEur
    {
        public List<string[]> XETHZEUR { get; set; }
        public string Last { get; set; }
    }


    public class TradeDataBtcEur
    {
        public string[] Error { get; set; }
        public BtcEur Result { get; set; }
    }

    public class BtcEur
    {
        public List<string[]> XXBTZEUR { get; set; }
        public string Last { get; set; }
    }

}
