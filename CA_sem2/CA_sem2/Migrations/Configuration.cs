namespace CA_sem2.Migrations
{
    using CA_sem2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CA_sem2.DAL.TourContext> 
    {
        public Configuration()
        {

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CA_sem2.DAL.TourContext context) 
        {
            foreach (GuestLeg item in context.GuestLegs)        // added this to ensure no duplication of data
            {
                context.GuestLegs.Remove(item);
            }
            foreach (Trip item in context.Trips)
            {
                context.Trips.Remove(item);
            }
            foreach (Leg item in context.Legs)
            {
                context.Legs.Remove(item);
            }
            foreach (Guest item in context.Guests)
            {
                context.Guests.Remove(item);
            }
                
                var trips = new List<Trip>
                {
                new Trip{ TripName="West Coast Spin",FinishLocation="Malin",StartLocation="West Cork", StartDate=DateTime.Parse("2014-06-01"), FinishDate=DateTime.Parse("2014-08-21"), MinGuests= 4},
                new Trip{ TripName="GalWay/Dublin",FinishLocation="Galway",StartLocation="Dublin", StartDate=DateTime.Parse("2015-09-01"), FinishDate=DateTime.Parse("2015-11-21"), MinGuests= 4},
                new Trip{ TripName="Spanish Hike",FinishLocation="San Sabastain",StartLocation="Malaga", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-11-21"), MinGuests= 4},
                new Trip{ TripName="Viva Le France",FinishLocation="Le Harve",StartLocation="Biarritz", StartDate=DateTime.Parse("2015-09-01"), FinishDate=DateTime.Parse("2015-11-21"), MinGuests= 4},
                new Trip{ TripName="Sligo Surfing",FinishLocation="Grange",StartLocation="Inniscrone", StartDate=DateTime.Parse("2014-08-01"), FinishDate=DateTime.Parse("2014-10-04"), MinGuests= 788}
                };
                    trips.ForEach(s => context.Trips.AddOrUpdate(s));
                    context.SaveChanges();

                    var legs = new List<Leg>
                {
                new Leg{StartLocation="West Cork",Trip=trips[0], FinishLocation= "BallyBunion", StartDate=DateTime.Parse("2014-06-01"), FinishDate=DateTime.Parse("2014-06-21")  },
                new Leg{StartLocation="BallyBunion",Trip=trips[0], FinishLocation= "Lahinch", StartDate=DateTime.Parse("2014-06-21"), FinishDate=DateTime.Parse("2014-07-01")  },
                new Leg{StartLocation="Lahinch",Trip=trips[0], FinishLocation= "Galway", StartDate=DateTime.Parse("2014-07-01"), FinishDate=DateTime.Parse("2014-07-21")  },
                new Leg{StartLocation="Galway",Trip=trips[0], FinishLocation= "Sligo", StartDate=DateTime.Parse("2014-07-21"), FinishDate=DateTime.Parse("2014-08-01")  },
                new Leg{StartLocation="Sligo",Trip=trips[0], FinishLocation= "Malin", StartDate=DateTime.Parse("2014-08-01"), FinishDate=DateTime.Parse("2014-08-21")  }
                };
                    legs.ForEach(s => context.Legs.AddOrUpdate(s));
                    context.SaveChanges();
                    Trip t = new Trip();

                    var legs2 = new List<Leg>
                {
                new Leg{StartLocation="Galway",Trip=trips[1], FinishLocation= "Athlone", StartDate=DateTime.Parse("2015-09-01"), FinishDate=DateTime.Parse("2015-09-21")  },
                new Leg{StartLocation="Athlone",Trip=trips[1], FinishLocation= "Roscrea", StartDate=DateTime.Parse("2015-09-21"), FinishDate=DateTime.Parse("2015-10-01")  },
                new Leg{StartLocation="Roscrea",Trip=trips[1], FinishLocation= "Portloise", StartDate=DateTime.Parse("2015-10-01"), FinishDate=DateTime.Parse("2015-10-21")  },
                new Leg{StartLocation="Portloise",Trip=trips[1], FinishLocation= "Naas", StartDate=DateTime.Parse("2015-10-21"), FinishDate=DateTime.Parse("2015-11-01")  }               
                };
                    legs2.ForEach(s => context.Legs.AddOrUpdate(s));
                    context.SaveChanges();

                    var legs3 = new List<Leg>
                {
                new Leg{StartLocation="Malaga",Trip=trips[2], FinishLocation= "Cordoba", StartDate=DateTime.Parse("2014-09-01"), FinishDate=DateTime.Parse("2014-09-21")  },
                new Leg{StartLocation="Cordoba",Trip=trips[2], FinishLocation= "Toledo", StartDate=DateTime.Parse("2014-09-21"), FinishDate=DateTime.Parse("2014-10-01")  },
                new Leg{StartLocation="Toledo",Trip=trips[2], FinishLocation= "Madrid", StartDate=DateTime.Parse("2014-10-01"), FinishDate=DateTime.Parse("2014-10-21")  },
                new Leg{StartLocation="Madrid",Trip=trips[2], FinishLocation= "Burgos", StartDate=DateTime.Parse("2014-10-21"), FinishDate=DateTime.Parse("2014-11-01")  },
                new Leg{StartLocation="Burgos",Trip=trips[2], FinishLocation= "San Sabastain", StartDate=DateTime.Parse("2014-11-01"), FinishDate=DateTime.Parse("2014-11-21")  }
                };
                    legs3.ForEach(s => context.Legs.AddOrUpdate(s));
                    context.SaveChanges();

                    var legs4 = new List<Leg>
                {
                new Leg{StartLocation="Biarritz",Trip=trips[3], FinishLocation= "Bordeaux", StartDate=DateTime.Parse("2015-09-01"), FinishDate=DateTime.Parse("2015-09-21")  },
                new Leg{StartLocation="Bordeaux",Trip=trips[3], FinishLocation= "La Rochelle", StartDate=DateTime.Parse("2015-09-21"), FinishDate=DateTime.Parse("2015-10-01")  },
                new Leg{StartLocation="La Rochelle",Trip=trips[3], FinishLocation= "Nantes", StartDate=DateTime.Parse("2015-10-01"), FinishDate=DateTime.Parse("2015-10-21")  }
                };
                    legs4.ForEach(s => context.Legs.AddOrUpdate(s));
                    context.SaveChanges();

                    var legs5 = new List<Leg>
                {
                new Leg{StartLocation="Inniscrone",Trip=trips[4], FinishLocation= "Easkey", StartDate=DateTime.Parse("2014-08-01"), FinishDate=DateTime.Parse("2014-08-11")  },
                new Leg{StartLocation="Easkey",Trip=trips[4], FinishLocation= "Tra Bui", StartDate=DateTime.Parse("2014-08-11"), FinishDate=DateTime.Parse("2014-08-23")  },
                new Leg{StartLocation="Tra Bui",Trip=trips[4], FinishLocation= "Strandhill", StartDate=DateTime.Parse("2014-08-23"), FinishDate=DateTime.Parse("2014-09-03")  },
                new Leg{StartLocation="Strandhill",Trip=trips[4], FinishLocation= "Blue Rock", StartDate=DateTime.Parse("2014-09-03"), FinishDate=DateTime.Parse("2014-09-19")  },
                new Leg{StartLocation="Blue Rock",Trip=trips[4], FinishLocation= "Lisslarry", StartDate=DateTime.Parse("2014-09-19"), FinishDate=DateTime.Parse("2014-10-04")  }
                };
                    legs5.ForEach(s => context.Legs.AddOrUpdate(s));
                    context.SaveChanges();

                var guests = new List<Guest>
                {
                new Guest{Name="Joe"},
                new Guest{Name="Bob"},
                new Guest{Name="Mary"},
                new Guest{Name="Helen"},
                new Guest{Name="Marmaduke"},
                new Guest{Name="Jose"},
                new Guest{Name="Franko"},
                new Guest{Name="Fred"},
                new Guest{Name="Sacha"},
                new Guest{Name="Johnny"}
                };
                guests.ForEach(s => context.Guests.AddOrUpdate(s));
                context.SaveChanges();
                List<Leg> lgs = new List<Leg>();
                List<Leg> lgs2 = new List<Leg>();
                var allLegs = context.Legs.ToList();
                var allGuests = context.Guests.ToList();
                for (int i = 0; i < allLegs.Count(); i++)
                {
                    if (i < allLegs.Count() / 2)
                    { 
                        lgs.Add(allLegs[i]);
                    }
                    else
                        lgs2.Add(allLegs[i]);
                }
                Random rnd = new Random();
                foreach (Leg al in lgs)        
                {
                    foreach (Guest ag in allGuests)
                    {
                        var lnk = new GuestLeg { LegId = al.Id, GuestId = ag.GuestId };
                        context.GuestLegs.AddOrUpdate(lnk);
                    }
                }
                context.SaveChanges();

                foreach (Leg al in lgs2)
                {
                    int rNo = rnd.Next(0, allGuests.Count());
                    for (int i = 0; i < rNo; i++)
                    {
                        var lnk = new GuestLeg { LegId = al.Id, GuestId = allGuests[i].GuestId };
                        context.GuestLegs.AddOrUpdate(lnk);
                    }
                }
                context.SaveChanges();

                foreach (var trip in trips)     // set if complete / valid
                {
                    if (trip.LegsColl[trip.LegsColl.Count() - 1].FinishDate == trip.FinishDate)
                        trip.complete = true;
                    else
                        trip.complete = false;
                    int moreThan2 = 0;                          // check if at least 2 legs have guests
                    int allGuestsCount = 0;                     // count all guests for this trip
                    foreach (Leg item in trip.LegsColl)
                    {
                        var guestCount = context.GuestLegs.Where(a => a.LegId == item.Id).Select(n => n.GuestId).Count();
                        if (guestCount > 0) moreThan2++;
                        allGuestsCount += guestCount;
                    }
                    if (moreThan2 >= 2 && allGuestsCount >= trip.MinGuests)
                        trip.VStatus = ViaStatus.valid;
                    else
                    { trip.VStatus = ViaStatus.invalid; }

                    context.SaveChanges();
                }
        }
    }
}
