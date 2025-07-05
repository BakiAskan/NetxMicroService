using BusinessLayers.BLCommands;
using BusinessLayers.BLCommands.Abstract;
using BusinessLayers.BLLQueries.Abstract;
using BusinessLayers.BLLQueries.Concrete;
using DataAcessLayer;
using ErpMikroservis.AspectCore.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace BusinessLayers
{
    public static class IocContainer
    {
        public static IServiceCollection AddCustomBusiness(this IServiceCollection services,string ConnectionString)
        {
   


            services.ConfigureAOPCore();
            //Client Kaç adet Request atarsa her Client için 1 Nesne
            services.AddInterceptedScoped<IBLStockQueries, BLStockQueries>();
            services.AddInterceptedScoped<IBLCompanyQueries, BLCompanyQueries>();
            services.AddInterceptedScoped<IBLProjectQueries, BLProjectQueries>();
            services.AddInterceptedScoped<IBLCurrencyCommand, BLCurrencyCommand>();



            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            services.AddScoped(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));


            services.AddCustomDataAcces(ConnectionString);

            return services;
        }
    }
}