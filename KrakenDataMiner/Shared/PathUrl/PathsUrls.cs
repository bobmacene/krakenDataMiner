using System.Collections.Generic;

namespace Shared.PathUrl
{
    public enum CurrencyPair { BtcEur, EthEur, LtcEur }

    public interface IPathsUrls
    {
        string OhlcUrl { get; }
        string OhlcPath { get; }
         
    }
    public class LtcEurPathsUrls : IPathsUrls
    {
        public string OhlcUrl => "https://api.kraken.com/0/public/OHLC?pair=XLTCZEUR&amp;since=";
        public string OhlcPath => @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur";
    }

    public class EthEurPathsUrls : IPathsUrls
    {
        public string OhlcUrl => "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR&amp;since=";
        public string OhlcPath => @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur";
    }

    public class BtcEurPathsUrls : IPathsUrls
    {
        public string OhlcUrl => "https://api.kraken.com/0/public/OHLC?pair=XXBTZEUR&amp;since=";
        public string OhlcPath => @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur" ;
    }

    public class Addresses
    {
        public Dictionary<string, string> AddressData;
        public Addresses()
        {
            AddressData = new Dictionary<string, string>
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
                { "UrlBtcEur", "https://api.kraken.com/0/public/Trades?pair=XXBTZEUR&amp;since" },
                { "UrlEthEur", "https://api.kraken.com/0/public/Trades?pair=XETHZEUR&amp;since" },
                { "ServerTimeUrl", @"https://api.kraken.com/0/public/Time" }
            };
        }
    }
}