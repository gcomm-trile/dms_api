using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class PhieuXuat
    {
        public Guid id { get; set; }

        public int seq { get; set; }
        public string seq_no { get; set; }
   
        public Guid import_stock_id { get; set; }    
        public string import_stock_name { get; set; }
        public Guid export_stock_id { get; set; }
        public string export_stock_name { get; set; }
        public DateTime created_on { get; set; }
        public Guid created_by { get; set; }
        public string created_by_name { get; set; }

        public DateTime? approved_on { get; set; }
        public string approved_by_name { get; set; }
        public Guid approved_by { get; set; }

        public int status { get; set; }
        public string status_name { get; set; }

        public int type { get; set; }

        public List<Product> products { get; set; }
    }
}
