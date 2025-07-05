using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CommonMiddleware
{
    public static class CustomSwaggerSetup
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web API",
                    Description = "",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "WEB API",
                        Email = "info@mail.com.tr",
                        Url = new Uri("https://www.website.com.tr"),
                    }
                });

               
                c.OperationFilter<AddAcceptLanguageHeaderOperationFilter>();
            });
            return services;
        }
    }
}
