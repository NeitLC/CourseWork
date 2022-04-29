using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Enums;

namespace CourseWork.Domain.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes);
        
        Task<TEntity> Get(TId id, params Expression<Func<TEntity, object>>[] includes);
        
        EntityPageDto<TEntity> Paginate(
            int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null,
            Sort sort = Sort.Desc,
            Func<TEntity, object> sortPredicate = null,
            params Expression<Func<TEntity, object>>[] includes);

        TEntity Update(TEntity entity);

        TEntity Add(TEntity entity);
        
        Task<TEntity> Delete(TId id);
        
        IEnumerable<TEntity> Find(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includes);

    }
}