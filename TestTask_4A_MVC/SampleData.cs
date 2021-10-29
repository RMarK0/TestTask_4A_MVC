using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask_4A_MVC.Models;

namespace TestTask_4A_MVC
{
    /// <summary>
    /// Класс, отвечающий за заполнение базы данных тестовыми данными
    /// </summary>
    public class SampleData
    {
        /// <summary>
        /// Метод, заполняющий базу Books тестовыми данными
        /// </summary>
        /// <param name="context">Контекст данных, содержащий в себе базу данных</param>
        public static void Initialize(BookContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Test",
                        Publisher = "TestPublisher",
                        Author = "Test T.T.",
                        DatePublished = new DateTime(2021, 10, 10),
                        Price = 1000
                    },
                    new Book
                    {
                        Title = "Wow! Book",
                        Publisher = "Publisher #2",
                        Author = "Rybalko Dmitry",
                        DatePublished = new DateTime(2020, 6, 1),
                        Price = 1670
                    },
                    new Book
                    {
                        Title = "4A Consulting Guide",
                        Publisher = "4A Consulting",
                        Author = "Melkov Anatoly",
                        DatePublished = new DateTime(2021, 6, 9),
                        Price = 2000
                    }
                );

                context.SaveChanges();
            }

        }
    }
}
