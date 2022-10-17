using goodreads;
using goodreads.Models;
using goodreads.Repository;
using goodreads.Repository.DAL;
using goodreads.Repository.Entities;
using goodreads.Repository.Interfaces;
using goodreads.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program { 
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, builder) =>
        {
            var env = context.HostingEnvironment;
            builder.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            // Override config by env, using like Logging:Level or Logging__Level
            .AddEnvironmentVariables();

        })
        .ConfigureLogging((context, logging) => {
            var env = context.HostingEnvironment;
            var config = context.Configuration.GetSection("Logging");
            // ...
            logging.AddConfiguration(config);
            logging.AddConsole();
            // ...
            logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            logging.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
            logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);
        })
        .ConfigureServices((hostContext, services) => {

            IConfiguration configuration = hostContext.Configuration;
            services.AddDbContext<BookContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DbConnectionString"]);
            });
            services.Configure<BookFinderOptions>(hostContext.Configuration.GetSection("BookFinderOptions"));
            services.AddSingleton<ILocalBookFinderService, LocalBookFinderService>();
            services.AddSingleton<IGoodreadsService, GoodreadsService>();
            
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddHostedService<BookWorker>();
        });
}