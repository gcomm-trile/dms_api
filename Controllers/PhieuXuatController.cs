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
    public class PhieuXuatController : ControllerBase
    {
        private readonly ILogger<PhieuXuatController> _logger;

        public PhieuXuatController(ILogger<PhieuXuatController> logger)
        {
            _logger = logger;
        }

        [HttpGet()] 
        public async Task<ActionResult<List<PhieuXuat>>> ListItems()
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_phieu_xuat_list");
           
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                return ds.Tables[0].ToModel<PhieuXuat>();
            }
        }
     
    }
}
