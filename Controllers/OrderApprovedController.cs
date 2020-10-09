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
    public class OrderApprovedController : ControllerBase
    {
        private readonly ILogger<OrderApprovedController> _logger;

        public OrderApprovedController(ILogger<OrderApprovedController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        public async Task<ActionResult<string>> PostItem(string order_id,int status, string export_stock_id="")
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            DataAccess.RequestCollection query ;
            if (status == 1)
            {
                query = DataAccess.DataQuery.Create("dms", "ws_ordermasters_export4order_approve", new
                {
                    order_id = order_id,
                    export_stock_id
                });
            }
            else
            {
                if (status == 2)
                {
                    query = DataAccess.DataQuery.Create("dms", "ws_ordermasters_export4order_cancel", new
                    {
                        order_id = order_id                    
                    });
                }
                else
                {
                    return BadRequest("Không tìm thấy status");
                }
            }
            var ds = Services.Execute(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            return "Ok";
        }
      

    }
}
