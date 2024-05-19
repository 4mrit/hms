using Microsoft.AspNetCore.Identity;
using thms.Identity.API.Authorization.Constants;
using thms.Identity.API.Data;
using thms.Identity.API.Models;
using thms.Identity.API.Services;
using thms.Identity.API.Services.Interfaces;
using thms.Identity.API.DTOs;
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at
// https://aka.ms/aspnetcore/swashbuckle
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddTransient<ILoginService<ApplicationUserLoginDTO>, EFLoginService>();
builder.Services.AddTransient<IPasswordService, EFPasswordService>();
builder.Services.AddTransient<IRegisterService<ApplicationUserRegisterDTO>,
                              EFRegisterService>();
builder.Services
    .AddTransient<IUserInformationService, EFUserInformationService>();
builder.Services.AddTransient<IAccountEmailService, EFAccountEmailService>();

builder.Services.AddTransient<IEmailSender, SMTPEmailSender>();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy(ApplicationPolicy.Administrators,
                    policy => policy.RequireClaim(ApplicationClaims.Role,
                                                  ApplicationRoles.SuperAdmin,
                                                  ApplicationRoles.Admin));
});

builder.AddMySqlDbContext<AppDbContext>("identitydb");

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
      options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapDefaultEndpoints();
app.Run();
