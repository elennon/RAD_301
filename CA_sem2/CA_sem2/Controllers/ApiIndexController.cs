using CA_sem2.DAL;
using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CA_sem2.Controllers
{
    public class ApiIndexController : ApiController
    {
        private ITourRepository db;


        public ApiIndexController(ITourRepository repo)
        {
            db = repo;           
        }

        public IEnumerable<Trip> Get()
        {
            var trips = db.getAllTrips();
            return trips;
        }

        //public string Post([FromBody]string value)
        //{
        //    return PartialView();
        //}
       
        // PUT api/apiindex/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/apiindex/5
        public void Delete(int id)
        {
        }
    }
}
