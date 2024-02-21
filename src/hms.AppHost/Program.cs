var builder = DistributedApplication.CreateBuilder(args);

var identityApi = builder.AddProject<Projects.Identity_API>("identity-api")
    .WithLaunchProfile("https");

var MediaApi = builder.AddProject<Projects.Media_API>("media-api")
    .WithLaunchProfile("https");

var TenantApi = builder.AddProject<Projects.Tenant_API>("tenant-api")
    .WithLaunchProfile("https");
    
var WebApp = builder.AddProject<Projects.WebApp>("WebApp")
    .WithLaunchProfile("https");

builder.Build().Run();
