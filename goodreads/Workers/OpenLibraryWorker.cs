using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.Services.Interfaces;

namespace Test.Workers
{
    public class OpenLibraryWorker : BackgroundService
    {
        IHostApplicationLifetime _lifetime;
        IServiceProvider _serviceProvider;

        public OpenLibraryWorker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
        {
            _lifetime = hostApplicationLifetime;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("Test Worker Started**************************************************************");

                using var scope = _serviceProvider.CreateScope();
                var TestService = scope.ServiceProvider.GetRequiredService<IOpenLibraryService>();
                var foundCount = await TestService.FindAndInsertOpenLibraryBookDetails();

                Console.WriteLine($"Books matched Count: {foundCount}");

                Console.WriteLine("Test Worker Finished**************************************************************");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            _lifetime.StopApplication();
        }
    }
}
