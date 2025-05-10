using Castle.DynamicProxy;
using ErpMikroservis.GlobalModel;
using ErpMikroservis.AspectCore.LogWriting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ErpMikroservis.AspectCore;
public class ExceptionAspect : InterceptorBase<AOPExceptionAttribute>
{
    private readonly IOptions<SettingsOptions> settings;
    private readonly ILogWriteExtensions logWirte;
    private readonly IHttpContextAccessor _accessor;
    public ExceptionAspect(IOptions<SettingsOptions> _settings, ILogWriteExtensions _logWirte, IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        logWirte = _logWirte;
        settings = _settings;
    }
    protected override void OnException(IInvocation invocation, Exception ex, AOPExceptionAttribute attribute)
    {
        if (settings.Value.IsException)
        {
            logWirte.FileLogAdd($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name} Path : {ex.StackTrace}  Message : {ex.Message}" + "Date : " + DateTime.Now.ToString() + " IP : " + _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), settings.Value.LogPathFile, "Exception");
        }
    }
}
