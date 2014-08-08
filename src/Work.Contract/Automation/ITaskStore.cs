using System.Linq;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    using Lucas.Solutions.Persistence;

    public interface ITaskStore : IEntityStore<Task>
    {
        IQueryable<Task> Tasks { get; }

        /// <summary>
        /// Find a host by id
        /// </summary>
        /// <param name="hostId"></param>
        /// <returns></returns>
        Task<Task> FindByIdAsync(int taskId);

        /// <summary>
        /// Find a task by name
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        Task<Task> FindByNameAsync(string taskName);
    }
}
