using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public interface IWorker
    {
        ITask Task { get; set; }

        void Start();

        void Stop();
    }

    public interface IWorker<TTask> : IWorker
        where TTask : ITask
    {
        new TTask Task { get; set; }
    }
}
