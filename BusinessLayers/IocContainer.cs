using BusinessLayers.BLLQueries;
using BusinessLayers.BLLQueries.Abstract;
using DataAcessLayer;
using ErpMikroservis.AspectCore.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayers
{
    public static class IocContainer
    {
        public static IServiceCollection AddCustomBusiness(this IServiceCollection services,string ConnectionString)
        {
            services.ConfigureAOPCore();
            //Client Kaç adet Request atarsa her Client için 1 Nesne
            services.AddInterceptedScoped<IBLPersonelQueries, BLPersonelQueries>();

            services.AddInterceptedScoped<IBLOrderQueries, BLOrderQueries>();
         



            services.AddCustomDataAcces(ConnectionString);

            return services;
        }
    }
}