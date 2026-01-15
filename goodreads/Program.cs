using GoodreadsBooks.Models;
using GoodreadsBooks.Repository;
using GoodreadsBooks.Repository.DAL;
using GoodreadsBooks.Repository.Interfaces;
using GoodreadsBooks.Resources.Repository;
using GoodreadsBooks.Resources.Repository.Interfaces;
using GoodreadsBooks.Services;
using GoodreadsBooks.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
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
            .AddEnvironmentVariables();

        })
        .ConfigureLogging((context, logging) =>
        {
            var env = context.HostingEnvironment;
            var config = context.Configuration.GetSection("Logging");

            logging.AddConfiguration(config);
            logging.AddConsole();

            logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            logging.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
            logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);
        })
        .ConfigureServices((hostContext, services) =>
        {

            IConfiguration configuration = hostContext.Configuration;
            services.AddDbContext<BookContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DbConnectionString"]);
            });

            if (Boolean.Parse(configuration["Workers:BookWorker"]))
            {
                services.Configure<BookFinderOptions>(hostContext.Configuration.GetSection("BookFinderOptions"));
                services.AddSingleton<ILocalBookFinderService, LocalBookFinderService>();
                services.AddScoped<IBookRepository, BookRepository>();
                services.AddHostedService<BookWorker>();
            }
            if (Boolean.Parse(configuration["Workers:TestWorker"]))
            {
                services.AddScoped<IOpenLibraryBookRepository, OpenLibraryBookRepository>();
                services.AddSingleton<IOpenLibraryService, OpenLibraryService>();
                services.AddHostedService<OpenLibraryWorker>();
            }
            if (Boolean.Parse(configuration["Workers:CardInputWorker"]))
            {
                services.AddScoped<IInputRepository, InputRepository>();
                services.AddSingleton<ICardInputService, CardInputService>();
                services.AddHostedService<CardInputWorker>();
            }
        });
}