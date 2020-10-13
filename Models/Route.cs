using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Route
    {
   
        public Guid id { get; set; }

    
        public string name { get; set; }

        public string province { get; set; }


        public bool is_week1 { get; set; }

        public bool is_week2 { get; set; }

        public bool is_week3 { get; set; }

        public bool is_week4 { get; set; }

        public List<RouteStore> Stores { get; set; }

        public List<RouteUser> Users { get; set; }

        public DonViHanhChanh DonViHanhChanh { get; set; }
    }
}
