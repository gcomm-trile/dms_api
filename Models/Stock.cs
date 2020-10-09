using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Stock
    {
        public Guid id { get; set; }
        public string no { get; set; }
        public string name { get; set; }
        public bool is_active { get; set; }
    }
}
