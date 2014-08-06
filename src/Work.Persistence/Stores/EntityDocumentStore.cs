using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    public class EntityDocumentStore : IEntityStore
    {
    }

    public class EntityDocumentStore<TEntity> : EntityDocumentStore, IEntityStore<TEntity>, IQueryableEntityStore<TEntity>
        where TEntity : class
    {
        public IQueryable<TEntity> Entities
        {
            get { throw new NotImplementedException(); }
        }

        public Task CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task IEntityStore<TEntity>.CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task IEntityStore<TEntity>.DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task IEntityStore<TEntity>.UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
