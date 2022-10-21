using Test.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test
{
    public class BookWorker : BackgroundService
    {
        IHostApplicationLifetime _lifetime;
        IServiceProvider _serviceProvider;

        public BookWorker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
        { 
            _lifetime = hostApplicationLifetime;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("********************Book Finder Worker Started**********************");
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var bookFinderService = scope.ServiceProvider.GetRequiredService<ILocalBookFinderService>();
                var results = await bookFinderService.FindAndAddBooks();

                Console.WriteLine("********************Book Finder Worker Finished**********************");

                Console.WriteLine($"Books Added Count: {results.addedBooks.Count}");

                if (results.errorBuilder.HasErrors)
                {
                    foreach (var error in results.errorBuilder.Errors)
                    {
                        Console.WriteLine("Error in service: " + error.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            _lifetime.StopApplication();
        }
    }
}
