using goodreads.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goodreads
{
    public class GoodreadsWorker : BackgroundService
    {
        IHostApplicationLifetime _lifetime;
        IServiceProvider _serviceProvider;

        public GoodreadsWorker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
        {
            _lifetime = hostApplicationLifetime;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("********************Goodreads Worker Started**********************");
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var bookFinderService = scope.ServiceProvider.GetRequiredService<LocalBookFinderService>();
                
                Console.WriteLine("********************Goodreads Worker Finished**********************");

                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            _lifetime.StopApplication();
        }
    }
}
