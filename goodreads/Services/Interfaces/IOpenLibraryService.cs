namespace GoodreadsBooks.Services
{
    public interface IOpenLibraryService
    {
        Task<int> FindAndInsertOpenLibraryBookDetails();

    }
}
