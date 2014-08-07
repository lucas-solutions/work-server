using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class TransferScheduler : TaskScheduler
    {
        private TransferTaskCategory _taskCategory;
        private Queue<Task> _queue;

        public TransferScheduler(TransferTaskCategory taskCategory)
        {
            _queue = new Queue<Task>();
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _queue;
        }

        protected override void QueueTask(Task task)
        {
            _queue.Enqueue(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new NotImplementedException();
        }
    }
}
