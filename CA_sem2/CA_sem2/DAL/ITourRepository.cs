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
        List<Trip> getAllTrips();
        void insertTrip(Trip tr);
        void insertLeg(Leg lg);
        Leg getTripId(int id);
        Trip findTrip(int TripId);
    }
}
