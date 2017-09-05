
namespace Shared
{
    public class SharedData
    {
        public bool StopApp { get; set; }

        public Logger Log = new Logger();
        public ApiCall Call = new ApiCall();
        public DataAccess Data = new DataAccess();
        public ExitApp Exit = new ExitApp();
        public PathsUrls PathUrl = new PathsUrls();
    }
}
