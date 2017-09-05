using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HtmlInspection
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://trade.kraken.com/kraken/etheur/30m";

            //var client = new HttpClient();
            //var html = client.GetAsync(url);

            var client = new HtmlWeb();
            var doc = client.Load(url);


        }
    }
}
