using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Models
{
    public class Tour : ModelBase
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Country Country { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public TimeSpan Length
        {
            get
            {
                return this.DateTo - this.DateFrom;
            }
        }
    }
}