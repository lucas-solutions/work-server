using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public class Scheduler
    {
        private ICollection<ITask> _tasks;
        private ICollection<IWorker> _workers;

        public ICollection<ITask> Tasks
        {
            get { return _tasks ?? (_tasks = new List<ITask>()); }
            set { _tasks = value; }
        }

        public ICollection<IWorker> Workers
        {
            get { return _workers ?? (_workers = new List<IWorker>()); }
            set { _workers = value; }
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
