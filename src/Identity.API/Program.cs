using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Aspire.Pomelo.EntityFrameworkCore.MySql;
using hms.Identity.API.Data;
using hms.Identity.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at
// https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

builder.AddMySqlDbContext<AppDbContext>("identitydb");

builder.Services.AddIdentityCore<MyUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.MapIdentityApi<MyUser>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries =
    new[] { "Freezing", "Bracing", "Chilly", "Cool",       "Mild",
            "Warm",     "Balmy",   "Hot",    "Sweltering", "Scorching" };

app.MapGet("/weatherforecast", () =>
{
  var forecast =
      Enumerable.Range(1, 5)
          .Select(index => new WeatherForecast(
                      DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                      Random.Shared.Next(-20, 55),
                      summaries[Random.Shared.Next(summaries.Length)]))
          .ToArray();
  return forecast;
}).WithName("GetWeatherForecast").WithOpenApi().RequireAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
