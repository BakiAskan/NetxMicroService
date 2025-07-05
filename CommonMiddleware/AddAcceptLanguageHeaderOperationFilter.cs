using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class AddAcceptLanguageHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Accept-Language başlığını parametre olarak ekle
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Description = "Dil tercihinizi belirtin (örneğin, en-US, tr-TR)",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("tr")
            }
        });
    }
}
