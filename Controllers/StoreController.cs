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
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;

        public StoreController(ILogger<StoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<Store>> GetItem(Guid store_id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_stores_get", new
            {
                id = store_id
            });
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {

                return BadRequest(Services.LastError);
            }
          
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToModel<Store>()[0];
            else
                return new Store() { id = Guid.NewGuid().ToString(), is_active = true };
    
           
        }
      

    }
}
