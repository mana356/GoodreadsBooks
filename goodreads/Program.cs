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
        Program.FindAndAddBooks();

    }

    public static void FindAndAddBooks()
    {
        string[] filePaths = Directory.GetFiles(@"E:\Novels & Books\", "*", SearchOption.AllDirectories);
        using (var context = new BookContext())
        {
            foreach (var path in filePaths)
            {
                var fileName = Path.GetFileName(path);
                string ext = Path.GetExtension(path);
                if (!context.Books.Any(b => b.Path.Equals(path)))
                {
                    Book book = new Book()
                    {
                        Name = fileName,
                        Path = path,
                        Author = "NA",
                        Isbn = "NA",
                        Extension = ext,
                        CreatedOn = DateTime.Now
                    };
                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }           
            
        }
           
    }   
}