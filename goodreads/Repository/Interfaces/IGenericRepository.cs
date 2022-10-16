using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using goodreads.Repository.Entities;

namespace goodreads.Repository.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        Task Create(T entity);

        Task Update(int id, T entity);

        Task Delete(int id);
    }

}
