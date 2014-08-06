using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public interface ITask
    {
        int Id { get; }

        string Name { get; }

        TaskStart Start { get; set; }

        TaskState State { get; }

        string Summary { get; }

        string Type { get; }
    }
}
