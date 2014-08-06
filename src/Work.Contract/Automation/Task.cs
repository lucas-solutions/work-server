using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public abstract class Task : ITask
    {
        private TaskStart _start;
        private readonly string _type;

        protected Task()
        {
            Type = _type = GetType().Name;
        }

        /// <summary>
        /// Transfer ID
        /// </summary>
        public virtual int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public virtual string Start
        {
            get { return (string)_start; }
            set { _start = (TaskStart)value; }
        }

        TaskStart ITask.Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public TaskState State
        {
            get;
            set;
        }

        public virtual string Summary
        {
            get;
            set;
        }

        public virtual string Type
        {
            get;
            set;
        }
    }
}
