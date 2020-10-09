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
    public class DuyetPhieuXuatController : ControllerBase
    {
        private readonly ILogger<DuyetPhieuXuatController> _logger;

        public DuyetPhieuXuatController(ILogger<DuyetPhieuXuatController> logger)
        {
            _logger = logger;
        }

        [HttpPost()] 
        public async Task<ActionResult<string>> ListItems(string phieu_xuat_id,int status)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_phieu_xuat_approved", new
            {
                phieu_xuat_id = phieu_xuat_id,
                status
            });
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                return "Ok";
            }
        }
     
    }
}
