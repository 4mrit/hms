using Microsoft.EntityFrameworkCore;
using hms.Tenant.API.Data;
using hms.Tenant.API.Model;

namespace hms.Tenant.API.Services;

public class DatabaseFeatureService
{
  public IWebHostEnvironment WebHostEnvironment { get; }
  private readonly TenantContext _context;

  public DatabaseFeatureService(IWebHostEnvironment webHostEnvironment,
                                TenantContext context)
  {
    this.WebHostEnvironment = webHostEnvironment;
    this._context = context;
  }

  public async Task<IEnumerable<Feature>> GetAllFeatures()
  {
    IEnumerable<Feature> features;
    features = await _context.Features.AsNoTracking().ToListAsync();
    return features;
  }

  public async Task<Feature> GetFeatureById(int FeatureId)
  {
    Feature feature = null!;
    feature = await _context.Features.AsNoTracking().FirstOrDefaultAsync(
        f => f.Id == FeatureId);
    return feature;
  }
  public async Task<Feature> AddFeature(Feature feature)
  {
    await _context.AddAsync(feature);
    await _context.SaveChangesAsync();
    return feature;
  }

  public async Task<Feature> UpdateFeatureUsingId(int FeatureId,
                                                  Feature feature)
  {
    feature.Id = FeatureId;
    _context.Update(feature);
    await _context.SaveChangesAsync();
    return feature;
  }
  public async Task<Feature> DeleteFeatureUsingId(int FeatureId)
  {
    await _context.Features.Where(feature => feature.Id == FeatureId)
        .ExecuteDeleteAsync();
    return new Feature();
  }
}
