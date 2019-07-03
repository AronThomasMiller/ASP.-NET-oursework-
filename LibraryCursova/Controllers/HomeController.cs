using LibraryCursova.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryCursova.Controllers
{
   
    public class ManagedUsers
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
    }

    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [Authorize(Roles = "admin, worker, user")]
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;
            ViewBag.Books = books;
           
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin, worker, user")]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, worker, user")]
        public ActionResult Buy(double id)
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
            CalculateProfit(book.Price);
            return View("Check", book);
        }

        public void CalculateProfit(double newProfit)
        {
            string path = @"C:\Users\AronThomasMiller\source\repos\LibraryCursova\LibraryCursova\Sources\Profit.txt";
            string text = String.Empty;
            double profit;
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            profit = Double.Parse(text);
            profit += newProfit;
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(profit);
            }
        }
    }
}
