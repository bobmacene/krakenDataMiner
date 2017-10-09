
//Pairs available on Kraken listed here: https://api.kraken.com/0/public/AssetPairs

namespace StopDataMiner.PairFactory
{
    public interface IPairPathsUrl
    {
        string JsonPath { get; }
        string CsvPath { get; }
        string OhlcUrl { get; }
    }
    public class LtcBtcPairPathsUrl : IPairPathsUrl
    {
        public string OhlcUrl { get; set; } =
          "https://api.kraken.com/0/public/OHLC?pair=XLTCXXBT&amp;since=";
        public string CsvPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcBtc\Csv\KrakenOHLC_LtcBtc.csv";
        public string JsonPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcBtc\Json\KrakenOHLC_LtcBtc.json";
    }

    public class LtcEurPairPathUrl : IPairPathsUrl
    {
        public string OhlcUrl { get; set; } =
            "https://api.kraken.com/0/public/OHLC?pair=XLTCZEUR&amp;since=";
        public string CsvPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur\Csv\KrakenOHLC_LtcEur.csv";
        public string JsonPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur\Json\KrakenOHLC_LtcEur.json";
    }

    public class BtcEurPairPathUrl : IPairPathsUrl
    {
        public string OhlcUrl { get; set; } =
           "https://api.kraken.com/0/public/OHLC?pair=XXBTZEUR&amp;since=";
        public string CsvPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur\Csv\KrakenOHLC_BtcEur.csv";
        public string JsonPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur\Json\KrakenOHLC_BtcEur.json";
    }

    public class EthEurPairPathUrl : IPairPathsUrl
    {
        public string OhlcUrl { get; set; } =
            "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR&amp;since=";
        public string CsvPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur\Csv\KrakenOHLC_EthEur.csv";
        public string JsonPath =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur\Json\KrakenOHLC_EthEur.json";

    }
}
