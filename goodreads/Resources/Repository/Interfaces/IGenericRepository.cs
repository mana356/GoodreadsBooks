using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using goodreads.Repository.Entities;

namespace goodreads.Repository.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();

        public Task<List<T>> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task Create(T entity);

        Task CreateRange(List<T> entities);

        Task Update(int id, T entity);

        Task Delete(int id);
    }

}
