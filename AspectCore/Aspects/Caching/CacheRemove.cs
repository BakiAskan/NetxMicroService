using Castle.DynamicProxy;
using ErpMikroservis.GlobalModel;
using ErpMikroservis.AspectCore;
using Microsoft.Extensions.Options;

namespace ErpMikroservis.AspectCore
{
    public class CacheRemove : InterceptorBase<CacheRemoveAttribute>
    {
        private readonly ICacheService _cacheService;
        private readonly IOptions<SettingsOptions> settings;
        public CacheRemove(IOptions<SettingsOptions> _settings)
        {
            settings = _settings;
            _cacheService = (ICacheService)HttpContext.Current.RequestServices.GetService(typeof(ICacheService));
        }
        protected override void OnCacheRemove(IInvocation invocation, CacheRemoveAttribute attribute)
        {
            if (settings.Value.IsCache)
            {
                var data = _cacheService.Get($"{attribute.methodName}()");
                _cacheService.Remove($"{attribute.methodName}()");
            }
            Console.WriteLine($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name} Cache Deleted.");
        }
    }
}
