var builder = DistributedApplication.CreateBuilder(args);

var hms = builder.AddProject<Projects.hms_ApiGateway>("hms")
    .WithLaunchProfile("https");

var app = builder.Build();
app.Run();
