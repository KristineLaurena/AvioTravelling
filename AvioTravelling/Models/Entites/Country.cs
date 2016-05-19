using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Models
{
    public class Country : ModelBase
    {
        public string Climate { get; set; }
        public string CountryCode { get; set; }
    }
}