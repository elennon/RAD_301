using CA_sem2.Migrations;
using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            : base("TourDB222")
        {

           // Database.SetInitializer(new TripInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TourContext, Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //public class TripInitializer : DropCreateDatabaseAlways<TourContext>
    //{
    //    protected override void Seed(TourContext context)
    //    {
    //        var trips = new List<Trip>
    //        {
    //        new Trip{ TripName="West Coast Spin",FinishLocation="Malin",StartLocation="West Cork", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
    //        new Trip{ TripName="GalWay/Dublin",FinishLocation="Galway",StartLocation="Dublin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
    //        new Trip{ TripName="Spanish Hike",FinishLocation="San Sabastain",StartLocation="Malaga", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
    //        new Trip{ TripName="Viva Le France",FinishLocation="Le Harve",StartLocation="Biarritz", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 4},
    //        new Trip{ TripName="Sligo Surfing",FinishLocation="Grange",StartLocation="Inniscrone", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11"), MinGuests= 788}
    //        };
    //        trips.ForEach(s => context.Trips.Add(s));
    //        context.SaveChanges();

    //        var legs = new List<Leg>
    //        {
    //        new Leg{StartLocation="West Cork",Trip=trips[0], FinishLocation= "BallyBunion", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="BallyBunion",Trip=trips[0], FinishLocation= "Lahinch", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Lahinch",Trip=trips[0], FinishLocation= "Galway", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Galway",Trip=trips[0], FinishLocation= "Sligo", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Sligo",Trip=trips[0], FinishLocation= "Malin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
    //        };
    //        legs.ForEach(s => context.Legs.Add(s));
    //        context.SaveChanges();
    //        Trip t = new Trip();
            
    //        foreach (var item in legs)
    //        {
    //            t = item.Trip;
    //            t.LegsColl.Add(item);
    //        }
    //        context.SaveChanges();

    //        var legs2 = new List<Leg>
    //        {
    //        new Leg{StartLocation="Galway",Trip=trips[1], FinishLocation= "Athlone", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Athlone",Trip=trips[1], FinishLocation= "Roscrea", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Roscrea",Trip=trips[1], FinishLocation= "Portloise", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Portloise",Trip=trips[1], FinishLocation= "Naas", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Naas",Trip=trips[1], FinishLocation= "Dublin", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
    //        };
    //        legs2.ForEach(s => context.Legs.Add(s));
    //        context.SaveChanges();
            
    //        foreach (var item in legs2)
    //        {
    //            t = item.Trip;
    //            t.LegsColl.Add(item);
    //        }
    //        context.SaveChanges();

    //        var legs3 = new List<Leg>
    //        {
    //        new Leg{StartLocation="Malaga",Trip=trips[2], FinishLocation= "Cordoba", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Cordoba",Trip=trips[2], FinishLocation= "Toledo", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Toledo",Trip=trips[2], FinishLocation= "Madrid", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Madrid",Trip=trips[2], FinishLocation= "Burgos", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Burgos",Trip=trips[2], FinishLocation= "San Sabastain", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
    //        };
    //        legs3.ForEach(s => context.Legs.Add(s));
    //        context.SaveChanges();
            
    //        foreach (var item in legs3)
    //        {
    //            t = item.Trip;
    //            t.LegsColl.Add(item);
    //        }
    //        context.SaveChanges();

    //        var legs4 = new List<Leg>
    //        {
    //        new Leg{StartLocation="Biarritz",Trip=trips[3], FinishLocation= "Bordeaux", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Bordeaux",Trip=trips[3], FinishLocation= "La Rochelle", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="La Rochelle",Trip=trips[3], FinishLocation= "Nantes", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
    //        };
    //        legs4.ForEach(s => context.Legs.Add(s));
    //        context.SaveChanges();
            
    //        foreach (var item in legs4)
    //        {
    //            t = item.Trip;
    //            t.LegsColl.Add(item);
    //        }
    //        context.SaveChanges();

    //        var legs5 = new List<Leg>
    //        {
    //        new Leg{StartLocation="Inniscrone",Trip=trips[4], FinishLocation= "Easkey", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Easkey",Trip=trips[4], FinishLocation= "Tra Bui", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Tra Bui",Trip=trips[4], FinishLocation= "Strandhill", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Strandhill",Trip=trips[4], FinishLocation= "Blue Rock", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  },
    //        new Leg{StartLocation="Blue Rock",Trip=trips[4], FinishLocation= "Lisslarry", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-11")  }
    //        };
    //        legs5.ForEach(s => context.Legs.Add(s));
    //        context.SaveChanges();
            
    //        foreach (var item in legs5)
    //        {
    //            t = item.Trip;
    //            t.LegsColl.Add(item);
    //        }
    //        context.SaveChanges();
                  
    //        var guests = new List<Guest>
    //        {
    //        new Guest{Name="Joe"},
    //        new Guest{Name="Bob"},
    //        new Guest{Name="Mary"},
    //        new Guest{Name="Helen"},
    //        new Guest{Name="Marmaduke"}
    //        };
    //        guests.ForEach(s => context.Guests.Add(s));
    //        context.SaveChanges();

    //        var allLegs = context.Legs;
    //        for (int i = 1; i < allLegs.Count() + 1; i++)      // added all guests to all legs(as test data)-- all trips will be viable except spanish hike
    //        {
    //            for (int j = 1; j < guests.Count() + 1; i++)
    //            {
    //                var link = new GuestLeg { LegId = i, GuestId = j };
    //                context.GuestLegs.Add(link);
    //            }
    //        }           
    //        context.SaveChanges();
    //    }
    //}
}