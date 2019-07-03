using LibraryCursova.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryCursova.Controllers
{
    public class WorkersController : Controller
    {
        BookContext db = new BookContext();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IEnumerable<Seller> sellers = db.Sellers;
            ViewBag.Sellers = sellers;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddWorker()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddWorker(Seller seller)
        {
            db.Sellers.Add(seller);
            db.SaveChanges();
            return Redirect("/Workers/Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditWorker(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Seller seller = db.Sellers.Find(id);
            if (seller != null)
            {
                return View(seller);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditWorker(Seller seller)
        {
            db.Entry(seller).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Workers/Index");
        }
    }
}