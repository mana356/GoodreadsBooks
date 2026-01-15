using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoodreadsBooks.Resources.Entities;

namespace GoodreadsBooks.Repository.Entities
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime AddedOn { get; set; }
        public bool IsAddedToTest { get; set; } = true;
        public ICollection<OpenLibraryBook> OpenLibraryBooks { get; set; }

    }
}
