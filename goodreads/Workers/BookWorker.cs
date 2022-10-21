using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.Services.Interfaces;

namespace Test.Workers
{
    public class BookWorker : BackgroundService
    {
        readonly IHostApplicationLifetime _lifetime;
        readonly IServiceProvider _serviceProvider;

        public BookWorker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
        {
            _lifetime = hostApplicationLifetime;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("Book Finder Worker Started******************************************************************");

                using var scope = _serviceProvider.CreateScope();
                var bookFinderService = scope.ServiceProvider.GetRequiredService<ILocalBookFinderService>();
                var results = await bookFinderService.FindAndAddBooks();

                Console.WriteLine($"Books Added Count: {results.addedBooks.Count}");

                if (results.errorBuilder.HasErrors)
                {
                    foreach (var error in results.errorBuilder.Errors)
                    {
                        Console.WriteLine("Error in service: " + error.Message);
                    }
                }
                Console.WriteLine("Book Finder Worker Finished******************************************************************");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            _lifetime.StopApplication();
        }
    }
}
