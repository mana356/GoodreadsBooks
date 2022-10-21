using Test.Repository.Entities;
using Test.Resources.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<InputType> InputTypes {get; set;}
        public DbSet<InputValue> InputValues {get; set;}
        public DbSet<OpenLibraryBook> OpenLibraryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<InputType>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<InputValue>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<OpenLibraryBook>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

        }
    }
}

