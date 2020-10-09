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
        public int price { get; set; }
        public bool is_active { get; set; }
        public int qty { get; set; }
        public string image_path { get; set; }
        public Int64 total { get; set; }
    }
}
