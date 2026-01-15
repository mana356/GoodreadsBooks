using GoodreadsBooks.Repository.Entities;
using GoodreadsBooks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using GoodreadsBooks.Repository.Interfaces;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.DependencyInjection;
using GoodreadsBooks.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using GoodreadsBooks.Services.Interfaces;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;

namespace GoodreadsBooks.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient client;

        public OpenLibraryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            client = new HttpClient();
        }
        public async Task<int> FindAndInsertOpenLibraryBookDetails()
        {
            var scope = _serviceProvider.CreateScope();
            var openLibBookRepo = scope.ServiceProvider.GetRequiredService<IOpenLibraryBookRepository>();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();

            var bookList = await dbContext.Books.ToListAsync();
            var openLibBookList = openLibBookRepo.GetAll().ToList();
            var foundCount = 0;
            client.BaseAddress = new Uri("https://openlibrary.org/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (var book in bookList)
            {
                //if any open library books are already added for this local book then skip
                if (openLibBookList != null && openLibBookList.Any(x => x.BookId == book.Id))
                {
                    continue;
                }
                var bookName = Path.GetFileName(book.Path).Replace(book.Extension, "");
                HttpResponseMessage response = await client.GetAsync($"search.json?q={bookName}");
                if (response.IsSuccessStatusCode)
                {
                    //parse response body
                    var result = response.Content.ReadAsStringAsync().Result;
                    var searchResult = JsonConvert.DeserializeObject<OpenLibraryResult>(result);
                    if (searchResult != null && searchResult.num_found > 0)
                    {
                        var openLibBooks = new List<OpenLibraryBook>();
                        openLibBooks.AddRange(searchResult.docs.Select(x => new OpenLibraryBook()
                        {
                            Key = x.key,
                            Type = x.type,
                            Title = x.title,
                            TitleSuggest = x.title_suggest,
                            PublishDateData = x.publish_date,
                            IsbnData = x.isbn,
                            PublisherData = x.publisher,
                            LanguageData = x.language,
                            AuthorNameData = x.author_name,
                            AuthorAlternativeNameData = x.author_alternative_name,
                            Book = book
                        }));

                        await openLibBookRepo.CreateRange(openLibBooks);
                        foundCount++;
                    }
                    else
                    {
                        Console.WriteLine($"\nNo results found from openlibrary: {book.Path}; " +
                        $"\n{response.StatusCode} " +
                        $"\n{response.ReasonPhrase} ");
                    }
                }
                else
                {
                    Console.WriteLine($"\nError in fetching book info from openlibrary: {book.Path}; " +
                        $"\n{response.StatusCode} " +
                        $"\n{response.ReasonPhrase} " +
                        $"\n{response.Content.ReadAsStringAsync().Result}");
                }
            }
            return foundCount;
        }


    }
}
