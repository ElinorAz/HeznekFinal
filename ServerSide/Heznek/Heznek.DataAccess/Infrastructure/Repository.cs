using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Heznek.DataAccess.Infrastructure
{
    public sealed class EFRepository<T> : IRepository<T>
       where T : class
    {
        private readonly HeznekDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public EFRepository(HeznekDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public IQueryable<T> Set { get { return _entities; } }

        public T GetById(object id)
        {
            return this._entities.Find(id);
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> InsertRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _entities.AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public async Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            await _entities.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _entities.UpdateRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _entities.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public void Delete(T entity, bool acceptNull = false)
        {
            if (entity == null)
            {
                if (acceptNull)
                    return;

                throw new ArgumentNullException(nameof(entity));
               
            }
            _entities.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(Guid id)
        {
            T entity = this._entities.Find(id);

            if (entity != null)
            {
                _entities.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _entities.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] include)
        {
            return include.Aggregate((IQueryable<T>)_entities, (current, inc) => current.Include(inc));
        }
    }
}