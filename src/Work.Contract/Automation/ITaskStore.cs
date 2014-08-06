using System.Linq;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public interface ITaskStore : IEntityStore<Task>
    {
        IQueryable<Task> Tasks { get; }

        /// <summary>
        /// Find a host by id
        /// </summary>
        /// <param name="hostId"></param>
        /// <returns></returns>
        Task<ITask> FindByIdAsync(int taskId);

        /// <summary>
        /// Find a task by name
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        Task<ITask> FindByNameAsync(string taskName);
    }
}
