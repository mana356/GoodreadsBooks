using GoodreadsBooks.Repository.Entities;
using GoodreadsBooks.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodreadsBooks.Repository.DAL
{
    public class OpenLibraryBookRepository : GenericRepository<OpenLibraryBook>, IOpenLibraryBookRepository
    {
        public OpenLibraryBookRepository(BookContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<List<OpenLibraryBook>> GetOpenLibraryBooks()
        {
            return await GetAll()
                .OrderBy(c => c.Title)
                .ToListAsync();
        }
    }
}
