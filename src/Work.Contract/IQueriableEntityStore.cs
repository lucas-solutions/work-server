using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public interface IQueryableEntityStore<TEntity> : IEntityStore<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
    }

}
