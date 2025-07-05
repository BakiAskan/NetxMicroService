using BusinessLayers.BLLQueries.Abstract;
using BusinessLayers.BLLQueries.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModel;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IBLCompanyQueries bLCompanyQueries) : ControllerBase 
    {
        [HttpPost("CompanyGetAll")] //swagger görünecek ve http prtokolü POST olacak   
        public async Task<IActionResult> CompanyGetAll(RequestSqlParams ParamSql)
        {
            var result = await bLCompanyQueries.CompanyGetAll(ParamSql.SQLParams);
            return Ok(result);
        }
    }
}
