using BusinessLayers.BLLQueries.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModel;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController(IBLStockQueries bLStockQueries) : ControllerBase
    {
        [HttpPost("ItemGetAll")] //swagger görünecek ve http prtokolü POST olacak   
        public async Task<IActionResult>ItemGetAll(RequestSqlParams ParamSql)
        {
            var result = await bLStockQueries.ItemGetAll(ParamSql.SQLParams);
            return Ok(result);
        }   
    }
}
