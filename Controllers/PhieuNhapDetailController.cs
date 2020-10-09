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
    public class PhieuNhapDetailController : ControllerBase
    {
        private readonly ILogger<PhieuNhapDetailController> _logger;

        public PhieuNhapDetailController(ILogger<PhieuNhapDetailController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<PhieuNhap>> GetItem(string phieu_nhap_id)
        {
            string sessionID
              = Request.Headers["Session-ID"];
            ClientServices Services = new ClientServices(sessionID);
            var query = DataAccess.DataQuery.Create("dms", "ws_phieu_nhap_get", new
            {
                phieu_nhap_id = phieu_nhap_id
            });

            query += DataAccess.DataQuery.Create("dms", "ws_phieu_nhap_detail_list", new
            {
                phieu_nhap_id= phieu_nhap_id
            });

            var ds = await Services.ExecuteAsync(query);
            if (ds == null)
            {
                return BadRequest(Services.LastError);
            }
            else
            {
                var result = new PhieuNhap();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].ToModel<PhieuNhap>()[0];
                }

                result.products = ds.Tables[1].ToModel<Product>();
                return result;
              
            }
        }
        [HttpPost()]
        public async Task<ActionResult<string>> PostItem(string import_stock_id, string export_stock_id, string phieu_nhap_id, string phieu_xuat_id)
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
                    var query = DataAccess.DataQuery.Create("dms", "ws_phieu_nhap_detail_save", new
                    {
                        import_stock_id,
                        export_stock_id,
                        phieu_nhap_id,
                        phieu_xuat_id,
                        product_json= body
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
