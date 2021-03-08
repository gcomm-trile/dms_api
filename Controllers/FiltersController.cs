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
    public class FiltersController : ControllerBase
    {
        private readonly ILogger<FiltersController> _logger;

        public FiltersController(ILogger<FiltersController> logger)
        {
            _logger = logger;
        }


        [HttpGet("values")]
        public async Task<ActionResult<List<FilterFieldNameValues>>> GetValues(string module)
        {
            var result = new List<FilterFieldNameValues>();
            string sessionID
             = Request.Headers["Session-ID"];
            try
            {
                ClientServices Services = new ClientServices(sessionID);

                if (module == "inventory_adjustments")
                {
                    var query = DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
                    query += DataAccess.DataQuery.Create("dms", "ws_categories_get", new
                    {
                        module = "adjustment_reasons"
                    });
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        var stocks = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var stock = new FilterValue();
                            stock.id = row["id"].ToString();
                            stock.value = row["name"].ToString();
                            stocks.Add(stock);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "stock_id",
                            filter_values = stocks
                        });

                        var adjustment_reasons = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            var item = new FilterValue();
                            item.id = row["id"].ToString();
                            item.value = row["name"].ToString();
                            adjustment_reasons.Add(item);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "adjustment_reason_id",
                            filter_values = adjustment_reasons
                        });

                    }
                }
                if (module == "inventory_transactions")
                {
                    var query = DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
                    query += DataAccess.DataQuery.Create("dms", "ws_products_list");
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        var stocks = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var item = new FilterValue();
                            item.id = row["id"].ToString();
                            item.value = row["name"].ToString();
                            stocks.Add(item);
                        }

                        var products = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            var item = new FilterValue();
                            item.id = row["id"].ToString();
                            item.value = row["name"].ToString();
                            products.Add(item);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "stock_id",
                            filter_values = stocks
                        });
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "product_id",
                            filter_values = products
                        });

                    }
                }
                if (module == "inventory_purchase_orders")
                {
                    var query = DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
                    query += DataAccess.DataQuery.Create("dms", "ws_purchase_order_status_list");
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        var stocks = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var stock = new FilterValue();
                            stock.id = row["id"].ToString();
                            stock.value = row["name"].ToString();
                            stocks.Add(stock);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "stock_id",
                            filter_values = stocks
                        });

                        var purchase_order_status = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            var item = new FilterValue();
                            item.id = row["id"].ToString();
                            item.value = row["name"].ToString();
                            purchase_order_status.Add(item);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "purchase_order_status_id",
                            filter_values = purchase_order_status
                        });
                    }
                }
                if (module == "orders")
                {
                    var query = DataAccess.DataQuery.Create("dms", "ws_stocks_list_by_permission");
                    //query += DataAccess.DataQuery.Create("dms", "ws_purchase_order_status_list");
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        var stocks = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var stock = new FilterValue();
                            stock.id = row["id"].ToString();
                            stock.value = row["name"].ToString();
                            stocks.Add(stock);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "stock_id",
                            filter_values = stocks
                        });

                        var exportStatus = new List<FilterValue>();

                        var item = new FilterValue();
                        item.id = "0";
                        item.value = "Chưa xuất";
                        exportStatus.Add(item);

                        var item2 = new FilterValue();
                        item2.id = "1";
                        item2.value = "Đã xuất";
                        exportStatus.Add(item2);


                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "is_export_stock",
                            filter_values = exportStatus
                        });
                    }
                }
                if (module == "visits")
                {
                    var query = DataAccess.DataQuery.Create("dms", "ws_stores_list");
                    //query += DataAccess.DataQuery.Create("dms", "ws_purchase_order_status_list");
                    var ds = await Services.ExecuteAsync(query);
                    if (ds == null)
                    {
                        return BadRequest(Services.LastError);
                    }
                    else
                    {
                        var stores = new List<FilterValue>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var store = new FilterValue();
                            store.id = row["id"].ToString();
                            store.value = row["name"].ToString();
                            stores.Add(store);
                        }
                        result.Add(new FilterFieldNameValues()
                        {
                            field_name = "store_id",
                            filter_values = stores
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public async Task<ActionResult<List<Filter>>> PostItem(string module, string id, string name)
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
                        filter_expression = body
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}