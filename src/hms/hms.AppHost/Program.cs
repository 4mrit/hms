var builder = DistributedApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var hms = builder.AddProject<Projects.hms_ApiGateway>("hms")
    .WithLaunchProfile("https");

var app = builder.Build();
app.MapDefaultEndpoints();
app.Run();
