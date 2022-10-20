using goodreads.Repository.Entities;
using goodreads.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using goodreads.Repository.Interfaces;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.DependencyInjection;
using goodreads.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace goodreads.Services
{
    public class GoodreadsService : IGoodreadsService
    {
        private readonly BookFinderOptions _options;
        private readonly IServiceProvider _serviceProvider;
        private HttpClient client;

        public GoodreadsService(IOptions<BookFinderOptions> options, IServiceProvider serviceProvider)
        {
            _options = options.Value;
            _serviceProvider = serviceProvider;
            client = new HttpClient();
        }
        public async Task FindAndUpdateBookDetails()
        {
            var scope = _serviceProvider.CreateScope();
            var bookRepo = scope.ServiceProvider.GetRequiredService<IBookRepository>();
            var bookList = bookRepo.GetAll().Where(x => x.Isbn.Equals("NA")).ToList();

            client.BaseAddress = new Uri("https://openlibrary.org/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (var book in bookList)
            {
                var bookName = Path.GetFileName(book.Path).Replace(book.Extension, "");
                HttpResponseMessage response = await client.GetAsync($"search.json?q={bookName}");
                if (response.IsSuccessStatusCode)
                {
                    //parse response body
                    var result = response.Content.ReadAsStringAsync().Result;
                    var searchResult = JsonConvert.DeserializeObject <OpenLibraryResult> (result);

                    book.Isbn = searchResult.docs[0].isbn[0];
                    await bookRepo.Update(book.Id, book);
                }
                else
                {
                    Console.WriteLine($"Error for book: {book.Path}");
                }
            }            
        }

        
    }
}
