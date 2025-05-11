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

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CultureMiddleware>();
app.UseMiddleware<InjectionMiddleWare>();
app.UseMiddleware<IpRateLimitingMiddleware>();
//app.UseMiddleware<IPSafeMiddleWare>();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers().RequireRateLimiting("IPPolicy");


app.Run();


