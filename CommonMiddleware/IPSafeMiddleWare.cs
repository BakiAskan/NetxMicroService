using ErpMikroservis.ResultMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Json;

namespace CommonMiddleware
{
    // .𝐍𝐄𝐓 𝐂𝐨𝐫𝐞 𝐑𝐄𝐒𝐓 𝐀𝐏𝐈 Projeleriniz için kullanabileceğiniz IP bazlı Request yasaklaması yapmamızı veya IP Bazlı Request izni vermemizi sağlayan 𝐌𝐢𝐝𝐝𝐥𝐞𝐰𝐚𝐫𝐞 (Ara Katman Yazılımı) Yapısı. 
    public class IPSafeMiddleWare(RequestDelegate next, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly string[] _ipList = configuration["ipList:ClientIp"].Split(',');

        public async Task Invoke(HttpContext context)
        {
            var requestIpAdress = context.Connection.RemoteIpAddress;
            if (!_ipList.Where(x => IPAddress.Parse(x).Equals(requestIpAdress)).Any())
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(ResultMessages<string>.ErrorMessage(new List<string>() { configuration["ipList:Messages"] }, HttpStatusCode.BadRequest)));
                return;
            }
            await _next(context);
        }
    }
}
