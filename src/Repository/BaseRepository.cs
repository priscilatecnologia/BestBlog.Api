using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BlogContext _context;

        public BlogContext Context => _context;

        protected BaseRepository(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Get(Guid id)
        {
            return _context.Find<TEntity>(id);
        }

        public TEntity Create(TEntity entity)
        {
            var result = _context.Set<TEntity>().Add(entity).Entity;
            _context.SaveChanges();
            return result;
        }

        public TEntity Update(TEntity entity)
        {
            var result = _context.Set<TEntity>().Update(entity).Entity;
            _context.SaveChanges();
            return result;
        }

        public bool Delete(Guid id)
        {
            var entity = _context.Find<TEntity>(id);
            var result = _context.Remove(entity);
            _context.SaveChanges();
            return result.State == EntityState.Deleted;
        }
    }
}
