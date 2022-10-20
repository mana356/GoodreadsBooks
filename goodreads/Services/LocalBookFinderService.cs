using goodreads.Repository.Entities;
using goodreads.Repository;
using goodreads.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using goodreads.Repository.Interfaces;
using goodreads.Resources.Repository.Interfaces;

namespace goodreads.Services
{
    public class LocalBookFinderService: ILocalBookFinderService
    {
        private readonly BookFinderOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public LocalBookFinderService(IOptions<BookFinderOptions> options, IServiceProvider serviceProvider) {
            _options = options.Value;
            _serviceProvider = serviceProvider;
        }

        public async Task<(ErrorBuilder errorBuilder, List<Book> addedBooks)> FindAndAddBooks()
        {
            var errorBuilder = new ErrorBuilder();
            var addedBooks = new List<Book>();
            var filePaths = Directory.GetFiles(_options.BooksDirectory, "*", SearchOption.AllDirectories);
            var newPaths = new HashSet<string>();
            var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();
            var bookRepo = scope.ServiceProvider.GetRequiredService<IBookRepository>();
            var existingBooks = await bookRepo.GetBooks();

            if (existingBooks != null && existingBooks.Any())
            {
                var existingPaths = existingBooks.Select(x => x.Path).ToHashSet<string>();
                newPaths = filePaths.ExceptBy(existingPaths, x => x).ToHashSet<string>();
            }

            var newbooks = newPaths.Any() ? newPaths : filePaths.ToHashSet<string>();
            foreach (var path in newbooks.Select((value, i) => new { i, value }))
            {
                try
                {
                    var fileName = Path.GetFileName(path.value);
                    var ext = Path.GetExtension(path.value);
                    var book = new Book()
                    {
                        Name = fileName,
                        Path = path.value,
                        Author = "NA",
                        Isbn = "NA",
                        Extension = ext,
                        CreatedOn = DateTime.Now
                    };

                    addedBooks.Add(book);
                }
                catch (Exception ex)
                {
                    errorBuilder.AddError(path, ex.Message);
                }
                Console.WriteLine();

            }
            await bookRepo.CreateRange(addedBooks);
            //var inputRepo = scope.ServiceProvider.GetRequiredService<IInputRepository>();
            //var addedRows = inputRepo.AddValuesForCard(1);
            return (errorBuilder, addedBooks);
        }
    }
}
