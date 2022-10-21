using Test.Repository.Entities;
using Test.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Test.Repository.DAL
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
