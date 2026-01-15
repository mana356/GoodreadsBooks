using GoodreadsBooks.Models;
using GoodreadsBooks.Repository.Entities;

namespace GoodreadsBooks.Services
{
    public interface ILocalBookFinderService
    {
        Task<(ErrorBuilder errorBuilder, List<Book> addedBooks)> FindAndAddBooks();
    }
}
