using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core
{
    public enum Status
    {
        Running = 0,
        Finished = 1,
        Paused = 2,
        Stopped = 3,
        Exception = 4
    }
}
