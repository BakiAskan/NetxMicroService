using BusinessLayers.BLLQueries.Abstract;
using IdentityApi.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModel;
using System.Reflection;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController(IBLPersonelQueries bLPersonelQueries,IToken token) : Controller
    {

        [HttpPost("PersonelInfo"),Authorize]
        public async Task<IActionResult> PersonelInfo(int Personelid)
        {
            return Ok(await bLPersonelQueries.PersonelInfo(Personelid));
        }
        [HttpPost("PersonelYetki"), Authorize]
        public async Task<IActionResult> PersonelYetki(int Personelid)
        {
            return Ok(await bLPersonelQueries.PersonelInfo(Personelid));
        }
        [HttpPost("PersonelLogin")]
        public async Task<IActionResult> PersonelLogin(RequestLogin model)
        {
            var result = await bLPersonelQueries.PersonelLogin(model);
            if (result != null) {
                result.DataResult =token.GenerateToken("","","");
            }
            return Ok(result);
        }
        [HttpPost("DenemeMethod")]
        public async Task<IActionResult> DenemeMethod(RequestLogin model)
        { 
            return Ok(await bLPersonelQueries.Deneme(model));
        }
    }
}
