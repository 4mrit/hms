var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

var tenantDb = sql.AddDatabase("tenantdb");

var identityApi = builder.AddProject<Projects.Identity_API>("identity-api")
    .WithLaunchProfile("https");

var MediaApi = builder.AddProject<Projects.Media_API>("media-api")
    .WithLaunchProfile("https");

var TenantApi = builder.AddProject<Projects.Tenant_API>("tenant-api")
    .WithReference(tenantDb)
    .WithLaunchProfile("https");

var WebApp = builder.AddProject<Projects.WebApp>("WebApp")
    .WithLaunchProfile("https");

builder.Build().Run();
