using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestTask_4A_MVC.Models
{
    public sealed class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Request> Requests { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public IQueryable<Book> GetBooks()
        {
            return Books;
        }

    }
}
