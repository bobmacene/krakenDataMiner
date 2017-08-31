using Newtonsoft.Json;
using System;
using System.IO;

namespace Shared
{
    public class DataAccess
    {
        public T Deserialise<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T DeserializeAnonymousType<T>(string json, T typeObject)
        {
            return JsonConvert.DeserializeAnonymousType<T>(json, typeObject);
        }

        public T Read<T>(string path)
        {
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Write(object obj, string path)
        {
            File.AppendAllText(path, JsonConvert.SerializeObject(obj));
        }

        public void WriteTrades(object obj, string path)
        {
            File.AppendAllText(path, JsonConvert.SerializeObject(obj));
        }

        public void OverWriteExistingTrades(object obj, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(obj));
        }
        private static string BuildTradesPath(string path)
        {
            return Path.Combine(path, $"{DateTime.Today.ToString("yyyy.MM.dd")}_KrakenTrades.json");
        }
    }
}
