using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CommonMiddleware
{
    public static class CustomSwaggerSetup
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // API dokümantasyonu bilgileri
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web API",
                    Description = "Bu API, sistem bileşenlerine erişimi sağlar.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "WEB API Destek",
                        Email = "info@mail.com.tr",
                        Url = new Uri("https://www.website.com.tr"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Telif Hakkı © 2025",
                        Url = new Uri("https://www.website.com.tr/license")
                    }
                });

                // JWT Bearer güvenlik şeması tanımı
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Bearer yetkilendirmesi. Örnek: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                // Tüm endpoint'lere JWT zorunluluğu
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    }
                });
              c.OperationFilter<AddAcceptLanguageHeaderOperationFilter>();
            });

            return services;
        }
    }
}
