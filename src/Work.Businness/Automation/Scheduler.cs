using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public class Scheduler
    {
        private ICollection<Task> _tasks;
        private ICollection<IWork> _workers;

        public ICollection<Task> Tasks
        {
            get { return _tasks ?? (_tasks = new List<Task>()); }
            set { _tasks = value; }
        }

        public ICollection<IWork> Workers
        {
            get { return _workers ?? (_workers = new List<IWork>()); }
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
