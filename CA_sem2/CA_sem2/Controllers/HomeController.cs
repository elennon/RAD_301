using CA_sem2.DAL;
using CA_sem2.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CA_sem2.Controllers
{
    public class HomeController : Controller
    {
        //TourContext db = new TourContext();
        private ITourRepository db;
        HttpClient client;

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
        public JsonResult Create(Trip tr)
        {
            if (tr.TripName == null) return Json(new { hasError = true, message = "You must enter a name for this trip!" }, JsonRequestBehavior.AllowGet);
            else if (tr.StartLocation == null) return Json(new { hasError = true, message = "You must enter a start location!" }, JsonRequestBehavior.AllowGet);
            else if (tr.FinishLocation == null) return Json(new { hasError = true, message = "You must enter a finish location!" }, JsonRequestBehavior.AllowGet);
            else if (tr.StartDate.ToShortDateString() == "01/01/0001") return Json(new { hasError = true, message = "You must enter a start date!" }, JsonRequestBehavior.AllowGet);
            else if (tr.FinishDate.ToShortDateString() == "01/01/0001") return Json(new { hasError = true, message = "You must enter a finish date!" }, JsonRequestBehavior.AllowGet);
            else if (tr.StartDate  > tr.FinishDate) return Json(new { hasError = true, message = "You must start before you finish!" }, JsonRequestBehavior.AllowGet);
            else if (tr.StartDate > DateTime.Now) return Json(new { hasError = true, message = "You must enter a date in the future!" }, JsonRequestBehavior.AllowGet);
            else if (tr.MinGuests == 0) return Json(new { hasError = true, message = "You must enter a minnimum number of guests!" }, JsonRequestBehavior.AllowGet);
            else
            {
                try
                {
                    db.insertTrip(tr);
                    return Json(new { hasError = false, message = "All went well - new trip created" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { hasError = true, message = "error: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult _Create()
        {
            //return View("Create");
            return PartialView();
        }

        public JsonResult AddLeg(Leg lg)
        {
            var trp = db.findTrip(lg.TripId);
            if (lg.FinishDate > trp.FinishDate)
            {
                ViewBag.resultMessage = "Finish date can't be later than trip finish date";
                return Json(new { hasError = true, message = "Finish date can't be later than trip finish date" },
                    JsonRequestBehavior.AllowGet);                           
            }
            else if (lg.FinishLocation == "" || lg.FinishLocation == null) return Json(new { hasError = true, message = "You must enter a finish location!" }, JsonRequestBehavior.AllowGet);
            else if (lg.FinishDate == null || lg.FinishDate.ToShortDateString() != "01/01/0001") return Json(new { hasError = true, message = "You must enter a finish date!" }, JsonRequestBehavior.AllowGet);
            else
            {
                try
                {
                    db.insertLeg(lg);
                    return Json(new { hasError = false, message = "All went well - Leg added" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { hasError = true, message = "error: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }        
            }
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

        public async Task<ActionResult> _ShowLegs(int Id)  //http://en.wikipedia.org/w/api.php?action=query&prop=revisions&rvprop=content&format=xml&titles=sligo
        {
           
            client = new HttpClient();
            //client.BaseAddress = new Uri("http://it.wikipedia.org/w/api.php?");
            client.BaseAddress = new Uri("http://en.wikipedia.org/w/api.php?");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));

            Leg lg = db.getLegDets(Id);
            //string getUri = "action=query&prop=images&format=xml&titles=sligo";
            //var response = client.GetAsync(getUri).Result;
            var response = await client.PostAsync("http://en.wikipedia.org/w/api.php?action=query&prop=images&format=xml&titles=" + lg.StartLocation, null);
            //var response = await client.PostAsync(getUri, null);
            response.EnsureSuccessStatusCode(); 
            var xmlString = response.Content.ReadAsStringAsync().Result;
            XDocument doc = XDocument.Parse(xmlString);
            //var pic = doc.Root.Elements().Select(a => a.Element("Item")).FirstOrDefault().Value;
            //var pics = doc.Root.Elements().Select(x => x.Element("images"));
            var title =  from e in doc.Descendants("page")
                        select new {
                            Title = (string)e.Attribute("title")                   
                        };
            var pics = from e in doc.Descendants("images").Elements("im").Attributes("title") 
                       select e;
            string picFile = "";
            foreach (var item in pics)
            {
                if (item.Value.Contains(".jpg") && item.Value.Contains(lg.StartLocation)) 
                {
                    //string s = item.Value.Replace(" ", "%");
                    string s = item.Value.Replace("File", "");
                    picFile = s; 
                }
            }
            if (picFile == "")
            {
                picFile = "http://upload.wikimedia.org/wikipedia/commons/a/ab/Benbulbenmount.jpg";
                ViewBag.pic = picFile;
                ViewBag.guests = new SelectList(db.getGuestList(lg), "GuestId", "Name");
                return PartialView("_ShowLegs", lg);
            }

            var response2 = await client.PostAsync("http://en.wikipedia.org/w/api.php?action=query&titles=Image" + picFile + "&prop=imageinfo&iiprop=url&format=xml", null);
            //var response2 = await client.PostAsync("http://en.wikipedia.org/w/api.php?action=query&titles=Image:North%20Sligo%20Town.jpg&prop=imageinfo&iiprop=url&format=xml", null);
            response2.EnsureSuccessStatusCode();
            var xmlString2 = response2.Content.ReadAsStringAsync().Result;
            XDocument doc2 = XDocument.Parse(xmlString2);

            var picUrl = (from e in doc2.Descendants("imageinfo").Elements("ii").Attributes("url")
                            select e).FirstOrDefault();

            ViewBag.pic = picUrl.Value; 

            ViewBag.guests = new SelectList(db.getGuestList(lg), "GuestId", "Name");
            return PartialView("_ShowLegs", lg);
        }

        public ActionResult _AddGuests(int Id)
        {
            ViewBag.LegId = Id;
            ViewBag.allGuests = new SelectList(db.getAllGuests(), "GuestId", "Name", Id);
            return PartialView();
        }

        public JsonResult AddGuest(int gts, int id)
        {
            if(ModelState.IsValid)
            {
                db.insertGuest(gts, id);
                return Json(new { hasError = false, message = "All went well - guest added" },
                     JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { hasError = true, message = "Failed to insert guest" },
                    JsonRequestBehavior.AllowGet);
        }
    }
}