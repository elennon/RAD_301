using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CA_sem2.DAL
{
    
    public class TourContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<GuestLeg> GuestLegs { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public TourContext()
            : base("TourDB")
        {
            Database.SetInitializer(new TripInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class TripInitializer : DropCreateDatabaseAlways<TourContext>
    {
        protected override void Seed(TourContext context)
        {
            var trips = new List<Trip>
            {
            new Trip{ TripName="trip1",FinishLocation="Belfast",StartLocation="Kerry", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
            new Trip{ TripName="GalWay/Dublin",FinishLocation="Galway",StartLocation="Dublin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
            new Trip{ TripName="trip89",FinishLocation="Sligo",StartLocation="Clare", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 788}
            };
            trips.ForEach(s => context.Trips.Add(s));
            context.SaveChanges();

            var legs = new List<Leg>
            {
            new Leg{StartLocation="Galway",Trip=trips[1], FinishLocation= "Athlone", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            new Leg{StartLocation="Athlone",Trip=trips[1], FinishLocation= "Porloise", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            new Leg{StartLocation="Porloise",Trip=trips[1], FinishLocation= "Dublin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
            };
            legs.ForEach(s => context.Legs.Add(s));
            context.SaveChanges();

            Trip t = new Trip();
            foreach (var item in legs)
            {
                t = item.Trip;
                t.LegsColl.Add(item);
            }
            
            context.SaveChanges();

            var guests = new List<Guest>
            {
            new Guest{Name="Joe"},
            new Guest{Name="Bob"},
            new Guest{Name="Mary"},
            new Guest{Name="Helen"},
            };
            guests.ForEach(s => context.Guests.Add(s));
            context.SaveChanges();

            var links = new List<GuestLeg>
            {
            new GuestLeg{LegId=1,GuestId=1},
            new GuestLeg{LegId=1,GuestId=2},
            new GuestLeg{LegId=1,GuestId=3},
            new GuestLeg{LegId=1,GuestId=4},
            new GuestLeg{LegId=2,GuestId=1},
            new GuestLeg{LegId=2,GuestId=2},
            new GuestLeg{LegId=2,GuestId=3},
            new GuestLeg{LegId=2,GuestId=4},
            
            };
            links.ForEach(s => context.GuestLegs.Add(s));
            context.SaveChanges();

            //var lgs = context.Legs;
            //var gts = context.Guests;
            //foreach (var item in lgs)
            //{
            //    foreach (var m in gts)
            //    {
            //        var guestLeg = new GuestLeg { GuestId = m.GuestId, LegId = item.Id };
            //        context.GuestLegs.Add(guestLeg);
            //    }
            //}
            //context.SaveChanges();
        }
    }
}