using System.Collections.Generic;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface ICrudRepository<TEntity>
    {
        void Add(TEntity entity);
        
        void Update(TEntity entity);

        void Remove(long entityId);

        IEnumerable<TEntity> GetAll();

        TEntity Find(long entityId);

        long Count();
    }
}
