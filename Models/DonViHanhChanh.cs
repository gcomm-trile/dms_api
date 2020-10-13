using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dms_api.Models
{

    public class Level3s
    {
        public string level3_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }

    }

    public class Level2s
    {
        public string level2_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Level3s> level3s { get; set; }

    }

    public class Datum
    {
        public string level1_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Level2s> level2s { get; set; }

    }

    public class Stats
    {
        public double elapsed_time { get; set; }
        public int level1_count { get; set; }
        public int level2_count { get; set; }
        public int level3_count { get; set; }

    }

    public class DonViHanhChanh
    {
        public List<Datum> data { get; set; }
        public string data_date { get; set; }
        public int generate_date { get; set; }
        public Stats stats { get; set; }

    }
}

