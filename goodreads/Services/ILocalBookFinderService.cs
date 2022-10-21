using Test.Models;
using Test.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
{
    public interface ILocalBookFinderService
    {
        Task<(ErrorBuilder errorBuilder, List<Book> addedBooks)> FindAndAddBooks();
    }
}
