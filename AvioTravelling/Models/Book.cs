using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AvioTravelling.Models
{
    [DataContract]
    public class Book
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "pages")]
        public int Pages { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "SmallDescription")]
        public string SmallDescription
        {
            get { return this.Description.Length < 50 ? this.Description.Substring(0, this.Description.Length) :this.Description.Substring(0, 50); }
            set { }
        }

        public bool ShowLargeDescription
        {
            get
            {
                if (this.SmallDescription.Length < this.Description.Length)
                    return true;

                else return false;
            }
        }
    }
}