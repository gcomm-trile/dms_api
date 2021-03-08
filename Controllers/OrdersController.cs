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
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }
      
        [HttpGet()]
        public async Task<ActionResult<List<Order>>> getAll(string searchText = "", string filter = "[]")
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_ordermasters_list",
                new
                {
                    filter_expression= filter == null ? "" : filter,
                    search_text= searchText == null ? "" : searchText,
                });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            return ds.Tables[0].ToModel<Order>();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetItem(String id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_ordermasters_get", new
            {
                order_id = id
            });
            query += DataAccess.DataQuery.Create("dms", "ws_orderdetails_list", new
            {
                order_id = id
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                return NotFound();
            }

            var result = ds.Tables[0].ToModel<Order>()[0];
            result.products = ds.Tables[1].ToModel<Product>();
            return result;

        }

        [HttpPost("xuatHang")]
        public async Task<ActionResult> xuathang(
          string id, string in_stock_id, int reason_id
      )
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_ordermasters_xuathang", new
                {
                    id,

                });
                var ds = await Services.ExecuteAsync(query);
                if (ds == null)
                {
                    return Ok(Services.LastError);
                }
                else
                {
                    return Ok("Ok");
                }
                // Do something
            }

        }

    }
}
