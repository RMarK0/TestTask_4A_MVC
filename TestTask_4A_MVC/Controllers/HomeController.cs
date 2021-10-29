using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using TestTask_4A_MVC.Models;
using static TestTask_4A_MVC.Program;

namespace TestTask_4A_MVC.Controllers
{
    /// <summary>
    /// Контроллер, который дает доступ к методам взаимодействия с моделью при помощи видов.
    /// </summary>
    public class HomeController : Controller
    {
        BookContext db;
        /// <summary>
        /// Конструктор контроллера. Прикрепляет контекст данных.
        /// </summary>
        /// <param name="context">Контекст данных для прикрепления к контроллеру</param>
        public HomeController(BookContext context)
        {
            db = context;
        }

        /// <returns>Вид Index, выдающий базу книг в виде List</returns>
        public IActionResult Index()
        {
            return View(db.Books.ToList());
        }


        /// <summary>
        /// GET-метод для добавления новой книги
        /// </summary>
        /// <returns>Вид Add</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// POST-метод для сохранения добавленной книги
        /// </summary>
        /// <param name="book">Книга, которую надо добавить в БД</param>
        /// <returns>Сообщение об успешном добавлении книги</returns>
        [HttpPost]
        public string Add(Book book)
        {
            try
            {
                book.DatePublished = DateTime.Now;
                db.Books.Add(book);
                db.SaveChanges();
                return "Спасибо за добавление!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// GET-метод для удаления книги
        /// </summary>
        /// <returns>Вид Delete</returns>
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// POST-метод для удаления книги и сохранения БД после этого
        /// </summary>
        /// <param name="id">Id книги, которую необходимо удалить</param>
        /// <returns>Сообщение об успешном удалении, либо сообщение о несуществующем Id</returns>
        [HttpPost]
        public string Delete(int? id)
        {
            if (id == null)
            {
                return "id was null";
            }

            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return $"Book with id == {id} was successfully deleted";
        }

        /// <summary>
        /// Метод для вывода информации о книге
        /// </summary>
        /// <param name="id">Id книги, информацию о которой надо вывести</param>
        /// <returns>Вид Details с данными о книге, либо вид NotFound с ответом Status404NotFound</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }

        /// <summary>
        /// GET-метод для редактирования информации о книге
        /// </summary>
        /// <param name="id">Id книги, информацию которой надо редактировать</param>
        /// <returns>Вид Edit с полями для редактирования, либо вид NotFound с ответом Status404NotFound</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }

        /// <summary>
        /// POST-метод для сохранения отредактированной информации 
        /// </summary>
        /// <param name="book">Книга, информацию о которой надо редактировать</param>
        /// <returns>Вид Index без какого-либо сообщения</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод, очищающий базу данных. Если база данных на момент закрытия приложения будет пуста, то
        /// при следующем запуске приложение само создаст 3 тестовых книги, исходя из логики, описанной в SampleData.cs
        /// </summary>
        /// <returns>Сообщение об успешной очистке данных</returns>
        public string ClearDB()
        {
            try
            {
                foreach (var entity in db.Books)
                    db.Books.Remove(entity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return $"База данных успешно очищена";
        }

        /// <summary>
        /// Метод, отвечающий за экспорт БД в XML файл, в путь C:\Программа\file.xml
        /// </summary>
        /// <returns>Сообщение об успешном экспорте базы данных, либо Exception</returns>
        public string ExportToXML()
        {
            string path;
            try
            {
                Type[] types = { typeof(Book) };

                var aElements = db.GetBooks().ToList();

                XmlSerializer writer = new XmlSerializer(aElements.GetType());

                path = "C:\\Программа\\file.xml";
                
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    writer.Serialize(fs, aElements);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return $"База данных успешно выгружена в XML-файл. Path - {path}";
        }
    }
}
