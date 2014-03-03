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
        //TourContext db = new TourContext();
        private ITourRepository db;

        public HomeController(ITourRepository repo)
        {
            db = repo;
        }

        public ActionResult Index()
        {
            var y = db.getAllTrips();
            ViewBag.trips = new SelectList(y, "TripId", "TripName");

            return View(y);
        }

        [HttpPost]
        public ActionResult Create(Trip tr)
        {
            db.insertTrip(tr);
            return RedirectToAction("Index");
        }

        public ActionResult _Create()
        {
            //return View("Create");
            return PartialView();
        }

        public ActionResult AddLeg(Leg lg)
        {
            db.insertLeg(lg);
            return RedirectToAction("Index");
        }

        public ActionResult _AddLeg(int id)
        {
            Leg lg = db.getTripId(id);
            return PartialView("_AddLeg", lg);
        }

        public ActionResult EditTrip(int TripId)
        {
            Trip trip = db.findTrip(TripId);
            return View(trip);
        }

    }
}