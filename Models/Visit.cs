using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
  
    public class Visit
    {
        public Store store { get; set; }

        public Guid id { get; set; }

        public string no { get; set; }
          
        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string store_owner { get; set; }
        public string store_phone { get; set; }
        public string store_address { get; set; }
        public string store_street { get; set; }
        public string store_ward { get; set; }
        public string store_district { get; set; }
        public string store_province { get; set; }
        public double store_gps_latitude { get; set; }
        public double store_gps_longitude { get; set; }
        public bool store_is_active { get; set; }

        public bool is_opened { get; set; }
        public string feed_back { get; set; }
        public int duration { get; set; }
        public DateTime created_on { get; set; }
        public string created_by_name { get; set; }
        public Order order { get; set; }

        public List<ImageS3> checkin_images { get; set; }
        public List<ImageS3> checkout_images { get; set; }
        public double location_checkin_lat { get; set; }
        public double location_checkin_long { get; set; }
        public double location_checkout_lat { get; set; }
        public double location_checkout_long { get; set; }

    }
}
