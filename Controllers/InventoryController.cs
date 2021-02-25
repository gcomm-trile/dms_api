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

        [HttpPost("transactions")]
        public async Task<ActionResult<Transaction>> GetItem()
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_transactions_list", new
                {
                    filter_expression= body
                });
                query += DataAccess.DataQuery.Create("dms", "ws_filter_get", new
                {
                    module = "inventory_transactions"
                });
                query += DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
                var ds = await Services.ExecuteAsync(query);
                if (ds == null)
                {
                    return BadRequest(Services.LastError);
                }
                else
                {
                    var result = new Transaction();
                    result.products = ds.Tables[0].ToModel<Product>();
                    result.filters = ds.Tables[1].ToModel<Filter>();
                    foreach (var item in result.filters)
                    {
                        item.filter_expressions = JsonConvert.DeserializeObject<List<FilterExpression>>(item.expressions);
                    }
                    result.stocks = ds.Tables[2].ToModel<Stock>();
                   
                    return result;
                }
                // Do something
            }

         
        }
        //[HttpGet("purchaseorders")]
        //public async Task<ActionResult<List<PurchaseOrder>>> inventory_purchase_orders_getAll()
        //{
        //    string sessionID
        //      = Request.Headers["Session-ID"];
        //    ClientServices Services = new ClientServices(sessionID);
        //    var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_list");

        //    var ds = await Services.ExecuteAsync(query);
        //    if (ds == null)
        //    {
        //        return BadRequest(Services.LastError);
        //    }
        //    else
        //    {
        //        return ds.Tables[0].ToModel<PurchaseOrder>();
        //    }
        //}
        //[HttpGet("purchaseorders/{id}")]
        //public async Task<ActionResult<PurchaseOrder>> inventory_purchase_orders_getId(string id)
        //{
        //    string sessionID
        //      = Request.Headers["Session-ID"];
        //    ClientServices Services = new ClientServices(sessionID);
        //    var query = DataAccess.DataQuery.Create("dms", "ws_purchase_orders_get", new
        //    {
        //        id = id
        //    });
        //    query += DataAccess.DataQuery.Create("dms", "ws_vendors_list");
        //    query += DataAccess.DataQuery.Create("dms", "ws_stocks_list");
        //    var ds = await Services.ExecuteAsync(query);
        //    if (ds == null)
        //    {
        //        return BadRequest(Services.LastError);
        //    }
        //    else
        //    {
        //        var result = new PurchaseOrder();
        //        if (ds.Tables[0].ToModel<PurchaseOrder>().Count>0)
        //        {
        //            result = ds.Tables[0].ToModel<PurchaseOrder>()[0];
        //            result.products = ds.Tables[1].ToModel<Product>();
                  
        //        }
        //        result.vendors = ds.Tables[2].ToModel<Vendor>();
        //        result.stocks = ds.Tables[3].ToModel<Stock>();
                
        //        return result;
              
        //    }
        //}
        

        [HttpGet("transfers")]
        public async Task<ActionResult<List<Transfer>>> inventory_transfers_getAll()
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_transfers_list");

            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                return ds.Tables[0].ToModel<Transfer>();
            }
        }

        [HttpGet("transfers/{id}")]
        public async Task<ActionResult<Transfer>> inventory_transfers_getId(string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_transfers_get", new
            {
                id = id
            });
            query += DataAccess.DataQuery.Create("dms", "ws_stocks_list");
            query += DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
           
          
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                var result = new Transfer();
                if (ds.Tables[0].ToModel<Transfer>().Count > 0)
                {
                    result = ds.Tables[0].ToModel<Transfer>()[0];
                    result.products = ds.Tables[1].ToModel<Product>();

                }
                result.in_stocks = ds.Tables[2].ToModel<Stock>();
                result.out_stocks = ds.Tables[3].ToModel<Stock>();             
                return result;

            }
        }
        [HttpPost("transfers/add")]
        public async Task<ActionResult> inventory_transfers_new(
           int status, string id, string out_stock_id,string in_stock_id,
         DateTime plan_date, string ref_document_note, string note)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_transfers_save", new
                {
                    id,
                    out_stock_id,
                    in_stock_id,
                    plan_date,
                    ref_document_note,
                    note,                   
                    product_json = body,
                    status
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

        [HttpPost("transfers/nhan")]
        public async Task<ActionResult> inventory_transfers_nhan(
          string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_transfers_nhan", new
                {
                    id                 
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

        [HttpPost("transfers/huy")]
        public async Task<ActionResult> inventory_transfers_huy(
         string id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                _logger.LogInformation(body);
                var query = DataAccess.DataQuery.Create("dms", "ws_transfers_huy", new
                {
                    id
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
