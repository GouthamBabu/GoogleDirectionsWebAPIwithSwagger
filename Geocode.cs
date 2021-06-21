using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Geocode
    {
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public bool PartialMatch { get; set; }
        public string PlaceId { get; set; }
    }
    public class Geometry
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
