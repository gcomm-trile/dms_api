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
        public bool is_active { get; set; }
        public string image_path { get; set; }
      


      
        public int order_qty { get; set; }
        public int remaining_qty { get; set; }     
        public int out_qty { get; set; }
        public int in_qty { get; set; }

        public Int64 sell_price { get; set; }
        public Int64 order_price { get; set; }
        public Int64 total_price_avg { get; set; }
      
        public Guid stock_id { get; set; }
        public string stock_name { get; set; }
      
        public int in_stock_qty { get; set; }
        public int out_stock_qty { get; set; }
    }
}
