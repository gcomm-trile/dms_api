using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Transaction
    {
        public List<Product> products { get; set; }

        public List<Filter> filters { get; set; }
    }


}
