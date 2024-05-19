using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using thms.Identity.API.Models;

namespace thms.Identity.API.Data;

public class AppDbContext
(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options) {}
