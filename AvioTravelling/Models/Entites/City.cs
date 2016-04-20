using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Models
{
    public class City : ModelBase
    {
        public string PictureLink { get; set; }
        public Country Country { get; set; }
        public List<Showplace> Showplaces { get; set; }
        public string Description { get; set; } 

        public decimal PricePerDay { get; set; }

        public City()
        {
            this.Showplaces = new List<Showplace>();
            this.Country = new Country();
        }
    }
}