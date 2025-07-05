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

app.UseMiddleware<ExceptionMiddleware>(); // kullan�c�ya standart hata mesaj� d�nd�r�r. orjinal hata mesaj�n� loglar
app.UseMiddleware<CultureMiddleware>(); // kullan�c�n�n dilini ayarlar. (CultureInfo)
app.UseMiddleware<InjectionMiddleWare>(); // SQL ve XSS injection korumas� yapar.
app.UseMiddleware<IpRateLimitingMiddleware>(); // IP bazl� rate limiting middleware. IP ba��na istek say�s�n� s�n�rlar.
//app.UseMiddleware<IPSafeMiddleWare>(); // IP bazl� istek izni middleware. Belirli IP'lere izin verir, di�erlerini engeller.
app.UseAuthorization(); // Yetkilendirme middleware'i. JWT token kontrol� yapar.
app.UseCors("CorsPolicy"); // CORS ayarlar�n� uygular. CorsPolicy ad�nda bir policy tan�mlanm�� olmal�.
app.UseHttpsRedirection(); // HTTPS y�nlendirmesi yapar. HTTP isteklerini HTTPS'ye y�nlendirir.
app.MapControllers().RequireRateLimiting("IPPolicy"); // Controller'lara rate limiting uygular. IPPolicy ad�nda bir policy tan�mlanm�� olmal�.


app.Run();



