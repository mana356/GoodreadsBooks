using Test.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
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
            Console.WriteLine("********************Test Worker Started**********************");
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var TestService = scope.ServiceProvider.GetRequiredService<ITestService>();
                await TestService.FindAndUpdateBookDetails();
                Console.WriteLine("********************Test Worker Finished**********************");

                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            _lifetime.StopApplication();
        }
    }
}
