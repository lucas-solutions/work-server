using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;
    using Lucas.Solutions.Persistence;
    
    public class HostMemoryStore : IQueryableEntityStore<Host>, IHostStore
    {
        private ICollection<Host> _collection;

        public HostMemoryStore()
            : this(new List<Host>())
        {
        }

        public HostMemoryStore(ICollection<Host> collection)
        {
            _collection = collection;
        }

        IQueryable<Host> IQueryableEntityStore<Host>.Entities
        {
            get { return _collection.AsQueryable(); }
        }

        public Task<IQueryable<Host>> Hosts
        {
            get { return new Task<IQueryable<Host>>(() => _collection.AsQueryable()); }
        }

        public Task CreateAsync(Host entity)
        {
            return new Task(() => _collection.Add(entity));
        }

        public Task DeleteAsync(Host entity)
        {
            return new Task(() => _collection.Remove(entity));
        }

        public Task<Host> FindByIdAsync(int hostId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Host> FindByNameAsync(string hostName)
        {
            return new Task<Host>(() => _collection.FirstOrDefault(item => item.Name == hostName));
        }

        public Task UpdateAsync(Host entity)
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
