using goodreads.Repository;
using goodreads.Repository.DAL;
using goodreads.Repository.Entities;
using goodreads.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program { 
    private readonly IBookRepository _bookRepository;

    public Program(IBookRepository bookRepository)
    {
        this._bookRepository = bookRepository;
    }


    //entry
    static void Main(string[] args)
    {
        IServiceProvider serviceProvider = RegisterServices();

        Program program = serviceProvider.GetService<Program>();

        program.FindAndAddBooks();

        DisposeServices(serviceProvider);
    }

    public void FindAndAddBooks()
    {
        string[] filePaths = Directory.GetFiles(@"E:\Novels & Books\", "*", SearchOption.AllDirectories);
        foreach (var path in filePaths)
        {
            var fileName = Path.GetFileName(path);
            if (!_bookRepository.GetAll().Any(b => b.Path.Equals(path)))
            {
                Book book = new Book()
                {
                    Name = fileName,
                    Path = path
                };
                _bookRepository.Create(book);
            }
        }
    }

    //Support
    static IServiceProvider RegisterServices()
    {
        var services = new ServiceCollection();

        //repositories
        services.AddDbContext<DbContext, BookContext>(options => options.UseSqlServer("Server=localhost;Database=goodreads_db;Trusted_Connection=True;"));
        services.AddScoped<IBookRepository, BookRepository>();
        //services
        services.AddLogging();
                               
        services.AddScoped<Program>();

        return services.BuildServiceProvider();
    }

    static void DisposeServices(IServiceProvider serviceProvider)
    {
        if (serviceProvider == null)
        {
            return;
        }
        if (serviceProvider is IDisposable sp)
        {
            sp.Dispose();
        }
    }
}