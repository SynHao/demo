using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoData
{
    class Clock:IClock
    {
        public DateTime Now
        {
            get { return System.DateTime.Now; }
        }

        public DateTime UtcNow
        {
            get { return System.DateTime.UtcNow; }
        }
    }
}
