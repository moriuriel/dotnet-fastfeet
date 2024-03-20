using FastFeet.Api;
using FastFeet.Api.Middleware;
using FastFeet.Application;
using FastFeet.CrossCutting.AppSettings;
using FastFeet.Infrastructure.Database;
using FastFeet.Infrastructure.ExternalService.Cryptography;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
loggerConfiguration.ReadFrom.Configuration(context.Configuration));

var configuration = Configuration.GetConfiguration();
builder.Services.AddSingleton(configuration);

builder.Services.AddCustomMediatr();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(p =>
{
    p.GroupNameFormat = "'v'VVV";
    p.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCryptographyService();
builder.Services.AddDatabaseContext(configuration);
builder.Services.AddCustomHealthChecks();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseHealthChecks("/health");
app.Run();