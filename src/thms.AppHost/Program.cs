var builder = DistributedApplication.CreateBuilder(args);

// var sqlpw = builder.Configuration["sqlpassword"];
// if (string.IsNullOrEmpty(sqlpw))
// {
//     throw new InvalidOperationException("""
//         A password for the local SQL Server container is not configured.
//         Add one to the AppHost project's user secrets with the key 'sqlpassword', e.g. dotnet user-secrets set sqlpassword <password>
//         """);
// }

// var sqlServer = builder.AddSqlServerContainer("sqlserver" , sqlpw);
// var postgresServer = builder.AddPostgresContainer("postgres");
// // var dbServer = builder.AddMySqlContainer("mysqlserver");

// var dbServer = postgresServer;

// var tenantdb = dbServer.AddDatabase("tenantdb");


// var tenantDb = builder.AddMySql("sql")
//     // .WithVolumeMount("VolumeMount.sqlserver.data", "/var/opt/mssql", VolumeMountType.Named)
//     .AddDatabase("tenantdb");

var identityApi = builder.AddProject<Projects.Identity_API>("identity-api")
    .WithLaunchProfile("https");

var MediaApi = builder.AddProject<Projects.Media_API>("media-api")
    .WithLaunchProfile("https");

var TenantApi = builder.AddProject<Projects.Tenant_API>("tenant-api")
    //.WithReference(TenantDB)
    .WithLaunchProfile("https");

var hmsApiGateway = builder.AddProject<Projects.hms_ApiGateway>("hmsApiGateway")
    .WithLaunchProfile("https");

var WebApp = builder.AddProject<Projects.WebApp>("WebApp")
    .WithReference(TenantApi)
    .WithReference(hmsApiGateway)
    .WithLaunchProfile("https");


identityApi.WithReference(MediaApi);

builder.Build().Run();
