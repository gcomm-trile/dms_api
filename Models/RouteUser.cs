using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class RouteUser
    {
        public Guid route_id { get; set; }
        public Guid user_id { get; set; }
        public string user_name { get; set; }
        public string full_name { get; set; }
        public bool is_checked { get; set; }
    }
}
