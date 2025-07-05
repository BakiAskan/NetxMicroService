using BusinessLayers;
using CommonMiddleware;
using ErpMikroservis.GlobalModel;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureCorsPolicy();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomBusiness(@"Server=BAD3\BAD319;Database=CARKC_CR16;User Id=sa;Password=;");

builder.Services.AddCustomJwtAuthentication(builder.Configuration["SecretKey"]);

builder.Services.Configure<SettingsOptions>(builder.Configuration.GetSection("AOPSettings"));
var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.DefaultModelsExpandDepth(-1);

});

app.UseMiddleware<ExceptionMiddleware>(); // kullanýcýya standart hata mesajý döndürür. orjinal hata mesajýný loglar
app.UseMiddleware<CultureMiddleware>(); // kullanýcýnýn dilini ayarlar. (CultureInfo)
app.UseMiddleware<InjectionMiddleWare>(); // SQL ve XSS injection korumasý yapar.
app.UseMiddleware<IpRateLimitingMiddleware>(); // IP bazlý rate limiting middleware. IP baþýna istek sayýsýný sýnýrlar.
//app.UseMiddleware<IPSafeMiddleWare>(); // IP bazlý istek izni middleware. Belirli IP'lere izin verir, diðerlerini engeller.
app.UseAuthorization(); // Yetkilendirme middleware'i. JWT token kontrolü yapar.
app.UseCors("CorsPolicy"); // CORS ayarlarýný uygular. CorsPolicy adýnda bir policy tanýmlanmýþ olmalý.
app.UseHttpsRedirection(); // HTTPS yönlendirmesi yapar. HTTP isteklerini HTTPS'ye yönlendirir.
app.MapControllers().RequireRateLimiting("IPPolicy"); // Controller'lara rate limiting uygular. IPPolicy adýnda bir policy tanýmlanmýþ olmalý.


app.Run();



