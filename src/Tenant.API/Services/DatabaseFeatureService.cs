using Microsoft.EntityFrameworkCore;
using hms.Tenant.API.Data;
using hms.Tenant.API.Model;

namespace hms.Tenant.API.Services;

public class DatabaseFeatureService {
  public IWebHostEnvironment WebHostEnvironment { get; }
  private readonly TenantContext _context;

  public DatabaseFeatureService(IWebHostEnvironment webHostEnvironment,
                                TenantContext context) {
    this.WebHostEnvironment = webHostEnvironment;
    this._context = context;
  }

  public async Task<IEnumerable<Feature>> GetAllFeatures() {
    IEnumerable<Feature> features;
    features = await _context.Features.AsNoTracking().ToListAsync();
    return features;
  }
}
