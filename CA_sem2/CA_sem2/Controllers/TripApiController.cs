using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CA_sem2.Controllers
{
    public class TripApiController : ApiController
    {
        
        private ITourRepository db;

        public TripApiController(ITourRepository repo)
        {
            db = repo;            
        }

        [HttpGET]
        public List<Trip> GetTrips()
        {
            return db.getAllTrips();
        }

        //[HttpGET]
        //public List<Leg> GetLegs()
        //{
        //    return db.getAllTrips();
        //}
    }
}
