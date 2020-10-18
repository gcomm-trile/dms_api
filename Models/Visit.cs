using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
  
    public class Visit
    {
        public Guid id { get; set; }
        public Guid store_id { get; set; }
        public bool is_opened { get; set; }
        public string feed_back { get; set; }
        public int duration { get; set; }
        public DateTime CreatedOn { get; set; }
        public string seq { get; set; }
        public string store_name { get; set; }
        public string user_name { get; set; }
        public Order orders { get; set; }
        public List<ImageS3> checkin_images { get; set; }
        public List<ImageS3> checkout_images { get; set; }
        public double location_checkin_lat { get; set; }
        public double location_checkin_long { get; set; }
        public double location_checkout_lat { get; set; }
        public double location_checkout_long { get; set; }
    
        public double gps_longitude { get; set; }
        public double gps_latitude { get; set; }

    }
}
