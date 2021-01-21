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
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Product>>> GetItem(string stock_id= "00000000-0000-0000-0000-000000000000")
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_inventory_list",new
            {
                stock_id=stock_id
            });
           
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                return ds.Tables[0].ToModel<Product>();
            }
        }
        [HttpGet("purchaseorders")]
        public async Task<ActionResult<List<PurchaseOrder>>> inventory_purchase_orders_getAll()
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_list");

            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                return ds.Tables[0].ToModel<PurchaseOrder>();
            }
        }
        [HttpGet("purchaseorders/{id}")]
        public async Task<ActionResult<PurchaseOrder>> inventory_purchase_orders_getId(string id)
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
                if (ds.Tables[0].ToModel<PurchaseOrder>().Count>0)
                {
                    result = ds.Tables[0].ToModel<PurchaseOrder>()[0];
                    result.products = ds.Tables[1].ToModel<Product>();
                  
                }
                result.vendors = ds.Tables[2].ToModel<Vendor>();
                result.stocks = ds.Tables[3].ToModel<Stock>();
                
                return result;
              
            }
        }
        [HttpPost("purchaseorders/add")]
        public async Task<ActionResult<PurchaseOrder>> inventory_purchase_orders_new(string id,string import_stock_id,
            DateTime plan_import_date,string ref_document_note,string vendor_id,string note)
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
                    import_stock_id,
                    plan_import_date,
                    ref_document_note,
                    note,
                    vendor_id,
                    product_json = body
                }) ;
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
        [HttpPost("purchaseorders/import")]
        public async Task<ActionResult<PurchaseOrder>> inventory_purchase_orders_import(string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_import", new
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
