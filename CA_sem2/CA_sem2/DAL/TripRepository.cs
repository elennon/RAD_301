using CA_sem2.Controllers;
using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_sem2.DAL
{
    public class TripRepository : ITripRepository
    {
        public TripContext cd;

        public TripRepository()
        {
            cd = new TripContext();
        }

        public List<Trip> getAllTrips()
        {
            return (List<Trip>)cd.Trips.ToList();
        }

        public void insertTrip(Trip tr)
        {
            cd.Trips.Add(tr);
            cd.SaveChanges();           
        }

        public void Dispose()
        {
            cd.Dispose();
        }
    }
}