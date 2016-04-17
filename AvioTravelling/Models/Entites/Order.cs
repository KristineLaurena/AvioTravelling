using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Models
{
    public class Order //: ModelBase
    {
        public int Id { get; set; }
        public ApplicationUser Customer { get; set; }
        public Decimal Total { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public City City { get; set; }


        public void CalculateTotal()
        {
            var journeyLength = this.To - this.From;
            this.Total = this.City.PricePerDay * journeyLength.Days;            
        }
    }
}