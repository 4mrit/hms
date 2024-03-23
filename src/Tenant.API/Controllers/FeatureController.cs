// TenantsController.cs
using Microsoft.AspNetCore.Mvc;
using hms.Tenant.API.Services;
using hms.Tenant.API.Model;
namespace hms.Tenant.API.Controllers;

[Route("api/Tenants/[controller]")]
[ApiController]
public class FeatureController : ControllerBase {
  public DatabaseFeatureService FeatureService { get; }
  public FeatureController(DatabaseFeatureService featureService) {
    this.FeatureService = featureService;
  }

  [HttpGet]
  public async Task<IEnumerable<Feature>> GetAllFeatures() {
    return await FeatureService.GetAllFeatures();
  }
}
