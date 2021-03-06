﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Report_Chart
    {
        public Guid id { get; set; }
        public string name { get; set; }       
        public int count_visit { get; set; }
        public int count_store_order { get; set; }
        public int count_order { get; set; }
        public Int64 sum_order_price { get; set; }
    }

    public class Report
    {
        public List<Report_Chart> data_chart { get; set; }
        public List<Report_Activity> data_activity { get;  set; }
    }
    public class Report_TongHop
    {
        public string province { get; set; }
        public int count_visit { get; set; }
        public int count_store_order { get; set; }
        public int count_order { get; set; }
        public Int64 sum_order_price { get; set; }
    }
    public class Report_Tuyen
    {
        public Guid route_id { get; set; }
        public string route_name { get; set; }
        public string province { get; set; }
        public int count_visit { get; set; }
        public int count_store_order { get; set; }
        public int count_order { get; set; }
        public Int64 sum_order_price { get; set; }
    }
    public class Report_NVBH
    {
        public Guid user_id { get; set; }
        public Guid route_id { get; set; }
        public DateTime report_date { get; set; }
        public string full_name { get; set; }
        public string route_name { get; set; }
        public string province { get; set; }
        public int count_visit { get; set; }
        public int count_store_order { get; set; }
        public int count_order { get; set; }
        public Int64 sum_order_price { get; set; }
    }
}
