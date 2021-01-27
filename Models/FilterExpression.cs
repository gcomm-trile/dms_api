using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class FilterExpression
    {
        public string expression { get; set; }
        public string field_name { get; set; }
        public string logic { get; set; }
        public string value { get; set; }
       
    }
}
