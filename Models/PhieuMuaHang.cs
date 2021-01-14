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
        public Guid import_stock_id { get; set; }
        public string import_stock_name { get; set; }
        public int status { get; set; }
        public string status_name { get; set; }
        public Guid approved_by { get; set; }
        public DateTime approved_on { get; set; }
        public DateTime plan_import_date { get; set; }
        public string ref_document_note { get; set; }
        public Guid vendor_id { get; set; }
        public string vendor_name { get; set; }
        public Guid created_by { get; set; }
        public DateTime created_on { get; set; }
        public Guid modified_by { get; set; }
        public DateTime modified_on { get; set; }
        public int total_imported_qty { get; set; }
        public int total_order_qty { get; set; }
        public List<Product> products { get; set; }
        public List<Vendor> vendors { get; internal set; }
        public List<Stock> stocks { get; internal set; }
    }
    public class PurchaseOrderDetail
    {
        public Guid purchase_order_id { get; set; }
        public int product_id { get; set; }
        public int qty { get; set; }      
    }
}
