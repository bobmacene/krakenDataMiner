using System.Diagnostics;
using System.Net.Http;

namespace Shared
{
    public interface IApiCall
    {
        string CallApi(string url);
    }

    public class ApiCall : IApiCall
    {
        private DataAccess data = new DataAccess();

        public string CallApi(string url)
        {
            using (var http = new HttpClient())
            {
                var result = http.GetStringAsync(url);
                return result.Result;
            }
        }

        public string CallApi(string url, out long timeTaken)
        {
            using (var http = new HttpClient())
            {
                var sw = new Stopwatch();
                sw.Start();

                var result = new HttpClient().GetStringAsync(url);

                sw.Stop();
                timeTaken = sw.ElapsedMilliseconds;

                return result.Result;
            }
        }
    }
}
