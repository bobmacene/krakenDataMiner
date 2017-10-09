using System;
using Shared;
using System.Threading.Tasks;

namespace KrakenDataMiner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tradeData = new ProcessTradeData();
            var shared = new SharedData();

            try
            {
                var serverTime = string.Empty;
                shared.Log.AddServerTimeToLog(shared.Call, out serverTime);
                Console.WriteLine($"ServerTime: {serverTime}");

                Action LtcBtcCall = () => tradeData.CallApi(shared, CurrencyPair.LtcBtc);
                Action EthEurCall = () => tradeData.CallApi(shared, CurrencyPair.EthEur);
                Action BtcEurCall = () => tradeData.CallApi(shared, CurrencyPair.BtcEur);
                Action LtcEurCall = () => tradeData.CallApi(shared, CurrencyPair.LtcEur);

                var actions = new[] { LtcBtcCall, EthEurCall, BtcEurCall, LtcEurCall };

                while (!shared.StopApp)
                {
                    foreach (var action in actions)
                    {
                        action.Invoke();
                        Console.WriteLine($"ApiCall Processed: {nameof(action)} @ {DateTime.Now}");
                        Task.Delay(10 * 1000).Wait();
                    }

                    Task.Delay(60 * 60 * 1000).Wait();
                }
               
            }
            catch (Exception ex)
            {
                shared.Log.AddLogEvent(ex.ToString());
                shared.Log.PersistLog();
            }
        }
    
    }
}


#region altCode


//var exitTask = Task.Run(() =>
//{
//    shared.Exit.ExitAppProcess(shared, out _stop);

//    if (_stop)
//    {
//        shared.Log.AddLogEvent("\t\t\tAPP MANUALLY EXITED\n\n");
//        shared.Log.AddServerTimeToLog(shared.Call);
//        shared.Log.AddLogEvent("FINISHED");
//        shared.Log.PersistLog();
//        Environment.Exit(-1);
//    }
//});
//shared.Log.AddLogEvent("Exit Task Started\n\n");

//var tasks = new[]
//{
//    new Task(() =>
//        {
//            tradeData.CallApi(shared, CurrencyPair.EthEur);
//        }),
//    new Task(() =>
//        {
//            tradeData.CallApi(shared, CurrencyPair.BtcEur);
//        }),
//    new Task(() =>
//        {
//            tradeData.CallApi(shared, CurrencyPair.LtcEur);
//        })
// };

//var _ethEurTask = new Task(() => 
//{
//    tradeData.RunApiCallWriteTradeData(shared, CurrencyPair.EthEur);
//});
//shared.Log.AddLogEvent($"{CurrencyPair.EthEur} Process Task Started\n\n");
//_ethEurTask.RunSynchronously();

//Thread.Sleep(1000 * 10);

//var _btcEurTask = new Task(() =>
//{
//    tradeData.RunApiCallWriteTradeData(shared, CurrencyPair.BtcEur);
//});
//shared.Log.AddLogEvent($"{CurrencyPair.BtcEur} Process Task Started\n\n");
//_btcEurTask.RunSynchronously();

//Thread.Sleep(1000 * 10);

//var _ltcEurTask = new Task(() =>
//{
//    tradeData.RunApiCallWriteTradeData(shared, CurrencyPair.LtcEur);
//});
//shared.Log.AddLogEvent($"{CurrencyPair.LtcEur} Process Task Started\n\n");
//_ltcEurTask.RunSynchronously(); 
#endregion