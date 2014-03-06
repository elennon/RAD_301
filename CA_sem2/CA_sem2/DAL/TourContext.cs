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
            : base("TouDB")
        {
            Database.SetInitializer(new TripInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class TripInitializer : DropCreateDatabaseIfModelChanges<TourContext>
    {
        protected override void Seed(TourContext context)
        {
            var trips = new List<Trip>
            {
            new Trip{ TripName="West Coast Spin",FinishLocation="Malin",StartLocation="West Cork", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
            new Trip{ TripName="GalWay/Dublin",FinishLocation="Galway",StartLocation="Dublin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
            new Trip{ TripName="Iberian",FinishLocation="San Sabastain",StartLocation="Malaga", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 788}
            new Trip{ TripName="Gaul Surf",FinishLocation="Le Harve",StartLocation="Biarritz", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
            new Trip{ TripName="Sligo surf spots",FinishLocation="Grange reefs",StartLocation="Inniscrone", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 788}

            };
            trips.ForEach(s => context.Trips.Add(s));
            context.SaveChanges();

            var legs = new List<Leg>
            {
            new Leg{StartLocation="West Cork",Trip=trips[0], FinishLocation= "BallyBunion", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            new Leg{StartLocation="BallyBunion",Trip=trips[0], FinishLocation= "Lahinch", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            new Leg{StartLocation="Lahinch",Trip=trips[0], FinishLocation= "Galway", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
            new Leg{StartLocation="Galway",Trip=trips[0], FinishLocation= "Sligo", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            new Leg{StartLocation="Sligo",Trip=trips[0], FinishLocation= "Malin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
            };
            legs.ForEach(s => context.Legs.Add(s));
            context.SaveChanges();

            //var legs2 = new List<Leg>
            //{
            //new Leg{StartLocation="Galway",Trip=trips[0], FinishLocation= "BallyBunion", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            //new Leg{StartLocation="BallyBunion",Trip=trips[0], FinishLocation= "Lahinch", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            //new Leg{StartLocation="Lahinch",Trip=trips[0], FinishLocation= "Galway", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
            //new Leg{StartLocation="Galway",Trip=trips[0], FinishLocation= "Sligo", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
            //new Leg{StartLocation="Sligo",Trip=trips[0], FinishLocation= "Malin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
            //};
            //legs.ForEach(s => context.Legs.Add(s));
            //context.SaveChanges();

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