using Castle.DynamicProxy;
using ErpMikroservis.AspectCore.LogWriting;
using ErpMikroservis.GlobalModel;
using ErpMikroservis.ResultMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace CommonMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<SettingsOptions> settings;
        private readonly ILogWriteExtensions logWirte;
        public ExceptionMiddleware(RequestDelegate next, IOptions<SettingsOptions> _settings, ILogWriteExtensions _logWirte)
        {
            _next = next;
            logWirte = _logWirte;
            settings = _settings;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (settings.Value.IsException)
            {
                logWirte.FileLogAdd($"Message : {exception.Message}" + "Date : " + DateTime.Now.ToString() + " IP : " + context.Connection.RemoteIpAddress.ToString(), settings.Value.LogPathFile, "Exception");
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = ResultMessages<string>.ErrorMessage(new List<string>() { "Beklenmedik Bir Hata İle Karşılaşıldı. Lütfen daha sonra tekrar deneyin." }, HttpStatusCode.BadRequest);
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
