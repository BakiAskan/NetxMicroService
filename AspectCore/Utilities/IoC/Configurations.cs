using Castle.DynamicProxy;
using ErpMikroservis.AspectCore.LogWriting;
using Microsoft.Extensions.DependencyInjection;
namespace ErpMikroservis.AspectCore.Utilities.IoC
{
    public static class Configurations
    {
        public static IServiceCollection ConfigureAOPCore(this IServiceCollection services)
        {
            services.AddSingleton<ILogWriteExtensions,LogWriteExtensions>();
            services.AddSingleton<IProxyGenerator, ProxyGenerator>();
            services.AddScoped<InterceptorBase<AOpPerformanceAttribute>, PerformanceAspect>();
            services.AddSingleton<InterceptorBase<AOPLogAttribute>, LogAspect>();
            services.AddTransient<InterceptorBase<CacheAddAttribute>, CacheAspect>();
            services.AddTransient<InterceptorBase<CacheRemoveAttribute>, CacheRemove>();
           
            services.AddTransient<ICacheService, MemoryCacheService>();
            services.AddCustomHttpContextAccessor();
            return services;
        }
    }
}
