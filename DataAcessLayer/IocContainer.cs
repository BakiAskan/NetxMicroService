using DataAcessLayer.DALCommands.Currency;
using DataAcessLayer.Queries.Companies;
using DataAcessLayer.Queries.Projects;
using DataAcessLayer.Queries.Stocks;
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


            services.AddScoped<ICurrencyCommand, CurrencyCommand>();
            services.AddScoped<IStockQueries, StockQueries>();
            services.AddScoped<ICompanyQueries, CompanyQueries>();
            services.AddScoped<IProjectQueries, ProjectQueries>();

            // Bütün Client'lar için tek nesne. Herkes Aynı nesneyi kullanacak.
            services.AddSingleton<IDbConnection>((sp) => new SqlConnection(ConnectionString));

            return services;
        }
    }
}