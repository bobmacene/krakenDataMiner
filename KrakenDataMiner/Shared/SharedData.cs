namespace Shared
{
    public enum CurrencyPair { EthEur, BtcEur, LtcEur, LtcBtc, InvalidPair }
    public class SharedData
    {
        public bool StopApp { get; set; } = false;
        public int Count { get; set; } = 0;

        public Logger Log = new Logger();
        public ApiCall Call = new ApiCall();
        public DataAccess Data = new DataAccess();
    }
}
