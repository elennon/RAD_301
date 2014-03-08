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
            double fullTrip = (trip.FinishDate - trip.StartDate).Days;
            Leg g = trip.LegsColl[trip.LegsColl.Count() - 1];
            double doneSoFar = (((trip.LegsColl[trip.LegsColl.Count() - 1]).FinishDate) - trip.StartDate).Days;
            ViewBag.progress = Convert.ToInt32((doneSoFar / fullTrip) * 100);
            ViewBag.Legs = new SelectList(trip.LegsColl, "id", "StartLocation");
            return View(trip);
        }
        
        public ActionResult _ShowLegs(int Id)
        {
            Leg lg = db.getLegDets(Id);
            ViewBag.guests = new SelectList(db.getGuestList(lg), "GuestId", "Name");
            return PartialView("_ShowLegs", lg);
        }

        public ActionResult _AddGuests(int Id)
        {
            ViewBag.LegId = Id;
            ViewBag.allGuests = new SelectList(db.getAllGuests(), "GuestId", "Name", Id);
            return PartialView();
        }

        public ActionResult AddGuest(int gts, int id)
        {
            Leg lg = db.getLegDets(id);
            db.insertGuest(gts, id);
            ViewBag.guests = new SelectList(db.getGuestList(lg), "GuestId", "Name");
            ViewBag.LegId = id;
            ViewBag.allGuests = new SelectList(db.getAllGuests(), "GuestId", "Name");
            return PartialView("_AddGuests");
            //return PartialView("_ShowLegs", lg);
        }
    }
}