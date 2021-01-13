using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class PurchaseOrder
    {
        public string id { get; set; }
        public string no { get; set; }
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
        public List<PurchaseOrderDetail> products { get; set; }
        public List<Vendor> vendors { get; internal set; }
        public List<Stock> stocks { get; internal set; }
    }
    public class PurchaseOrderDetail
    {
        public Guid purchase_order_id { get; set; }
        public Guid product_id { get; set; }
        public int qty { get; set; }      
    }
}
