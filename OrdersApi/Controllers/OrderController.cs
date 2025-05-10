using BusinessLayers.BLCommands.Abstract;
using BusinessLayers.BLLQueries.Abstract;
using ErpMikroservis.ResultMessages;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.RequestModel;
using System.Net;

namespace OrdersApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrderController(IBLOrderQueries bLOrder, IBLCurrencyCommand currencyCommand) : Controller
    {
        [HttpPost("OrderGetAll")]
        public async Task<IActionResult> OrderGetAll(RequestSqlParams Paramater)
        {
            return Ok(await bLOrder.OrderGetAll(Paramater.SQLParams));
        }
        [HttpPost("OrderById")]
        public async Task<IActionResult> OrderById(RequestSqlParams Paramater)
        {
            return Ok(await bLOrder.OrderById(Paramater.SQLParams));
        }
        [HttpPost("CurrencyWrite")]
        public async Task<IActionResult> CurrencyWrite(IList<RequestCurrency> Paramater)
        {
            List<ResponseGlobalMessage> resultStatus = new List<ResponseGlobalMessage>();
            foreach (var paramater in Paramater)
            {
                var result = await currencyCommand.WriteCurrency(paramater);
                resultStatus.Add(new ResponseGlobalMessage { AutoID = result.DataResult.ToString(), UID = "1", ErrDescription = "21" });
            }
            return Ok(ResultMessages<List<ResponseGlobalMessage>>.SuccessMessage(resultStatus, HttpStatusCode.BadRequest));
        }
    }
}
