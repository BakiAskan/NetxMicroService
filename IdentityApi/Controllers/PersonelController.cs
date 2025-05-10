using BusinessLayers.BLLQueries.Abstract;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModel;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController(IBLPersonelQueries bLPersonelQueries) : Controller
    {
        [HttpPost("PersonelLogin")]
        public async Task<IActionResult> PersonelLogin(RequestLogin model)
        {
            return Ok(await bLPersonelQueries.PersonelLogin(model));
        }

        [HttpPost("DenemeMethod")]
        public async Task<IActionResult> DenemeMethod(RequestLogin model)
        { 
            return Ok(await bLPersonelQueries.Deneme(model));
        }
    }
}
