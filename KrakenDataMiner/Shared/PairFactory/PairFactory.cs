using Shared;

namespace StopDataMiner.PairFactory
{
    public class PairFactory
    {
        public IPairPathsUrl GetPairData(CurrencyPair pair)
        {
            IPairPathsUrl pairData;
            switch(pair)
            {
                case CurrencyPair.BtcEur: return pairData = new BtcEurPairPathUrl();
                case CurrencyPair.EthEur: return pairData = new EthEurPairPathUrl();
                case CurrencyPair.LtcBtc: return pairData = new LtcBtcPairPathsUrl();
                case CurrencyPair.LtcEur: return pairData = new LtcEurPairPathUrl();
                default: return pairData = null;
            }
        }
    }
}
