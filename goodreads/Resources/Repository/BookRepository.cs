using Test.Repository.Entities;
using Test.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Test.Repository.DAL
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<List<Book>> GetBooks()
        {
            return await GetAll()
                .OrderByDescending(c => c.Name)
                .ToListAsync();
        }
    }
}
