using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class PurchaseOrder
    {
        public Guid id { get; set; }
        public string no { get; set; }
        public Guid in_stock_id { get; set; }
        public string in_stock_name { get; set; }
        public int status { get; set; }
        public string status_name { get; set; }
        public Guid approved_by { get; set; }
        public DateTime approved_on { get; set; }
        public DateTime plan_date { get; set; }
       
        public Guid vendor_id { get; set; }
        public string vendor_name { get; set; }
     
        public int total_order_qty { get; set; }
        public int total_in_qty { get; set; }
        public List<Product> products { get; set; }
        public List<Vendor> vendors { get; internal set; }
        public List<Stock> stocks { get; internal set; }
    }
   
}
