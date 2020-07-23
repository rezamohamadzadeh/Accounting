using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepositori<TEntity> :IGenericRepository<TEntity> where TEntity:class
    {

        protected ApplicationDbContext _context;
        protected DbSet<TEntity> _dbset;

        public GenericRepositori(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }
        public virtual TEntity GetById(object id)
        {
            return _dbset.Find(id);
        }
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbset.FindAsync(id);
        }
        public virtual void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }
        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }
        public virtual void InsertRange(List<TEntity> entity)
        {
            _dbset.AddRange(entity);
        }
        public virtual async Task InsertRangeAsync(List<TEntity> entity)
        {
            await _dbset.AddRangeAsync(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _dbset.Update(entity);
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entity)
        {
            _dbset.UpdateRange(entity);
        }
        /// <summary>
        /// this method for preventing update specific property
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        public virtual void UpdateWithOption(TEntity entity, string property)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(property).IsModified = false;
        }
        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }

            _dbset.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }
        /// <summary>
        /// for prevent tracked instance when i want update tracke entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
