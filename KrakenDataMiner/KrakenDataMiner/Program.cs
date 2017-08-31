using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace KrakenDataMiner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var log = new Logger();
            var call = new ApiCall();
            log.AddServerTimeToLog(call);

            string _since = "0";
            string _newUrl = string.Empty;
            string _url = _newUrl == string.Empty ?
                Path.Combine(ConfigurationManager.AppSettings["UrlEthEur"], _since) :
                _newUrl;

            Console.WriteLine("Enter x to stop program: ");

            //Action exitAction = () => ExitApp(log, call);
            //var exitThread = new Thread(new ThreadStart(exitAction));
            //exitThread.Start();

            try
            {
                var fileName = $"{DateTime.Now.ToString("yyyy.MM.dd_HHmmss")}_KrakenTrades.json";
                log.AddLogEvent("Trades filename: ", fileName);

                var savePath = Path.Combine(ConfigurationManager.AppSettings["PathEthEur"], fileName);
                log.AddLogEvent("TradeFile Path:", savePath);

                long apiCallTime;

                var dataAccess = new DataAccess();

                log.AddLogEvent("Last Trade Number: ", $"{_since}\n");
                log.AddLogEvent("Api Call Path:", _url);

                Action timerAction = () => GetApiTradeWriteToFile(
                        log, out _since, savePath, out apiCallTime,
                        call, dataAccess, _url, out _newUrl);

                var count = 0;
                var stop = false;

                while (!stop)
                {
                    timerAction.Invoke();
                    Thread.Sleep(5 * 60 * 1000);
                    log.AddLogEvent($"Run {++count} Finished\n\n");
                    log.PersistLog();
                    log.Log = string.Empty;
                }

                log.AddServerTimeToLog(call);
                log.AddLogEvent("FINISHED");
                log.PersistLog();
            }
            catch (Exception ex)
            {
                log.AddLogEvent(ex.ToString());
                log.PersistLog();
            }
        }

        private static void ExitApp(Logger log, ApiCall call)
        {
            var stop = false;

            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey();

                    if (cki.Key == ConsoleKey.X)
                    {
                        log.AddLogEvent("\t\tAPP MANUALY EXITED\n\n");
                        Console.WriteLine("App has been EXITED");
                        stop = true;
                    }

                    log.AddServerTimeToLog(call);
                    log.AddLogEvent("FINISHED");
                    log.PersistLog();
                    Environment.Exit(-1);
                }
            } while (stop);
        }

        private static void GetApiTradeWriteToFile(
            Logger log, out string since, string savePath,
            out long apiCallTime, ApiCall call, DataAccess dataAccess,
            string url, out string newUrl)
        {
            var json = call.CallApi(url, out apiCallTime);
            log.AddLogEvent("ApiCall time Taken: ", apiCallTime.ToString());

            var data = dataAccess.Deserialise<TradeDataEthEur>(json);

            dataAccess.WriteTrades(data, savePath);
            log.AddLogEvent("Trades Saved To: ", savePath);

            since = data.Result.Last;
            log.AddLogEvent($"Last Trade Number: ", data.Result.Last);

            newUrl = Path.Combine(ConfigurationManager.AppSettings["UrlEthEur"], since);
            log.AddLogEvent($"NewUrl: ", newUrl);

            log.AddLogEvent($"First Trade:");
            var first = string.Empty;
            foreach (var v in data.Result.XETHZEUR.First())
            {
                first += v.ToString() + "; ";
            }
            first += "\n";
            log.AddLogEvent(first.ToString());

            log.AddLogEvent($"Last Trade:");
            var last = string.Empty;
            foreach (var v in data.Result.XETHZEUR.Last())
            {
                last += v.ToString() + "; ";
            }
            last += "\n";
            log.AddLogEvent(last.ToString());
        }
    }
}
