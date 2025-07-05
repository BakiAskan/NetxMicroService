using ErpMikroservis.ResultMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Json;

public class IpRateLimitingMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private static readonly MemoryCache _memoryCache = new(new MemoryCacheOptions());

    private readonly int _limit = Convert.ToInt16(configuration["IpRateLimiting:RequestLimit"]); // Maksimum istek sayısı
    private readonly TimeSpan _period = TimeSpan.FromMinutes(Convert.ToInt16(configuration["IpRateLimiting:RequestPeriod"])); // Süre
    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrEmpty(ipAddress))
        {
            await next(context);
            return;
        }
        var cacheKey = $"RateLimit_{ipAddress}";
        if (_memoryCache.TryGetValue(cacheKey, out int requestCount))
        {
           
            if (requestCount >= _limit)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(ResultMessages<string>.ErrorMessage(new List<string>() { "Çok fazla istek. Lütfen daha sonra tekrar deneyin." }, HttpStatusCode.RequestTimeout)));
                return;
            }
            _memoryCache.Set(cacheKey, requestCount + 1, DateTimeOffset.UtcNow.Add(_period));
        }
        else
        {
            _memoryCache.Set(cacheKey, 1, DateTimeOffset.UtcNow.Add(_period));
        }
        await next(context);
    }
}
