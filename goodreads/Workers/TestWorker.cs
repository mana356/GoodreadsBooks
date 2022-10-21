using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.Services.Interfaces;

namespace Test.Workers
{
    public class TestWorker : BackgroundService
    {
        IHostApplicationLifetime _lifetime;
        IServiceProvider _serviceProvider;

        public TestWorker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
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
                var TestService = scope.ServiceProvider.GetRequiredService<ITestService>();
                var updateCount = await TestService.FindAndUpdateBookDetails();

                Console.WriteLine($"Books Updated Count: {updateCount}");

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
