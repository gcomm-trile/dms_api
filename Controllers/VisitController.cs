using System;
using System.Collections.Generic;
using System.Data;
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
    public class VisitController : ControllerBase
    {
        private readonly ILogger<VisitController> _logger;

        public VisitController(ILogger<VisitController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Visit>>> GetItem()
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_visits_list");
          
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }

            return ds.Tables[0].ToModel<Visit>();

        }
      

    }
}
