using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class PhieuMuaHang
    {
        public string purchase_order_id { get; set; }
        public string purchase_order_no { get; set; }
        public string import_stock_id { get; set; }
        public string status { get; set; }
        public string approved_by { get; set; }
        public string approved_on { get; set; }
        public string plan_import_date { get; set; }
        public string ref_document_note { get; set; }
        public string vendor_id { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }
       
    }
}
