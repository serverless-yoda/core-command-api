using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CoreCommandContracts;
using CoreCommandEntities.Data;

namespace CoreCommandRepositories
{
    public abstract class BaseRepository<T> : 
    IBaseRepository<T> where T : class
    {
        protected readonly CoreCommandContext  CoreCommandContext;       
        protected BaseRepository(CoreCommandContext repoContext)
        {
            CoreCommandContext = repoContext;
        }
        public void Create(T entity) 
        => CoreCommandContext.Set<T>().Add(entity);

        public void Delete(T entity) 
        => CoreCommandContext.Set<T>().Remove(entity);

        public void Update(T entity) 
        =>  CoreCommandContext.Set<T>().Update(entity);

        public IQueryable<T> GetAll(bool trackChanges) 
        => trackChanges ? CoreCommandContext.Set<T>() 
           :CoreCommandContext.Set<T>().AsNoTracking();
        
         public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, 
         bool trackChanges) 
         => trackChanges ? CoreCommandContext.Set<T>().Where(expression) 
            :CoreCommandContext.Set<T>().Where(expression).AsNoTracking();
        }
    }

