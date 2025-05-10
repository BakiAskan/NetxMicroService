using BusinessLayers.BLLQueries.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IBLOrderQueries bLOrder) : Controller
    {
        [HttpPost("OrderGetAll")]
        public async Task<IActionResult> OrderGetAll()
        {
            return Ok(await bLOrder.OrderGetAll(""));
        }
        [HttpPost("OrderById")]
        public async Task<IActionResult> OrderById()
        {
            return Ok(await bLOrder.OrderById(""));
        }
    }
}
