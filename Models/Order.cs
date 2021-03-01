using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Order
    {
        public string no { get; set; }
        public Guid id { get; set; }

        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }

        public string contact_person { get; set; }
        public string note { get; set; }

        public string created_by_name { get; set; }
        public DateTime created_on { get; set; }

        public bool is_export_stock { get; set; }
        public Guid export_stock_id { get; set; }
        public string export_stock_name { get; set; }

        public List<Product> products { get; set; }
    }
}
