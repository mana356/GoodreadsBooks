using Test.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository.Interfaces
{
    public interface IOpenLibraryBookRepository : IGenericRepository<OpenLibraryBook>
    {
        Task<List<OpenLibraryBook>> GetOpenLibraryBooks();
    }
}
