var builder = DistributedApplication.CreateBuilder(args);

var sqlpw = builder.Configuration["sqlpassword"];

// var sqlServer = builder.AddSqlServerContainer("sqlserver" , sqlpw);
// var postgresServer = builder.AddPostgresContainer("postgres");
var dbServer = builder.AddMySqlContainer("mysqlserver");

var sqlServer = dbServer;
var tenantDb = sqlServer.AddDatabase("tenantdb");

var identityApi = builder.AddProject<Projects.Identity_API>("identity-api")
    .WithLaunchProfile("https");

var MediaApi = builder.AddProject<Projects.Media_API>("media-api")
    .WithLaunchProfile("https");

var TenantApi = builder.AddProject<Projects.Tenant_API>("tenant-api")
    .WithReference(tenantDb)
    .WithLaunchProfile("https");

var WebApp = builder.AddProject<Projects.WebApp>("WebApp")
    .WithLaunchProfile("https");

identityApi.WithReference(MediaApi);

builder.Build().Run();
