using Castle.DynamicProxy;
using ErpMikroservis.GlobalModel;
using Microsoft.Extensions.Options;

namespace ErpMikroservis.AspectCore
{
    public class CacheAspect : InterceptorBase<CacheAddAttribute>
    {
        private readonly ICacheService _cacheService;
        private readonly IOptions<SettingsOptions> settings;
        public CacheAspect(IOptions<SettingsOptions> _settings)
        {
            settings = _settings;
            _cacheService = (ICacheService)HttpContext.Current.RequestServices.GetService(typeof(ICacheService));
        }
        public override void Intercept(IInvocation invocation)
        {
            if (settings.Value.IsCache)
            {
                var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
                var arguments = invocation.Arguments.ToList();
                var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
                if (_cacheService.IsAdd(key))
                {
                    invocation.ReturnValue = _cacheService.Get(key);
                    return;
                }
                invocation.Proceed();
                _cacheService.Add(key, invocation.ReturnValue, settings.Value.CacheDuracation);
            }
            Console.WriteLine($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name} Cache Successful");
        }
    }
}