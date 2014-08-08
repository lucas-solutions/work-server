using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.Authentication;
    using Lucas.Solutions.Persistence;

    public class UserMemoryStore : IQueryableEntityStore<User>
    {
        private ICollection<User> _collection = new List<User>();

        IQueryable<User> IQueryableEntityStore<User>.Entities
        {
            get { return _collection.AsQueryable(); }
        }

        public Task CreateAsync(User entity)
        {
            return new Task(() => _collection.Add(entity));
        }

        public Task DeleteAsync(User entity)
        {
            return new Task(() => _collection.Remove(entity));
        }

        public Task UpdateAsync(User entity)
        {
            return new Task(() =>
            {
                var current = _collection.FirstOrDefault(item => item.Email == entity.Email);

                if (current != null)
                {
                    _collection.Remove(current);
                }

                _collection.Add(entity);
            });
        }
    }
}