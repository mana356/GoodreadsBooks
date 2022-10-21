using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.Services.Interfaces;

namespace Test.Workers
{
    public class CardInputWorker : BackgroundService
    {
        readonly IHostApplicationLifetime _lifetime;
        readonly IServiceProvider _serviceProvider;

        public CardInputWorker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
        {
            _lifetime = hostApplicationLifetime;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("Card Input Worker Started******************************************************************");

                using var scope = _serviceProvider.CreateScope();
                var cardInputService = scope.ServiceProvider.GetRequiredService<ICardInputService>();
                var random = new Random();
                var results = cardInputService.AddInputsForCard(random.Next(1000));

                Console.WriteLine($"Inputs Added Count: {results.Count}");

                Console.WriteLine("Card Input Worker Finished******************************************************************");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            _lifetime.StopApplication();
        }
    }
}
