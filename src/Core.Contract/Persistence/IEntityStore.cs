using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Persistence
{
    public interface IEntityStore
    {
    }

    public interface IEntityStore<TEntity> : IEntityStore
        where TEntity : class
    {
        // Summary:
        //     Create a new entity
        //
        // Parameters:
        //   entity:
        Task CreateAsync(TEntity entity);
        
        //
        // Summary:
        //     Delete an entity
        //
        // Parameters:
        //   entity:
        Task DeleteAsync(TEntity entity);
        
        //
        // Summary:
        //     Update an entity
        //
        // Parameters:
        //   entity:
        Task UpdateAsync(TEntity entity);
    }
}
