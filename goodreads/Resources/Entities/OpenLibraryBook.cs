using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GoodreadsBooks.Repository.Entities
{
    [Table("OpenLibraryBook")]
    public class OpenLibraryBook
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string TitleSuggest { get; set; }
        public string PublishDate { get; set; }
        [NotMapped]
        public string[] PublishDateData
        {
            get
            {
                return PublishDate.Split(';');
            }
            set
            {
                PublishDate = value != null ? string.Join(";", value) : string.Empty;
            }
        }
        public string Isbn { get; set; }
        [NotMapped]
        public string[] IsbnData
        {
            get
            {
                return Isbn.Split(';');
            }
            set
            {
                Isbn = value != null ? string.Join(";", value) : string.Empty;
            }
        }
        public string Publisher { get; set; }
        [NotMapped]
        public string[] PublisherData
        {
            get
            {
                return Publisher.Split(';');
            }
            set
            {
                Publisher = value != null ? string.Join(";", value) : string.Empty;
            }
        }
        public string Language { get; set; }
        [NotMapped]
        public string[] LanguageData
        {
            get
            {
                return Language.Split(';');
            }
            set
            {
                Language = value != null ? string.Join(";", value) : string.Empty;
            }
        }
        public string AuthorName { get; set; }
        [NotMapped]
        public string[] AuthorNameData
        {
            get
            {
                return AuthorName.Split(';');
            }
            set
            {
                AuthorName = value != null ? string.Join(";", value) : string.Empty;
            }
        }
        public string AuthorAlternativeName { get; set; }
        [NotMapped]
        public string[] AuthorAlternativeNameData
        {
            get
            {
                return AuthorAlternativeName.Split(';');
            }
            set
            {
                AuthorAlternativeName = value != null ? string.Join(";", value) : string.Empty;
            }
        }
        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}
