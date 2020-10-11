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
    public class DashboardTongHopController : ControllerBase
    {
        private readonly ILogger<DashboardTongHopController> _logger;

        public DashboardTongHopController(ILogger<DashboardTongHopController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Report_TongHop>>> GetItem(DateTime from_date,DateTime to_date)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "rp_report_tonghop", new
            {
                from_date = from_date.ToString("yyyy-MM-dd HH:mm:ss"),
                to_date = to_date.ToString("yyyy-MM-dd HH:mm:ss")
            });

            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
              
                return ds.Tables[0].ToModel<Report_TongHop>();
     
               
            }
        }
      

    }
}
