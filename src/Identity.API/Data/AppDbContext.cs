using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hms.Identity.API.Models;

namespace hms.Identity.API.Data;

public class AppDbContext
(DbContextOptions<AppDbContext> options) : IdentityDbContext<MyUser>(options)
{ }
