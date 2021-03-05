using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using dms_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace albus_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<Report>> GetItem(string province,string route_id,string user_id,int filter,int level)
        {
            try
            {
                string sessionID
           = Request.Headers["Session-ID"];
                ClientServices Services = new ClientServices(sessionID);
                if (province == null || province.Length == 0)
                {
                    province = "";
                }
                if (route_id == null || route_id.Length == 0)
                {
                    route_id = "00000000-0000-0000-0000-000000000000";
                }
                if (user_id == null || user_id.Length == 0)
                {
                    user_id = "00000000-0000-0000-0000-000000000000";
                }

                var query = DataAccess.DataQuery.Create("dms", "ws_report", new
                {
                    province,
                    route_id,
                    user_id,
                    filter,
                    level
                });
                query += DataAccess.DataQuery.Create("dms", "ws_report_activity_list", new
                {
                    province,
                    route_id,
                    user_id,
                    filter,
                    level
                });
                var ds = await Services.ExecuteAsync(query);
                if (ds == null)
                {
                    return BadRequest(Services.LastError);
                }
                var result = new Report();
                result.data_chart=
                 ds.Tables[0].ToModel<Report_Chart>();
                result.data_activity =
                ds.Tables[1].ToModel<Report_Activity>();
                return result;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
               
        }   
    }
}
