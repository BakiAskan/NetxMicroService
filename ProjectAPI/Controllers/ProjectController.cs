using BusinessLayers.BLLQueries.Abstract;
using BusinessLayers.BLLQueries.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModel;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IBLProjectQueries bLProjectQueries) : ControllerBase
    {

        [HttpPost("ProjectGetAll")] //swagger görünecek ve http prtokolü POST olacak   
        public async Task<IActionResult> ProjectGetAll(RequestSqlParams ParamSql)
        {
            var result = await bLProjectQueries.ProjectGetAll(ParamSql.SQLParams);
            return Ok(result);
        }
    }
}
