using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Models
{
    public class Showplace : ModelBase
    {
        public string LongDescription { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }

    }
}