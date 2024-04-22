using Microsoft.AspNetCore.Identity;
using hms.Identity.API.Authorization.Constants;
using hms.Identity.API.Data;
using hms.Identity.API.Models;
using hms.Identity.API.Services;
using hms.Identity.API.Services.Interfaces;
using hms.Identity.API.DTOs;
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
