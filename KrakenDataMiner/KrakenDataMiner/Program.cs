using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace KrakenDataMiner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var log = new Logger();
            long since = 1493344795799390000;

            try
            {
                long apiCallTime;

                log.AddLogEvent("since.Trade.Number: ", since.ToString());

                var call = new ApiCall();

                var path = Path.Combine(ConfigurationManager.AppSettings["UrlEthEur"], since.ToString());
                log.AddLogEvent("Api Call Path:", path);

                var json = call.CallApi(path, out apiCallTime);

                log.AddLogEvent("ApiCall time Taken: ", apiCallTime.ToString());

                var dataAccess = new DataAccess();
                var data = dataAccess.Deserialise<TradeDataEthEur>(json);

                var savePath = Path.Combine(
                    ConfigurationManager.AppSettings["PathEthEur"], 
                    $"KrakenTrades_{DateTime.Now}.json"
                    );

                dataAccess.WriteTrades(data, ConfigurationManager.AppSettings["PathEthEur"]);
                log.AddLogEvent("Trades saved to: ", savePath);
                log.AddLogEvent($"Last trade number: ", data.Result.Last);
                log.AddLogEvent($"Last trade:");

                foreach(var v in data.Result.XETHZEUR.Last())
                {
                    log.AddLogEvent(v.ToString());
                }

                log.AddLogEvent("FINISHED");
            }
            catch(Exception ex)
            {
                log.AddLogEvent(ex.ToString());
            }
        }
    }
}
