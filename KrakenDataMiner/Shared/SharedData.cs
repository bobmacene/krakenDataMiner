namespace Shared
{
    public class SharedData
    {
        public bool StopApp { get; set; } = false;
        public int Count { get; set; } = 0;

        public string LtcOhlcUrl { get; set; } = 
            "https://api.kraken.com/0/public/OHLC?pair=XLTCZEUR&amp;since=";
        public string LtcOhlcPathCsv =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur\Csv\KrakenOHLC_LtcEur.csv";
        public string LtcOhlcPathJson =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur\Json\KrakenOHLC_LtcEur.json";

        public string EthOhlcUrl { get; set; } =
            "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR&amp;since=";
        public string EthOhlcPathCsv =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur\Csv\KrakenOHLC_EthEur.csv";
        public string EthOhlcPathJson =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur\Json\KrakenOHLC_EthEur.json";

        public string BtcOhlcUrl { get; set; } = 
            "https://api.kraken.com/0/public/OHLC?pair=XXBTZEUR&amp;since=";
        public string BtcOhlcPathCsv =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur\Csv\KrakenOHLC_BtcEur.csv";
        public string BtcOhlcPathJson =>
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur\Json\KrakenOHLC_BtcEur.json";

        //public string UrlEtcEurOhlc => "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR&amp;since=";
        //public string UrlBtcEurOhlc => "https://api.kraken.com/0/public/OHLC?pair=XXBTZEUR&amp;since=";
        //public string UrlLtcEurOhlc => "https://api.kraken.com/0/public/OHLC?pair=XLTCZEUR&amp;since=";

        public Logger Log = new Logger();
        public ApiCall Call = new ApiCall();
        public DataAccess Data = new DataAccess();
        public ExitApp Exit = new ExitApp();
    }
}
