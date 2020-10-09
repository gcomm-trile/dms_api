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
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Stock>>> GetItem()
        {
            string sessionID
            = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_stocks_list");
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                return ds.Tables[0].ToModel<Stock>();
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
                    var item = JsonConvert.DeserializeObject<Stock>(body);
                 
                    ClientServices Services = new ClientServices(sessionID);
                    var query = DataAccess.DataQuery.Create("dms", "ws_stocks_save", new
                    {
                        id = item.id,
                        no = item.no,
                        name = item.name,                    
                        is_active = item.is_active,

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
