using GoodreadsBooks.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodreadsBooks.Repository.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<Book>> GetBooks();
    }
}
