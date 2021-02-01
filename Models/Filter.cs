using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Filter
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public string expressions { get; set; }
        public List<FilterExpression> filter_expressions{ get; set; }
    }
    public class FilterFieldNameValues
    {
        public string field_name { get; set; }
        public List<FilterValue> filter_values { get; set; }
    }
    public class FilterValue
    {
        public string id { get; set; }
        public string value { get; set; }
    }
}
