using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Transfer
    {
        public Guid id { get; set; }
        public string no { get; set; }
        public Guid in_stock_id { get; set; }
        public string in_stock_name { get; set; }
        public Guid out_stock_id { get; set; }
        public string out_stock_name { get; set; }
        public int status { get; set; }
        public string status_name { get; set; }
        public DateTime plan_date { get; set; }
        public string ref_document_note { get; set; }
        public string note { get; set; }

        public List<Product> products { get; set; }  
        public List<Stock> in_stocks { get; internal set; }
        public List<Stock> out_stocks { get; internal set; }
    }
   
}
