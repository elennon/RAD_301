using CA_sem2.DAL;
using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA_sem2.Controllers
{
    public class HomeController : Controller
    {
        //TripContext db = new TripContext();
        private ITripRepository _repo;

        public HomeController(ITripRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var y = _repo.getAllTrips();
                return View(y);
            
        }
        [HttpPost]
        public ActionResult Create(Trip tr)
        {
            _repo.insertTrip(tr);
            return RedirectToAction("Index");
        }
        public ActionResult create()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
