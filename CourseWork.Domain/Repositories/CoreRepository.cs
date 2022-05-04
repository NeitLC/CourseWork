using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Enums;
using CourseWork.Domain.Extension;
using CourseWork.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Domain.Repositories
{
    public abstract class CoreRepository<TEntity, TContext, TId> : IRepository<TEntity, TId> where TEntity : class,
        IEntityWithId<TId>
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected CoreRepository(TContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return await _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .ToListAsync();
        }

        public async Task<TEntity> Get(TId id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }
        
        public EntityPageDto<TEntity> Paginate(
            int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null,
            Sort sort = Enums.Sort.Desc,
            Func<TEntity, object> sortPredicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = _context.Set<TEntity>();
            bool DefaultPredicate(TEntity entity) => true;
            
            var count = dbSet.Where(predicate ?? DefaultPredicate).Count();

            var entities = dbSet.IncludeMultiple(includes).AsEnumerable()
                .Where(predicate ?? DefaultPredicate);

            if (sortPredicate != null)
            {
                entities = Sort(entities, sortPredicate: sortPredicate);
            }

            entities = entities.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            var entityPageDto = new EntityPageDto<TEntity>
            {
                Page = new Page(page, pageSize, count),
                Entities = entities
            };

            return entityPageDto;
        }
        
        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> Delete(TId id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }

            return entity;
        }

        public IEnumerable<TEntity> Find(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .AsEnumerable().Where(predicate)
                .ToList();
        }

        private IEnumerable<TEntity> Sort(
            IEnumerable<TEntity> entities,
            Func<TEntity, object> sortPredicate = null,
            Sort sort = Enums.Sort.Desc
            )
        {
            if (sort == Enums.Sort.Asc)
            {
                if (sortPredicate != null) entities = entities.OrderBy(sortPredicate);
            }
            else if (sort == Enums.Sort.Desc)
            {
                if (sortPredicate != null) entities = entities.OrderByDescending(sortPredicate);
            }

            return entities;
        }
    }
}