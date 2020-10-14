using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Report_Activity
    {
        public DateTime updated_on { get; set; }
        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string user_fullname { get; set; }
        public string province { get; set; }
        public Guid user_id { get; set; }
        public int action_type { get; set; }
        public string content { get; set; }
        public Guid visit_id { get; set; }
    }
}
