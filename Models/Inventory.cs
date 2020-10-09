using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Inventory
    {
        public Guid stock_id { get; set; }
        public string stock_name { get; set; }
        public int product_id { get; set; }
        public int current_qty { get; set; }
        public string product_no { get; set; }     
        public string product_name { get; set; }
        public string product_unit { get; set; }
        public string product_description { get; set; }
        public int product_price { get; set; }
        public bool product_is_active { get; set; }
        public string product_image_path { get; set; }

    }
}
