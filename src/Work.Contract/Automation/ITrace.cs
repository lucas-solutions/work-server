using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public interface ITrace
    {
        TimeSpan Duration { get; }

        int Id { get; }

        string Message { get; }

        DateTimeOffset Start { get; }

        bool Success { get; }
    }
}
