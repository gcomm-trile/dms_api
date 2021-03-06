﻿using System;
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
    public class OrderDetailController : ControllerBase
    {
        private readonly ILogger<OrderDetailController> _logger;

        public OrderDetailController(ILogger<OrderDetailController> logger)
        {
            _logger = logger;
        }
      
        [HttpGet()]
        public async Task<ActionResult<Order>> ListItem(String order_id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_ordermasters_get", new
            {
                order_id
            });
            query += DataAccess.DataQuery.Create("dms", "ws_orderdetails_list", new
            {
                order_id
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            if(ds.Tables[0].Rows.Count==0)
            {
                return NotFound();
            }

            var result = ds.Tables[0].ToModel<Order>()[0];
            result.products= ds.Tables[1].ToModel<Product>();
            return result;

        }
      

    }
}
