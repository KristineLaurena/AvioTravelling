using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Models
{
    public abstract class ModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}