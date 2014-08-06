using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public interface IWork
    {
        Task Task { get; set; }

        System.Threading.Tasks.Task WorkAsync();
    }

    public interface IWork<TTask> : IWork
        where TTask : Task
    {
        new TTask Task { get; set; }
    }
}
