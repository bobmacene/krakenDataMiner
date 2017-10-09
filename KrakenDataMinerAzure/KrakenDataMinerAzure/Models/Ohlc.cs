using System;
using System.ComponentModel.DataAnnotations;

namespace KrakenDataMinerAzure.Models
{
    public class Ohlc
    {
        [Key]
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
}