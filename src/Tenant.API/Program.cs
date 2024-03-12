using hms.Tenant.API.Services;
using hms.Tenant.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at
// https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// builder.AddSqlServerDbContext<TenantContext>("tempdb");
// builder.AddNpgsqlDbContext<TenantContext>("tenantdb")
// ,static settings => settings.ConnectionString =
// "Server=localhost;Database=tenantdb;Timeout=300;"); Apply database migration
// automatically. Note that this approach is not recommended for production
// scenarios. Consider generating SQL scripts from migrations instead.
// builder.Services.AddMigration<TenantContext, UsersSeed>();
builder.AddMySqlDbContext<TenantContext>("tenantdb");
builder.Services.AddTransient<DatabaseTenantService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<TenantContext>();
//     context.Database.Migrate();
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapDefaultEndpoints();
app.Run();
