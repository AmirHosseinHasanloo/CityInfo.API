using CityInfo.API.DbContexts;
using CityInfo.API.DTOs;
using CityInfo.API.Repositories;
using CityInfo.API.Repositories.Services;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

//add logger to project =>
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityInfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();



var builder = WebApplication.CreateBuilder(args);

//change default logger to serilog logger
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region IOC 
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
#if DEBUG
builder.Services.AddScoped<IMailService, LocalMailService>();
#else
builder.Services.AddScoped<IMailService, CloudMailService>();
#endif
builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
#endregion

#region Context
builder.Services.AddDbContext<CityInfoContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:CityInfo_DB"]);
});
#endregion

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
