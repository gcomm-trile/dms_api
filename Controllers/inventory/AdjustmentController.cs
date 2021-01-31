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

namespace albus_api.Controllers.Invenroty
{
    [ApiController]
    [Route("inventory/[controller]")]
    public class AdjustmentsController : ControllerBase
    {
        private readonly ILogger<AdjustmentsController> _logger;

        public AdjustmentsController(ILogger<AdjustmentsController> logger)
        {
            _logger = logger;
        }
      
      
        public async Task<ActionResult<AdjustmentList>> inventory_adjustments_getAll(string filter="[]")
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_adjustments_list",new
            {
                filter_expression=filter
            });
            query += DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
            query += DataAccess.DataQuery.Create("dms", "ws_filter_get", new
            {
                module = "inventory_transactions"
            });
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                var result =new AdjustmentList();
                result.adjustments= ds.Tables[0].ToModel<Adjustment>();
                result.stocks = ds.Tables[1].ToModel<Stock>();
                result.filters = ds.Tables[2].ToModel<Filter>();
                foreach (var item in result.filters)
                {
                    item.filter_expressions = JsonConvert.DeserializeObject<List<FilterExpression>>(item.expressions);
                }
                return result;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdjustmentItem>> inventory_adjustments_getId(string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_adjustments_get", new
            {
                id = id
            });          
            query += DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
            query += DataAccess.DataQuery.Create("dms", "ws_categories_get", new
            {
                module= "adjustment_reasons"
            });
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);

            }
            else
            {
                var result = new AdjustmentItem();
                if (ds.Tables[0].ToModel<Adjustment>().Count > 0)
                {
                    result.adjustment = ds.Tables[0].ToModel<Adjustment>()[0];
                    result.adjustment.products = ds.Tables[1].ToModel<Product>();
                }             
                result.stocks = ds.Tables[2].ToModel<Stock>();
                result.adjustment_reasons = ds.Tables[3].ToModel<Category>();
                return result;

            }
        }

        [HttpPost("dieuchinh")]
        public async Task<ActionResult> inventory_adjustments_dieuchinh(
           string id,  string in_stock_id,int reason_id
       )
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_adjustments_dieuchinh", new
                {
                    id,                
                    in_stock_id,               
                    product_json = body  ,
                    reason_id
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
