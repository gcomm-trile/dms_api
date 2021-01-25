using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Product
    {     
        public int id { get; set; }    
        public string no { get; set; }   
        public string name { get; set; }    
        public string unit { get; set; }
        public string description { get; set; }      
        public Int64 price_sell { get; set; }
        public Int64 price_order { get; set; }
        public bool is_active { get; set; }
        public int qty_order { get; set; }
        public int qty_imported { get; set; }
        public int qty_remaining { get; set; }     
        public int qty_out { get; set; }
        public int qty_in { get; set; }

        public Int64 total_price_avg { get; set; }
        public string image_path { get; set; }
        public Guid stock_id { get; set; }
        public string stock_name { get; set; }
        public int qty_current_stock { get; set; }
        public int qty_stock_in { get; set; }
        public int qty_stock_out { get; set; }
    }
}
