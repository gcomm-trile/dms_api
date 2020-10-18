using System;
using System.Collections.Generic;
using System.Data;
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
    public class VisitDetailController : ControllerBase
    {
        private readonly ILogger<VisitDetailController> _logger;

        public VisitDetailController(ILogger<VisitDetailController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<Visit>> GetItem(String visit_id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_visits_get", new
            {
               id=visit_id
            });
            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }

            var visit = ds.Tables[0].ToModel<Visit>()[0];
            var order = new Order();
            order.products = ds.Tables[1].ToModel<Product>();
            visit.orders = order;
            List<ImageS3> image_checkin = new List<ImageS3>();
            List<ImageS3> image_checkout = new List<ImageS3>();
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                if (row["type"].ToString() == "Check-in")
                {
                    image_checkin.Add(new ImageS3() { path = row["server_path"].ToString() });
                }
                if (row["type"].ToString() == "Check-out")
                {
                    image_checkout.Add(new ImageS3() { path = row["server_path"].ToString() });

                }
            }
   
            visit.checkin_images = image_checkin;
            visit.checkout_images = image_checkout;
            return visit;

        }
      

    }
}
