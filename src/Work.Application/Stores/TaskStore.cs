using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.Automation;
    using Lucas.Solutions.Persistence;

    public class TaskStore : IQueryableEntityStore<Task>
    {
        IQueryable<Task> IQueryableEntityStore<Task>.Entities
        {
            get { throw new NotImplementedException(); }
        }

        System.Threading.Tasks.Task IEntityStore<Task>.CreateAsync(Task entity)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task IEntityStore<Task>.DeleteAsync(Task entity)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task IEntityStore<Task>.UpdateAsync(Task entity)
        {
            throw new NotImplementedException();
        }
    }
}