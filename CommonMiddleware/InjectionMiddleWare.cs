using ErpMikroservis.ResultMessages;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CommonMiddleware
{
    public class InjectionMiddleWare
    {
        private readonly RequestDelegate _next;

        private static readonly Regex SqlInjectionRegex = new Regex(
        @"\b(SELECT|INSERT|UPDATE|DELETE|DROP|ALTER|TRUNCATE|TABLE|DATABASE|DECLARE|EXEC|CAST|CONVERT|VARCHAR|NVARCHAR|XP_CMDSHELL|UNION|HAVING|GROUP BY|ORDER BY|WAITFOR DELAY)\b",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex XssRegex = new Regex(
        @"(<\s*script|javascript:|on\w+\s*=|<\s*style|vbscript:|eval\(|"
        + @"document\.(location|cookie|write|execCommand)|window\.|alert\(|"
        + @"char\s*\(|fromcharcode|settimeout\(|setinterval\(|innerHTML\s*=|outerHTML\s*=|"
        + @"src\s*=\s*[""]?javascript:|background\s*=\s*[""]?javascript:|expression\(|"
        + @"data\s*:\s*image/svg\+xml|<\s*object|<\s*embed|<\s*iframe|<\s*meta|<\s*link|"
        + @"<\s*xml|<\s*xss|<\s*marquee|<\s*bgsound|\bxxe\b|<!ENTITY|<!DOCTYPE|"
        + @"base64,|%3cscript%3e|%22%3e%3cscript%3e|&#\d+;|&#x[a-fA-F0-9]+;)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly string[] SQLKeywords = new string[]
        {
        ";", "--", "/*", "*/", "EXEC", "EXECUTE", "SELECT", "INSERT", "UPDATE", "DELETE",
        "CREATE", "DROP", "ALTER", "TRUNCATE", "TABLE", "DATABASE", "DECLARE",
        "CAST(", "CONVERT(", "VARCHAR(", "NVARCHAR(", "XP_CMDSHELL", "SYSOBJECTS",
        "SYSCOLUMNS", "INFORMATION_SCHEMA", "CHAR(", "NCHAR(", "NVARCHAR(", "UNION",
        "HAVING", "GROUP BY", "ORDER BY", "WAITFOR DELAY", "OR 1=1", "OR '1'='1'",
        "xp_cmdshell", "sp_executesql", "sp_OAcreate", "sp_OAgetProperty", "sp_OAmethod",
        "sp_OAsetProperty", "sp_MSForEachTable", "sp_MSForEachDB",
        "<script>", "javascript:", "onerror=", "onload=", "onclick=", "<style>",
        "vbscript:", "eval(", "document.cookie", "document.location"
        };
        public InjectionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string queryString = context.Request.QueryString.Value ?? string.Empty;
            string queryPath = context.Request.Path.ToString() ?? string.Empty;

            if (IsMalicious(queryString) || IsMalicious(queryPath))
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(ResultMessages<string>.ErrorMessage(new List<string>() { "Beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyiniz." }, HttpStatusCode.BadRequest)));
                return;
            }

            context.Request.EnableBuffering(); // Body'yi tekrar okunabilir yap

            using (var reader = new StreamReader(context.Request.Body)) // Stream'i açık bırak
            {
                var requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Stream başa alınmalı, yoksa Body kaybolur

                if (IsMalicious(requestBody))
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(ResultMessages<string>.ErrorMessage(new List<string>() { "Saldırı Algılandı. Yasaklandınız." }, HttpStatusCode.BadRequest)));
                    return;
                }
                await _next(context);
            }
        }
        private bool IsMalicious(string input)
        {
            return SQLKeywords.Any(keyword => input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1)
                   || SqlInjectionRegex.IsMatch(input)
                   || XssRegex.IsMatch(input);
        }
    }
}
