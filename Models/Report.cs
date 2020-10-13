﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{
    public class Report
    {
        public List<Report_TongHop> report_tonghop { get; set; }
        public List<Report_Tuyen> report_tuyen { get; set; }
        public List<Report_NVBH> report_nvbh { get; set; }
        public List<province_item> provinces { get; set; }

        public List<Route> routes { get; set; }
        public List<RouteUser> routes_user { get; internal set; }
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
        public string route { get; set; }
        public string province { get; set; }
        public int count_visit { get; set; }
        public int count_store_order { get; set; }
        public int count_order { get; set; }
        public Int64 sum_order_price { get; set; }
    }
}
