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

        public Task<Automation.Task> FindByIdAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<Automation.Task> FindByNameAsync(string taskName)
        {
            throw new NotImplementedException();
        }
    }
}
