using CA_sem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_sem2.DAL
{
    public interface ITourRepository
    {
        IEnumerable<Trip> getAllTrips();
        void insertTrip(Trip tr);
        void insertLeg(Leg lg);
        Leg getLegDets(int id);
        Trip findTrip(int TripId);
        
        IEnumerable<Guest> getGuestList(Leg lg);
        IEnumerable<Guest> getAllGuests();
        bool insertGuest(int gId, int LId);
        IEnumerable<Leg> getLegs();
        Guest getGuest(int gts);
    }
}
