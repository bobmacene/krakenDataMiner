
using Shared.PathUrl;
using System;
using System.IO;

namespace Shared
{
    public class SharedData
    {
        private string _csvSavePath => $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenOHLC.csv";

        public string CsvLtcEurPath => Path.Combine(
           @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur", _csvSavePath);
        public string CsvBtcEurPath => Path.Combine(
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur", _csvSavePath);
        public string CsvEthEurPath => Path.Combine(
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur", _csvSavePath);


        public bool StopApp { get; set; } = false;
        public int Count { get; set; } = 0;

        private string _jsonFilename => $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenOHLC.json";

        public string JsonLtcEurPath => Path.Combine(
          @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur", _jsonFilename);
        public string JsonBtcEurPath => Path.Combine(
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur", _jsonFilename);
        public string JsonEthEurPath => Path.Combine(
            @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur", _jsonFilename);

        public string JsonLtcEurDirPath => @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\LtcEur";
        public string JsonBtcEurDirPath => @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\BtcEur";
        public string JsonEthEurDirPath => @"C:\Users\bob\Documents\KrakenDataMiner\OHLC\EthEur";

        public string UrlEtcEurOhlc => "https://api.kraken.com/0/public/OHLC?pair=XETHZEUR&amp;since=";
        public string UrlBtcEurOhlc => "https://api.kraken.com/0/public/OHLC?pair=XXBTZEUR&amp;since=";
        public string UrlLtcEurOhlc => "https://api.kraken.com/0/public/OHLC?pair=XLTCZEUR&amp;since=";

        public Logger Log = new Logger();
        public ApiCall Call = new ApiCall();
        public DataAccess Data = new DataAccess();
        public ExitApp Exit = new ExitApp();
    }
}
