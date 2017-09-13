using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Models
{
    public class Ohlc
    {
        public int UnixTime { get; set; }
        public DateTime Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Vwap { get; set; }
        public decimal Volume { get; set; }
        public decimal AveragePrice { get; set; }
        public long Last { get; set; }

        public Ohlc() { }

        public Ohlc(string[] ohclString, string last)
        {
            UnixTime = Convert.ToInt32(ohclString[0]);
            Time = GetTime(Convert.ToDouble(ohclString[0]));
            Open = Convert.ToDecimal(ohclString[1]);
            Low = Convert.ToDecimal(ohclString[2]);
            High = Convert.ToDecimal(ohclString[3]);
            Close = Convert.ToDecimal(ohclString[4]);
            Vwap = Convert.ToDecimal(ohclString[5]);
            Volume = Convert.ToDecimal(ohclString[6]);
            AveragePrice = (Close + High + Low) / 3;
            Last = Convert.ToInt64(last);
        }

        public override string ToString()
        {
            return $"Unixtime:, {UnixTime}, Time:, {Time}, Open:, {Open}, High:, {High}, Low:, {Low}, Close:, {Close}," +
                        $"Vwap:, {Vwap}, Volume:, {Volume}, AvePrice:, {AveragePrice}, Last:, {Last}";
        }

        private static DateTime GetTime(double msFrmUnix)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(msFrmUnix).ToLocalTime();
            return dtDateTime;
        }
    }

    public class OhlcSma
    {
        public Ohlc Ohlc { get; set; }

        public IList<Sma> Smas = new List<Sma>
        {
            new Sma(1),
            new Sma(3),
            new Sma(5),
            new Sma(15),
            new Sma(30)
        };

        public OhlcSma GetOhlcSimpleMovingAverages(IList<Ohlc> ohlcs)
        {
            var sma = new OhlcSma();

            var ranges = GetRanges(ohlcs);

            var OhlcSmas = new List<OhlcSma>();

            var count = 0;
            foreach (var range in ranges)
            {
                sma.Smas[count].TimePeriods[0] = range.Take(12).Average(x => x.AveragePrice);
                sma.Smas[count].TimePeriods[1] = range.Take(25).Average(x => x.AveragePrice);
                sma.Smas[count].TimePeriods[2] = range.Take(40).Average(x => x.AveragePrice);
                count++;
            }

            return sma;
        }

        public IEnumerable<IEnumerable<Ohlc>> GetRanges(IList<Ohlc> ohlcs)
        {
            var totalLoopCount = 40 * 30;

            var count = ohlcs.Count - 1;
            var finish = count - 40 * 30;

            var oneSma = new List<Ohlc>();
            var threeSma = new List<Ohlc>();
            var fiveSma = new List<Ohlc>();
            var fifteenSma = new List<Ohlc>();
            var thirtySma = new List<Ohlc>();

            var loopCount = 1;


            foreach (var ohlc in ohlcs)
            {
                if (loopCount > totalLoopCount) break;

                oneSma.Add(ohlcs[count]);
                if (loopCount / 3 == 0) threeSma.Add(ohlcs[count]);
                if (loopCount / 5 == 0) fiveSma.Add(ohlcs[count]);
                if (loopCount / 15 == 0) fifteenSma.Add(ohlcs[count]);
                if (loopCount / 30 == 0) thirtySma.Add(ohlcs[count]);

                loopCount++;
                count--;
            }

            var ranges = new List<List<Ohlc>>
            {
                oneSma, threeSma, fiveSma, fifteenSma, thirtySma
            };

            return ranges;
        }
    }

    public class Sma
    {
        public int Interval;
        public decimal[] TimePeriods = new decimal[3];

        public Sma(int interval)
        {
            Interval = interval;
        }
    }

}

