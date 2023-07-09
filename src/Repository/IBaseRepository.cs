using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IBaseRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        bool Delete(Guid id);
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity entity);
    }
}
