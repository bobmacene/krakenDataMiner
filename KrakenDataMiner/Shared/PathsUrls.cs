using System.Collections.Generic;

namespace Shared
{
    public class PathsUrls
    {
        public Dictionary<string, string> Addresses;
        public PathsUrls()
        {
            Addresses = new Dictionary<string, string>
            {
                { "PathEthEurOhlc", @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur" },
                { "PathLtcEurOhlc", @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur" },
                { "PathBtcEurOhlc", @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur" },
                { "UrlEtcEurOhlc", "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR&amp;since" },
                { "UrlLtcEurOhlc", "https://api.kraken.com/0/public/OHLC?pair=XLTCZEUR&amp;since" },
                { "UrlBtcEurOhlc", "https://api.kraken.com/0/public/OHLC?pair=XXBTZEUR&amp;since" },
                { "PathEthEur", @"C:\Users\bob\Documents\KrakenDataMiner\Trades\EthEur" },
                { "PathBtcEur", @"C:\Users\bob\Documents\KrakenDataMiner\Trades\BtcEur" },
                { "LogFilePath", @"C:\Users\bob\Documents\KrakenDataMiner\Log"},
                { "UrlEthBtc", "https://api.kraken.com/0/public/Trades?pair=XETHXXBT&amp;since" },
                { "UrlBtcEur", @"https://api.kraken.com/0/public/Trades?pair=XXBTZEUR&amp;since" },
                { "UrlEthEur", "https://api.kraken.com/0/public/Trades?pair=XETHZEUR&amp;since" },
                { "ServerTimeUrl", @"https://api.kraken.com/0/public/Time" }
            };
        }
    }
}