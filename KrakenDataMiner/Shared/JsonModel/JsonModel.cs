using Shared.Models;
using System.Collections.Generic;

namespace Shared
{
       public class ListEthEurTrades
    {
        public List<EthEurOhlc> EthEurTradesList { get; set; }
    }


    public class EthEurOhlc
    {
        public string[] Error { get; set; }
        public EthEurData Result { get; set; }
    }

    public class EthEurData
    {
        public List<string[]> XETHZEUR { get; set; }
        public string Last { get; set; }
    }


    public class LtcEurOhlc
    {
        public string[] Error { get; set; }
        public LtcEurData Result { get; set; }
    }

    public class LtcEurData
    {
        public List<string[]> XLTCZEUR { get; set; }
        public string Last { get; set; }
    }

    public class BtcEurOhlc
    {
        public string[] Error { get; set; }
        public BtcEurData Result { get; set; }
    }

    public class BtcEurData
    {
        public List<string[]> XXBTZEUR { get; set; }
        public string Last { get; set; }
    }

}
