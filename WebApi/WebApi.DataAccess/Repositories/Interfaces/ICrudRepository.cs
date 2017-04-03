using System.Collections.Generic;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface ICrudRepository<TEntity>
    {
        void Add(TEntity dto);
        
        void Update(TEntity dto);

        void Remove(long entityId);

        IEnumerable<TEntity> GetAll();

        TEntity Find(long entityId);

        long Count();
    }
}
