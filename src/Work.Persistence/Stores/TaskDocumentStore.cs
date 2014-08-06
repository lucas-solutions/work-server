using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.Automation;

    public class TaskDocumentStore : EntityDocumentStore<Task>, ITaskStore
    {
        public IQueryable<Task> Tasks
        {
            get { return Entities; }
        }

        public Task<ITask> FindByIdAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<ITask> FindByNameAsync(string taskName)
        {
            throw new NotImplementedException();
        }
    }
}
