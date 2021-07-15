using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TextSnippetDemo.Infra.Data;

namespace TextSnippetDemo.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(TextSnippetDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        protected DbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;
            query = filter != null ? query.Where(filter) : query;
            return query;
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Add(IEnumerable<T> entity)
        {
            DbSet.AddRange(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
