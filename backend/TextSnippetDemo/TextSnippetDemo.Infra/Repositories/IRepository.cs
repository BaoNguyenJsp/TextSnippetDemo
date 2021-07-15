using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TextSnippetDemo.Infra.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null);

        void Add(T entity);

        void Add(IEnumerable<T> entity);

        void Update(T entity);

        void Update(IEnumerable<T> entity);

        void Remove(T entity);

        void Remove(IEnumerable<T> entity);

        IDbContextTransaction BeginTransaction();

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
