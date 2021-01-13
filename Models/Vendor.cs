using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Vendor
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string person_contact { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
    }
}
