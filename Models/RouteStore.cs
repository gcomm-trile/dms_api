using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class RouteStore
    {
        public Guid route_id { get; set; }
        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string store_province { get; set; }
        public bool is_active { get; set; }
        public bool is_checked { get; set; }
    }
}
