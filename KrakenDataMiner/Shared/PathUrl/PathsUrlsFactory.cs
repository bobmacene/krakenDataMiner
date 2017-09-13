using Shared.PathUrl;

namespace Shared.PathsUrls
{
    public class PathsUrlsFactory
    {
        public IPathsUrls GetPathUrl(CurrencyPair pair)
        {
            switch (pair)
            {
                case CurrencyPair.BtcEur:
                    return new BtcEurPathsUrls();
                case CurrencyPair.EthEur:
                    return new EthEurPathsUrls();
                case CurrencyPair.LtcEur:
                    return new LtcEurPathsUrls();
                default:
                    return null;
            }
        }
       
    }
}
