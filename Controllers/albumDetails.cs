using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1_mvc4.Controllers
{
    public class albumDetails
    {
        public int Album_id { get; set; }
        public int Artist_id { get; set; }
        public int Genre_id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string AlbumArtUrl { get; set; }
        public string ArtistName { get; set; }
        public int OrderId { get; set; }

        public albumDetails()
        {

        }
    }
}