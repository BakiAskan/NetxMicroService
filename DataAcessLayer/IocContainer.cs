using DataAcessLayer.DALCommands.Currency;
using DataAcessLayer.Queries.Orders;
using DataAcessLayer.Queries.Personels;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace DataAcessLayer
{
    public static class IocContainer
    {
        public static IServiceCollection AddCustomDataAcces(this IServiceCollection services,string ConnectionString)
        {
            //Client Kaç adet Request atarsa her Client için 1 Nesne
            services.AddScoped<ISqlQueries, SqlQueries>();
            services.AddScoped<ICommands, Commands>();


            services.AddScoped<IPersonelQueries, PersonelQueries>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<ICurrencyCommand, CurrencyCommand>();

            // Bütün Client'lar için tek nesne. Herkes Aynı nesneyi kullanacak.
            services.AddSingleton<IDbConnection>((sp) => new SqlConnection(ConnectionString));

            return services;
        }
    }
}