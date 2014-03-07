﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA_sem2.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        [Display(Name = "Name")]
        public string TripName { get; set; }
        [Display(Name = "Start")]
        public string StartLocation { get; set; }
        [Display(Name = "Finish")]
        public string FinishLocation { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FinishDate { get; set; }
        [Display(Name = "Guests")]
        public int MinGuests { get; set; }

        public virtual List<Leg> LegsColl { get; set; }
    }

    public class Leg
    {
        public int Id { get; set; }
        public Trip Trip { get; set; }
        public string StartLocation { get; set; }
        public string FinishLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TripId { get; set; }

        public virtual List<Guest> GuestColl { get; set; }
    }

    public class GuestLeg
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int LegId { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Leg Leg { get; set; }
    }

    public class Guest
    {
        public int GuestId { get; set; }
        public string Name { get; set; }
    }
}