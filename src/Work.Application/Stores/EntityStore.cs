using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.Persistence;

    public class EntityStore : IEntityStore
    {
    }

    public class EntityStore<TEntity> : EntityStore, IEntityStore<TEntity>, IQueryableEntityStore<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> IQueryableEntityStore<TEntity>.Entities
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