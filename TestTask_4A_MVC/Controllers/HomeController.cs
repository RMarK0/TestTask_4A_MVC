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
    public class HomeController : Controller
    {
        BookContext db;
        public HomeController(BookContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Books.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(Book book)
        {
            book.DatePublished = DateTime.Now;
            db.Books.Add(book);
            db.SaveChanges();
            return "Спасибо за добавление!";
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

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

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                {
                    return View(book);
                }
            }
            return NotFound();
        }

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
        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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

        public string ExportToXML()
        {
            string path = "";
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
