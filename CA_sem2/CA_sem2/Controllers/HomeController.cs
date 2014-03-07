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
            ViewBag.Legs = new SelectList(trip.LegsColl, "id", "StartLocation");
            return View(trip);
        }
        
        public ActionResult _ShowLegs(int Id)
        {
            Leg lg = db.getLegDets(Id);
            ViewBag.guests = new SelectList(lg.GuestColl, "GuestId", "Name");
            return PartialView("_ShowLegs", lg);
        }

        public ActionResult _AddGuests(int Id)
        {
            Leg lg = db.getLegDets(Id);
            return PartialView(lg);
        }

        public ActionResult AddGuests(Guest gts, int Id)
        {          
            db.insertGuest(gts, Id);
            return RedirectToAction("EditTrip");
        }
    }
}