using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CA_sem2.DAL
{
    public class TripContext:DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<GuestLeg> GuestLegs { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public TripContext(): base("TripDB")           
        {
            Database.SetInitializer(new TripInitializer());           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class TripInitializer : DropCreateDatabaseIfModelChanges<TripContext>
    {
        protected override void Seed(TripContext context)
        {
            var trips = new List<Trip>
            {
            new Trip{ TripName="trip1", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
            new Trip{ TripName="trip2", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4}
            };
            trips.ForEach(s => context.Trips.Add(s));
            context.SaveChanges();

            var legs = new List<Leg>
            {
            new Leg{StartLocation="sligo", FinishLocation= "carlow", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            new Leg{StartLocation="clare", FinishLocation= "kerry", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
            };
            legs.ForEach(s => context.Legs.Add(s));
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