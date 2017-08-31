﻿using System;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Configuration;
using KrakenDataMiner;

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

        //[Test]
        //public void GetLastTradeNumber_Test()
        //{
        //    var path = @"C:\Users\bob\Documents\KrakenDataMiner\Trades\EthEur";
        //    var number = new LastTradeNumber();
        //    var result =  number.GetLastTradeNumber(path);
        //}
    }
}
