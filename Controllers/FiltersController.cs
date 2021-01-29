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
    public class FiltersController : ControllerBase
    {
        private readonly ILogger<FiltersController> _logger;

        public FiltersController(ILogger<FiltersController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Filter>>> Get(string module)
        {
            string sessionID
             = Request.Headers["Session-ID"];
            try
            {
                ClientServices Services = new ClientServices(sessionID);
                var query = DataAccess.DataQuery.Create("dms", "ws_filter_get", new
                {
                    module = module
                });
                var ds = await Services.ExecuteAsync(query);
                if (ds == null)
                {
                    return BadRequest(Services.LastError);
                }
                else
                {
                    var result = new List<Filter>();
                    result = ds.Tables[0].ToModel<Filter>();
                    foreach (var item in result)
                    {
                        item.filter_expressions = JsonConvert.DeserializeObject<List<FilterExpression>>(item.expressions);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost()]
        public async Task<ActionResult<List<Filter>>> PostItem(string module,string id,string name)
        {
            string sessionID
             = Request.Headers["Session-ID"];
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var body = reader.ReadToEnd();
                    _logger.LogInformation(body);                              
                    ClientServices Services = new ClientServices(sessionID);
                    var query = DataAccess.DataQuery.Create("dms", "ws_filters_save", new
                    {
                        module,
                        id,
                        name,
                        filter_expression =body
                    });
                    query += DataAccess.DataQuery.Create("dms", "ws_filter_get", new
                    {
                        module = module
                    });
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        var result = ds.Tables[1].ToModel<Filter>();                      
                        foreach (var item in result)
                        {
                            item.filter_expressions = JsonConvert.DeserializeObject<List<FilterExpression>>(item.expressions);
                        }
                        return result;                     
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
