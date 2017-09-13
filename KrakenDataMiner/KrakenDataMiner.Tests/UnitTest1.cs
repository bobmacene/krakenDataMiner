using NUnit.Framework;
using System.IO;
using Shared;
using Shared.PathsUrls;
using System.Collections.Generic;

namespace KrakenDataMiner.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void OverWriteTextFile_Test()
        {
            var path = @"C:\Users\bob\Documents\KrakenDataMiner\Log\previous\2017.08.30_173701_KrakenDataMiner.txt";
            var text = "testOverWrite";

            File.WriteAllText(path, text);

            var writtenText = File.ReadAllText(path);

            Assert.AreEqual(text, writtenText);
        }

        [Test]
        public void SerialiseOhlcFromApi_Test() // not working... must serilise to string[]
        {
            var api = new ApiCall();
            var shared = new SharedData();

            var url = shared.UrlEtcEurOhlc; 
            var rawTrds = api.CallApi(url);

            var data = new DataAccess();
            var trds = data.Deserialise<EthEurOhlc>(rawTrds);

            Assert.IsNotNull(trds);

        }

        [Test]
        public void StringsEqual_Test()
        {
            var str = "34";
            var list = new List<string> { "12", "23", "34" };

            var newList = new List<string>();

            foreach(var v in list)
            {
                if (v == str) break;
                newList.Add(v);
            }

            Assert.AreEqual(newList.Count, 2);
        }

        //[Test]
        //public void GetLastTradeNumber_Test()
        //{
        //    var path = @"C:\Users\bob\Documents\KrakenDataMiner\Trades\EthEur";
        //    var number = new LastTradeNumber();
        //    var result =  number.GetLastTradeNumber(path);
        //}
    }
}
