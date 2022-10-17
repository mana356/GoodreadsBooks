using goodreads.Repository.Entities;
using goodreads.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace goodreads.Repository.DAL
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
