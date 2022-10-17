using goodreads.Models;
using goodreads.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goodreads.Services
{
    public interface ILocalBookFinderService
    {
        Task<(ErrorBuilder errorBuilder, List<Book> addedBooks)> FindAndAddBooks();
    }
}
