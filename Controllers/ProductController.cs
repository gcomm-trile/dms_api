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
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        [HttpGet("stock/{id}")]
        public async Task<ActionResult<List<Product>>> inventory_products_listAll(string id)
        {
            string sessionID
             = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_inventory_list",new
            {
                stock_id=id
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
        [HttpGet()]
        public async Task<ActionResult<List<Product>>> GetItem(string stock_id_in = "00000000-0000-0000-0000-000000000000", string stock_id_out = "00000000-0000-0000-0000-000000000000")
        {
           string sessionID 
             = Request.Headers["Session-ID"];     
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_products_list",new
            {
                stock_id_in,
                stock_id_out
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
        [HttpPost()]
        public async Task<ActionResult<string>> PostItem()
        {
            string sessionID
             = Request.Headers["Session-ID"];
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var body = reader.ReadToEnd();
                    _logger.LogInformation(body);
                    var product = JsonConvert.DeserializeObject<Product>(body);
                 
                    ClientServices Services = new ClientServices(sessionID);
                    var query = DataAccess.DataQuery.Create("dms", "ws_products_save", new
                    {
                        id =product.id,
                        no =product.no,
                        name =product.name,
                        description= product.description,
                        unit =product.unit,
                        price =product.sell_price,
                        is_active =product.is_active,
                        image_path=product.image_path
                    });
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        return Ok("Ok");
                    }                 
                    // Do something
                }
             
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
