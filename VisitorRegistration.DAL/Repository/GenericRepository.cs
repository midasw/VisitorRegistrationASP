using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _entities = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return _entities.Add(entity).Entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
