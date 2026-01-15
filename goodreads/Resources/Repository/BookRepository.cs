using GoodreadsBooks.Repository.Entities;
using GoodreadsBooks.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodreadsBooks.Repository.DAL
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
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
