using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public enum TaskState : byte
    {
        Initial = 0,

        Running = 1,

        Completed = 2,

        Error = 3
    }
}
