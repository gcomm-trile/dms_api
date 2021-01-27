using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Filter
    {
        public string filter_name { get; set; }

        public string expressions { get; set; }
        public List<FilterExpression> filter_expressions{ get; set; }
    }
}
