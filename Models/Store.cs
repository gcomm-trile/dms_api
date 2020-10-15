using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Store
    {
        public string id { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string ward { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public double gps_latitude { get; set; }
        public double gps_longitude { get; set; }
        public List<LocationPoint> store_location { get; set; }
        public bool is_active { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DonViHanhChanh DonViHanhChanh { get; set; }
    }
}
