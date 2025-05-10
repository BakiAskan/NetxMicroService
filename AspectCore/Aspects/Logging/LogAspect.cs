using Castle.DynamicProxy;
using ErpMikroservis.GlobalModel;
using ErpMikroservis.AspectCore.LogWriting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ErpMikroservis.AspectCore
{
    public class LogAspect : InterceptorBase<AOPLogAttribute>
    {
        private readonly IOptions<SettingsOptions> settings;
        private readonly ILogWriteExtensions logWirte;
        private IHttpContextAccessor _accessor;
        private readonly JsonSerializerOptions options;
        public LogAspect(IOptions<SettingsOptions> _settings, ILogWriteExtensions _logWirte, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            logWirte = _logWirte;
            settings = _settings;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All)
            };
        }

        protected override void OnBefore(IInvocation invocation, AOPLogAttribute attribute)
        {
            if (settings.Value.IsLog)
            {
                logWirte.FileLogAdd($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name} LoggerRequest {JsonSerializer.Serialize(invocation.Arguments, options)}" + " Date : " + DateTime.Now.ToString() + " IP : " + _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), settings.Value.LogPathFile, "Logger");
            }
        }

        protected override void OnAfter(IInvocation invocation, AOPLogAttribute attribute)
        {
            if (settings.Value.IsLog)
            {
                logWirte.FileLogAdd($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name} LoggerResponse {JsonSerializer.Serialize(invocation.ReturnValue, options)}" + " Date : " + DateTime.Now.ToString(), settings.Value.LogPathFile, "Logger");
            }
        }
    }
}
