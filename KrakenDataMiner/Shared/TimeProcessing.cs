using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TimeProcessing
    {
        public DateTime GetTime(double msFrmUnix)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(msFrmUnix).ToLocalTime();
            return dtDateTime;
        }
    }
}

