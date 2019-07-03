using LibraryCursova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryCursova.Controllers
{
    public class BooksController : Controller
    {
        BookContext db = new BookContext();
        // GET: Books

        [Authorize(Roles = "admin, worker")]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, worker")]
        public ActionResult AddBook(Book book)
        {
            db.Books.Add(book);
            book.Count++;
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin, worker")]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin, worker")]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin, worker, user")]
        public ActionResult OrderBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, worker, user")]
        public ActionResult OrderBook(string Name)
        {
            double id=0;
            var books = db.Books.ToList();
            foreach( var b in books)
            {
                if (b.Name == Name)
                {
                    id = b.Id;
                }
            }
            if (id != 0)
            {
                Book book = db.Books.Find(id);
                if (book.Count > 1)
                {
                    book.Count--;
                }
                else
                {
                    db.Books.Remove(book);
                }
                db.SaveChanges();
                return View("Check", book);
            }
            else
            {
                Book b = new Book();
                b.Name = Name;
                return View("Order", b);
            }
        }
    }
}