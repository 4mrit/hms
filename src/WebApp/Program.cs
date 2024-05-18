using WebApp.Components;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddCors();

builder.Services.AddBlazorBootstrap();


builder.Services.AddHttpClient("MyHttpClient", client =>
{

    client.BaseAddress = new Uri("http://tenant-api");
});


var app = builder.Build();

app.MapDefaultEndpoints();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for
  // production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseCors(policy=> policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
