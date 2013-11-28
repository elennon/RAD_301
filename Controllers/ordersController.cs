using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment1_mvc4.Models;

namespace Assignment1_mvc4.Controllers
{
    public class ordersController : Controller
    {
        private ordersContext db = new ordersContext();
        MvcMusicStoreEntities cd = new MvcMusicStoreEntities();
        //
        // GET: /orders/

        public ActionResult Index(string orderName)
        {
            var aa = cd.Orders.Where(a => a.FirstName.Contains(orderName));
                
            return View("ordersByDate",aa);
            //return View(db.Orders.ToList());
        }

        public ActionResult sortByDate()
        {
            var ord = cd.Orders.OrderBy(a => a.OrderDate);
            return View("ordersByDate",ord);
        }
        public ActionResult sortByValue()
        {
            var ord = cd.Orders.OrderBy(a => a.Total);
            return View("ordersByDate", ord);
        }

        public ActionResult getAlbums(int id = 0) //
        {
            List<albumDetails> albumPerOreder = new List<albumDetails>();
            var al = from a in cd.Albums
                     join b in cd.OrderDetails on a.AlbumId equals b.AlbumId
                     where b.OrderId == id
                     select a;
            
            foreach (var m in al)
	        {
                var artist = cd.Artists.Select(a => a).Where(n => n.ArtistId == m.ArtistId).SingleOrDefault();
                albumDetails albumPicked = new albumDetails { Album_id = m.AlbumId, OrderId=id, AlbumArtUrl = m.AlbumArtUrl, Artist_id = m.ArtistId, Genre_id = m.GenreId, Price = m.Price, Title = m.Title, ArtistName = artist.Name };
                albumPerOreder.Add(albumPicked);
	        }
            
            return View("ArtistDetails", albumPerOreder);
        }
        // GET: /orders/Details/5

        public ActionResult Details(int id = 0)
        {
            //Order order = db.Orders.Find(id);
            var order = cd.Orders.Select(a => a).Where(n => n.OrderId == id).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // GET: /orders/Create

        public ActionResult Create()
        {
            return View("createView");
        }

        //
        // POST: /orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                cd.Orders.Add(order);
                cd.SaveChanges();
                
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        //
        // GET: /orders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = cd.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View("editView",order);
        }

        //
        // POST: /orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                cd.Entry(order).State = EntityState.Modified;
                cd.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

        //
        // GET: /orders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            // order = db.Orders.Find(id);
            var t = cd.Orders.Select(a => a).Where(n => n.OrderId == id).SingleOrDefault();
          
            if (t == null)
            {
                return HttpNotFound();
            }
            return View("delTest",t);
        }

        //
        // POST: /orders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = cd.Orders.Find(id);
            cd.Orders.Remove(order);
            cd.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            cd.Dispose();
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}