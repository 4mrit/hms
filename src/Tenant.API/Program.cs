using hms.Tenant.API.Services;
using hms.Tenant.API.Data;

using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// builder.AddSqlServerDbContext<TenantContext>("tenantdb");
// builder.AddNpgsqlDbContext<TenantContext>("tenantdb" 
//     ,static settings => settings.ConnectionString = "Host=localhost;Database=CatalogDB;Username=postgres;Password=yourWeak(!)Password;Timeout=300;CommandTimeout=300");
// Apply database migration automatically. Note that this approach is not
// recommended for production scenarios. Consider generating SQL scripts from
// migrations instead.
// builder.Services.AddMigration<TenantContext, UsersSeed>();
builder.AddMySqlDbContext<TenantContext>("tenantdb");

builder.Services.AddTransient<DatabaseTenantService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

