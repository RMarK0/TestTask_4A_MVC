using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TestTask_4A_MVC.Models;

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
        public IActionResult Read(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Read(Request request)
        {
            db.Requests.Add(request);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + request.User + ", за заявку!";
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
    }
}
