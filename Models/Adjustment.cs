using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Adjustment
    {
        public Guid id { get; set; }
        public string no { get; set; }
        public Guid in_stock_id { get; set; }
        public string in_stock_name { get; set; }
        public DateTime created_on { get; set; }
        public int total_qty { get; set; }
        public List<Product> products { get; set; }     
        public List<Stock> stocks { get;  set; }
    }
   
}
