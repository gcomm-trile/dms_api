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
        public async Task<ActionResult<Report>> GetItem(DateTime from_date,DateTime to_date)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_report_tonghop", new
            {
                from_date = from_date.ToString("yyyy-MM-dd HH:mm:ss"),
                to_date = to_date.ToString("yyyy-MM-dd HH:mm:ss")
            });
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
       
            var result = new Report();
            result.report_tonghop = ds.Tables[0].ToModel<Report_TongHop>();
            result.report_tuyen = ds.Tables[1].ToModel<Report_Tuyen>();
            result.report_nvbh = ds.Tables[2].ToModel<Report_NVBH>();
            result.provinces = ds.Tables[3].ToModel<province_item>();
            result.routes = ds.Tables[4].ToModel<Route>();
            result.routes_user = ds.Tables[5].ToModel<RouteUser>();
            return result;
           
        }
      

    }
}
