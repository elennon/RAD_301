using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_sem2.DAL
{
    public class TourRepository : ITourRepository
    {
        public TourContext cd;

        public TourRepository()
        {
            cd = new TourContext();
        }

        //public List<Trip> getAllTrips()
        //{
        //    return (List<Trip>)cd.Trips.ToList();
        //}
        public IEnumerable<Trip> getAllTrips()
        {
            cd.Configuration.ProxyCreationEnabled = false;

            //var trp = cd.Trips.Find(id);
            var lgs = cd.Trips;
            return lgs;
        }


        public void insertTrip(Trip tr)
        {
            cd.Trips.Add(tr);
            cd.SaveChanges();
        }

        public void insertLeg(Leg lg)
        {
            cd.Legs.Add(lg);
           // var trp = cd.Trips.Find(lg.TripId);
           // trp.LegsColl.Add(lg);
            cd.SaveChanges();
        }

        

        public Trip findTrip(int TripId)
        {
            Trip tr = cd.Trips.Find(TripId);
            return tr;
        }

        public Leg getLegDets(int Id)
        {
            Leg lg = cd.Legs.Find(Id);
            return lg;
        }

        public IEnumerable<Guest> getGuestList(Leg lg)
        {
            var guests = from a in cd.Guests
                         join b in cd.GuestLegs on a.GuestId equals b.GuestId
                         join c in cd.Legs on b.LegId equals c.Id
                         where c.Id == lg.Id
                         select a;
            return guests;
        }

        public IEnumerable<Guest> getAllGuests()
        {
            var guests = cd.Guests;
            return guests;
        }

        public bool insertGuest(int gId, int legId)
        {
            var checkIfThereAlready = cd.GuestLegs.Where(a => a.GuestId == gId && a.LegId == legId).Count();
            if (checkIfThereAlready == 0)
            {
                GuestLeg gl = new GuestLeg { GuestId = gId, LegId = legId };
                cd.GuestLegs.Add(gl);
                cd.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public IEnumerable<Leg> getLegs()
        {
            cd.Configuration.ProxyCreationEnabled = false;
            
            //var trp = cd.Trips.Find(id);
            var lgs = cd.Legs;
            return lgs;
        }

        public Guest getGuest(int gts)
        {
            return cd.Guests.Find(gts);
        }

        public void Dispose()
        {
            cd.Dispose();
        }
    }
}