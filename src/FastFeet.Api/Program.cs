using FastFeet.Application;
using FastFeet.CrossCutting.AppSettings;
using FastFeet.Infrastructure.ExternalService.Cryptography;

var builder = WebApplication.CreateBuilder(args);

var configuration = Configuration.GetConfiguration();
builder.Services.AddSingleton(configuration);

builder.Services.AddCustomMediatr();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCryptographyService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();