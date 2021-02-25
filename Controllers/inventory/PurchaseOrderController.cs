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
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(ILogger<PurchaseOrderController> logger)
        {
            _logger = logger;
        }
         
        public async Task<ActionResult<List<PurchaseOrder>>> getAll(string searchText="",string filter="[]")
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_list", new
            {
                filter_expression=filter,
                search_text=searchText
            });            
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
               
                return ds.Tables[0].ToModel< PurchaseOrder>();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> getId(string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_get", new
            {
                id = id
            });
            query += DataAccess.DataQuery.Create("dms", "ws_vendors_list");
            query += DataAccess.DataQuery.Create("dms", "ws_stocks_list");
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                var result = new PurchaseOrder();
                if (ds.Tables[0].ToModel<PurchaseOrder>().Count > 0)
                {
                    result = ds.Tables[0].ToModel<PurchaseOrder>()[0];
                    result.products = ds.Tables[1].ToModel<Product>();
                }
                result.vendors = ds.Tables[2].ToModel<Vendor>();
                result.stocks = ds.Tables[3].ToModel<Stock>();
                return result;
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<PurchaseOrder>> inventory_purchase_orders_new(string id, string in_stock_id,
            DateTime plan_date, string vendor_id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_save", new
                {
                    id,
                    in_stock_id,
                    plan_date,
                    vendor_id,
                    product_json = body
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
        [HttpPost("nhanhang")]
        public async Task<ActionResult<PurchaseOrder>> inventory_purchase_orders_nhanhang(string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_nhanhang", new
                {
                    id,
                    product_json = body
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
