﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{   
    public class TaskMemoryStore : IQueryableEntityStore<Automation.Task>, Automation.ITaskStore
    {
        private ICollection<Automation.Task> _collection;

        public TaskMemoryStore()
            : this(new List<Automation.Task>())
        {
        }

        public TaskMemoryStore(ICollection<Automation.Task> collection)
        {
            _collection = collection;
        }

        IQueryable<Automation.Task> IQueryableEntityStore<Automation.Task>.Entities
        {
            get { return _collection.AsQueryable(); }
        }

        public IQueryable<Automation.Task> Tasks
        {
            get { return _collection.AsQueryable(); }
        }

        public Task CreateAsync(Automation.Task entity)
        {
            return new Task(() => _collection.Add(entity));
        }

        public Task DeleteAsync(Automation.Task entity)
        {
            return new Task(() => _collection.Remove(entity));
        }

        public Task<Automation.ITask> FindByIdAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<Automation.ITask> FindByNameAsync(string taskName)
        {
            return new Task<Automation.ITask>(() => _collection.FirstOrDefault(item => item.Name == taskName));
        }

        public Task UpdateAsync(Automation.Task entity)
        {
            return new Task(() =>
            {
                var current = _collection.FirstOrDefault(item => item.Name == entity.Name);

                if (current != null)
                {
                    _collection.Remove(current);
                }

                _collection.Add(entity);
            });
        }
    }
}