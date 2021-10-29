using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestTask_4A_MVC.Models
{
    /// <summary>
    /// Класс, описывающий поведение контекста данных. Содержит 2 базы данных - Books, Requests.
    /// </summary>
    public sealed class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Request> Requests { get; set; }

        /// <summary>
        /// Конструктор, позволяющий при инициализации контекста удостовериться в том, что база создана.
        /// </summary>
        /// <param name="options"></param>
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Метод, позволяющий удобно конвертировать Books в IQueryable объект
        /// </summary>
        /// <returns>DbSet Books в качестве IQueryable объекта</returns>
        public IQueryable<Book> GetBooks()
        {
            return Books;
        }

    }
}
