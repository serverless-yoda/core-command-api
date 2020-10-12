using System;
using System.Linq;
using System.Linq.Expressions;

namespace CoreCommandContracts
{
    public interface IBaseRepository<T>
    {
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, 
        bool trackChanges);
        IQueryable<T> GetAll(bool trackChanges);
    }
}
