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

        public List<Trip> getAllTrips()
        {
            return (List<Trip>)cd.Trips.ToList();
        }

        public void insertTrip(Trip tr)
        {
            cd.Trips.Add(tr);
            cd.SaveChanges();
        }

        public void insertLeg(Leg lg)
        {
            cd.Legs.Add(lg);
            cd.SaveChanges();
        }

        public Leg getTripId(int id)
        {
            Leg lg = new Leg { Trip = cd.Trips.Find(id) };
            return lg;
        }

        public Trip findTrip(int TripId)
        {
            Trip tr = cd.Trips.Find(TripId);
            return tr;
        }

        //public void seedData()
        //{
        //    var trips = new List<Trip>
        //    {
        //    new Trip{ TripName="trip1", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
        //    new Trip{ TripName="trip2", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4}
        //    };
        //    trips.ForEach(s => cd.Trips.Add(s));
        //    cd.SaveChanges();

        //    var legs = new List<Leg>
        //    {
        //    new Leg{StartLocation="sligo", FinishLocation= "carlow", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
        //    new Leg{StartLocation="clare", FinishLocation= "kerry", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
        //    };
        //    legs.ForEach(s => cd.Legs.Add(s));
        //    cd.SaveChanges();

        //    var guests = new List<Guest>
        //    {
        //    new Guest{Name="Joe"},
        //    new Guest{Name="Bob"},
        //    new Guest{Name="Mary"},
        //    new Guest{Name="Helen"},
        //    };
        //    guests.ForEach(s => cd.Guests.Add(s));
        //    cd.SaveChanges();

        //    var links = new List<GuestLeg>
        //    {
        //    new GuestLeg{LegId=1,GuestId=1},
        //    new GuestLeg{LegId=1,GuestId=2},
        //    new GuestLeg{LegId=1,GuestId=3},
        //    new GuestLeg{LegId=1,GuestId=4},
        //    new GuestLeg{LegId=2,GuestId=1},
        //    new GuestLeg{LegId=2,GuestId=2},
        //    new GuestLeg{LegId=2,GuestId=3},
        //    new GuestLeg{LegId=2,GuestId=4},
            
        //    };
        //    links.ForEach(s => cd.GuestLegs.Add(s));
        //    cd.SaveChanges();

        //    //var lgs = context.Legs;
        //    //var gts = context.Guests;
        //    //foreach (var item in lgs)
        //    //{
        //    //    foreach (var m in gts)
        //    //    {
        //    //        var guestLeg = new GuestLeg { GuestId = m.GuestId, LegId = item.Id };
        //    //        context.GuestLegs.Add(guestLeg);
        //    //    }
        //    //}
        //    //context.SaveChanges();
        //}

        public void Dispose()
        {
            cd.Dispose();
        }
    }
}